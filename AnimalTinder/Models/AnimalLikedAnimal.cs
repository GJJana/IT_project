using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AnimalTinder.Models
{
    public class AnimalLikedAnimal
    {
        [Key]
        [Column(Order=0)]
        public int ProfileAnimalId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int LikedAnimalId { get; set; }


    }
}