namespace Talabat.APIs.Errors
{
    public class ServerErrorException:ApisResponse
    {
        public string? Details { get; set; }
        public ServerErrorException(int statusCode,string? messege=null,string? details=null)
            :base(statusCode,messege)
        {
            Details = details;
        }
    }
}
