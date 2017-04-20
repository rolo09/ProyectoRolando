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
    public class inventariosController : ApiController
    {
        private inventariosEntities db = new inventariosEntities();

        // GET: api/inventarios
        public IQueryable<app_inventario> Getapp_inventario()
        {
            return db.app_inventario;
        }

        // GET: api/inventarios/5
        [ResponseType(typeof(app_inventario))]
        public IHttpActionResult Getapp_inventario(int id)
        {
            app_inventario app_inventario = db.app_inventario.Find(id);
            if (app_inventario == null)
            {
                return NotFound();
            }

            return Ok(app_inventario);
        }

        // PUT: api/inventarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putapp_inventario(int id, app_inventario app_inventario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != app_inventario.id_inventario)
            {
                return BadRequest();
            }

            db.Entry(app_inventario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!app_inventarioExists(id))
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

        // POST: api/inventarios
        [ResponseType(typeof(app_inventario))]
        public IHttpActionResult Postapp_inventario(app_inventario app_inventario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.app_inventario.Add(app_inventario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (app_inventarioExists(app_inventario.id_inventario))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = app_inventario.id_inventario }, app_inventario);
        }

        // DELETE: api/inventarios/5
        [ResponseType(typeof(app_inventario))]
        public IHttpActionResult Deleteapp_inventario(int id)
        {
            app_inventario app_inventario = db.app_inventario.Find(id);
            if (app_inventario == null)
            {
                return NotFound();
            }

            db.app_inventario.Remove(app_inventario);
            db.SaveChanges();

            return Ok(app_inventario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool app_inventarioExists(int id)
        {
            return db.app_inventario.Count(e => e.id_inventario == id) > 0;
        }
    }
}