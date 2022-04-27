using Bibliotec;
using ClassLibrary1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Tests
{
    [TestClass()]
    public class OperacionesDBContextTests
    {


        [TestMethod()]
        public void insertaTrabajadorTest()
        {
            PresenciaContext presenciaContext = new PresenciaContext();
            var grupo = presenciaContext.Grupo_Trabajo.Where(x => x.Turno == "Mañana").FirstOrDefault();
            Trabajador trab = new()
            {
                nombre = "Nimex2",
                grupo = grupo
            };
            presenciaContext.Trabajador.Add(trab);
            var trabajadores = presenciaContext.Trabajador.ToList();
            foreach (Trabajador traba in trabajadores)
            {
                if (trab == traba)
                    Assert.Equals(traba, trab);
            }


        }

        [TestMethod()]
        public void insertarGrupoTrabajoTest()
        {
            Turnos g = new Turnos()
            {
                Turno = "4to turno",
                HoraEntrada = "09:45",
                HoraSalida = "18:55"
            };
            bool inserta = OperacionesDBContext.insertarGrupoTrabajo(g.Turno, g.HoraEntrada, g.HoraSalida);
            Assert.IsTrue(inserta);

        }

        [TestMethod()]
        public void insertaFichajeTest()
        {
            PresenciaContext p = new PresenciaContext();

            Fichajes f = new()
            {
                Trabajador = p.Trabajador.Find(1),
                Grupo_Trabajo = p.Grupo_Trabajo.Find(1),
                FechaFichaje = DateTime.Now,
                Entrada_Salida = "Entrada"
            };

            bool inserta = OperacionesDBContext.insertaFichaje(f.Trabajador.numero_tarjeta, f.Grupo_Trabajo.IdGrupo, "Entrada");
            Assert.IsTrue(inserta);
        }

        [TestMethod()]
        public void insertaUsuarioTest()
        {
            Usuarios us = new Usuarios()
            {
                Username = "Prueba1234",
                Password = "1",
                esAdmin = false
            };
            bool inserta = OperacionesDBContext.insertaUsuario(us.Username, us.Password, us.esAdmin);
            Assert.IsTrue(inserta);
        }

        [TestMethod()]
        public void borraTrabajadorTest()
        {
            PresenciaContext p = new PresenciaContext();
            Trabajador tr = p.Trabajador.Find(2);
            bool borra = OperacionesDBContext.borraTrabajador(tr.numero_tarjeta);
            Assert.IsTrue(borra);
        }

        [TestMethod()]
        public void borraGrupoTrabajoTest()
        {
            PresenciaContext p = new PresenciaContext();
            Turnos g = p.Grupo_Trabajo.Find(1);
            bool borra = OperacionesDBContext.borraGrupoTrabajo(g.IdGrupo);
            Assert.IsTrue(borra);
        }

        [TestMethod()]
        public void borraFichajeTest()
        {
            PresenciaContext p = new PresenciaContext();
            Fichajes f = p.TablaFichajes.Find(150);
            bool borra = OperacionesDBContext.borraFichaje(f.Id);
            Assert.IsTrue(borra);
        }

        [TestMethod()]
        public void borraUsuarioTest()
        {
            PresenciaContext p = new PresenciaContext();
            var us = p.Usuarios.ToList();
            bool borra = OperacionesDBContext.borraUsuario(us[0].IdUser);
            Assert.IsTrue(borra);
        }

        [TestMethod()]
        public void actualizaTrabajadorTest()
        {
            PresenciaContext p = new PresenciaContext();
            var trabs = p.Trabajador.ToList();
            Trabajador t = trabs[0];
            t.nombre = "prueba2";
            t.grupo = p.Grupo_Trabajo.Find(1);
            bool actualiza = OperacionesDBContext.actualizaTrabajador(t.numero_tarjeta, t.nombre, t.grupo.IdGrupo);
            Assert.IsTrue(actualiza);
        }

        [TestMethod()]
        public void actualizarGrupoTrabajoTest()
        {
            PresenciaContext p = new PresenciaContext();
            var grupos = p.Grupo_Trabajo.ToList();
            Turnos g = grupos[0];
            g.Turno = "Mañana";
            g.HoraEntrada = "00:00";
            g.HoraSalida = "00:00";
            bool actualiza = OperacionesDBContext.actualizarGrupoTrabajo(g.Turno, g.HoraSalida, g.HoraSalida);
            Assert.IsTrue(actualiza);
        }

        [TestMethod()]
        public void actualizaUsuarioTest()
        {
            PresenciaContext p = new PresenciaContext();
            var users = p.Usuarios.ToList();
            Usuarios us = users[0];
            string busca = us.Username;
            us.Username = "UserDePrueba";
            us.Password = "2";
            us.esAdmin = false;
            bool actualiza = OperacionesDBContext.actualizaUsuario(busca, us.Username, us.Password, us.esAdmin);
            Assert.IsTrue(actualiza);
        }

        [TestMethod()]
        public void insertaTareasTest()
        {
            PresenciaContext p = new PresenciaContext();
            var tareas = p.Tareas.ToList();
            Tareas t = tareas[0];
            t.NombreTarea = "Cortar plasticos";
            t.Descripcion = "Te cojes unos plastiquetes y los cortas.";
            t.TiempoEstimado = 8;
            bool inserta = OperacionesDBContext.insertaTareas(t.NombreTarea, t.Descripcion, t.TiempoEstimado);
            Assert.IsTrue(inserta);
        }

        [TestMethod()]
        public void insertaTareaEnGrupoTest()
        {
            PresenciaContext p = new PresenciaContext();
            var tareas = p.Tareas.ToList();
            var grupos = p.Grupo_Trabajo.ToList();
            Turnos g = grupos[0];
            Tareas t = tareas[0];
            bool actualiza = OperacionesDBContext.insertaTareaEnGrupo(g.IdGrupo, t.NombreTarea, t.Descripcion, 7);
            Assert.IsTrue(actualiza);
        }

        [TestMethod()]
        public void insertaZonaTest()
        {
            PresenciaContext p = new PresenciaContext();
            Zonas z = new Zonas()
            {
                Nombre = "Zona 3"
            };
            bool inserta = OperacionesDBContext.insertaZona(z.Nombre);
            Assert.IsTrue(inserta);
        }

        [TestMethod()]
        public void ActualizaZonaTest()
        {
            PresenciaContext p = new PresenciaContext();
            Zonas z = p.Zonas.Find(1);
            string oldName = z.Nombre;
            z.Nombre = "Zona 5";
            bool actualiza = OperacionesDBContext.ActualizaZona(oldName, z.Nombre);
            Assert.IsTrue(actualiza);
        }

        [TestMethod()]
        public void BorraZonaTest()
        {
            PresenciaContext p = new PresenciaContext();
            Zonas z = p.Zonas.Find(2);
            bool actualiza = OperacionesDBContext.BorraZona(z.IdZona);
            Assert.IsTrue(actualiza);
        }
    }
}