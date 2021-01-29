using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimalTinder.Models
{
    public class Animal
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        public string Breed { get; set; }
        [Range(0,99)]
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        [Display(Name="Image")]
        [Required]
        public string ImgURL { get; set; }
        [Required]
        public string Email { get; set; }

        public string userID { get; set; }
        
      

       

    }
}