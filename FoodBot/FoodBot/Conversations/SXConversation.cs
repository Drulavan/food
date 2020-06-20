using FoodBot.States;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Conversations
{
    internal class SXConversation : ConversationBase, IConversation
    {
      

        public SXConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.SeX;
        
        public UserState Execute(Message message, UserState userState)
        {
                    var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                          
                                                  new[] //
                                                {
                                                    new KeyboardButton("Мужчина"),
                                                    new KeyboardButton("Женщина")
                                                },
            },
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };

            
            Client.SendTextMessageAsync(message.Chat.Id, "Выберите свой пол:", replyMarkup: keyboard);
          if(message.Text=="Мужчина" || message.Text=="Женщина")
            {
            userState.ConversationState = ConversationState.Years;

           
           
            }
            return userState;
        }
    }
}