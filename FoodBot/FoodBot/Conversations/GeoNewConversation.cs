using FoodBot.Dal.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System;

namespace FoodBot.Conversations
{


    internal class GeoNewConversation : ConversationBase, IConversation
    {
        const float latMocsow = 55.7522f;
        const float lotMocsow = 37.6175f;

        const float latPiter = 59.939736f;
        const float lotPiter = 30.361002f;

        const float minusLatM = latMocsow - 0.2f;
        const float plusLatM = latMocsow + 0.2f;
        const float minusLotM = lotMocsow - 0.2f;
        const float plusLotM = lotMocsow + 0.2f;

        const float minusLatP = latPiter - 0.2f;
        const float plusLatP = latPiter + 0.2f;
        const float minusLotP = lotPiter - 0.2f;
        const float plusLotP = lotPiter + 0.2f;

     
        public GeoNewConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.GeoNew;

        public UserState Execute(Message message, UserState userState)
        {

            var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                          new[] // row 1
                                                {
                                                               new KeyboardButton("Геолокация")
                                                               {
                                                                   RequestLocation = true
                                                               }
                                                },
          },
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };


            if (message.Location != null)
            {
                userState.UsrLatitude = message.Location.Latitude;
                userState.UsrLongitude = message.Location.Longitude;
                if ((userState.UsrLatitude > minusLatM && userState.UsrLatitude < plusLatM) && (userState.UsrLongitude > minusLotM && userState.UsrLongitude < plusLotM)) /*||
                        ((userState.UsrLatitude > minusLatP && userState.UsrLatitude < plusLatP) && (userState.UsrLongitude > minusLotP && userState.UsrLongitude < plusLotP)))*/
                {
                    userState.SityName = "Москва";
                   
                    userState.IsRegistered = true;
                    Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!\n" +
                      "Для открытие меню наберите любой символ ");

                    userState.ConversationState = ConversationState.END;

                }
                else if ((userState.UsrLatitude > minusLatP && userState.UsrLatitude < plusLatP) && (userState.UsrLongitude > minusLotP && userState.UsrLongitude < plusLotP))
                {
                    userState.SityName = "Санкт-Петербург";
                   
                    userState.IsRegistered = true;
                    Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!\n" +
                      "Для открытие меню наберите любой символ ");

                    userState.ConversationState = ConversationState.END;

                }
                else
                {
                    Client.SendTextMessageAsync(message.Chat.Id, $"К сожалению, мы пока что не работаем в Вашем городе.Следите за новостями фудшеринга на @onemlntons");
                }



                // Client.SendTextMessageAsync(message.Chat.Id, $"Напишите радиус поиска");
            }
            else
            {
                Client.SendTextMessageAsync(message.Chat.Id, $"Неверно указаны данные геолокации");
                Client.SendTextMessageAsync(message.Chat.Id, $"Ещё раз пожалуйста отправьте геолокацию", replyMarkup: keyboard);
                userState.ConversationState = ConversationState.GeoNew;
            }


          

            return userState;
        }
    }
}
