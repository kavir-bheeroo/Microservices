using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microservices.Services.Identity.API.Extensions
{
    public static class SigningKeyExtensions
    {
        public static IIdentityServerBuilder AddCertificateFromFile(this IIdentityServerBuilder builder, IHostingEnvironment environment, IConfiguration configuration)
        {
            var keyFilePath = Path.Combine(environment.ContentRootPath, configuration.GetValue<string>("SigningKeyCredentials:KeyFilePath"));
            var keyFilePassword = configuration.GetValue<string>("SigningKeyCredentials:KeyFilePassword");

            if (File.Exists(keyFilePath))
            {
                Console.WriteLine($"SigningCredentialExtension adding key from file {keyFilePath}");

                // You can simply add this line in the Startup.cs if you don't want an extension. 
                // This is neater though ;)
                builder.AddSigningCredential(new X509Certificate2(keyFilePath, keyFilePassword));
            }
            else
            {
                Console.WriteLine($"SigningCredentialExtension cannot find key file {keyFilePath}");
            }

            return builder;
        }

    }
}