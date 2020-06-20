using FoodBot.States;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Conversations
{
    internal class AllergyConversation : ConversationBase, IConversation
    {
      

        public AllergyConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.Allergy;
        public UserState Execute(Message message, UserState userState)
        {
                   var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                          
                                                  new[] //
                                                {
                                                              new KeyboardButton("коровье молоко"),
                                                              new KeyboardButton("куриное яйцо")
                                                                
                                                },
                                                   new[] 
                                                {
                                                             new KeyboardButton("Орехи и миндаль"),
                                                            new KeyboardButton("Овощи и фрукты")
                                                },
                                                     new[] 
                                                {
                                                             new KeyboardButton("Рыба"),
                                                            new KeyboardButton("Специи"),
                                                            new KeyboardButton("Цветение")
                                                },
            
            
                   },   ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
            userState.ConversationState = ConversationState.END;
            Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!", replyMarkup: keyboard);
            return userState;
        }
    }
}