using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L02P02_2022HM651_2022DP650.Models
{
    public class Comentarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int Id { get; set; }

        [Column("id_libro")] // Mapear explícitamente a la columna id_libro
        [ForeignKey("Libro")]
        public int idLibro { get; set; }

        [Required]
        [StringLength(500)] // Ajusta según varchar(max) si necesitas más longitud
        public string comentarios { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [Required]
        [Column("created_at")] // Mapear a la columna created_at
        public DateTime createdAt { get; set; }

        public virtual Libros Libro { get; set; }
    }
}