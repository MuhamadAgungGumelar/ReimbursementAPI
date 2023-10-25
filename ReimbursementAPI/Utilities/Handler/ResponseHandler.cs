using System.Net;

namespace ReimbursementAPI.Utilities.Handler
{
    //Handler Response OK
    public class ResponseOKHandler<Tentity> where Tentity : class
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public Tentity? Data { get; set; }

        public ResponseOKHandler()
        {

        }

        public ResponseOKHandler(Tentity? data, string message)
        {
            Code = StatusCodes.Status200OK;
            Status = HttpStatusCode.OK.ToString();
            Message = message;
            Data = data;
        }

        public ResponseOKHandler(string message)
        {
            Code = StatusCodes.Status200OK;
            Status = HttpStatusCode.OK.ToString();
            Message = message;
        }
    }

    //Handler Response Bad Request
    public class ResponseBadRequestHandler
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string? Error { get; set; }

        public ResponseBadRequestHandler(string message, string error)
        {
            Code = StatusCodes.Status400BadRequest;
            Status = HttpStatusCode.BadRequest.ToString();
            Message = message;
            Error = error;
        }
    }

    //Handler Response Not Found
    public class ResponseNotFoundHandler
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string? Error { get; set; }

        public ResponseNotFoundHandler(string message)
        {
            Code = StatusCodes.Status404NotFound;
            Status = HttpStatusCode.NotFound.ToString();
            Message = message;
        }
        public ResponseNotFoundHandler(string message, string error)
        {
            Code = StatusCodes.Status404NotFound;
            Status = HttpStatusCode.NotFound.ToString();
            Message = message;
            Error = error;
        }
    }

    //Handler Response Internal Server Error
    public class ResponseInternalServerErrorHandler
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string? Error { get; set; }

        public ResponseInternalServerErrorHandler(string message, string error)
        {
            Code = StatusCodes.Status500InternalServerError;
            Status = HttpStatusCode.InternalServerError.ToString();
            Message = message;
            Error = error;
        }
    }

    public class ResponseValidatorHandler
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }

        public ResponseValidatorHandler(object error)
        {
            Code = StatusCodes.Status400BadRequest;
            Status = HttpStatusCode.BadRequest.ToString();
            Message = "Validations Error";
            Error = error;
        }
    }
}
