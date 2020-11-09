using FoodBot.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FoodBot.Conversations
{
    internal class DeleteCatListConversation : ConversationBase, IConversation
    {
        public DeleteCatListConversation(TelegramBotClient client) : base(client)
        {
        }

        
        public ConversationState ConversationState => ConversationState.DelCL;

        public UserState Execute(Message message, UserState userState)
        {
            Console.WriteLine("del"+ message.Text);
             
           
            ReplyKeyboardMarkup GetKeyboardButtons(List<string> strArr)
            {
                ReplyKeyboardMarkup keyboard;
                keyboard = new ReplyKeyboardMarkup();
                switch (strArr.Count)
                {
                    case 0:
                        keyboard = new ReplyKeyboardMarkup()
                        {
                            Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton("Закрыть меню выбора")

                    })
                },
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true,
                        };

                        break;
                    case 1:
                        keyboard = new ReplyKeyboardMarkup()
                        {
                            Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[0])
                                          }),
                     new List<KeyboardButton>(new List<KeyboardButton>
                    {
                       new KeyboardButton("Закрыть меню выбора")
                    })

                },
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true,
                        };

                        break;
                    case 2:
                        keyboard = new ReplyKeyboardMarkup()
                        {
                            Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[0]),
                        new KeyboardButton(strArr[1])
                    }),
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {

                        new KeyboardButton("Закрыть меню выбора")
                    })

                },
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true,
                        };

                        break;

                    case 3:
                        keyboard = new ReplyKeyboardMarkup()
                        {
                            Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[0]),
                        new KeyboardButton(strArr[1])
                    }),
                     new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[2]),

                    }),
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {

                        new KeyboardButton("Закрыть меню выбора")
                    })

                },
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true,
                        };

                        break;

                    case 4:
                        keyboard = new ReplyKeyboardMarkup()
                        {
                            Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[0]),
                        new KeyboardButton(strArr[1])
                    }),
                     new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[2]),
                        new KeyboardButton(strArr[3])

                    }),
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {

                        new KeyboardButton("Закрыть меню выбора")
                    })

                },
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true,
                        };

                        break;
                    case 5:
                        keyboard = new ReplyKeyboardMarkup()
                        {
                            Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[0]),
                        new KeyboardButton(strArr[1])
                    }),
                     new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[2]),
                        new KeyboardButton(strArr[3])

                    }),
                      new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[4])


                    }),
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {

                        new KeyboardButton("Закрыть меню выбора")
                    })

                },
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true,
                        };

                        break;
                    case 6:
                        keyboard = new ReplyKeyboardMarkup()
                        {
                            Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[0]),
                        new KeyboardButton(strArr[1])
                    }),
                     new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[2]),
                        new KeyboardButton(strArr[3])

                    }),
                      new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[4]),
                        new KeyboardButton(strArr[5])


                    }),
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {

                        new KeyboardButton("Закрыть меню выбора")
                    })

                },
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true,
                        };

                        break;
                    case 7:
                        keyboard = new ReplyKeyboardMarkup()
                        {
                            Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[0]),
                        new KeyboardButton(strArr[1])
                    }),
                     new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[2]),
                        new KeyboardButton(strArr[3])

                    }),
                      new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[4]),
                        new KeyboardButton(strArr[5])


                    }),
                        new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[6])



                    }),
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {

                        new KeyboardButton("Закрыть меню выбора")
                    })

                },
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true,
                        };

                        break;
                    case 8:
                        keyboard = new ReplyKeyboardMarkup()
                        {
                            Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[0]),
                        new KeyboardButton(strArr[1])
                    }),
                     new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[2]),
                        new KeyboardButton(strArr[3])

                    }),
                      new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[4]),
                        new KeyboardButton(strArr[5])


                    }),
                         new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton(strArr[6]),
                        new KeyboardButton(strArr[7])


                    }),
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {

                        new KeyboardButton("Закрыть меню выбора")
                    })

                },
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true,
                        };

                        break;
                    default:
                        
                        break;
                }


                return keyboard;
            }

       

            var replyKeyboard2 = GetKeyboardButtons(userState.MenuListCat);


            switch (message.Text)
            {

                case "Мясо":
                case "Рыба":
                case "Выпечка":
                case "Овощи":
                case "Фрукты":
                case "Молочная продукция":
                case "Крупы":
                case "Сладости":


                    if (userState.MenuListCat.Contains(message.Text)==true)
                    {

                        userState.MenuListCat.Remove(message.Text);

                        userState.menuCat = userState.MenuListCat.ToArray();
                        Client.SendTextMessageAsync(message.Chat.Id, $"Удалить ещё одну категорию?", replyMarkup: replyKeyboard2);
                        Console.WriteLine(userState.MenuListCat.Count());
                        userState.ConversationState = ConversationState.DelCL;

                    }
                    else 
                    {//var replyKeyboard1 = new ReplyKeyboardMarkup(GetKeyboardButtons(reg));
                        Client.SendTextMessageAsync(message.Chat.Id, "Список пуст");
                        userState.ConversationState = ConversationState.END;
                    }

                    //else
                    //{
                       
                    //    Client.SendTextMessageAsync(message.Chat.Id, "Вы уже удалили данную категорию. Возможно вы хотите удалить другую категорию?", replyMarkup: replyKeyboard2);
                    //    //userState.ConversationState = ConversationState.DelCL;
                    //}
                    break;
                case "Закрыть меню выбора":
                    userState.ConversationState = ConversationState.END;
                    break;
                default:
                    Client.SendTextMessageAsync(message.Chat.Id, $"Вы ввели неверные данные, выберете пожалуйста из меню категорию", replyMarkup: replyKeyboard2);
                    break;
            }

            return userState;
        }
    }
}