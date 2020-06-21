using FoodBot.States;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Conversations
{
    
    internal class RegistrationConversation : ConversationBase, IConversation
    {
   
        public RegistrationConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.Reg;

        public UserState Execute(Message message, UserState userState)
        {  
         /*   var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                          
                                                  new[] //
                                                {
                                                              new KeyboardButton("Да"),
                                                                new KeyboardButton("Нет")
                                                                
                                                },
            },
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
         
            */

         

         // Client.SendTextMessageAsync(message.Chat.Id, "Хотите зарегистрироваться?", replyMarkup: keyboard);
            
           

          if(message.Text=="Да")
            {
            
            userState.ConversationState = ConversationState.SeX;      
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
            }
           if (message.Text=="Нет")
            {  
                userState.ConversationState = ConversationState.END; 
                 Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!");
            }
           
            return userState;
        }
    }
}