using FoodBot.Dal.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System;
using System.Globalization;

namespace FoodBot.Conversations
{


    internal class RadNewConversation : ConversationBase, IConversation
    {

        bool isInt;
  
        float Radius;
        public RadNewConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.RadNew;


        public UserState Execute(Message message, UserState userState)
        {
            var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                     new[] //
                                                   {
                                                                 new KeyboardButton(Categories.Fish.DescriptionAttr()),
                                                                   new KeyboardButton(Categories.Meat.DescriptionAttr())
                                                   },
                                                         new[] //
                                                   {
                                                                 new KeyboardButton(Categories.Bake.DescriptionAttr()),
                                                                   new KeyboardButton(Categories.Vegetables.DescriptionAttr())
                                                   },
                                                             new[] //
                                                   {
                                                                 new KeyboardButton(Categories.Fruits.DescriptionAttr()),
                                                                   new KeyboardButton(Categories.Milk.DescriptionAttr())
                                                   },
                                                                       new[] //
                                                   {
                                                                 new KeyboardButton(Categories.Groats.DescriptionAttr()),
                                                                   new KeyboardButton(Categories.Sweets.DescriptionAttr())
                                                   },
                                                                       new[] //
                                                   {
                                                                 new KeyboardButton("Закрыть меню выбора")

                                                   },
               },
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };


          //  Console.WriteLine(userState.RadiusFind);
            
            isInt = float.TryParse(message.Text.Replace(".", ","), out Radius);
           
            if (isInt == true)
            {
                //// Client.SendTextMessageAsync(message.Chat.Id, $"Укажите радиус поиска предложений (в километрах)");
                // userState.RadiusFind = Radius;
                // if (userState.IsNewRegistered == true)
                // {
                //     Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категории продуктов, которые Вы готовы забирать", replyMarkup: keyboard);
                //     //MenuLi
                //    userState.ConversationState = ConversationState.CatNew;
                // }
                // else 
                // { 
                // //  Console.WriteLine(userState.RadiusFind); 
                // Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!\n" +
                //         "Для открытия меню наберите любой символ ");
                //   //  userState.IsRegistered = true;
                // userState.ConversationState = ConversationState.END;
                // }
                Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категории продуктов, которые Вы готовы забирать", replyMarkup: keyboard);
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
