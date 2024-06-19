
namespace Talabat.APIs.Errors
{
    public class ApisResponse
    {
        public int StatusCode { get; set; }
        public string? Messege { get; set; }
        public ApisResponse(int statusCode,string? messege=null)
        {
            StatusCode = statusCode;
            Messege = messege ?? GetDefaultMessege(statusCode);
        }

        private string? GetDefaultMessege( int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "not authorized",
                404 => "resource not found",
                500 => "error",
                _=>null
            };
        }
    }
}
