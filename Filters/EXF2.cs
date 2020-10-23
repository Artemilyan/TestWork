using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using NLog;

namespace file_uploader.Filters
{
    public class EXF2 : ExceptionFilterAttribute

    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext == null)
            {
                logger.Error("NullpointerException");
                throw new InvalidOperationException("Nullpointer");
            }

            var errorResponse = new HttpResponseMessage(HttpStatusCode.BadGateway);
            if (actionExecutedContext.Exception != null)
            {
                errorResponse.ReasonPhrase = actionExecutedContext.Exception.Message;
                actionExecutedContext.Response = errorResponse;
                logger.Error(actionExecutedContext.Exception, actionExecutedContext.Exception.Message);
                base.OnException(actionExecutedContext);
            }
        }
    }
}