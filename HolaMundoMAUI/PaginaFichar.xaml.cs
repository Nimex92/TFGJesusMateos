
using Microsoft.Maui.Dispatching;
using System.Diagnostics;
using Persistencia;
using ClassLibrary1;
using Microsoft.EntityFrameworkCore;
using Bibliotec;

namespace HolaMundoMAUI;

public partial class PaginaFichar : ContentPage
{
	string username;
	Trabajador trabajador;
    public IDispatcherTimer MyTimer { get; set; }
	PresenciaContext presenciaContext = new PresenciaContext();
	Grupo_Trabajo gt;
	DateTime Entrada;
	DateTime Salida;
	DateTime dt = DateTime.Now;
	DateTime HoraEntrada;
	public PaginaFichar(string username)
    {
		InitializeComponent();
		CompruebaFichajes(username);
		CompruebaTareas();

		this.username = username;
		var trab = presenciaContext.Trabajador.Where(x => x.usuario.Username == username)
			.Include(x => x.grupo).Include(x => x.usuario).FirstOrDefault();
		trabajador = trab;

		MyTimer = Reloj.Dispatcher.CreateTimer();
		MyTimer.Interval = TimeSpan.FromSeconds(1);
		MyTimer.IsRepeating = true;
		MyTimer.Tick += (x, y) =>
		{
			DateTime dt = DateTime.Now;

			Reloj.Text =
				dt.Hour.ToString("00") + ":" +
				dt.Minute.ToString("00") + ":" +
				dt.Second.ToString("00");
			//return true; // return true to repeat counting, false to stop timer
		};

		MyTimer.Start();

		var Trabajador = presenciaContext.Trabajador.Where(x => x.usuario.Username == username).Include(x=>x.grupo).FirstOrDefault();
		var GrupoTrabajo = presenciaContext.Grupo_Trabajo.Where(x => x.IdGrupo == Trabajador.grupo.IdGrupo).Include(x => x.Tareas).FirstOrDefault();
		var tareas = GrupoTrabajo.Tareas;

		foreach (Tareas t in tareas)
        {
			SelectorTareas.Items.Add(t.NombreTarea);
        }
		string EntradaCompleta = GrupoTrabajo.HoraEntrada;
		string HoraEntrada = EntradaCompleta.Substring(0, 2);
		string MinutosEntrada = EntradaCompleta.Substring(3, 2);
		string SalidaCompleta = GrupoTrabajo.HoraSalida;
		string HoraSalida = SalidaCompleta.Substring(0, 2);
		string MinutosSalida = SalidaCompleta.Substring(3, 2);
		int HorEnt, MinEnt, HorSal, MinSal;
		HorEnt = int.Parse(HoraEntrada);
		MinEnt = int.Parse(MinutosEntrada);
		HorSal = int.Parse(HoraSalida);
		MinSal = int.Parse(MinutosSalida);
		Entrada = DateTime.Today.AddHours(HorEnt).AddMinutes(MinEnt);
		Salida = DateTime.Today.AddHours(HorSal).AddMinutes(MinSal);
	}

	private void CompruebaFichajes(string user)
    {
			var Trabajador = presenciaContext.TrabajadorEnTurno.Where(x => x.trabajador.usuario.Username == user).FirstOrDefault();
			if(Trabajador is not null)
            {
				BotonFichar.IsVisible = false;
				BotonFichar.IsEnabled = false;
				BotonPlegar.IsVisible = true;
				BotonPlegar.IsEnabled = true;
			}
            else
			{
				BotonFichar.IsVisible = true;
				BotonFichar.IsEnabled = true;
				BotonPlegar.IsVisible = false;
				BotonPlegar.IsEnabled = false;
			}
	}
	private void CompruebaTareas()
	{	
		   
    }
	public void irMainPage(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new MainPage());

	}
	private void BotonFichar_Clicked(object sender, EventArgs e)
	{
		//var trabajador = presenciaContext.Trabajador.Where(x => x.usuario.Username == username).Include(x=>x.grupo).FirstOrDefault();
		OperacionesDBContext.insertaFichaje(trabajador.numero_tarjeta, trabajador.grupo.IdGrupo, "Entrada");
		Fichajes fich = new Fichajes(trabajador, trabajador.grupo, dt, "Entrada");

		presenciaContext.TrabajadorEnTurno.Add(new TrabajadorEnTurno(trabajador, fich));
        presenciaContext.SaveChanges();
        BotonFichar.IsVisible = false;
		BotonFichar.IsEnabled = false;
		BotonPlegar.IsVisible = true;
		BotonPlegar.IsEnabled = true;
		SelectorTareas.IsEnabled = true;
		SelectorTareas.IsVisible = true;
		BotonIniciarTarea.IsEnabled = true;
		BotonIniciarTarea.IsVisible = true;

		if(dt > Entrada.AddMinutes(5))
        {
			var operacion = Entrada- dt;
			presenciaContext.Logs.Add(new Log("Retraso", "El trabajador" + trabajador.numero_tarjeta + " Ha llegado "+operacion.TotalMinutes+"m tarde."));
        }
        else
        {
			presenciaContext.Logs.Add(new Log("En hora", "El trabajador" + trabajador.numero_tarjeta + " Ha llegado a tiempo"));
		}
		presenciaContext.SaveChanges();
	}
	private void BotonPlegar_Clicked(object sender, EventArgs e)
	{
		//var grupo = presenciaContext.Grupo_Trabajo.Where(x => x.IdGrupo == 1).Include(x => x.Tareas).FirstOrDefault();
  //      foreach (var tarea in grupo.Tareas)
  //      {
		//	Debug.WriteLine(tarea.Descripcion);
  //      }
		//grupo.Tareas.Add(new Tareas());

		var trabajador = presenciaContext.Trabajador.Where(x => x.usuario.Username == username).Include(x => x.grupo).FirstOrDefault();
		OperacionesDBContext.insertaFichaje(trabajador.numero_tarjeta, trabajador.grupo.IdGrupo, "Salida");
		var TrabajadorEnTurno = presenciaContext.TrabajadorEnTurno.Where(x => x.trabajador == trabajador).FirstOrDefault();
		presenciaContext.TrabajadorEnTurno.Remove(TrabajadorEnTurno);
		presenciaContext.SaveChanges();
		BotonFichar.IsVisible = true;
		BotonFichar.IsEnabled = true;
		BotonPlegar.IsVisible = false;
		BotonPlegar.IsEnabled = false;
		SelectorTareas.IsEnabled = false;
		SelectorTareas.IsVisible = false;
		if(SelectorTareas.IsVisible == false)
        {
			BotonIniciarTarea.IsVisible = false;
			BotonIniciarTarea.IsEnabled = false;
			BotonAcabarTarea.IsVisible = false;
			BotonAcabarTarea.IsEnabled = false;
		}
		if (dt <= Salida.AddMinutes(5))
		{
			if(dt < Salida)
            {
				presenciaContext.Logs.Add(new Log("En hora", "El trabajador"+trabajador.numero_tarjeta + " Ha salido en hora."));
			}
            else
            {
				var operacion = Salida - dt;
				presenciaContext.Logs.Add(new Log("Sale pronto", "El trabajador" +trabajador.numero_tarjeta + " Ha salido "+operacion.TotalMinutes+"m tarde."));
            }
				
		}
		else
		{
			var operacion = dt - Salida;
			presenciaContext.Logs.Add(new Log("Sale Tarde", "El trabajador" + trabajador.numero_tarjeta + " Ha salido "+operacion.TotalMinutes));
		}
		presenciaContext.SaveChanges();
	}
	void OnPickerSelectedIndexChanged(object sender, EventArgs e)
	{
		int selectedIndex = SelectorTareas.SelectedIndex;

        var tareaActual = SelectorTareas.SelectedItem.ToString();
        var tarea = presenciaContext.Tareas.Where(x => x.NombreTarea == tareaActual).FirstOrDefault();
		var TareaIniciada = presenciaContext.TareasComenzadas.Where(x => x.tarea.NombreTarea == tarea.NombreTarea && x.InicioTarea.Date == dt.Date).OrderBy(x => x.InicioTarea).LastOrDefault();
		
        if (TareaIniciada is not null)
        {
            BotonAcabarTarea.IsEnabled = true;
            BotonAcabarTarea.IsVisible = true;
            BotonIniciarTarea.IsEnabled = false;
            BotonIniciarTarea.IsVisible = false;
        }
        else
        {
			BotonAcabarTarea.IsEnabled = false;
			BotonAcabarTarea.IsVisible = false;
			BotonIniciarTarea.IsEnabled = true;
			BotonIniciarTarea.IsVisible = true;
		}
    }

    private void BotonIniciarTarea_Clicked(object sender, EventArgs e)
    {
		BotonAcabarTarea.IsEnabled = true;
		BotonAcabarTarea.IsVisible = true;
		BotonIniciarTarea.IsEnabled = false;
		BotonIniciarTarea.IsVisible = false;
		HoraEntrada = dt;
		var tareaActual = SelectorTareas.SelectedItem.ToString();
		var tarea = presenciaContext.Tareas.Where(x => x.NombreTarea == tareaActual).FirstOrDefault();
		var grupo = presenciaContext.Grupo_Trabajo.Find(trabajador.grupo.IdGrupo);
		
		presenciaContext.Add(new TareaComenzada(tarea, trabajador, grupo, dt));
		presenciaContext.SaveChanges();
	}

    private void BotonAcabarTarea_Clicked(object sender, EventArgs e)
    {
		BotonAcabarTarea.IsEnabled = false;
		BotonAcabarTarea.IsVisible = false;
		BotonIniciarTarea.IsEnabled = true;
		BotonIniciarTarea.IsVisible = true;
		var tareaActual = SelectorTareas.SelectedItem.ToString();
		var tarea = presenciaContext.Tareas.Where(x=>x.NombreTarea == tareaActual).FirstOrDefault();
		var TareaIniciada = presenciaContext.TareasComenzadas.Where(x => x.tarea.NombreTarea == tarea.NombreTarea).Where(x=>x.InicioTarea.Date == dt.Date).OrderBy(x=>x.InicioTarea).Last();
		var grupo = presenciaContext.Grupo_Trabajo.Find(trabajador.grupo.IdGrupo);
		var HorasUsadas = (DateTime.Now - TareaIniciada.InicioTarea).TotalHours;
		bool EnHora=false;
        if (HorasUsadas <= tarea.TiempoEstimado)
        {
			EnHora = true;
        }
        
		presenciaContext.TareasFinalizadas.Add(new TareaFinalizada(tarea,trabajador,grupo,TareaIniciada.InicioTarea,dt,HorasUsadas,EnHora));
		presenciaContext.TareasComenzadas.Remove(TareaIniciada);
		presenciaContext.SaveChanges();
	}


}

