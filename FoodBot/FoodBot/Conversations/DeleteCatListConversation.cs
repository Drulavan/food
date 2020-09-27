using FoodBot.Dal.Models;
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
            var ListCat = userState.MenuListCat;
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


                    if (userState.MenuListCat.Contains(message.Text) == true)
                    {
                        ListCat.Remove(message.Text);

                        userState.MenuListCat = ListCat;
                        userState.menuCat = ListCat.ToArray();
                        Client.SendTextMessageAsync(message.Chat.Id, $"Удалить ещё одну категорию?", replyMarkup: keyboard);
                        userState.ConversationState = ConversationState.DelCL;

                    }
                    else if (userState.MenuListCat.Count == 0)
                    {
                        Client.SendTextMessageAsync(message.Chat.Id, "Список пуст,нажмите в меню Закрыть меню выбора", replyMarkup: keyboard);
                        userState.ConversationState = ConversationState.END;
                    }



                    else
                    {
                        Client.SendTextMessageAsync(message.Chat.Id, "Хотите удалить ещё категорию?",replyMarkup: keyboard);
                    }
                    break;
                case "Закрыть меню выбора":
                    userState.ConversationState = ConversationState.END;
                    break;
                default:

                    break;
            }

            return userState;
        }
    }
}