using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleWebApp.core.Context;
using SimpleWebApp.core.Interface;
using SimpleWebApp.core.Model;
using SimpleWebApp.data.Model;
using SimpleWebApp.data.ViewModel;

namespace SimpleWebApp.core.Service
{
    public class SimpleWebAppService : ISimpleWebAppService
    {
        private readonly SimpleDbContext _dbContext;

        public SimpleWebAppService(SimpleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // For login function
        public async Task<Result<UserDetails>> ValidateUserCredentials(LoginViewModel loginData)
        {
            if (loginData == null)
            {
                return new Result<UserDetails> { ErrorMessage = "Invalid login data." };
            }

            try
            {
                var dbUser = await _dbContext.UserDetails
                    .Where(u => u.EmailAddress == loginData.UserEmail && u.IsActive)
                    .Select(u => new { u.Password, u })
                    .SingleOrDefaultAsync(u => u.Password == loginData.Password);

                if (dbUser == null)
                {
                    return new Result<UserDetails> { ErrorMessage = "Please enter a valid email or password." };
                }

                return new Result<UserDetails> { Data = dbUser.u };
            }
            catch (Exception)
            {
                return new Result<UserDetails> { ErrorMessage = "Login failed. Please try again later." };
            }
        }

        // For register client/admin function
        public async Task<Result<UserDetails>> RegisterNewUser(UserDetails newUser)
        {
            if (newUser == null)
            {
                return new Result<UserDetails> { ErrorMessage = "Invalid data. Please fill in all the needed information." };
            }

            try
            {
                // Check if existing email
                var existingUser = await _dbContext.UserDetails
                    .Where(u => u.EmailAddress == newUser.EmailAddress)
                    .AnyAsync();

                if (existingUser)
                {
                    return new Result<UserDetails> { ErrorMessage = "Email already used." };
                }

                await _dbContext.AddAsync(newUser);
                await _dbContext.SaveChangesAsync();
                return new Result<UserDetails> { Data = newUser };
            }
            catch (Exception)
            {
                return new Result<UserDetails> { ErrorMessage = "User creation failed." };
            }
        }

        // For edit client/admin function
        public async Task<Result<UserDetails>> ModifyUserDetails(UserDetails userDetails)
        {
            if (userDetails == null)
            {
                return new Result<UserDetails> { ErrorMessage = "Invalid user data." };
            }

            try
            {
                // Retrieve the existing user from the database
                var existingUser = await _dbContext.UserDetails
                    .SingleOrDefaultAsync(u => u.ID == userDetails.ID);

                if (existingUser == null)
                {
                    return new Result<UserDetails> { ErrorMessage = "User not found." };
                }

                // Check if email is already used by another user
                var emailInUse = await _dbContext.UserDetails
                    .Where(u => u.EmailAddress == userDetails.EmailAddress && u.ID != userDetails.ID)
                    .AnyAsync();

                if (emailInUse)
                {
                    return new Result<UserDetails> { ErrorMessage = "Email already used by another user." };
                }

                // Save changes to the database
                existingUser.FirstName = userDetails.FirstName;
                existingUser.LastName = userDetails.LastName;
                existingUser.PhoneNumber = userDetails.PhoneNumber;
                existingUser.EmailAddress = userDetails.EmailAddress;
                existingUser.Address = userDetails.Address;
                existingUser.Notes = userDetails.Notes;
                existingUser.Birthday = userDetails.Birthday;
                await _dbContext.SaveChangesAsync();

                return new Result<UserDetails> { Data = existingUser };
            }
            catch (Exception)
            {
                return new Result<UserDetails> { ErrorMessage = "User detail modification failed." };
            }
        }

        // For delete client/admin function - but instead of delete the user account is only deactivated
        public async Task<Result<UserDetails>> DeactivateUser(int userId)
        {
            try
            {
                var user = await _dbContext.UserDetails.SingleOrDefaultAsync(u => u.ID == userId);

                if (user != null)
                {
                    user.IsActive = false;
                    await _dbContext.SaveChangesAsync();
                    return new Result<UserDetails> { Data = user };
                }

                return new Result<UserDetails> { ErrorMessage = "User not found." };
            }
            catch (Exception)
            {
                return new Result<UserDetails> { ErrorMessage = "User deactivation failed." };
            }
        }

        // Get user details by ID
        public async Task<Result<UserDetails>> FetchUserById(int userId)
        {
            try
            {
                var user = await _dbContext.UserDetails.SingleOrDefaultAsync(u => u.ID == userId);
                if (user != null)
                {
                    return new Result<UserDetails> { Data = user };
                }
                return new Result<UserDetails> { ErrorMessage = "User not found." };
            }
            catch (Exception)
            {
                return new Result<UserDetails> { ErrorMessage = "Failed to retrieve user details." };
            }
        }

        // Get all clients
        public async Task<Result<List<UserDetails>>> RetrieveAllClients()
        {
            try
            {
                var clients = await _dbContext.UserDetails
                    .Where(u => u.UserTypeID == 2 && u.IsActive)
                    .ToListAsync();
                return new Result<List<UserDetails>> { Data = clients };
            }
            catch (Exception)
            {
                return new Result<List<UserDetails>> { ErrorMessage = "Failed to retrieve clients." };
            }
        }

        // Get all admins
        public async Task<Result<List<UserDetails>>> RetrieveAllAdmins()
        {
            try
            {
                var admins = await _dbContext.UserDetails
                    .Where(u => u.UserTypeID == 1 && u.IsActive)
                    .ToListAsync();
                return new Result<List<UserDetails>> { Data = admins };
            }
            catch (Exception)
            {
                return new Result<List<UserDetails>> { ErrorMessage = "Failed to retrieve admins." };
            }
        }
    }

}
