using System.ComponentModel.DataAnnotations.Schema;

namespace TaskShare.Entities.Efos
{
    public class TaskEfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
    }
}
