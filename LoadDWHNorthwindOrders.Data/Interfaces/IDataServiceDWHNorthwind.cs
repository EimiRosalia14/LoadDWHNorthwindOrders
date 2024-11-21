using LoadDWHNorthwindOrders.Data.Result;

namespace LoadDWHNorthwindOrders.Data.Interfaces
{
    public interface IDataServiceDWHNorthwind
    {
        Task<OperationResult> LoadDWHAsync();
    }
}
