using ApiConsumer.Models;

namespace ApiConsumer.Services
{
    public interface IServiceAPI
    {
        Task<List<Custommer>> GetAllCustommers();
        Task<Custommer> GetCustommerById(string id);
        Task<bool> InsertCustommer(Custommer custommer);
        Task<bool> UpdateCustommer(Custommer custommer);
        Task<bool> DeleteCustommer(string id);
    }
}
