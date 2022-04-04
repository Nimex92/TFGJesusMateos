using Bibliotec;
using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class OperacionesDBContext
    {
        public OperacionesDBContext()
        {

        }

        public static void insertaTrabajador(string Name, int Grup)
        {
            using var presenciaContext = new PresenciaContext();
            var Grupo = presenciaContext.Grupo_Trabajo.Find(Grup);
            
            Trabajador trabajador = new Trabajador()
            {
                nombre = Name,
                grupo = Grupo
            };
            presenciaContext.Trabajador.Add(trabajador);
            presenciaContext.SaveChanges();
        }
        public static void insertarGrupoTrabajo(string nombre, string horaEntrada, string horaSalida)
        {
            using var presenciaContext = new PresenciaContext();
            Grupo_Trabajo grupo = new Grupo_Trabajo(nombre, horaEntrada, horaSalida);
            presenciaContext.Grupo_Trabajo.Add(grupo);
            presenciaContext.SaveChanges();
        }
        public static void insertaFichaje(int Trabajador, int GrupoTrabajo, string Entrada_Salida)
        {
            using var presenciaContext = new PresenciaContext();
            Trabajador trab = presenciaContext.Trabajador.Find(Trabajador);
            Grupo_Trabajo grupo = presenciaContext.Grupo_Trabajo.Find(GrupoTrabajo);
            if (grupo == null)
            {
                //Grupo_Trabajo grupo = new Grupo_Trabajo(2, "Tarde", "14:00", "22:00");
                DateTime fechaFichaje = DateTime.Now;
                Fichajes fich = new Fichajes(trab, grupo, fechaFichaje, Entrada_Salida);

                presenciaContext.TablaFichajes.Add(fich);
                presenciaContext.SaveChanges();
            }
            else
            {
                //El trabajador ya existe
            }
        }
        public static void insertaUsuario(string username, string password)
        {
            using var presenciaContext = new PresenciaContext();
            Usuarios us = new Usuarios(username, password);
            presenciaContext.Usuarios.Add(us);
            presenciaContext.SaveChanges();
        }

        public static void borraTrabajador(int NumeroTarjeta)
        {
            using var presenciaContext = new PresenciaContext();
            Trabajador trabajador = presenciaContext.Trabajador.Find(NumeroTarjeta);
            presenciaContext.Remove(trabajador);
            presenciaContext.SaveChanges();
        }
        public static void borraGrupoTrabajo(int IdGrupo)
        {
            using var presenciaContext = new PresenciaContext();
            Grupo_Trabajo grupo = presenciaContext.Grupo_Trabajo.Find(IdGrupo);
            presenciaContext.Remove(grupo);
            presenciaContext.SaveChanges();
        }
        public static void borraFichaje(int Id)
        {
            using var presenciaContext = new PresenciaContext();
            Fichajes fich = presenciaContext.TablaFichajes.Find(Id);
            presenciaContext.Remove(fich);
            presenciaContext.SaveChanges();
        }
        public static void borraUsuario(int Id)
        {
            using var presenciaContext = new PresenciaContext();
            Usuarios us = presenciaContext.Usuarios.Find(Id);
            presenciaContext.Remove(us);
            presenciaContext.SaveChanges();
        }

        public static void actualizaTrabajador(int Id, string Nombre, int GrupoTrabajo)
        {
            using var presenciaContext = new PresenciaContext();
            Trabajador trabajador = new Trabajador(Id, Nombre, presenciaContext.Grupo_Trabajo.Find(GrupoTrabajo));
            presenciaContext.Update(trabajador);
            presenciaContext.SaveChanges();
        }
        public static void actualizarGrupoTrabajo(int idGrupo, string Nombre, string HoraEntrada, string HoraSalida)
        {
            using var presenciaContext = new PresenciaContext();
            Grupo_Trabajo GrupoTrabajo = new Grupo_Trabajo(idGrupo, Nombre, HoraEntrada, HoraSalida);
            presenciaContext.Update(GrupoTrabajo);
            presenciaContext.SaveChanges();
        }
        public static void actualizaUsuario(int id,string username, string password)
        {
            using var presenciaContext = new PresenciaContext();
            Usuarios us = new Usuarios(id,username, password);
            presenciaContext.Update(us);
            presenciaContext.SaveChanges();
        }
    }
}
