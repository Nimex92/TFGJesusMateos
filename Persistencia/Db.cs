using Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using ClassLibray;

namespace Persistencia
{
    public class Db
    {
        public Db(){}
    }
    public static class DbInsert
    {
        /// <summary>
        /// Method to insert a bew Log on the MySql DB
        /// </summary>
        /// <param Log="log"></param>
        /// <param PresenciaContext="p"></param>
        public static void InsertLog(Log log, PresenciaContext p)
        {
            try
            {
                p.Logs.Add(log);
                p.SaveChanges();
            }
            catch (NullReferenceException ex)
            {
                Debug.Write(ex.ToString());
            }
        }
        //Metodo para insertar una incidencia en la base de datos
        public static void InsertIssue(Issue issue, PresenciaContext p)
        {
            try
            {
                p.Issues.Add(issue);
                p.SaveChanges();
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        //Metodo para insertar un trabajador en turno actual
        public static void InsertSignedWorker(Worker worker, Signing signing, PresenciaContext p)
        {
            try
            {
                p.SignedWorkers.Add(new SignedWorker(worker, signing));
                p.SaveChanges();
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        //Metodo para insertar un trabajador en la base de datos
        public static bool InsertWorker(Worker worker, PresenciaContext p)
        {
                try
                {
                    p.Workers.Add(worker);
                    p.SaveChanges();
                    return true;
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return false;
                }
        }
        //Metodo para insertar un fichaje en la base de datos
        public static bool InsertSigning(Signing signing,PresenciaContext p)
        {
            try
            {
                p.Signings.Add(signing);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
            
        }
        //Metodo para insertar un usuario en la base de datos
        public static bool InsertUser(User user, PresenciaContext p)
        {
            try
            {
                p.Users.Add(user);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }
        //Metodo para insertar una zona en la base de datos
        public static bool InsertWorkTask(string taskName, string description, double elapsedTime, PresenciaContext p)
        {
            if (taskName is not null && description is not null)
            {
                p.Add(new WorkTask(taskName, description, elapsedTime));
                p.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        //Metodo para insertar una zona en la base de datos
        public static bool InsertPlace(string name, PresenciaContext p)
        {
            try
            {
                p.Places.Add(new Places(name));
                p.SaveChanges();
                return true;
            }catch(NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }

        }
        //Metodo para insertar un equipo de trabajo en la base de datos
        public static bool InsertWorkGroup(WorkGroup workGroup,PresenciaContext p)
        {
            try
            {
                p.WorkGroups.Add(workGroup);
                p.SaveChanges();
                return true;
            }
            catch (NullReferenceException ex)
            {
                return false;
                Debug.WriteLine(ex.ToString());
            }

        }
        public static bool InsertStartedTask(StartedTask startedTask,PresenciaContext p)
        {
            try
            {
                p.StartedTasks.Add(startedTask);
                p.SaveChanges();
                return true;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        public static bool InsertEndedTask(EndedTask endedTask, PresenciaContext p)
        {
            try
            {
                p.EndedTasks.Add(endedTask);
                p.SaveChanges();
                return true;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }


    }
    public static class DbDelete
    {
        //#################################### Metodos para Borrar datos de la BD ################################
        //Metodo para borrar un trabajador existente de la base de datos
        public static bool DeleteWorker(Worker worker, PresenciaContext p)
        {
            try
            {
                p.Remove(worker);
                p.SaveChanges();
                return true;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;

            }
        }
        //Metodo para borrar un trabajador actual en turno(Plegar de su turno de trabajo)
        public static bool DeleteSignedWorker(SignedWorker signedWorker, PresenciaContext p)
        {
            try
            {
                p.SignedWorkers.Remove(signedWorker);
                p.SaveChanges();
                return true;
            }
            catch (NullReferenceException ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }
        //Metodo para borrar un fichaje concreto de la base de datos
        public static bool DeleteSigning(Signing signing, PresenciaContext p)
        {
            try
            {
                p.Signings.Remove(signing);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        //Metodo para borrar un usuario concreto de la base de datos
        public static bool DeleteUser(User user, PresenciaContext p)
        {
            try
            {
                p.Users.Remove(user);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        //Metodo para borrar una zona concreta de la base de datos
        public static bool DeletePlace(Places place, PresenciaContext p)
        {
            try
            {
                p.Places.Remove(place);
                p.SaveChanges();
                return true;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        //Metodo para borrar un turno de la base de datos
        public static bool DeleteWorkShift(WorkShift workShift, PresenciaContext p)
        {
            try
            {
                p.WorkShifts.Remove(workShift);
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
        public static bool DeleteWorkTask(WorkTask workTask, PresenciaContext p)
        {
            try
            {
                p.WorkTasks.Remove(workTask);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        public static bool InsertStartedTask(StartedTask startedTask, PresenciaContext p)
        {
            try
            {
                p.StartedTasks.Remove(startedTask);
                p.SaveChanges();
                return true;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        public static bool InsertEndedTask(EndedTask endedTask, PresenciaContext p)
        {
            try
            {
                p.EndedTasks.Remove(endedTask);
                p.SaveChanges();
                return true;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
    }
    public static class DbUpdate
    {
        public static bool UpdateWorker(Worker worker, string name, WorkGroup workgroup)
        {
            try
            {
                using var p = new PresenciaContext();
                Worker tr = new Worker(worker.CardNumber, name, workgroup);
                p.Update(tr);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        //Metodo para actualizar un usuario existente en la base de datos
        public static bool UpdateUser(User user, PresenciaContext p)
        {
            try
            {
                p.Users.Update(user);
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
        public static bool UpdatePlace(string oldName, string newName)
        {
            try
            {
                using var presenciaContext = new PresenciaContext();
                var Zona = presenciaContext.Places.Where(x => x.Name == oldName).FirstOrDefault();
                if (Zona is not null)
                {
                    Zona.Name = newName;
                    presenciaContext.Places.Update(Zona);
                    presenciaContext.Logs.Add(new Log("Update", "Se ha actualizado la zona \"" + oldName + "\" a \"" + newName + "\""));
                    presenciaContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        //Metodo para actualizar un turno existente en la base de datos
        public static bool UpdateWorkShift(WorkShift worker, string name, DateTime checkIn, DateTime checkOut, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool sabado, bool sunday, DateTime validFrom, DateTime validUntil, bool enabled, bool deleted, PresenciaContext p)
        {
            try
            {
                WorkShift tu = new WorkShift();
                tu.Name = worker.Name;
                tu.CheckIn = checkIn;
                tu.CheckOut = checkOut;
                tu.Monday = monday;
                tu.Tuesday = tuesday;
                tu.Wednesday = wednesday;
                tu.Thursday = thursday;
                tu.Friday = friday;
                tu.Saturday = sabado;
                tu.Domingo = sunday;
                tu.ValidFrom = validFrom;
                tu.ValidUntil = validUntil;
                tu.Enabled = enabled;
                tu.Deleted = deleted;

                p.Update(tu);
                p.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        //Metodo para actualizar una tarea existente en la base de datos
        public static bool UpdateWorkTask(WorkTask workTask, string name, string descrption, double elapsedTime,PresenciaContext p)
        {
            try
            {
                WorkTask task = new WorkTask();
                task.TaskId = workTask.TaskId;
                task.Name = name;
                task.Description = descrption;
                task.ElapsedTime = elapsedTime;
                p.Update(task);
                p.SaveChanges();
                return true;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        //Metodo para actualizar un equipo de trabajo existente en la base de datos
        public static bool UpdateWorkGroup(WorkGroup workgroup, string name, PresenciaContext p)
        {
            try
            {
                WorkGroup e = new WorkGroup();
                e.Id = workgroup.Id;
                e.Name = name;
                p.Update(e);
                p.SaveChanges();
                return true;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        public static bool UpdateStartedTask(StartedTask startedTask, WorkTask workTask, Worker worker, DateTime taskStart, PresenciaContext p)
        {
            try
            {
                startedTask.Worker = worker;
                startedTask.Task = workTask;
                startedTask.TastStart = taskStart;
                p.StartedTasks.Update(startedTask);
                p.SaveChanges();
                return true;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
        public static bool UpdateEndedTask(EndedTask endedTask,WorkTask workTask,Worker worker,DateTime taskInit,DateTime taskEnd,double totalTimeUsed,bool onTime, PresenciaContext p)
        {
            try
            {
                endedTask.Task = workTask;
                endedTask.Worker = worker;
                endedTask.TaskTaskInit = taskInit;
                endedTask.TaskEnd = taskEnd;
                endedTask.TotalTimeUsed = totalTimeUsed;
                endedTask.OnTime = onTime;
                p.EndedTasks.Update(endedTask);
                p.SaveChanges();
                return true;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}