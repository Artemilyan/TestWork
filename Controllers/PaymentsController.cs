using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using file_uploader.Business;
using file_uploader.Filters;
using file_uploader.Models;
using NLog;

namespace file_uploader.Controllers
{
    [EXF2]
    public class PaymentsController : ApiController
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IUploader uploader;

        public PaymentsController()
        {
            uploader = new Uploader();
        }
        
        [HttpPost]
        [Route("api/Payments/upload")]
        public async Task<HttpResponseMessage> Upload()
        {
            logger.Info("Started");
            if (!Request.Content.IsMimeMultipartContent())
                throw new InvalidOperationException("Required MimeMultipartContent");

            logger.Info("File meet the requirements, upload starting");
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            var num = 0;

            foreach (var file in provider.Contents)
            {
                var name = file.Headers.ContentDisposition.FileName;
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');

                if (!filename.EndsWith(".txt"))
                {
                    throw new InvalidOperationException("Required '.txt' file only");
                }

                var text = Encoding.Default.GetString(await file.ReadAsByteArrayAsync());
                logger.Info("Perevedeno v stroku");

                try
                {
                    num += await uploader.Upload(text).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.InnerException.Message);
                }
            }

            if (num == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Выписок не найдено");
            }
                
            return Request.CreateResponse(HttpStatusCode.OK, $"Всего сохранено выписок {num}");
        }

        [HttpGet]
        [Route("api/Payments/{Number}")]
        public async Task<PaymentsModel> GetByNumber(int number)
        {
            PaymentsModel num = null;
            if (number < 0)
            {
                throw new InvalidOperationException("Number can't be 0"); 
            }

            num = await uploader.GetByNumber(number).ConfigureAwait(false);

            if (num == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound); 
            }

            return num; 
        }
    }
}