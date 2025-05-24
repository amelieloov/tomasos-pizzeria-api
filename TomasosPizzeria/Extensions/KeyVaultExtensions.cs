using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace TomasosPizzeria.Api.Extensions
{
    public static class KeyVaultExtensions
    {
        public static async Task<Dictionary<string, string>> GetSecretsFromKeyVaultAsync(this WebApplicationBuilder builder, params string[] secretNames)
        {
            //test this part
            var keyVaultUri = builder.Configuration.GetValue<string>("KeyVault:KeyVaultURL");
            var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());


            var secrets = new Dictionary<string, string>();

            foreach (var name in secretNames)
            {
                var secret = await client.GetSecretAsync(name);
                secrets[name] = secret.Value.Value;
            }

            return secrets;
        }
    }
}
