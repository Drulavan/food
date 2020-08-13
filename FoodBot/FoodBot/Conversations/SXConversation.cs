using FoodBot.Dal.Models;
using FoodBot.Parsers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System;

namespace FoodBot.Conversations
{
    internal class SXConversation : ConversationBase, IConversation
    {
        public SXConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.SeX;

        public UserState Execute(Message message, UserState userState)
        {
            /*           var keyboard = new ReplyKeyboardMarkup
               {
                   Keyboard = new[] {
                                                     new[] //
                                                   {
                                                       new KeyboardButton("Мужчина"),
                                                       new KeyboardButton("Женщина")
                                                   },
               },
                   ResizeKeyboard = true,
                   OneTimeKeyboard = true
               };
  
            var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                  new[] //
                                                {
                                                                new KeyboardButton("до 25 лет"),
                                                                new KeyboardButton("25-34лет")
                                                },
                                                new[]
                                                {
                                                                new KeyboardButton("35-44лет"),
                                                                new KeyboardButton("45-54лет")
                                               },
                                                new[]
                                                 {
                                                                new KeyboardButton("55-65лет"),
                                                                new KeyboardButton("более 65 лет")
                                                },
                },
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };

            Client.SendTextMessageAsync(message.Chat.Id, "Выберите Ваш возраст:", replyMarkup: keyboard);
            if (message.Text == "Мужчина" || message.Text == "Женщина")
            {
                
            }
            var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                                                     new[] //
                                                   {
                                                                 new KeyboardButton(Categories.Fish.ToString()),
                                                                   new KeyboardButton(Categories.Meat.ToString())
                                                   },
                                                         new[] //
                                                   {
                                                                 new KeyboardButton(Categories.Bake.ToString()),
                                                                   new KeyboardButton(Categories.Vegetables.ToString())
                                                   },
                                                             new[] //
                                                   {
                                                                 new KeyboardButton(Categories.Fruits.ToString()),
                                                                   new KeyboardButton(Categories.Milk.ToString())
                                                   },
               },
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };*/
            Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категории продуктов");
          
            userState.ConversationState = ConversationState.END;
            return userState;
        }
    }
}