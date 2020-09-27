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
            if (userState.MenuListCat.Count==0)
            {
                var MC = userState.menuCat;

                foreach (string ms in MC)
                {
                   userState.MenuListCat.Add(ms);
                }
            }
            var ListCat = userState.MenuListCat;
            var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                     new[] //
                                                   {
                                                                 new KeyboardButton("Просмотреть"),
                                                                   new KeyboardButton("Удалить"),
                                                                     new KeyboardButton("Добавить")
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
            switch (message.Text)
            {
                case "Просмотреть":
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
                case "Удалить":
                    Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категорию, которую хотите удалить", replyMarkup: keyboard1);
                    userState.ConversationState = ConversationState.DelCL;

                    break;
                case "Добавить":
                    Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категорию, которую хотите добавить", replyMarkup: keyboard1);
                    userState.ConversationState = ConversationState.SeX;

                    break;

                default:
                    Client.SendTextMessageAsync(message.Chat.Id, "Введите одну из команд", replyMarkup: keyboard);
                    break;
            }
            //userState.ConversationState = ConversationState.None;
            // Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!");
            return userState;
        }
    }
}