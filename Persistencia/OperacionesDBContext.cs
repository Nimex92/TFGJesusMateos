using Bibliotec;
using ClassLibrary1;
using Persistencia;
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
        //######################### Metodos para insertar  datos a la BD #############################################
        public static void InsertaLog(Log l)
        {
            try
            {
                using var p = new PresenciaContext();
                p.Logs.Add(l);
                p.SaveChanges();
            }
            catch (NullReferenceException ex)
            {
                Debug.Write(ex.Message);
            }
        }
        /// <summary>
        /// Metodo para insertar un trabajador en la base de datos pasandole
        /// un objecto trabajador como parametro
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool InsertaTrabajador(Trabajador t)
        {
            try
            {
                using var p = new PresenciaContext();
                if (t is not null)
                {
                    p.Trabajador.Add(t);
                    p.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(NullReferenceException ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }
        //Metodo para insertar un fichaje en la base de datos
        public static bool InsertaFichaje(Fichajes f)
        {
            try
            {
                using var p = new PresenciaContext();
                p.Fichajes.Add(f);
                p.SaveChanges();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            return true;
        }
        //Metodo para insertar un usuario en la base de datos
        public static bool InsertaUsuario(Usuarios us)
        {
            try
            {
                using var p = new PresenciaContext();
                p.Usuarios.Add(us);
                p.SaveChanges();
                return true;

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
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
            try { 
                using var p = new PresenciaContext();
                p.EquipoTrabajo.Add(eq);
                p.SaveChanges();
                return true; 
            }
            catch (Exception ex)
            {
                return false;
                Debug.WriteLine(ex.StackTrace);
            }
            
        }
        //Metodo para borrar un trabajador existente de la base de datos

        //########################################################################################################

        //#################### Metodos para Borrar datos de la BD ################################################
        public static bool BorraTrabajador(Trabajador t)
        {
            try
            {
                using var p = new PresenciaContext();
                p.Remove(t);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
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

        //######################Metodos para actualizar datos de la BD ############################################
        public static bool ActualizaTrabajador(Trabajador t,string nombre,EquipoTrabajo eq)
        {
            try
            {
                using var p = new PresenciaContext();
                Trabajador tr = new Trabajador(t.numero_tarjeta, nombre, eq);
                p.Update(tr);
                p.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }
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
        public static bool ActualizaUsuario(Usuarios us)
        {
            try
            {
                using var p = new PresenciaContext();
                p.Usuarios.Update(us);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
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

