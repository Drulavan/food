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
    internal class SXConversation : ConversationBase, IConversation
    {
        List<string> MenuList = new List<string>();
        public SXConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.SeX;

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
          //  Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категории продуктов", replyMarkup: keyboard);
            if (message.Text == "Закрыть меню выбора")
            {

                Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!");
                userState.menuCat = MenuList.ToArray();
              /*  foreach (string i in userState.menuCat)
                {
                    Console.WriteLine(i);
                }*/
                userState.ConversationState = ConversationState.END;
            }
            else
            {
                Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категории продуктов", replyMarkup: keyboard);

                //Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категории продуктов", replyMarkup: keyboard);
                if (MenuList.Contains(message.Text) == true)
                {
                    Client.SendTextMessageAsync(message.Chat.Id, $"Вы уже выбрали данную категорию");
                    //userState.ConversationState = ConversationState.SeX;
                }
                else
                {
                    MenuList.Add(message.Text);
                    userState.ConversationState = ConversationState.SeX;
                }
                
                       
            }
            return userState;
        }
    }
}