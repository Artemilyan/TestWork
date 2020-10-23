using file_uploader.Models;
using System.Threading.Tasks;

namespace file_uploader.Business
{
   public interface IUploader
    {
        Task<int> Upload(string str);
        Task<PaymentsModel> GetByNumber(int number);
    }
}
