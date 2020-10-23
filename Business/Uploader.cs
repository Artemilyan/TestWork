using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.WebPages;
using file_uploader.DAL;
using file_uploader.Extensions;
using file_uploader.Models;
using NLog;

namespace file_uploader.Business
{
    public class Uploader : IUploader
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IPaymentsModelRepository repository;

        public Uploader()
        {
            repository = new PaymentsModelRepository();
        }

        public Task<int> Upload(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                logger.Error("String can't be empty, NULL or whitespace");
                throw new InvalidOperationException("String can't be empty, NULL or whitespace");
            }

            var dataList = Regex.Matches(str, @"\nДата=(.*)\nНомер=(.*)\nСумма=(.*)\n");
            var Groupsize = new int[3];
            var a = 0;

            for (var i = 0; i < dataList.Count; i++)
            {
                Groupsize[a]++;
                Console.WriteLine(dataList[i].Groups[a + 1].Value);
                a = (a + 1) % 3;
            }

            var paymentsModelsList = dataList.OfType<Match>()
                .Select(x => new PaymentsModel
                {
                    Number = x.Groups[2].Value.AsInt(),
                    Date = x.Groups[1].Value.AsDateTime(),
                    Summ = x.Groups[3].Value.AsDecimalUs(),
                })
                .ToList();

            return repository.insertAsync(paymentsModelsList);
        }

        public Task<PaymentsModel> GetByNumber(int number)
        {
            return repository.GetByNumberAsync(number);
        }
    }
}