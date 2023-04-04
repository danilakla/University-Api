
namespace UniversityApi.Services.CryptoService;

public interface ICryptoService
{
    string EncryptSecretString<T>(T data);
    T DecryptSecretString<T>(string secretStirng);
}
