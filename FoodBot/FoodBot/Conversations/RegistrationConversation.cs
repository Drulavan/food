using FoodBot.Dal.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FoodBot.Conversations
{
    internal class RegistrationConversation : ConversationBase, IConversation
    {
        public RegistrationConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.Registration;

        public UserState Execute(Message message, UserState userState)
        {
            userState.ConversationState = ConversationState.None;
            Client.SendTextMessageAsync(message.Chat.Id, "Укажи свой город");
            return userState;
        }
    }
}