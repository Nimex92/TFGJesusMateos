using ClassLibray;
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
            /*
            PresenciaContext presenciaContext = new PresenciaContext();
            var grupo = presenciaContext.Grupo_Trabajo.Where(x => x.WorkShifts == "Mañana").FirstOrDefault();
            Worker trab = new()
            {
                Nombre = "Nimex2",
                Equipo = grupo
            };
            presenciaContext.Worker.Add(trab);
            var trabajadores = presenciaContext.Worker.ToList();
            foreach (Worker traba in trabajadores)
            {
                if (trab == traba)
                    Assert.Equals(traba, trab);
            }

            */
        }
        
        /*
        [TestMethod()]
        public void insertaFichajeTest()
        {
            PresenciaContext p = new PresenciaContext();

            Signings f = new()
            {
                Worker = p.Worker.Find(1),
                WorkGroups = p.WorkGroups.Find(1),
                FechaFichaje = DateTime.Now,
                Entrada_Salida = "Entrada"
            };

<<<<<<< HEAD
            bool inserta = Db.insertaFichaje(f.Worker.numero_tarjeta, f.WorkGroups.IdGrupo, "Entrada");
=======
            bool inserta = OperacionesDBContext.insertaFichaje(f.Trabajador.NumeroTarjeta, f.EquipoTrabajo.IdGrupo, "Entrada");
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
            Assert.IsTrue(inserta);
        }

        [TestMethod()]
        public void insertaUsuarioTest()
        {
            User us = new User()
            {
                Username = "Prueba1234",
                Password = "1",
                esAdmin = false
            };
            bool inserta = Db.insertaUsuario(us.Username, us.Password, us.esAdmin);
            Assert.IsTrue(inserta);
        }

        [TestMethod()]
        public void borraTrabajadorTest()
        {
            PresenciaContext p = new PresenciaContext();
<<<<<<< HEAD
            Worker tr = p.Worker.Find(2);
            bool borra = Db.borraTrabajador(tr.numero_tarjeta);
=======
            Trabajador tr = p.Trabajador.Find(2);
            bool borra = OperacionesDBContext.borraTrabajador(tr.NumeroTarjeta);
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
            Assert.IsTrue(borra);
        }
        /*
        [TestMethod()]
        public void borraGrupoTrabajoTest()
        {
            PresenciaContext p = new PresenciaContext();
            Grupo_Trabajo g = p.Grupo_Trabajo.Find(1);
            bool borra = Db.borraGrupoTrabajo(g.IdGrupo);
            Assert.IsTrue(borra);
        }

        [TestMethod()]
        public void borraFichajeTest()
        {
            PresenciaContext p = new PresenciaContext();
            Signings f = p.TablaFichajes.Find(150);
            bool borra = Db.borraFichaje(f.Id);
            Assert.IsTrue(borra);
        }
        */
        [TestMethod()]
        public void borraUsuarioTest()
        {
            Db db = new Db();
            PresenciaContext p = new PresenciaContext();
            var us = p.Users.ToList();
            bool borra = db.DeleteUser(us[0],p);
            Assert.IsTrue(borra);
        }
        /*
        [TestMethod()]
        public void actualizaTrabajadorTest()
        {
            PresenciaContext p = new PresenciaContext();
<<<<<<< HEAD
            var trabs = p.Worker.ToList();
            Worker t = trabs[0];
            t.nombre = "prueba2";
            //t.equipo = p.WorkGroups.Find(1);
            bool actualiza = Db.actualizaTrabajador(t.numero_tarjeta, t.nombre, t.equipo.IdGrupo);
=======
            var trabs = p.Trabajador.ToList();
            Trabajador t = trabs[0];
            t.Nombre = "prueba2";
            //t.Equipo = p.EquipoTrabajo.Find(1);
            bool actualiza = OperacionesDBContext.actualizaTrabajador(t.NumeroTarjeta, t.Nombre, t.Equipo.IdGrupo);
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
            Assert.IsTrue(actualiza);
        }
        
        [TestMethod()]
        public void actualizarGrupoTrabajoTest()
        {
            PresenciaContext p = new PresenciaContext();
            var grupos = p.Grupo_Trabajo.ToList();
            Grupo_Trabajo g = grupos[0];
            g.WorkShifts = "Mañana";
            g.HoraEntrada = "00:00";
            g.HoraSalida = "00:00";
            bool actualiza = Db.actualizarGrupoTrabajo(g.WorkShifts, g.HoraSalida, g.HoraSalida);
            Assert.IsTrue(actualiza);
        }

        [TestMethod()]
        public void actualizaUsuarioTest()
        {
            PresenciaContext p = new PresenciaContext();
            var users = p.User.ToList();
            User us = users[0];
            string busca = us.Username;
            us.Username = "UserDePrueba";
            us.Password = "2";
            us.esAdmin = false;
            bool actualiza = Db.actualizaUsuario(busca, us.Username, us.Password, us.esAdmin);
            Assert.IsTrue(actualiza);
        }

        [TestMethod()]
        public void insertaTareasTest()
        {
            PresenciaContext p = new PresenciaContext();
            var tareas = p.WorkTasks.ToList();
            WorkTasks t = tareas[0];
            t.NombreTarea = "Cortar plasticos";
            t.Descripcion = "Te cojes unos plastiquetes y los cortas.";
            t.TiempoEstimado = 8;
            bool inserta = Db.InsertWorkTask(t.NombreTarea, t.Descripcion, t.TiempoEstimado);
            Assert.IsTrue(inserta);
        }

        [TestMethod()]
        public void insertaTareaEnGrupoTest()
        {
            PresenciaContext p = new PresenciaContext();
            var tareas = p.WorkTasks.ToList();
            var grupos = p.Grupo_Trabajo.ToList();
            Grupo_Trabajo g = grupos[0];
            WorkTasks t = tareas[0];
            bool actualiza = Db.insertaTareaEnGrupo(g.IdGrupo, t.NombreTarea, t.Descripcion, 7);
            Assert.IsTrue(actualiza);
        }

        [TestMethod()]
        public void insertaZonaTest()
        {
            PresenciaContext p = new PresenciaContext();
            Places z = new Places()
            {
                Nombre = "Zona 3"
            };
            bool inserta = Db.InsertPlace(z.Nombre);
            Assert.IsTrue(inserta);
        }

        [TestMethod()]
        public void ActualizaZonaTest()
        {
            PresenciaContext p = new PresenciaContext();
            Places z = p.Places.Find(1);
            string oldName = z.Nombre;
            z.Nombre = "Zona 5";
            bool actualiza = Db.UpdatePlace(oldName, z.Nombre);
            Assert.IsTrue(actualiza);
        }

        [TestMethod()]
        public void BorraZonaTest()
        {
            PresenciaContext p = new PresenciaContext();
            Places z = p.Places.Find(2);
            bool actualiza = Db.DeletePlace(z.IdZona);
            Assert.IsTrue(actualiza);
        }*/
    }
}