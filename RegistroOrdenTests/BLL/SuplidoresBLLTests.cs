using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistroOrden.BLL;
using RegistroOrden.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroOrden.BLL.Tests
{
    [TestClass()]
    public class SuplidoresBLLTests
    {
        [TestMethod()]
        public void GetListTest()
        {
            var lista = new List<Suplidores>();
            lista = SuplidoresBLL.GetList(p => true);
            Assert.IsNotNull(lista);
        }
    }
}