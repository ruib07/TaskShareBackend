using System.ComponentModel.DataAnnotations.Schema;

namespace TaskShare.Entities.Efos
{
    public class RegisterUserEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegisterUserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
