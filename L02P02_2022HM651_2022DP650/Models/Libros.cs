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
        public string? url_imagen { get; set; }

        [Required]
        public int? id_autor { get; set; }

        [Required]
        public int? id_categoria { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? precio { get; set; }

        [Required]
        public char? estado { get; set; }

        [ForeignKey("Id_autor")]
        public Autores? autor { get; set; }

        [ForeignKey("Id_categoria")]
        public Categorias? categoria { get; set; }
    }
}
