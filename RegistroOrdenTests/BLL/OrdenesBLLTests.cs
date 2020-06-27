using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistroOrden.BLL;
using RegistroOrden.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroOrden.BLL.Tests
{
    [TestClass()]
    public class OrdenesBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
        
            Ordenes ordenes = new Ordenes();
          
            ordenes.OrdenId = 0;
            ordenes.Fecha = DateTime.Now;
            ordenes.SuplidorId = 1;
            ordenes.OrdenesDetalle.Add(new OrdenesDetalle
            {
                Id = 0,
                OrdenId = ordenes.OrdenId,
                ProductoId = 1,
                Cantidad = 4,
                Costo = 120
            });

            Assert.IsTrue(OrdenesBLL.Guardar(ordenes));
        }

        [TestMethod()]
        public void EliminarTest()
        {
            bool paso = OrdenesBLL.Eliminar(1);

            Assert.AreEqual(paso, true);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Ordenes ordenes = new Ordenes();

            ordenes = OrdenesBLL.Buscar(1);
            
            Assert.IsNotNull(ordenes);
        }


        [TestMethod()]
        public void GetListTest()
        {
            var lista = new List<Ordenes>();
            lista = OrdenesBLL.GetList(p => true);
            Assert.IsNotNull(lista);
        }


    }
}