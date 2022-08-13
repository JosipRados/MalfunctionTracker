namespace MalfunctionTrackerData.DataAccess
{
    public interface IPriorityData
    {
        Task CreatePriority(PriorityModel priority);
        Task<List<PriorityModel>> GetAllPriorities();
    }
}