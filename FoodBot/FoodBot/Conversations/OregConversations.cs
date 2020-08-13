using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using FoodBot.Dal.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System;


namespace FoodBot.Conversations
{
    internal class OregConversation : ConversationBase, IConversation
    {
        const float latMocsow = 55.7522f;
        const float lotMocsow = 37.6175f;

        const float latPiter = 59.939736f;
        const float lotPiter = 30.361002f;

        float minusLatM = latMocsow - 0.2f;
        float plusLatM = latMocsow + 0.2f;
        float minusLotM = lotMocsow - 0.2f;
        float plusLotM = lotMocsow + 0.2f;

        float minusLatP = latPiter - 0.2f;
        float plusLatP = latPiter + 0.2f;
        float minusLotP = lotPiter - 0.2f;
        float plusLotP = lotPiter + 0.2f;




        public OregConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.Oreg;

        public UserState Execute(Message message, UserState userState)
        {
            /* var keyboard = new ReplyKeyboardMarkup
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
             };*/

            userState.UsrLatitude = message.Location.Latitude;
            userState.UsrLongitude = message.Location.Longitude;


            if (((userState.UsrLatitude > minusLatM && userState.UsrLatitude < plusLatM) && (userState.UsrLongitude > minusLotM && userState.UsrLongitude < plusLotM)) ||
            ((userState.UsrLatitude > minusLatP && userState.UsrLatitude < plusLatP) && (userState.UsrLongitude > minusLotP && userState.UsrLongitude < plusLotP)))
            {
                Client.SendTextMessageAsync(message.Chat.Id, $"Напишите радиус поиска");


                userState.ConversationState = ConversationState.Reg;
            }
            else
            {
                Client.SendTextMessageAsync(message.Chat.Id, $"К сожалению в Вашем городе не работаем");
            }

            return userState;
        }
    }
}