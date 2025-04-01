using System.ComponentModel.DataAnnotations;

namespace L02P02_2022HM651_2022DP650.Models
{
    public class Autores
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? autor { get; set; }
    }
}
