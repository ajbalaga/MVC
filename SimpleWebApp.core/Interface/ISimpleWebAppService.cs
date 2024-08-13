using SimpleWebApp.core.Model;
using SimpleWebApp.data.Model;
using SimpleWebApp.data.ViewModel;

namespace SimpleWebApp.core.Interface
{
    public interface ISimpleWebAppService
    {
        Task<Result<UserDetails>> ValidateUserCredentials(LoginViewModel loginData);
        Task<Result<UserDetails>> RegisterNewUser(UserDetails newUser);
        Task<Result<UserDetails>> ModifyUserDetails(UserDetails userDetails);
        Task<Result<UserDetails>> DeactivateUser(int userId);
        Task<Result<UserDetails>> FetchUserById(int userId);
        Task<Result<List<UserDetails>>> RetrieveAllClients();
        Task<Result<List<UserDetails>>> RetrieveAllAdmins();


    }
}
