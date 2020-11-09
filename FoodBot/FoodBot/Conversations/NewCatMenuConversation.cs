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
    internal class NewCatMenuConversation : ConversationBase, IConversation
    {
      
        public NewCatMenuConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.CatNew;

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
            // Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категории продуктов", replyMarkup: keyboard);
            switch (message.Text)
            {
                case "Закрыть меню выбора":
                    Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!\n" +
                        "Для открытия меню наберите любой символ ");
                   
                    //userState.MenuListCat = MenuList;
                    userState.menuCat = userState.MenuListCat.ToArray();
                    userState.IsNewRegistered = false;
                    userState.IsRegistered = true;
                    userState.ConversationState = ConversationState.END;
                    break;

                case "Мясо":
                case "Рыба":
                case "Выпечка":
                case "Овощи":
                case "Фрукты":
                case "Молочная продукция":
                case "Крупы":
                case "Сладости":
                    if (userState.MenuListCat.Contains(message.Text) == true)
                    {
                        Client.SendTextMessageAsync(message.Chat.Id, $"Вы уже выбрали данную категорию. Выберите другую категорию продуктов, которые Вы готовы забирать", replyMarkup: keyboard);
                        //userState.ConversationState = ConversationState.SeX;
                    }
                   else if (userState.MenuListCat.Contains(message.Text) == false)
                    {
                        Client.SendTextMessageAsync(message.Chat.Id, $"Добавить ещё одну категорию?", replyMarkup: keyboard);
                        //userState.ConversationState = ConversationState.SeX;
                    }
                    else
                    {
                        userState.MenuListCat.Add(message.Text);
                        Client.SendTextMessageAsync(message.Chat.Id, $"Добавить ещё одну категорию?", replyMarkup: keyboard);
                        userState.ConversationState = ConversationState.CatNew;
                    }
                    break;
                default:
                    Client.SendTextMessageAsync(message.Chat.Id, $"Вы ввели некорректно данные.Выберите пожалуйста категории продуктов", replyMarkup: keyboard);
                    break;

            }
       
            return userState;
        }
    }
}