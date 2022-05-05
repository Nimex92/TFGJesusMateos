
using Microsoft.Maui.Dispatching;
using System.Diagnostics;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using Bibliotec;
using ClassLibrary1;

namespace HolaMundoMAUI;

public partial class PaginaFichar : ContentPage
{
	string username;
	Trabajador trabajador;
	public IDispatcherTimer MyTimer { get; set; }
	PresenciaContext presenciaContext = new PresenciaContext();
	DateTime Entrada;
	DateTime Salida;
	DateTime dt = DateTime.Now;
	DateTime HoraEntrada;
	string user;
	public PaginaFichar(string username)
	{
		InitializeComponent();
		CompruebaFichajes(username);
		CompruebaTareas(username);
		user = username;
		
		this.username = username;
		var trab = presenciaContext.Trabajador.Where(x => x.usuario.Username == username)
			.Include(x => x.equipo).Include(x => x.usuario).FirstOrDefault();
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
		};

		MyTimer.Start();

		var Trabajador = presenciaContext.Trabajador.Where(x => x.usuario.Username == username).Include(x => x.equipo).FirstOrDefault();
		var EquipoTrabajo = presenciaContext.EquipoTrabajo.Where(x => x.Trabajadores.Contains(Trabajador)).Include(x=>x.Turnos).Include(x => x.Tareas).FirstOrDefault();
		var tareas = EquipoTrabajo.Tareas;
		var turnos = EquipoTrabajo.Turnos;
		Turno TurnoActual = new Turno();
		
		foreach (Tareas t in tareas)
		{
			SelectorTareas.Items.Add(t.NombreTarea);
		}
		foreach (Turno t in turnos)
        {
			if(t.HoraEntrada >= dt && t.HoraSalida <= dt && t.Activo == true)
            {
				TurnoActual = t;
            }
        }
		
		
		SelectorTareas.SelectedIndex = 0;
		string EntradaCompleta = TurnoActual.HoraEntrada.ToString();
		string HoraEntrada = EntradaCompleta.Substring(0, 2);
		string MinutosEntrada = EntradaCompleta.Substring(3, 2);
		string SalidaCompleta = TurnoActual.HoraSalida.ToString();
		string HoraSalida = SalidaCompleta.Substring(0, 2);
		string MinutosSalida = SalidaCompleta.Substring(3, 2);
		int HorEnt, MinEnt, HorSal, MinSal;
		HorEnt = int.Parse(HoraEntrada);
		MinEnt = int.Parse(MinutosEntrada);
		HorSal = int.Parse(HoraSalida);
		MinSal = int.Parse(MinutosSalida);
		Entrada = DateTime.Today.AddHours(HorEnt).AddMinutes(MinEnt);
		Salida = DateTime.Today.AddHours(HorSal).AddMinutes(MinSal);
		BotonIniciarTarea.IsVisible = false;
	}

	private async void ImageButton_Clicked(object sender, EventArgs e)
	{
		BotonCerrarSession.BackgroundColor = Color.FromRgba("#b9b6bf");
		bool answer = await DisplayAlert("Question?", "¿Deseas cerrar sesión?", "Si", "No");
		if (answer == true)
		{
			App.Current.MainPage = new NavigationPage(new MainPage());
		}
	}
	private void CompruebaFichajes(string user)
	{
		var Trabajador = presenciaContext.TrabajadorEnTurno.Where(x => x.trabajador.usuario.Username == user).FirstOrDefault();
		if (Trabajador is not null)
		{
			BotonFichar.IsVisible = false;
			BotonFichar.IsEnabled = false;
			BotonPlegar.IsVisible = true;
			BotonPlegar.IsEnabled = true;
			SelectorTareas.IsVisible = true;
			SelectorTareas.IsEnabled = true;
			CompruebaTareas(user);
		}
		else
		{
			BotonFichar.IsVisible = true;
			BotonFichar.IsEnabled = true;
			BotonPlegar.IsVisible = false;
			BotonPlegar.IsEnabled = false;
			SelectorTareas.IsVisible = false;
			SelectorTareas.IsEnabled = false;
		}
	}
	private void CompruebaTareas(string user)
	{
		var trabajador = presenciaContext.TareasComenzadas.Where(x => x.trabajador.usuario.Username == user).Include(x => x.trabajador).Include(x => x.tarea).FirstOrDefault();
		if (trabajador is not null)
		{
			BotonAcabarTarea.IsVisible = true;
			BotonAcabarTarea.IsEnabled = true;
			BotonIniciarTarea.IsEnabled = false;
			BotonIniciarTarea.IsVisible = false;
		}
		else
		{
			BotonAcabarTarea.IsVisible = false;
			BotonAcabarTarea.IsEnabled = false;
			BotonIniciarTarea.IsEnabled = false;
			BotonIniciarTarea.IsVisible = false;
		}
	}
	public void irMainPage(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new MainPage());
		presenciaContext.Logs.Add(new Bibliotec.Log("Logout", username + " ha cerrado sesion - "+dt));
		presenciaContext.SaveChanges();

	}
	private async void BotonFichar_Clicked(object sender, EventArgs e)
	{
		
		var trabajador = presenciaContext.Trabajador.Where(x => x.usuario.Username == username).Include(x=>x.equipo).FirstOrDefault();
		OperacionesDBContext.insertaFichaje(trabajador.numero_tarjeta, "Entrada");
		Fichajes fich = new Fichajes(trabajador, dt, "Entrada");

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
		if (dt < Entrada)
		{
			var operacion = (Entrada-dt);
			presenciaContext.Logs.Add(new Log("Temprano", "El trabajador " + trabajador.numero_tarjeta + " Ha llegado " + operacion + "m antes. - "+dt));
		}
		if (dt > Entrada.AddMinutes(5))
		{
			var operacion = (dt - Entrada);
			string motivo = await DisplayPromptAsync("Usted llega tarde.", "¿Cual es la razon?");
			presenciaContext.Logs.Add(new Log("Retraso", "El trabajador " + trabajador.numero_tarjeta + " Ha llegado " + operacion + "m tarde debido a "+motivo+" - "+dt));
		}
		if (dt > Entrada && dt <= Entrada.AddMinutes(5))
		{
			presenciaContext.Logs.Add(new Log("En hora", "El trabajador " + trabajador.numero_tarjeta + " Ha llegado a tiempo - "+dt));
		}
		presenciaContext.SaveChanges(); 
		}
	
	private async void BotonPlegar_Clicked(object sender, EventArgs e)
		{
			var trabajador = presenciaContext.Trabajador.Where(x => x.usuario.Username == username).Include(x => x.equipo).FirstOrDefault();
			OperacionesDBContext.insertaFichaje(trabajador.numero_tarjeta , "Salida");
			var TrabajadorEnTurno = presenciaContext.TrabajadorEnTurno.Where(x => x.trabajador == trabajador).FirstOrDefault();
			presenciaContext.TrabajadorEnTurno.Remove(TrabajadorEnTurno);
			presenciaContext.SaveChanges();
			BotonFichar.IsVisible = true;
			BotonFichar.IsEnabled = true;
			BotonPlegar.IsVisible = false;
			BotonPlegar.IsEnabled = false;
			SelectorTareas.IsEnabled = false;
			SelectorTareas.IsVisible = false;
			if (SelectorTareas.IsVisible == false)
			{
				BotonIniciarTarea.IsVisible = false;
				BotonIniciarTarea.IsEnabled = false;
				BotonAcabarTarea.IsVisible = false;
				BotonAcabarTarea.IsEnabled = false;
			}
		if (dt < Salida)
		{
			var operacion = ( Salida- dt);
			string motivo = await DisplayPromptAsync("Esta saliendo antes de hora", "¿Cual es la razon?");
			presenciaContext.Logs.Add(new Log("Temprano", "El trabajador " + trabajador.numero_tarjeta + " Ha salido " + operacion + "m antes. debido a "+motivo+" - "+dt));
		}
		if (dt > Salida.AddMinutes(5))
		{
			var operacion = (dt - Salida);
			presenciaContext.Logs.Add(new Log("Retraso", "El trabajador " + trabajador.numero_tarjeta + " Ha salido " + operacion + "m tarde. - "+dt));
		}
		if (dt > Salida && dt <= Salida.AddMinutes(5))
		{
			presenciaContext.Logs.Add(new Log("En hora", "El trabajador " + trabajador.numero_tarjeta + " Ha llegado a tiempo - "+dt));
		}
		presenciaContext.SaveChanges();
	}
	private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
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
	private async void BotonIniciarTarea_Clicked(object sender, EventArgs e)
		{
			BotonAcabarTarea.IsEnabled = true;
			BotonAcabarTarea.IsVisible = true;
			BotonIniciarTarea.IsEnabled = false;
			BotonIniciarTarea.IsVisible = false;
			HoraEntrada = dt;

			if (SelectorTareas.SelectedItem.ToString() is not null)
			{
				var tareaActual = SelectorTareas.SelectedItem.ToString();
				var tarea = presenciaContext.Tareas.Where(x => x.NombreTarea == tareaActual).FirstOrDefault();
				presenciaContext.Add(new TareaComenzada(tarea, trabajador, dt));
				presenciaContext.Logs.Add(new Log("Tareas", username + " ha iniciado tarea " + tarea.NombreTarea + " - " + dt));
				presenciaContext.SaveChanges();
		}
			else
			{
				await DisplayAlert("Alert", "Por favor, Selecciona una tarea", "OK");
			}

		}
	private void BotonAcabarTarea_Clicked(object sender, EventArgs e)
		{
			BotonAcabarTarea.IsEnabled = false;
			BotonAcabarTarea.IsVisible = false;
			BotonIniciarTarea.IsEnabled = true;
			BotonIniciarTarea.IsVisible = true;
			var tareaActual = SelectorTareas.SelectedItem.ToString();
			var tarea = presenciaContext.Tareas.Where(x => x.NombreTarea == tareaActual).FirstOrDefault();
			var TareaIniciada = presenciaContext.TareasComenzadas.Where(x => x.tarea.NombreTarea == tarea.NombreTarea).Where(x => x.InicioTarea.Date == dt.Date).OrderBy(x => x.InicioTarea).Last();
			var HorasUsadas = (DateTime.Now - TareaIniciada.InicioTarea).TotalHours;
			bool EnHora = false;
			if (HorasUsadas <= tarea.TiempoEstimado)
			{
				EnHora = true;
			}

			presenciaContext.TareasFinalizadas.Add(new TareaFinalizada(tarea, trabajador, TareaIniciada.InicioTarea, dt, HorasUsadas, EnHora));
			presenciaContext.TareasComenzadas.Remove(TareaIniciada);
			presenciaContext.Logs.Add(new Log("Tareas", username + " ha finalizado tarea " + tarea.NombreTarea + " - " + dt));
			presenciaContext.SaveChanges();
		}
	private void Logout_png_Clicked(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new MainPage());
	}

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
}

