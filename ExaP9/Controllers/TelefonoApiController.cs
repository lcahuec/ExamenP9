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
    public class TelefonoApiController : ApiController
    {
        private ExaP9DBEntities db = new ExaP9DBEntities();

        // GET: api/TelefonoApi
        public IQueryable<Telefono> GetTelefono()
        {
            return db.Telefono;
        }

        // GET: api/TelefonoApi/5
        [ResponseType(typeof(Telefono))]
        public IHttpActionResult GetTelefono(int id)
        {
            Telefono telefono = db.Telefono.Find(id);
            if (telefono == null)
            {
                return NotFound();
            }

            return Ok(telefono);
        }

        // PUT: api/TelefonoApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTelefono(int id, Telefono telefono)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != telefono.Idtelefono)
            {
                return BadRequest();
            }

            db.Entry(telefono).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonoExists(id))
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

        // POST: api/TelefonoApi
        [ResponseType(typeof(Telefono))]
        public IHttpActionResult PostTelefono(Telefono telefono)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Telefono.Add(telefono);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = telefono.Idtelefono }, telefono);
        }

        // DELETE: api/TelefonoApi/5
        [ResponseType(typeof(Telefono))]
        public IHttpActionResult DeleteTelefono(int id)
        {
            Telefono telefono = db.Telefono.Find(id);
            if (telefono == null)
            {
                return NotFound();
            }

            db.Telefono.Remove(telefono);
            db.SaveChanges();

            return Ok(telefono);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TelefonoExists(int id)
        {
            return db.Telefono.Count(e => e.Idtelefono == id) > 0;
        }
    }
}