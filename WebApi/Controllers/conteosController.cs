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
using WebApi;

namespace WebApi.Controllers
{
    public class conteosController : ApiController
    {
        private inventariosEntities db = new inventariosEntities();

        // GET: api/conteos
        public IQueryable<app_conteo> Getapp_conteo()
        {
            return db.app_conteo;
        }

        // GET: api/conteos/5
        [ResponseType(typeof(app_conteo))]
        public IHttpActionResult Getapp_conteo(int id)
        {
            app_conteo app_conteo = db.app_conteo.Find(id);
            if (app_conteo == null)
            {
                return NotFound();
            }

            return Ok(app_conteo);
        }

        // PUT: api/conteos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putapp_conteo(int id, app_conteo app_conteo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != app_conteo.id_conteo)
            {
                return BadRequest();
            }

            db.Entry(app_conteo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!app_conteoExists(id))
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

        // POST: api/conteos
        [ResponseType(typeof(app_conteo))]
        public IHttpActionResult Postapp_conteo(app_conteo app_conteo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.app_conteo.Add(app_conteo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = app_conteo.id_conteo }, app_conteo);
        }

        // DELETE: api/conteos/5
        [ResponseType(typeof(app_conteo))]
        public IHttpActionResult Deleteapp_conteo(int id)
        {
            app_conteo app_conteo = db.app_conteo.Find(id);
            if (app_conteo == null)
            {
                return NotFound();
            }

            db.app_conteo.Remove(app_conteo);
            db.SaveChanges();

            return Ok(app_conteo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool app_conteoExists(int id)
        {
            return db.app_conteo.Count(e => e.id_conteo == id) > 0;
        }
    }
}