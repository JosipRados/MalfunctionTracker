namespace MalfunctionTrackerData.DataAccess
{
    public interface IMalfunctionData
    {
        Task CreateMalfunction(MalfunctionModel malfunction);
        Task<List<MalfunctionModel>> GetAllAcknowledgedMalfunctions();
        Task<List<MalfunctionModel>> GetAllMalfunctions();
        Task<List<MalfunctionModel>> GetAllMalfunctionsWaitingAcknowledgement();
        Task<MalfunctionModel> GetMalfunction(string id);
        Task<List<MalfunctionModel>> GetUsersMalfunctions(string userId);
        Task UpdateMalfunction(MalfunctionModel malfunction);
    }
}