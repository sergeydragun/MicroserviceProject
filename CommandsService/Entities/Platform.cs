using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommandsService.Entities
{
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExternalID { get; set; }
        [Required]
        public string Name { get; set; }
        [InverseProperty(nameof(Platform))]
        public virtual List<Command> Commands { get; set;}
    }
}
