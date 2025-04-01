using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace L02P02_2022HM651_2022DP650.Models
{
    public class Libros
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? nombre { get; set; }

        [StringLength(255)]
        public string? descripcion { get; set; }

        [StringLength(255)]
        public string? urlImagen { get; set; }

        [Required]
        public int? idAutor { get; set; }

        [Required]
        public int? idCategoria { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? precio { get; set; }

        [Required]
        public char? estado { get; set; }

        [ForeignKey("IdAutor")]
        public Autores autor { get; set; }

        [ForeignKey("IdCategoria")]
        public Categorias categoria { get; set; }
    }
}
