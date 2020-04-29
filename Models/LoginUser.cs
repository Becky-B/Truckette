using System;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Truckette.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "**E-mail address is required**")]
        [DataType(DataType.EmailAddress, ErrorMessage = "**E-mail address is not valid**")]
        public string LoginEmail {get; set;}

        [Required(ErrorMessage = "**Password is required**")]
        public string LoginPassword {get; set; }

    }
}