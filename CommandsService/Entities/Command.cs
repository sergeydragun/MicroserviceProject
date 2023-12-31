using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommandsService.Entities
{
    public class Command
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string HowTo {  get; set; }
        [Required]
        public string CommandLine { get; set; }
        [ForeignKey(nameof(Platform))]
        [Required]
        public int PlatformId { get; set; }
        
        public virtual Platform Platform { get; set; }
    }
}
