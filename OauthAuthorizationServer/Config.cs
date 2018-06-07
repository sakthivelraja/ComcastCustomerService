using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;


namespace OauthAuthorizationServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("scope.readAccess","Example API"),
                new ApiResource("scope.fullAccess", "Example API"),
                new ApiResource("You can do whatever you want","Example API")
            };

        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
           {
               new Client
               {
                   ClientId = "ReadOnlyClient",
                   AllowedGrantTypes  = GrantTypes.ClientCredentials,

                   ClientSecrets = { new Secret("secret1".Sha256())},
                   AllowedScopes = {"scope.readAccess"}
               },
               new Client
               {
                   ClientId = "FullAccessClient",
                   AllowedGrantTypes = GrantTypes.ClientCredentials,
                   ClientSecrets = {new Secret("secret2".Sha256())},
                   AllowedScopes = {"scope.fullAccess"}
               }
           };
        }
    }
}
