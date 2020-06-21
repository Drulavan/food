using FoodBot.Dal.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Conversations
{
    internal class NoneConversation : ConversationBase, IConversation
    {
        public NoneConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.None;

        public UserState Execute(Message message, UserState userState)
        {
            var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                new[] // row 1
                                                {
                                                               new KeyboardButton("Регистрация")
                                                },
                                                  new[] // row 2
                                                {
                                                              new KeyboardButton("О нас"),
                                                                new KeyboardButton("Описание")
                                                },
      },
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
            userState.ConversationState = ConversationState.Registration;
            Client.SendTextMessageAsync(message.Chat.Id, $"Привет! Я раздаю еду!", replyMarkup: keyboard);
            return userState;
        }
    }
}