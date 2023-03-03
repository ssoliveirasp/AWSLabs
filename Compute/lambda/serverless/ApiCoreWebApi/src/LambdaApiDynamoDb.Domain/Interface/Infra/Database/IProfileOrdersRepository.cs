using lambdaApi.Domain.Entities;
using lambdaApi.Domain.Entities.Orders;
using lambdaApi.Domain.Enums;
using static lambdaApi.Domain.Enums.EnumsApi;

namespace lambdaApi.Domain.Interface.Infra.Database
{
    public interface IOrdersProfileRepository
    {
        Task CreateTableFromAssembly();
        Task SaveAsync(ProfileOrders item);
        Task<ProfileOrders> SearchProfileByIdAsync(string Id);
        Task<IEnumerable<ProfileOrders>> SearchAllProfileByAddressOrdersType(List<AddressOrdersType> typeAddress);
        Task DeleteTable();
    }
}