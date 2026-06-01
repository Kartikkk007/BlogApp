using System.Threading.Tasks;

namespace BlogApp.Services
{
    public class SimpleAuthService
    {
        public bool IsAdminLoggedIn { get; private set; }

        public Task<bool> LoginAsync(string username, string password)
        {
      
            if (username.ToLower() == "admin" && password == "password123")
            {
                IsAdminLoggedIn = true;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task LogoutAsync()
        {
            IsAdminLoggedIn = false;
            return Task.CompletedTask;
        }
    }
}
