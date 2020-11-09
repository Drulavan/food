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
            //List<string> MLC = new List<string>();
            Console.WriteLine(userState.RadiusFind);

            //if (userState.MenuListCat == null)
            //{
            //    var MC = userState.menuCat;

            //    foreach (string ms in MC)

            //    {
            //        MLC.Add(ms);

            //    }

            //    userState.MenuListCat = MLC;


            //}
            var ListCat = userState.MenuListCat;

            var keyboardMenu = new ReplyKeyboardMarkup
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
            var keyboardListCat = new ReplyKeyboardMarkup
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
            var keyboardGeo = new ReplyKeyboardMarkup
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
            ReplyKeyboardMarkup GetKeyboardButtons(List<string> strArr)
            {
                ReplyKeyboardMarkup keyboard;
               keyboard = new ReplyKeyboardMarkup(); 
                switch (strArr.Count())
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

            
           
           var replyKeyboard1 = GetKeyboardButtons(userState.MenuListCat);

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
                    Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категорию, которую хотите удалить", replyMarkup: replyKeyboard1);
                   
                    userState.ConversationState = ConversationState.DelCL;

                    break;

                case "Добавить категорию":
                    Client.SendTextMessageAsync(message.Chat.Id, $"Выберите категорию, которую хотите добавить", replyMarkup: keyboardListCat);
                    userState.ConversationState = ConversationState.SeX;

                    break;
                case "Изменить радиус поиска":
                    Client.SendTextMessageAsync(message.Chat.Id, $"Укажите радиус поиска предложений (в километрах)");
                    userState.ConversationState = ConversationState.RadNew;

                    break;
                case "Изменить геолокацию":
                    Client.SendTextMessageAsync(message.Chat.Id, $" Пожалуйста, укажите Ваше местоположение.", replyMarkup: keyboardGeo);
                    userState.ConversationState = ConversationState.GeoNew;

                    break;
                case "Пройти регистрацию заново":
                   
                    Client.SendTextMessageAsync(message.Chat.Id, $" Пожалуйста, укажите Ваше местоположение.", replyMarkup: keyboardGeo);
                    userState.IsRegistered = false;
                    userState.IsNewRegistered = true;
                    userState.MenuListCat.Clear();
                    Console.WriteLine(userState.MenuListCat.Count);
                    userState.menuCat = userState.MenuListCat.ToArray();
                    Console.WriteLine(userState.menuCat.Length);
                    userState.ConversationState = ConversationState.GeoNew;
                    
                    break;

                default:
                    Client.SendTextMessageAsync(message.Chat.Id, "Введите одну из команд", replyMarkup: keyboardMenu);
                    break;
            }
           // Console.WriteLine(userState.RadiusFind);
            //userState.ConversationState = ConversationState.None;
            // Client.SendTextMessageAsync(message.Chat.Id, "Отлично! Здесь я буду показывать предложения для Вас!");
            return userState;
        }
    }
}