﻿using FoodBot.Dal.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Conversations
{
    internal class NoneConversation : ConversationBase, IConversation
    {
        public NoneConversation(TelegramBotClient client) : base(client)
        {
        }

        public ConversationState ConversationState => ConversationState.None;

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
            if (userState.IsRegistered == true)
            {
                Client.SendTextMessageAsync(message.Chat.Id, $"Вы зарегистрированы");
            }
            else
            {
                Client.SendTextMessageAsync(message.Chat.Id, $"Привет! Этот бот помогает участвовать в фудшеринге, то есть раздачах еды. Бот может присылать уведомления о предложениях забрать излишки еды: он будет ориентироваться на Ваше местоположение и предпочтения. Узнать больше о фудшеринге можно на сайте 1mlntons.ru\n Для начала, пожалуйста, укажите Ваше местоположение.", replyMarkup: keyboard);

                userState.ConversationState = ConversationState.Oreg;
            }

            return userState;
        }
    }
}