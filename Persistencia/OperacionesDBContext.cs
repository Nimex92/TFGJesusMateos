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
        static PresenciaContext p = new PresenciaContext();
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
        /// <param Trabajador="t"></param>
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
                Debug.WriteLine(ex.ToString());
                return false;
            }
            catch(ArgumentNullException ex2)
            {
                Debug.WriteLine(ex2.ToString());
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
        //Metodo para insertar una zona en la base de datos
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
        //Metodo para borrar un fichaje concreto de la base de datos
        public static bool BorraFichaje(Fichajes fichaje)
        {
            try
            {
                using var p = new PresenciaContext();
                p.Fichajes.Remove(fichaje);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }
        //Metodo para borrar un usuario concreto de la base de datos
        public static bool BorraUsuario(Usuarios us)
        {
            try
            {
                var p = new PresenciaContext();
                p.Usuarios.Remove(us);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }
        //Metodo para borrar una zona concreta de la base de datos
        public static bool BorraZona(Zonas zona)
        {
            try
            {
                using var p = new PresenciaContext();
                p.Zonas.Remove(zona);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }
        //Metodo para borrar un turno de la base de datos
        public static bool BorraTurno(Turno t)
        {
            try
            {
                using var p = new PresenciaContext();
                p.Turno.Remove(t);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }
        //Metodo para borrar una zona de la base de datos
        public static bool BorraTarea(Tareas tarea)
        {
            try
            {
                using var p = new PresenciaContext();
                p.Tareas.Remove(tarea);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }


        //######################Metodos para actualizar datos de la BD ############################################

        //Metodo para actualizar un trabajador existente de la base de datos
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
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        //Metodo para actualizar un usuario existente en la base de datos
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
        //Metodo para actualizar un turno existente en la base de datos
        public static bool ActualizaTurno(Turno t,string nombre,DateTime entrada,DateTime salida,bool EsLunes,bool EsMartes,bool EsMiercoles, bool EsJueves, bool EsViernes,bool EsSabado, bool EsDomingo,DateTime ValidoDesde,DateTime ValidoHasta,bool Activo, bool Eliminado)
        {
            try
            {
                using var p = new PresenciaContext();
                Turno tu = new Turno();
                tu.Nombre = t.Nombre;
                tu.HoraEntrada = entrada;
                tu.HoraSalida = salida;
                tu.EsLunes = EsLunes;
                tu.EsMartes = EsMartes;
                tu.EsMiercoles = EsMiercoles;
                tu.EsJueves = EsJueves;
                tu.EsViernes = EsViernes;
                tu.EsSabado = EsSabado;
                tu.EsDomingo = EsDomingo;
                tu.ValidoDesde = ValidoDesde;
                tu.ValidoHasta = ValidoHasta;
                tu.Activo = Activo;
                tu.Eliminado = Eliminado;
                
                p.Update(tu);
                p.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        //Metodo para actualizar una tarea existente en la base de datos
        public static bool ActualizaTarea(Tareas ta,string nombre,string descripcion,double tiempoestimado)
        {
            try
            {
                Tareas t = new Tareas();
                t.IdTarea = ta.IdTarea;
                t.NombreTarea = nombre;
                t.Descripcion = descripcion;
                t.TiempoEstimado = tiempoestimado;
                p.Update(t);
                p.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        //Metodo para actualizar un equipo de trabajo existente en la base de datos
        public static bool ActualizaEquipoTrabajo(EquipoTrabajo eq,string nombre)
        {
            try
            {
                EquipoTrabajo e = new EquipoTrabajo();
                e.Id = eq.Id;
                e.Nombre = nombre;
                p.Update(e);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex){
                Debug.WriteLine(ex.ToString());
                return false;
            }
            
        }
    }
}

