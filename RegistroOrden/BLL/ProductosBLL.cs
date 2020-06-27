﻿using RegistroOrden.DAL;
using RegistroOrden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RegistroOrden.BLL
{
    public class ProductosBLL
    {
        public static List<Productos> GetList(Expression<Func<Productos, bool> > productos)
        {
            Contexto db = new Contexto();
            List<Productos> Lista = new List<Productos>();

            try
            {
                Lista = db.Productos.Where(productos).ToList();
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