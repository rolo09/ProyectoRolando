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
    public class usuariosController : ApiController
    {
        private inventariosEntities db = new inventariosEntities();

        // GET: api/usuarios
        public IQueryable<app_usuario> Getapp_usuario()
        {
            return db.app_usuario;
        }

        // GET: api/usuarios/5
        [ResponseType(typeof(app_usuario))]
        public IHttpActionResult Getapp_usuario(int id)
        {
            app_usuario app_usuario = db.app_usuario.Find(id);
            if (app_usuario == null)
            {
                return NotFound();
            }

            return Ok(app_usuario);
        }

        // PUT: api/usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putapp_usuario(int id, app_usuario app_usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != app_usuario.id_usuario)
            {
                return BadRequest();
            }

            db.Entry(app_usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!app_usuarioExists(id))
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

        // POST: api/usuarios
        [ResponseType(typeof(app_usuario))]
        public IHttpActionResult Postapp_usuario(app_usuario app_usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.app_usuario.Add(app_usuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = app_usuario.id_usuario }, app_usuario);
        }

        // DELETE: api/usuarios/5
        [ResponseType(typeof(app_usuario))]
        public IHttpActionResult Deleteapp_usuario(int id)
        {
            app_usuario app_usuario = db.app_usuario.Find(id);
            if (app_usuario == null)
            {
                return NotFound();
            }

            db.app_usuario.Remove(app_usuario);
            db.SaveChanges();

            return Ok(app_usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool app_usuarioExists(int id)
        {
            return db.app_usuario.Count(e => e.id_usuario == id) > 0;
        }
    }
}