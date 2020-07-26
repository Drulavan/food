using FoodBot.Dal.Models;
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
                                                              new KeyboardButton("Коровье молоко"),
                                                              new KeyboardButton("Куриное яйцо")
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
                   },
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
            Client.SendTextMessageAsync(message.Chat.Id, "У Вас есть аллергия?", replyMarkup: keyboard);
            userState.ConversationState = ConversationState.Allergy;

            //  Client.SendTextMessageAsync(message.Chat.Id, "Есть у Вас аллергия на продукты?", replyMarkup: keyboard);
            return userState;
        }
    }
}