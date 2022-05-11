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
        //Metodo para insertar un trabajador a la base de datos
        public static bool InsertaTrabajador(Trabajador t)
        {
            PresenciaContext p = new PresenciaContext();
            if(t is not null)
            {
                p.Trabajador.Add(t);
                return true;
            }
            else
            {
                return false;
            }
        }
            
        public static bool insertaTrabajador(string Name, string Equipo, string user)
        {
            PresenciaContext presenciaContext = new PresenciaContext();
            var trab = presenciaContext.Trabajador.Where(x => x.nombre == Name).FirstOrDefault();
            var equipo = presenciaContext.EquipoTrabajo.Where(x => x.Nombre == Equipo).FirstOrDefault();
            var us = presenciaContext.Usuarios.Where(x => x.Username == user).FirstOrDefault();
            if (trab == null)
            {
                if (us is null)
                {
                    Trabajador t1 = new Trabajador(Name, equipo, new Usuarios(user, "1"));
                    presenciaContext.Add(t1);
                    presenciaContext.SaveChanges();
                    t1.equipo.Add(equipo);
                    presenciaContext.SaveChanges();
                    return true;
                }
                else
                {
                    user = "0" + user;
                    Trabajador t1 = new Trabajador(Name, equipo, new Usuarios(user, "1"));
                    presenciaContext.Add(t1);
                    presenciaContext.SaveChanges();
                    return true;

                }
            }
            else
            {
                return false;
            }

        }
    
        //Metodo para insertar un fichaje en la base de datos
        public static bool insertaFichaje(int Trabajador, string Entrada_Salida)
        {
            using var presenciaContext = new PresenciaContext();
            Trabajador trab = presenciaContext.Trabajador.Find(Trabajador);

     
            DateTime fechaFichaje = DateTime.Now;
            Fichajes fich = new Fichajes(trab, fechaFichaje, Entrada_Salida);
            if (fich is not null)
            {
                presenciaContext.Fichajes.Add(fich);
                presenciaContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        //Metodo para insertar un usuario en la base de datos
        public static bool insertaUsuario(string username, string password, bool esAdmin)
        {
            if (username is not null)
            {
                if (password is not null) {
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
        //Metodo para insertar una tarea en la base de datos
        public static bool insertaTareas(string NombreTarea, string Descripcion, double TiempoEstimado)
        {
            PresenciaContext presenciaContext = new PresenciaContext();
            if (NombreTarea is not null && Descripcion is not null)
            {
                presenciaContext.Add(new Tareas(NombreTarea, Descripcion, TiempoEstimado));
                presenciaContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        //Metodo para insertar una zona en la base de datos
        public static bool insertaZona(string NombreZona)
        {
            if (NombreZona is null || NombreZona.Equals(""))
            {
                return false;
            }
            else
            {
                using var presenciaContext = new PresenciaContext();
                presenciaContext.Zonas.Add(new Zonas(NombreZona));
                presenciaContext.SaveChanges();
                return true;
            }

        }
        //Metodo para insertar un equipo de trabajo en la base de datos
        public static bool InsertaEquipoTrabajo(EquipoTrabajo eq)
        {
            PresenciaContext p = new PresenciaContext();
            if(eq is not null)
            {
                p.EquipoTrabajo.Add(eq);
                p.SaveChanges();
                return true;
            }
            else
            {
                return false;   
            }
            
        }
        //Metodo para borrar un trabajador existente de la base de datos
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
        //Metodo para borrar un fichaje concreto de la base de datos
        public static bool borraFichaje(int Id)
        {
            using var presenciaContext = new PresenciaContext();
            Fichajes fich = presenciaContext.Fichajes.Find(Id);
            if (fich is not null) {
                presenciaContext.Remove(fich);
                presenciaContext.SaveChanges();
                return true;
            } else
            {
                return false;
            }
        }
        //Metodo para borrar un usuario concreto de la base de datos
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
        //Metodo para borrar una zona concreta de la base de datos
        public static bool BorraZona(int id)
        {
            using var presenciaContext = new PresenciaContext();
            var Zona = presenciaContext.Zonas.Find(id);
            if (Zona is not null)
            {
                presenciaContext.Zonas.Remove(Zona);
                presenciaContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        } 
        //Metodo para actualizar un trabajador existente de la base de datos
        public static bool actualizaTrabajador(int Id, string Nombre, int GrupoTrabajo)
        {
            using var presenciaContext = new PresenciaContext();
            Trabajador trabajador = new Trabajador(Id, Nombre, presenciaContext.EquipoTrabajo.Find(GrupoTrabajo));
            presenciaContext.Update(trabajador);
            presenciaContext.SaveChanges();
            return true;
        }
        /*Metodo para actualizar un usuario existente en la base de datos, aunque realmente no se usa
          ya que por seguridad no se necesita la modificacion del nombre de usuario por lo que solo podremos
        modificar las contraseñas y si es o no admin(En la interfaz)
         */
        public static bool actualizaUsuario(string usernamebusca,string username, string password, bool esAdmin)
        {
            using var presenciaContext = new PresenciaContext();
            var us = presenciaContext.Usuarios.Where(x=>x.Username== usernamebusca).FirstOrDefault();
            if(usernamebusca is not null) { 
                us.Username = usernamebusca;
                us.Password = password;
                us.esAdmin = esAdmin;
                presenciaContext.Update(us);
                presenciaContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }    
        //Metodo para actualizar una zona existente en la base de datos
        public static bool ActualizaZona(string OldName, string NewName)
        {
            using var presenciaContext = new PresenciaContext();
            var Zona = presenciaContext.Zonas.Where(x => x.Nombre == OldName).FirstOrDefault();
            if (Zona is not null)
            {
                Zona.Nombre = NewName;
                presenciaContext.Zonas.Update(Zona);
                presenciaContext.Logs.Add(new Log("Update", "Se ha actualizado la zona \""+OldName+"\" a \""+NewName+"\""));
                presenciaContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        }
    }

