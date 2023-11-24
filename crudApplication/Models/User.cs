using Microsoft.AspNetCore.Identity;

namespace crudApplication.Models
{
    public class User
    {
        public int userId { get; set; }
        public string? namalengkap { get; set; }

        public string? username { get; set; }

        public string? password { get; set; }
        public char status { get; set; }
    }
}
