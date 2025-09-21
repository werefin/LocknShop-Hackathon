using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LocknShop.Models
{
    public class UserCart
    {

        [Key]
        [Required]
        public string UserId { get; set; }
        [Required]
        public string CartDataJSON { get; set; }
    }
}
