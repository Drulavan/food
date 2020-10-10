using FoodBot.Dal.Models;
using FoodBot.Parsers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System;
using System.Collections.Generic;

namespace FoodBot.Conversations
{
    internal class EndConversation : ConversationBase, IConversation
    {
        public EndConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.END;

        public UserState Execute(Message message, UserState userState)
        {
            List<string> MLC = new List<string>();
            if (userState.MenuListCat==null)
            {
                var MC = userState.menuCat;

                foreach (string ms in MC)
                    
                {
                   MLC.Add(ms);
                   
                }
                
                userState.MenuListCat=MLC;
                

            }
            var ListCat = userState.MenuListCat;
            var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                     new[] //
                                                   {
                                                                 new KeyboardButton("Просмотреть список категории")
                                                                  
                                                   },
                                                       new[] //
                                                   {
                                                                
                                                                   new KeyboardButton("Удалить категорию"),
                                                                     new KeyboardButton("Добавить категорию")
                                                   },
                                                           new[] //
                                                   {

                                                                   new KeyboardButton("Изменить радиус поиска"),
                                                                     new KeyboardButton("Изменить геолокацию")
                                                   },
                                                              new[] //
                                                   {
                                                                 new KeyboardButton("Пройти регистрацию заново")

                                                   },
               },
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
            var keyboard1 = new ReplyKeyboardMarkup
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
            var keyboard2 = new ReplyKeyboardMarkup
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
            switch (message.Text)
            {
                case "Просмотреть список категории":
                    if (userState.MenuListCat.Count!= 0) {
                       

                        foreach(string listCat in ListCat)
                        {
                            Client.SendTextMessageAsync(message.Chat.Id, listCat);
                        }
                    }
                    else
                    {
                        Client.SendTextMessageAsync(message.Chat.Id, "Список пуст");
                    }
                    break;

                case "Удалить категорию":
                    Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категорию, которую хотите удалить", replyMarkup: keyboard1);
                    userState.ConversationState = ConversationState.DelCL;

                    break;

                case "Добавить категорию":
                    Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категорию, которую хотите добавить", replyMarkup: keyboard1);
                    userState.ConversationState = ConversationState.SeX;

                    break;
                case "Изменить радиус поиска":
                    Client.SendTextMessageAsync(message.Chat.Id, $"Укажите радиус поиска предложений (в километрах)");
                    userState.ConversationState = ConversationState.RadNew;

                    break;
                case "Изменить геолокацию":
                    Client.SendTextMessageAsync(message.Chat.Id, $" Пожалуйста, укажите Ваше местоположение.", replyMarkup: keyboard2);
                    userState.ConversationState = ConversationState.GeoNew;

                    break;
                case "Пройти регистрацию заново":
                    userState.IsRegistered = false;

                    Client.SendTextMessageAsync(message.Chat.Id, $" Пожалуйста, укажите Ваше местоположение.", replyMarkup: keyboard2);
                    userState.ConversationState = ConversationState.Oreg;
                    
                    break;

                default:
                    Client.SendTextMessageAsync(message.Chat.Id, "Введите одну из команд", replyMarkup: keyboard);
                    break;
            }
           // Console.WriteLine(userState.RadiusFind);
            //userState.ConversationState = ConversationState.None;
            // Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!");
            return userState;
        }
    }
}