using System.Collections.Generic;
using System.Threading.Tasks;
using file_uploader.Models;

namespace file_uploader.DAL
{
    public interface IPaymentsModelRepository
    {
        public Task<int> insertAsync(List<PaymentsModel> dataList);

        public Task<PaymentsModel> GetByNumberAsync(int number);
    }
}