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
using ExaP9.Models;

namespace ExaP9.Controllers
{
    public class GamaApiController : ApiController
    {
        private ExaP9DBEntities db = new ExaP9DBEntities();

        // GET: api/GamaApi
        public IQueryable<Gama> GetGama()
        {
            return db.Gama;
        }

        // GET: api/GamaApi/5
        [ResponseType(typeof(Gama))]
        public IHttpActionResult GetGama(int id)
        {
            Gama gama = db.Gama.Find(id);
            if (gama == null)
            {
                return NotFound();
            }

            return Ok(gama);
        }

        // PUT: api/GamaApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGama(int id, Gama gama)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gama.Idgama)
            {
                return BadRequest();
            }

            db.Entry(gama).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/GamaApi
        [ResponseType(typeof(Gama))]
        public IHttpActionResult PostGama(Gama gama)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gama.Add(gama);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gama.Idgama }, gama);
        }

        // DELETE: api/GamaApi/5
        [ResponseType(typeof(Gama))]
        public IHttpActionResult DeleteGama(int id)
        {
            Gama gama = db.Gama.Find(id);
            if (gama == null)
            {
                return NotFound();
            }

            db.Gama.Remove(gama);
            db.SaveChanges();

            return Ok(gama);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GamaExists(int id)
        {
            return db.Gama.Count(e => e.Idgama == id) > 0;
        }
    }
}