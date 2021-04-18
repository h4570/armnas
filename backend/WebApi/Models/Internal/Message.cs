using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Internal
{

    public enum MessageType
    {
        Information,
        Warning,
        Error
    }

    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ShortName { get; set; }
        [StringLength(200)]
        public string Tooltip { get; set; }
        [Required]
        public MessageType Type { get; set; }
        public bool HasBeenRead { get; set; }
        [Required]
        [StringLength(50)]
        public string Author { get; set; }
    }
}
