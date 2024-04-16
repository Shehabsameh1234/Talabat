namespace Talabat.APIs.Errors
{
    public class ApisValidationErrors:ApisResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApisValidationErrors()
            :base(400)
        {
            Errors = new List<string>();
        }
    }
}
