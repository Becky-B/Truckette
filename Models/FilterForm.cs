using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Truckette.Models
{
    public class FilterForm
    {
        [Required]
        public string ToLookFor { get; set; }
    }
}
