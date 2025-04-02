using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L02P02_2022HM651_2022DP650.Models
{
    public class Comentarios
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Libro")]
        public int idLibro { get; set; }

        [Required]
        [StringLength(500)]  
        public string comentarios { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        public virtual Libros id_libro { get; set; }
    }
}
