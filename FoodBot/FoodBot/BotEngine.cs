using FoodBot.Conversations;
using FoodBot.Dal.Models;
using FoodBot.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Args;

namespace FoodBot
{
    internal class BotEngine
    {
        private readonly IEnumerable<IConversation> conversations;
        private readonly StateRepository stateRepository;

        public BotEngine(IEnumerable<IConversation> conversations, StateRepository stateRepository)
        {
            this.conversations = conversations;
            this.stateRepository = stateRepository;
        }

        internal void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            UserState user = GetState(e.Message.Chat.Id);
            var conversation = conversations.Where(x => x.ConversationState == user.ConversationState).FirstOrDefault();
            user = conversation.Execute(e.Message, user);
            SaveState(user);
        }

        private void SaveState(UserState user)
        {
            stateRepository.Update(user);
        }

        private UserState GetState(long id)
        {
            var user = stateRepository.Get(id);
            if (user == null)
            {
                user = new UserState(id);
                stateRepository.Add(user);
            }
            return user;
        }

        internal void BotOnReceiveError(object sender, ReceiveErrorEventArgs e)
        {
            Console.WriteLine("Received error: {0} — {1}",
                e.ApiRequestException.ErrorCode,
                e.ApiRequestException.Message);
        }

        internal void BotOnInlineQuery(object sender, InlineQueryEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal void BotOnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}