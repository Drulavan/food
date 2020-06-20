namespace FoodBot.States
{
    public class UserState
    {
        public UserState(long id)
        {
            this.Id = id;
        }

        public ConversationState ConversationState { get; set; }
        public long Id { get; set; }
        
        public bool isRegistered { get; set; }
    }
}