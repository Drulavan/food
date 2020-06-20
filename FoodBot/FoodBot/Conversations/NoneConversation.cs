
﻿using System.Reflection.Emit;
using FoodBot.States;
using System.Threading.Tasks;

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
          
           Client.SendTextMessageAsync(message.Chat.Id, $"Привет! Я раздаю еду!", replyMarkup: keyboard);
                                    userState.ConversationState = ConversationState.Reg;
                                                
            return userState;
        }
        
    }
    

}