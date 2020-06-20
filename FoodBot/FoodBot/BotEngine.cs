using FoodBot.Conversations;
using FoodBot.States;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Args;

namespace FoodBot
{
    internal class BotEngine
    {
        private readonly IEnumerable<IConversation> conversations;
        private List<UserState> userStates;

        public BotEngine(IEnumerable<IConversation> conversations, List<UserState> states)
        {
            this.conversations = conversations;
            this.userStates = states;
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
            userStates.Remove(GetState(user.Id));
            userStates.Add(user);
        }

        private UserState GetState(long id)
        {
            var user = userStates.Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
            {
                user = new UserState(id);
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