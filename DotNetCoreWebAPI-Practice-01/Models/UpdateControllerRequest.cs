namespace DotNetCoreWebAPI_Practice_01.Models
{
    public class UpdateControllerRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long Phone { get; set; }
    }
}
