using Firebase.Auth;
using Firebase.Auth.Providers;
using GorselProgOdevFinal.Helpers;
using Newtonsoft.Json;

namespace GorselProgOdevFinal.Services
{
    public class AuthService
    {
        private readonly FirebaseAuthClient _authClient;

        public AuthService()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = Constants.FirebaseApiKey,
                AuthDomain = Constants.FirebaseAuthDomain,
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            };

            _authClient = new FirebaseAuthClient(config);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
            return userCredential.User.Uid;
        }

        public async Task<string> RegisterAsync(string email, string password, string username)
        {
            var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password, username);
            return userCredential.User.Uid;
        }
    }
}