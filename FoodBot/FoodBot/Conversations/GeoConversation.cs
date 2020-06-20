using FoodBot.States;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Conversations
{
    internal class GeoConversation : ConversationBase, IConversation
    {
      

        public GeoConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.None;
        public UserState Execute(Message message, UserState userState)
        {
            if(message.Type == Telegram.Bot.Types.Enums.MessageType.Location)
            {
                Client.SendTextMessageAsync(message.Chat.Id, "Верно");
                var lat=message.Location.Latitude;
                var lon=message.Location.Longitude;
                 Client.SendTextMessageAsync(message.Chat.Id, "Регистрация");

                userState.ConversationState = ConversationState.Registration;
            }
            else
    
             Client.SendTextMessageAsync(message.Chat.Id, "Неверно");
        
    
            return userState;
        }
    }
}