using System.ComponentModel.DataAnnotations;

namespace CrudWebSocket2.Models
{
    public class ClienteModels
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public string? Email { get; set; }

        public string? Rol { get; set; }
    }
}
