using FoodBot.States;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FoodBot.Conversations
{
    public interface IConversation
    {
        ConversationState ConversationState { get; }

        UserState Execute(Message message,  UserState userState);
    }
}