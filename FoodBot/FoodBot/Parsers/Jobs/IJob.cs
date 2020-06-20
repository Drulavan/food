using System.Threading.Tasks;

namespace FoodBot.Parsers.Jobs
{
    public interface IJob
    {
        Task Execute();
    }
}