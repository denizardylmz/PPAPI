using System.Threading.Tasks;
using HomeProjectAPI.Services.Model;
using HomeProjectAPI.Services.Model.Requests;
using HomeProjectAPI.Services.Model.Responses;

namespace HomeProjectAPI.Services.Contracts
{
    public interface IUserService2
    {
        Task<Response<Request<UserCreation>, User>> CreateAsync(Request<UserCreation> request);

        Task<Response<Request<User>, User>> UpdateAsync(Request<User> request);

        Task<Response<Request<string>, int>> DeleteAsync(Request<string> request);

        Task<User> GetAsync(string id);
    }
}