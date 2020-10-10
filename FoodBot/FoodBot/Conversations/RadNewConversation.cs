using FoodBot.Dal.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System;

namespace FoodBot.Conversations
{


    internal class RadNewConversation : ConversationBase, IConversation
    {

        bool isInt;
        int Radius;
        public RadNewConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.RadNew;

        public UserState Execute(Message message, UserState userState)
        {
           


            

            isInt = Int32.TryParse(message.Text, out Radius);
            if (isInt == true)
            {

                userState.RadiusFind = Radius;
                //  Console.WriteLine(userState.RadiusFind); 
                Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!\n" +
                        "Для открытие меню наберите любой символ ");

                userState.ConversationState = ConversationState.END;

            }
            else
            {
                Client.SendTextMessageAsync(message.Chat.Id, $"Неверно введен радиус,введите заново радиус");
                userState.ConversationState = ConversationState.RadNew;
            }

            return userState;
        }
    }
}
