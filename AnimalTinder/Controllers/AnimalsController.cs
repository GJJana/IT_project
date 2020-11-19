using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AnimalTinder.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AnimalTinder.Controllers
{
    public class AnimalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //FireBase parametars
        private static string ApiKey = "AIzaSyDthxJSwN2hrivXsVQvHn42lD679_KJMlE";
        private static string Bucket = "animal-tinder-2020.appspot.com";
        private static string AuthEmail = "tolevska.m8@gmail.com";
        private static string AuthPassword = "Jana1234!";
       
        [Authorize]
        public ActionResult MyProfile()
        {

            string s = User.Identity.GetUserId();
            Animal animal = new Animal();
            foreach (Animal a in db.Animals)
            {
                if (a.userID.Equals(s))
                {
                    animal = a;
                }
            }

            if (animal == null)
            {
                return HttpNotFound();
            }


            return View(animal);
        }

        //GET:Animals/LikedAnimals
        public ActionResult LikedAnimals(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = db.Animals.Find(id);
            ViewBag.Owner = animal;
            //kreira lista od site lajknai zivotni koi se vo Table so Profile id =id 
            List<Animal> likedAnimals = new System.Collections.Generic.List<Animal>();
            foreach (AnimalLikedAnimal a in db.Table)
            {
                if (a.ProfileAnimalId.Equals(animal.ID.ToString()))
                    likedAnimals.Add(db.Animals.Find(Int32.Parse(a.LikedAnimalId)));
            }
            return View(likedAnimals);
        }

        //GET Carousel
        public ActionResult Carousel()
        {
            ViewBag.Gender = "Non-Binery";
            ViewBag.Type = "All";
            

            foreach (Animal a in db.Animals.ToList())
            {
                if (a.Email.Equals(User.Identity.GetUserName()))
                {
                    ViewBag.Gender = a.Gender;
                    ViewBag.Type = a.Type;
                    
                }

            }
            List<Animal> likedAnimals = new System.Collections.Generic.List<Animal>();
            foreach (AnimalLikedAnimal a in db.Table)
            {
                if (a.ProfileAnimalId.Equals(User.Identity.GetUserName()))
                    likedAnimals.Add(db.Animals.Find(Int32.Parse(a.LikedAnimalId)));
            }
            ViewBag.LikedAnimals = likedAnimals;


            return View(db.Animals.ToList());
        }
        // GET: Animals
        public ActionResult Index()
        {
            ViewBag.Gender = "Non-Binery";
            ViewBag.Type = "All";
            
            foreach(Animal a in db.Animals.ToList())
            {
                if (a.Email.Equals(User.Identity.GetUserName()))
                {
                    ViewBag.Gender = a.Gender;
                    ViewBag.Type = a.Type;
                }

                

            }
            List<Animal> likedAnimals = new System.Collections.Generic.List<Animal>();
            foreach (AnimalLikedAnimal a in db.Table)
            {
                if (a.ProfileAnimalId.Equals(User.Identity.GetUserName()))
                    likedAnimals.Add(db.Animals.Find(Int32.Parse(a.LikedAnimalId)));
            }
            ViewBag.LikedAnimals = likedAnimals;
            return View(db.Animals.ToList());
        }

        // GET: Animals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // GET: Animals/Create
        public ActionResult Create()
        {
            //if the user is already registered he can not create another animal
           foreach (Animal a in db.Animals.ToList())
           {
              if (User.Identity.GetUserId().Equals(a.userID))
                   return RedirectToAction("MyProfile");
           }
            //types of animals list created
            Animal animal = new Animal();
            List<string> tmp= new System.Collections.Generic.List<string> { "Dog", "Turtle", "Rabbit", "Parrot", "Cat", "Fish", "Mouse", "Hamster", "Cow", "Duck", "Shrimp", "Pig", "Goat", "Crab", "Deer", "Bee", "Sheep", "Turkey", "Dove", "Chicken", "Horse", "Bird", "Squirrel", "Ox", "Lion", "Panda", "Walrus", "Otter", "Kangaroo", "Monkey", "Koala", "Mole", "Elephant", "Leopard", "Hippopotamus", "Giraffe", "Fox", "Coyote", "Hedgehong", "Camel", "Starfish", "Alligator", "Owl", "Tiger", "Bear", "Whale", "Raccoon", "Crocodile", "Dolphin", "Snake", "Elk", "Bat", "Hare", "Toad", "Frog", "Rat", "Badger", "Lizard", "Reindeer", "Insect" };
            tmp.Sort();
            ViewBag.AnimalsType = tmp;
            ViewBag.AnimalsGender = new System.Collections.Generic.List<string> { "Male", "Female" };
            return View(animal);
        }

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Type,Breed,Age,Gender,ImgURL")] Animal animal, HttpPostedFileBase file)
        {

            FileStream stream;


            if (ModelState.IsValid)
            {
                //get the loged user ID and email
                animal.userID = User.Identity.GetUserId();
                animal.Email = User.Identity.GetUserName();
                //if the user uplouded an img
                if (file?.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/images/"), file.FileName);
                    file.SaveAs(path);
                    stream = new FileStream(Path.Combine(path), FileMode.Open);
                    Task<string> link = Task.Run(() => Upload(stream, file.FileName));
                    await link.ContinueWith(m =>
                    {
                        Console.WriteLine(m);
                        animal.ImgURL = m.Result;
                        db.Animals.Add(animal);
                        db.SaveChanges();
                    });
                }
                else
                {
                    db.Animals.Add(animal);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(animal);
        }

        //FireBase Upload file 


        public async Task<string> Upload(FileStream stream, string fileName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            //cancalation 
            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
               Bucket,
               new FirebaseStorageOptions
               {
                   AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                   ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
               })
               .Child("images")
               .Child(fileName)
               .PutAsync(stream, cancellation.Token);

            try
            {
                //error during upload will be thrown when awaiting 
                string link = await task;
                return link;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
                return "";
            }
        }
        // GET: Animals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = new Animal();
            List<string> tmp = new System.Collections.Generic.List<string> { "Dog", "Turtle", "Rabbit", "Parrot", "Cat", "Fish", "Mouse", "Hamster", "Cow", "Duck", "Shrimp", "Pig", "Goat", "Crab", "Deer", "Bee", "Sheep", "Turkey", "Dove", "Chicken", "Horse", "Bird", "Squirrel", "Ox", "Lion", "Panda", "Walrus", "Otter", "Kangaroo", "Monkey", "Koala", "Mole", "Elephant", "Leopard", "Hippopotamus", "Giraffe", "Fox", "Coyote", "Hedgehong", "Camel", "Starfish", "Alligator", "Owl", "Tiger", "Bear", "Whale", "Raccoon", "Crocodile", "Dolphin", "Snake", "Elk", "Bat", "Hare", "Toad", "Frog", "Rat", "Badger", "Lizard", "Reindeer", "Insect" };
            tmp.Sort();
            ViewBag.AnimalsType = tmp;
            ViewBag.AnimalsGender = new System.Collections.Generic.List<string> { "Male", "Female" };

            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Type,Breed,Age,Gender,ImgURL,Email")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        // GET: Animals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Animal animal = db.Animals.Find(id);
            db.Animals.Remove(animal);
            db.Users.Remove(db.Users.Find(id));
            return RedirectToAction("Index");
        }

        // GET: Animals/DeleteLiked/5
        public ActionResult DeleteLiked(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // POST: Animals/DeleteLiked/5
        [HttpPost, ActionName("DeleteLiked")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedLiked(int id)
        {
            //Animal animal = db.Animals.Find(id);
            string userId = User.Identity.GetUserId();
            foreach(AnimalLikedAnimal a in db.Table.ToList())
            {
                if (userId.Equals(a.ProfileAnimalId) && id.ToString().Equals(a.LikedAnimalId))
                    db.Table.Remove(a);
            }
            db.SaveChanges();
           
            return RedirectToAction("MyProfile");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
