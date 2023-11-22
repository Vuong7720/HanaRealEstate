using Hana.Services;

namespace Hana.Interfaces
{
    public interface IVerification
    {
        Task<VerificationResult> StartVerificationAsync(string phoneNumber);

        Task<VerificationResult> CheckVerificationAsync(string phoneNumber, string code);
    }
}
