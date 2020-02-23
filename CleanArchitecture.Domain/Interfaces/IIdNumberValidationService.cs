using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IIdNumberValidationService
    {
        Task<bool> IsIdNumberValid(string idNumber);
    }
}
