using Bibliotec;
using ClassLibrary1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static bool insertaTrabajador(string Name,int Grup,string user)
        {
            PresenciaContext presenciaContext = new PresenciaContext();

            var Grupo = presenciaContext.Grupo_Trabajo.Where(x => x.IdGrupo == Grup).FirstOrDefault();
            var Us = presenciaContext.Usuarios.Where(x => x.Username == user).FirstOrDefault();

            if (Us is not null)
            {
                Trabajador trab = new Trabajador(Name, Grupo, Us);
                presenciaContext.Add(trab);
                presenciaContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
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

                //Grupo_Trabajo grupo = new Grupo_Trabajo(2, "Tarde", "14:00", "22:00");
                DateTime fechaFichaje = DateTime.Now;
                Fichajes fich = new Fichajes(trab, grupo, fechaFichaje, Entrada_Salida);

                presenciaContext.TablaFichajes.Add(fich);
                presenciaContext.SaveChanges();
        }
        public static bool insertaUsuario(string username, string password,bool esAdmin)
        {
            if (username is not null)
            {
                if(password is not null) { 
                using var presenciaContext = new PresenciaContext();
                Usuarios us = new Usuarios(username, password, esAdmin);
                presenciaContext.Usuarios.Add(us);
                presenciaContext.SaveChanges();
                return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool borraTrabajador(int NumeroTarjeta)
        {
            using var presenciaContext = new PresenciaContext();
            Trabajador trabajador = presenciaContext.Trabajador.Find(NumeroTarjeta);
            if (trabajador is not null)
            {
                presenciaContext.Remove(trabajador);
                presenciaContext.SaveChanges();
                return true;
            }
            else 
            {
                return false;
            }
        }
        public static bool borraGrupoTrabajo(int IdGrupo)
        {
            using var presenciaContext = new PresenciaContext();
            Grupo_Trabajo grupo = presenciaContext.Grupo_Trabajo.Find(IdGrupo);
            if(grupo is not null) { 
                presenciaContext.Remove(grupo);
                presenciaContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool borraFichaje(int Id)
        {
            using var presenciaContext = new PresenciaContext();
            Fichajes fich = presenciaContext.TablaFichajes.Find(Id);
            if(fich is not null) { 
                presenciaContext.Remove(fich);
                presenciaContext.SaveChanges();
                return true;
            }else
            {
                return false;
            }
        }
        public static bool borraUsuario(int Id)
        {
            using var presenciaContext = new PresenciaContext();
            Usuarios us = presenciaContext.Usuarios.Find(Id);
            if (us != null)
            {
                presenciaContext.Remove(us);
                presenciaContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool actualizaTrabajador(int Id, string Nombre, int GrupoTrabajo)
        {
            using var presenciaContext = new PresenciaContext();
            Trabajador trabajador = new Trabajador(Id, Nombre, presenciaContext.Grupo_Trabajo.Find(GrupoTrabajo));
            presenciaContext.Update(trabajador);
            presenciaContext.SaveChanges();
            return true;
        }

        public static bool actualizarGrupoTrabajo(string Nombre, string HoraEntrada, string HoraSalida)
        {
            using var presenciaContext = new PresenciaContext();
            var GrupoTrabajo = presenciaContext.Grupo_Trabajo.Where(x => x.Turno == Nombre).FirstOrDefault();
            GrupoTrabajo.Turno = Nombre;
            GrupoTrabajo.HoraEntrada=HoraEntrada;
            GrupoTrabajo.HoraSalida = HoraSalida;
            presenciaContext.Update(GrupoTrabajo);
            presenciaContext.SaveChanges();
            return true;
        }
        public static void actualizaUsuario(string usernamebusca,string username, string password, bool esAdmin)
        {
            using var presenciaContext = new PresenciaContext();
            var us = presenciaContext.Usuarios.Where(x=>x.Username== usernamebusca).FirstOrDefault();
            us.Username = usernamebusca;
            us.Password = password;
            us.esAdmin = esAdmin;

            presenciaContext.Update(us);
            presenciaContext.SaveChanges();
        }
    }
}
