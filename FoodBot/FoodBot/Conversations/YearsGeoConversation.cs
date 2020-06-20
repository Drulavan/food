using FoodBot.States;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Conversations
{
    internal class YearsConversation : ConversationBase, IConversation
    {
      

        public YearsConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.Years;
        public UserState Execute(Message message, UserState userState)
        {
            
                    var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                          
                                                  new[] //
                                                {
                                                                new KeyboardButton("до 25 лет"),
                                                                new KeyboardButton("25-34лет")
                                                               
                                                                
                                                },
                                                new[]
                                                {

                                                                new KeyboardButton("35-44лет"),
                                                                new KeyboardButton("45-54лет")     
                                               },
                                                new[]
                                                 {
                                                                new KeyboardButton("55-65лет"),
                                                                new KeyboardButton("более 65 лет")
                                                },
                },
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
            userState.ConversationState = ConversationState.Allergy;
            Client.SendTextMessageAsync(message.Chat.Id, "Есть у Вас аллергия на продукты?", replyMarkup: keyboard);
            return userState;
        }
    }
}