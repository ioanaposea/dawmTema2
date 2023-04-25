using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class RegisterDto
    {
        [Required, MaxLength(100)]
        public string Firstname { get; set; }

        [Required, MaxLength(100)]
        public string Lastname { get; set; }
        
        [Required, MaxLength(100)]
        public string Email { get; set; }
        
        [Required, MaxLength(100)]
        public string Password { get; set; }
        
        [Required]
        public int RoleId { get; set; }
    }
}
