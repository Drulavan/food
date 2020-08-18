using FoodBot.Dal.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using System;

namespace FoodBot.Conversations
{
    internal class EndConversation : ConversationBase, IConversation
    {
        public EndConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.END;

        public UserState Execute(Message message, UserState userState)
        {
            Console.WriteLine(userState.menuCat);
            //userState.ConversationState = ConversationState.None;
            // Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!");
            return userState;
        }
    }
}