using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface ITransactionImportService
    {
        Task Import(string filePath);
    }
}