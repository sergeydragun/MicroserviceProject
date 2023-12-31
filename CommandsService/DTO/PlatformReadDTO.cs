using CommandsService.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommandsService.DTO
{
    public class PlatformReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
