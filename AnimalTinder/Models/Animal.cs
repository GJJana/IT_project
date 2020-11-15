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
        
        public string Type { get; set; }
        public string Breed { get; set; }
        [Range(0,99)]
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        [Display(Name="Image")]
        public string ImgURL { get; set; }
        [Required]
        public string Email { get; set; }

        public string userID { get; set; }
        
        public virtual ICollection<Animal> LikedAnimals { get; set; }

        public Animal()
        {
            LikedAnimals = new List<Animal>();

        }


    }
}