using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rajpal.Models
{
    public class Person
    {
        [Key]
        [ScaffoldColumn(false)]
        public int PersonId { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string Country { get; set; }
    }
}