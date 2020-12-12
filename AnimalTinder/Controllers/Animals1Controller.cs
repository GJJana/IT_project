using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AnimalTinder.Models;
using Microsoft.AspNet.Identity;

namespace AnimalTinder.Controllers
{
    public class Animals1Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();





        // GET: api/Animals1
        public IQueryable<Animal> GetAnimals()
        {
            return db.Animals;
        }

        // GET: api/Animals1/5
        [ResponseType(typeof(Animal))]
        public IHttpActionResult GetAnimal(int id)
        {
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return NotFound();
            }
            string userId = animal.userID;
            foreach (Animal a in db.Animals.ToList())
            {
                if (a.userID.Equals(userId))
                {
                    //a.LikedAnimals.Add(animal);
                }
            }

            db.SaveChanges();

            return Ok(animal);
        }

        // PUT: api/Animals1/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnimal(int id)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != animal.ID)
            {
                return BadRequest();
            }

            db.Entry(animal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            */
            
            
           //se kreira nov red vo tabelata 
            string userId = User.Identity.GetUserId();
            int userAnimalID = 0;
            foreach(Animal a in db.Animals)
            {
                if (a.userID.Equals(userId))
                    userAnimalID = a.ID;

            }
            AnimalLikedAnimal animalLiked = new AnimalLikedAnimal();
            animalLiked.LikedAnimalId = id;
            animalLiked.ProfileAnimalId = userAnimalID;
            db.AnimalLikedAnimals.Add(animalLiked);
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
            //return Ok(animal);

        }

        // POST: api/Animals1
        [ResponseType(typeof(Animal))]
        public IHttpActionResult PostAnimal(Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Animals.Add(animal);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = animal.ID }, animal);
        }

        // DELETE: api/Animals1/5
        [ResponseType(typeof(Animal))]
        public IHttpActionResult DeleteAnimal(int id)
        {
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return NotFound();
            }
            string userId = animal.userID;
            db.Animals.Remove(animal);
            db.Users.Remove(db.Users.Find(userId));
            db.SaveChanges();

            return Ok(animal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnimalExists(int id)
        {
            return db.Animals.Count(e => e.ID == id) > 0;
        }
    }
}