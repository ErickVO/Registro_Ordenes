using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RegistroOrden.DAL;
using RegistroOrden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RegistroOrden.BLL
{
    public class OrdenesBLL
    {
        public static bool Guardar(Ordenes ordenes)
        {
            if (!Existe(ordenes.OrdenId))
            {
                return Guardar(ordenes);
            }
            else
            {
                return Modificar(ordenes);
            }
        }


        private static bool Insertar(Ordenes ordenes)
        {
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                db.Ordenes.Add(ordenes);
                paso = (db.SaveChanges() > 0);
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        private static bool Modificar(Ordenes ordenes)
        {
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                db.Database.ExecuteSqlRaw($"Delete FROM OrdenesDetalle Where OrdenId = {ordenes.OrdenId}");

                foreach (var item in ordenes.OrdenesDetalle)
                {
                    db.Entry(item).State = EntityState.Added;
                }

                db.Entry(ordenes).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static bool Eliminar(int id)
        {
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                var eliminado = db.Ordenes.Find(id);

                if (eliminado != null)
                {
                    db.Ordenes.Remove(eliminado);
                    paso = (db.SaveChanges() > 0);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static Ordenes Buscar(int id)
        {
            Contexto db = new Contexto();
            Ordenes ordenes;

            try
            {
                ordenes = db.Ordenes.
                    Where(o => o.OrdenId == id).
                    Include(o => o.OrdenesDetalle).
                    FirstOrDefault();
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return ordenes; 
        }

        private static bool Existe(int id)
        {
            Contexto db = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = db.Ordenes.Any(o => o.OrdenId == id);
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return encontrado;
        }

        public static List<Ordenes> GetOrdenes()
        {
            List<Ordenes> Lista = new List<Ordenes>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Ordenes.ToList();
            }
            catch 
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return Lista;
        }

        public static List<Ordenes> GetList(Expression<Func<Ordenes, bool>> ordenes)
        {
            Contexto db = new Contexto();
            List<Ordenes> Lista = new List<Ordenes>();

            try
            {
                Lista = db.Ordenes.Where(ordenes).ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return Lista;
        }
    }
}
