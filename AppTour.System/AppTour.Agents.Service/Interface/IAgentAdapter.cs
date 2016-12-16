
namespace AppTour.Agents.Service.Interface
{
    public interface IAgentAdapter
    {
        string Execute(int MaxRequestPerDay, int Rating, string LastReference, string AdapterName);
    }
}


