using FoodBot.Dal.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

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
            /*        var keyboard = new ReplyKeyboardMarkup
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
             */

            Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!");
            userState.ConversationState = ConversationState.END;
            return userState;
        }
    }
}