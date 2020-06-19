using Telegram.Bot;

namespace FoodBot.Conversations
{
    internal abstract class ConversationBase
    {
        public ConversationBase(TelegramBotClient client)
        {
            Client = client;
        }

        protected TelegramBotClient Client { get; private set; }
    }
}