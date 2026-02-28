using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [MaxLength(200)]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es requerida.")]
        public DateTime FechaDeNacimiento { get; set; }
    }
}
