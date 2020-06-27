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
                return Insertar(ordenes);
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

                foreach (var item in ordenes.OrdenesDetalle)
                {
                    var auxOrden = db.Productos.Find(item.ProductoId);
                    if (auxOrden != null)
                    {
                        auxOrden.Inventario += item.Cantidad;
                    }
                }


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
            var AuxOrden = Buscar(ordenes.OrdenId);
            bool paso = false;

            try
            {
                foreach (var item in AuxOrden.OrdenesDetalle)
                {
                    var auxProducto = db.Productos.Find(item.ProductoId);
                    if (!ordenes.OrdenesDetalle.Exists(d => d.OrdenId == item.OrdenId))
                    {
                        if (auxProducto != null)
                        {
                            auxProducto.Inventario -= item.Cantidad;
                        }

                        db.Entry(item).State = EntityState.Deleted;
                    }

                }

                foreach (var item in ordenes.OrdenesDetalle)
                {
                    var auxProducto = db.Productos.Find(item.ProductoId);
                    if (item.Id == 0)
                    {
                        db.Entry(item).State = EntityState.Added;
                        if (auxProducto != null)
                        {
                            auxProducto.Inventario += item.Cantidad;
                        }
                    }
                    else
                        db.Entry(item).State = EntityState.Modified;
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

        public static bool Eliminar(int id)
        {
            Contexto db = new Contexto();
            bool paso = false;
            var AuxOrden = Buscar(id);

            try
            {
               

                if (Existe(id))
                {
                    //resta las cantidades correspondientes a los producto
                    foreach (var item in AuxOrden.OrdenesDetalle)
                    {
                        var auxProducto = db.Productos.Find(item.ProductoId);
                        if (auxProducto != null)
                        {
                            auxProducto.Inventario -= item.Cantidad;
                        }
                    }

                    //remueve la entidad
                    var eliminado = db.Ordenes.Find(id);
                    if (eliminado != null)
                    {
                        db.Ordenes.Remove(eliminado);
                        paso = db.SaveChanges() > 0;
                    }
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
