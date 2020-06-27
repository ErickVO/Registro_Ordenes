using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistroOrden.BLL;
using RegistroOrden.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroOrden.BLL.Tests
{
    [TestClass()]
    public class ProductosBLLTests
    {
        [TestMethod()]
        public void BuscarTest()
        {
            Productos productos;
            productos = ProductosBLL.Buscar(1);
            Assert.IsNotNull(productos);
        }

        [TestMethod()]
        public void GetListTest()
        {
            var lista = new List<Productos>();
            lista = ProductosBLL.GetList(p => true);
            Assert.IsNotNull(lista);
        }
    }
}