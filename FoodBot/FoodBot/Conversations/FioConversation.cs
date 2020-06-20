using FoodBot.States;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Conversations
{
    
    internal class FIOConversation : ConversationBase, IConversation
    {
     public string NameUser;

        public FIOConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.FIO;
        public UserState Execute(Message message, UserState userState)
        {   NameUser=message.Text;
            if(NameUser!=null){
            Client.SendTextMessageAsync(message.Chat.Id, "Выберете Ваш пол:");
            
            userState.ConversationState = ConversationState.Sx;
            }
            return userState;
        }
    }
}