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

        public ConversationState ConversationState => ConversationState.Registration;

        public UserState Execute(Message message, UserState userState)
        {  
            var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                          
                                                  new[] //
                                                {
                                                              new KeyboardButton("Да")
                                                              {

                                                              },
                                                                new KeyboardButton("Нет")
                                                                {
                                                                   
                                                                }
                                                },
            },
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
            Client.SendTextMessageAsync(message.Chat.Id, "Хотите зарегистрироваться?", replyMarkup: keyboard);
            if(message.Text=="Да")
            {
            
            userState.ConversationState = ConversationState.Sx;       
            Client.SendTextMessageAsync(message.Chat.Id, "Введите Ваше имя", replyMarkup: keyboard);
            }
           if (message.Text=="Нет")
            {
                Client.SendTextMessageAsync(message.Chat.Id, "Для персонализации Ваших запросов необходимо зарегистрироваться", replyMarkup: keyboard);
                userState.ConversationState = ConversationState.END; 
            }
            return userState;
        }
    }
}