using System.Reflection.Emit;
using FoodBot.States;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Conversations
{
    internal class OregConversation : ConversationBase, IConversation
    {
        public OregConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.Oreg;

        public UserState Execute(Message message, UserState userState)
        {
            
            var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                          
                                                
                                                new[] // row 2
                                                {
                                                     new KeyboardButton("Да"),
                                                                new KeyboardButton("Нет")          
                                                               
                                                },
                                               
                                                
          },
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
          
           Client.SendTextMessageAsync(message.Chat.Id, $"Хотите зарегистрироваться?", replyMarkup: keyboard);
            
                                    userState.ConversationState = ConversationState.Reg;

                                    
                                               
            return userState;
        }
        
    }
    

}