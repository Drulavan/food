using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodBot.Parsers.Jobs
{
    public interface IJob
    {
        Task Execute();
    }
}
