using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Truckette.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        //First Name
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "**First name field is required**")]
        [DataType(DataType.Text, ErrorMessage = "**First name field must only contain valid characters**")]
        [MinLength(3, ErrorMessage = "**First name must be at least 3 characters**")]
        public string FirstName { get; set; }

        //Last Name
        [Required(ErrorMessage = "**Last name field is required**")]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text, ErrorMessage = "**Name field must only contain valid characters**")]
        [MinLength(2, ErrorMessage = "**Last name must be at least 2 characters**")]
        public string LastName { get; set; }

        //E-mail Address
        [Required(ErrorMessage = "**E-mail address is required**")]
        [DataType(DataType.EmailAddress, ErrorMessage = "**E-mail address is not valid**")]
        public string Email { get; set; }
        
        //Password
        [Required(ErrorMessage = "**Password is required**")]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
            ErrorMessage="Password should be a min length of 8 characters, contain at least 1 number, 1 letter, 1 capital letter, and a special character.")]
        public string Password { get; set; }

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        //Confirm Password
        [Required(ErrorMessage = "**Confirm password is required**")]
        [NotMapped]
        [Compare("Password", ErrorMessage = "**Passwords do not match**")]
        [Display(Name = "Confirm Password")]
        public string Confirm { get; set; }

        [Display(Name = "Street Address")]
        public string StreepAddress { get; set; }

        [Display(Name= "Zip Code")]
        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }

        [DataType(DataType.CreditCard)]
        public int CreditCardNo { get; set; }

        [DataType(DataType.DateTime)]
        public string DOB { get; set; }
    }
}