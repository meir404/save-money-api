using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DAL.Attributes;

namespace Logic.Models
{
    [TableInfo(Name = "Users")]
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password{ get; set; }

        public string Name { get; set; }
    }
}
