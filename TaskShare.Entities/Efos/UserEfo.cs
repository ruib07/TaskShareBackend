using System.ComponentModel.DataAnnotations.Schema;

namespace TaskShare.Entities.Efos
{
    public class UserEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegisterUserId { get; set; }

        public RegisterUserEfo RegisterUser { get; set; }
    }
}
