using Microsoft.VisualStudio.TestTools.UnitTesting;
using HolaMundoMAUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bibliotec;
using Persistencia;

namespace HolaMundoMAUI.Tests
{
    [TestClass()]
    public class PaginaAdminTests
    {

        [TestMethod()]
        public void NuevoUsuarioTest()
        {
            string nombreUsuario = "Nimex";
            DateTime dt = DateTime.Now;
            PresenciaContext presenciaContext = new PresenciaContext();
            presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Registrar usuario' - " + dt));
            presenciaContext.SaveChanges();
        }
    }
}