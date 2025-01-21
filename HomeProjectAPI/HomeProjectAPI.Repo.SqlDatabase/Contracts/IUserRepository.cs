using HomeProjectAPI.Repo.SqlDatabase.DTO;

namespace HomeProjectAPI.Repo.SqlDatabase.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string id);

        Task<User> CreateAsync(User project);

        Task<User> UpdateAsync(User project);

        Task<User> UpsertAsync(User project);

        Task<int> DeleteAsync(string projectId);
    }
}
