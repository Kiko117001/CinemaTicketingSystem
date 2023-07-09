using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaTicketingSystem.Domain.DTO
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Name required")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name required")]
        [StringLength(100)]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password required")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }

        [Range(1, 2)]
        public string Role { get; set; }
    }
}
