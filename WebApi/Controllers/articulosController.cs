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
    public class articulosController : ApiController
    {
        private inventariosEntities db = new inventariosEntities();

        // GET: api/articulos
        public IQueryable<app_articulo> Getapp_articulo()
        {
            return db.app_articulo;
        }

        // GET: api/articulos/5
        [ResponseType(typeof(app_articulo))]
        public IHttpActionResult Getapp_articulo(string id)
        {
            app_articulo app_articulo = db.app_articulo.Find(id);
            if (app_articulo == null)
            {
                return NotFound();
            }

            return Ok(app_articulo);
        }

        // PUT: api/articulos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putapp_articulo(string id, app_articulo app_articulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != app_articulo.id_articulo)
            {
                return BadRequest();
            }

            db.Entry(app_articulo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!app_articuloExists(id))
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

        // POST: api/articulos
        [ResponseType(typeof(app_articulo))]
        public IHttpActionResult Postapp_articulo(app_articulo app_articulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.app_articulo.Add(app_articulo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (app_articuloExists(app_articulo.id_articulo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = app_articulo.id_articulo }, app_articulo);
        }

        // DELETE: api/articulos/5
        [ResponseType(typeof(app_articulo))]
        public IHttpActionResult Deleteapp_articulo(string id)
        {
            app_articulo app_articulo = db.app_articulo.Find(id);
            if (app_articulo == null)
            {
                return NotFound();
            }

            db.app_articulo.Remove(app_articulo);
            db.SaveChanges();

            return Ok(app_articulo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool app_articuloExists(string id)
        {
            return db.app_articulo.Count(e => e.id_articulo == id) > 0;
        }
    }
}