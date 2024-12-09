using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Popsy.Models
{
    public class TaskModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaVencimiento { get; set; }

        [Required]
        public Estado Estado { get; set; }
    }

    public enum Estado
    {
        Pendiente,
        EnProgreso,
        Completada
    }
}
