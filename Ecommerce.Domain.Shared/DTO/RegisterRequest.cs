using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Shared.DTO;

    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }
    }

