using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ExaP9.Models;

namespace ExaP9.Controllers
{
    public class TelefonoMVCController : Controller
    {
        private ExaP9DBEntities db = new ExaP9DBEntities();

        // GET: TelefonoMVC
        public ActionResult Index()
        {
            //var telefono = db.Telefono.Include(t => t.Color1).Include(t => t.Gama1).Include(t => t.Ubicacion1);
            //return View(telefono.ToList());

            IEnumerable<Telefono> telefonos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60497/api/");
                //GET GETAlumnos
                //el siguente codigo obtiene la informacion de manera asincrona y espera hata obtener la data
                var reponseTask = client.GetAsync("telefonoapi");
                reponseTask.Wait();
                var result = reponseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // leer todo el cotenido y lo parseamos a una lista de alumno
                    var leer = result.Content.ReadAsAsync<IList<Telefono>>();
                    leer.Wait();
                    telefonos = leer.Result;
                }
                else
                {
                    telefonos = Enumerable.Empty<Telefono>();
                    ModelState.AddModelError(string.Empty, "Error...");
                }

            }
            return View(telefonos.ToList());
        }

        // GET: TelefonoMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefono telefono = db.Telefono.Find(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            return View(telefono);
        }

        // GET: TelefonoMVC/Create
        public ActionResult Create()
        {
            ViewBag.Color = new SelectList(db.Color, "Idcolor", "Descripcion");
            ViewBag.Gama = new SelectList(db.Gama, "Idgama", "Descripcion");
            ViewBag.Ubicacion = new SelectList(db.Ubicacion, "Idubicacion", "Pais");
            return View();
        }

        // POST: TelefonoMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Idtelefono,Marca,Gama,Ubicacion,Color,Precio,Fecha")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                telefono.Fecha = DateTime.Now;
                db.Telefono.Add(telefono);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Color = new SelectList(db.Color, "Idcolor", "Descripcion", telefono.Color);
            ViewBag.Gama = new SelectList(db.Gama, "Idgama", "Descripcion", telefono.Gama);
            ViewBag.Ubicacion = new SelectList(db.Ubicacion, "Idubicacion", "Pais", telefono.Ubicacion);
            return View(telefono);
        }

        // GET: TelefonoMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefono telefono = db.Telefono.Find(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            ViewBag.Color = new SelectList(db.Color, "Idcolor", "Descripcion", telefono.Color);
            ViewBag.Gama = new SelectList(db.Gama, "Idgama", "Descripcion", telefono.Gama);
            ViewBag.Ubicacion = new SelectList(db.Ubicacion, "Idubicacion", "Pais", telefono.Ubicacion);
            return View(telefono);
        }

        // POST: TelefonoMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Idtelefono,Marca,Gama,Ubicacion,Color,Precio,Fecha")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telefono).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Color = new SelectList(db.Color, "Idcolor", "Descripcion", telefono.Color);
            ViewBag.Gama = new SelectList(db.Gama, "Idgama", "Descripcion", telefono.Gama);
            ViewBag.Ubicacion = new SelectList(db.Ubicacion, "Idubicacion", "Pais", telefono.Ubicacion);
            return View(telefono);
        }

        // GET: TelefonoMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefono telefono = db.Telefono.Find(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            return View(telefono);
        }

        // POST: TelefonoMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Telefono telefono = db.Telefono.Find(id);
            db.Telefono.Remove(telefono);
            db.SaveChanges();
            return RedirectToAction("Index");
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
