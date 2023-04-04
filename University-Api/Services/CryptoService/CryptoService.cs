using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace UniversityApi.Services.CryptoService;

public class CryptoService : ICryptoService
{   
        private readonly IDataProtector dataProtectionProvider;

    public CryptoService(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
    {
        this.dataProtectionProvider = dataProtectionProvider.CreateProtector(configuration["AppSettings:SecretKeyCrypto"]);
    }

    public string EncryptSecretString<T>(T data)
    {
        try
        {
            try
            {
                var encryptData = dataProtectionProvider.Protect(JsonConvert.SerializeObject(data));
                return encryptData.ToString();

            }
            catch (Exception)
            {

                throw;
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    public T DecryptSecretString<T>(string secretStirng)
    {
        try
        {
            var decryptData = dataProtectionProvider.Unprotect(secretStirng);
            var data = JsonConvert.DeserializeObject<T>(decryptData);
            return data;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
