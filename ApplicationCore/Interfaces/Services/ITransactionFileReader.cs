using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.Services
{
    public interface ITransactionFileReader
    {
        string FileType { get; }
        Task<List<Transaction>> Read(string path);
    }
}
