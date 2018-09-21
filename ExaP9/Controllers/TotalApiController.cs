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
    public class TotalApiController : ApiController
    {
        private ExaP9DBEntities db = new ExaP9DBEntities();

        // GET: api/TotalApi
        public IQueryable<Total> GetTotal()
        {
            return db.Total;
        }

        // GET: api/TotalApi/5
        [ResponseType(typeof(Total))]
        public IHttpActionResult GetTotal(int id)
        {
            Total total = db.Total.Find(id);
            if (total == null)
            {
                return NotFound();
            }

            return Ok(total);
        }

        // PUT: api/TotalApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTotal(int id, Total total)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != total.Idtotal)
            {
                return BadRequest();
            }

            db.Entry(total).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TotalExists(id))
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

        // POST: api/TotalApi
        [ResponseType(typeof(Total))]
        public IHttpActionResult PostTotal(Total total)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Total.Add(total);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = total.Idtotal }, total);
        }

        // DELETE: api/TotalApi/5
        [ResponseType(typeof(Total))]
        public IHttpActionResult DeleteTotal(int id)
        {
            Total total = db.Total.Find(id);
            if (total == null)
            {
                return NotFound();
            }

            db.Total.Remove(total);
            db.SaveChanges();

            return Ok(total);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TotalExists(int id)
        {
            return db.Total.Count(e => e.Idtotal == id) > 0;
        }
    }
}