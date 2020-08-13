using FoodBot.Dal.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System;

namespace FoodBot.Conversations
{
    internal class RegistrationConversation : ConversationBase, IConversation
    {

        bool isInt;
        int Radius;
        public RegistrationConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.Reg;

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

               
            // Client.SendTextMessageAsync(message.Chat.Id, "Хотите зарегистрироваться?", replyMarkup: keyboard);

            isInt = Int32.TryParse(message.Text, out Radius);
            if (isInt == true)
            {

                userState.RadiusFind = Radius;
                //  Console.WriteLine(userState.RadiusFind); 
               Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категории продуктов", replyMarkup: keyboard);
               
                userState.ConversationState = ConversationState.SeX;

            }
            else
            {
                Client.SendTextMessageAsync(message.Chat.Id, $"Неверно введен радиус,введите заново радиус");
                userState.ConversationState = ConversationState.Reg;
            }
            /*  if (message.Text == "Да")
             {
                 userState.ConversationState = ConversationState.SeX;
                 var keyboard = new ReplyKeyboardMarkup
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
                 userState.IsRegistered = true;
                 Client.SendTextMessageAsync(message.Chat.Id, "Выберите свой пол:", replyMarkup: keyboard);
             }
             if (message.Text == "Нет")
             {
                 userState.ConversationState = ConversationState.END;
                 Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!");
             }

 */
            return userState;
        }
    }
}