using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L02P02_2022HM651_2022DP650.Models
{
    public class Clientes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string? apellido { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string? email { get; set; }

        [StringLength(255)]
        public string? direccion { get; set; }

        [Required]
        public DateTime? createdAt { get; set; }
    }
}
