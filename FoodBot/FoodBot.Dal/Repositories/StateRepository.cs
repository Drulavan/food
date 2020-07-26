using FoodBot.Dal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodBot.Dal.Repositories
{
    public class StateRepository : BaseRepository<UserState>
    {
        public StateRepository() : base("States")
        {
        }
    }
}
