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
        public string Name { get; set; }
        public int Age { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string ImgURL { get; set; }
        
        
        public string Email { get; set; }
        public string Gender { get; set; }

        public string userID { get; set; }
        public List<string> TypeAnimals { get; set; }
        public List<Animal> LikedAnimals { get; set; }

        public Animal()
        {
            LikedAnimals = new List<Animal>();
            TypeAnimals = new List<string>();
            


        }


    }
}