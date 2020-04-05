
namespace DotNetCoreApiAuthenticationWithJWT.Models
{
    public interface IJwtAuthenticationManager
    {
        // returns the jwt after verifying the user
        public string Authenticate(string username, string password);
    }
}
