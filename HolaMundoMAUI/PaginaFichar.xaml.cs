
using Microsoft.Maui.Dispatching;
using System.Diagnostics;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using Bibliotec;
using ClassLibrary1;

namespace HolaMundoMAUI;

public partial class PaginaFichar : ContentPage
{
	string Username;
	Trabajador trabajador;
	bool CalActivo,PrincipalActivo;
	public IDispatcherTimer MyTimer { get; set; }
	PresenciaContext presenciaContext = new PresenciaContext();
	List<DateTime> ListaEntradas = new List<DateTime>();
	List<DateTime> ListaSalidas = new List<DateTime>();
	DateTime dt = DateTime.Now;
	DateTime HoraEntrada;
	Dia dia;
	public PaginaFichar(string username)
	{
		InitializeComponent();
		CompruebaFichajes(username);
		CompruebaTareas(username);
		ComienzaElReloj();
		
		Username = username;
		var user = presenciaContext.Usuarios.Where(x=>x.Username.ToLower()==username.ToLower()).FirstOrDefault();
		LabelNameUser.Text = user.Username;
		CalActivo = false;
		if (SelectorTareas.IsVisible == true)
		{
			BotonIniciarTarea.IsVisible = false;
			BotonIniciarTarea.IsEnabled = false;
			BotonAcabarTarea.IsVisible = false;
			BotonAcabarTarea.IsEnabled = false;
		}

		var trab = presenciaContext.Trabajador
			.Where(x => x.usuario.Username == username)
			.Include(x => x.equipo)
			.Include(x => x.usuario)
			.FirstOrDefault();
		trabajador = trab;

		var EquipoTrabajo = presenciaContext.EquipoTrabajo
			.Where(x => x.Trabajadores.Contains(trab))
			.Include(x=>x.Turnos)
			.Include(x => x.Tareas)
			.FirstOrDefault();
		var tareas = EquipoTrabajo.Tareas.ToList(); 
		var turnos = EquipoTrabajo.Turnos.ToList();
		Turno TurnoActual = new Turno();
		foreach (Tareas t in tareas)
		{
			SelectorTareas.Items.Add(t.NombreTarea);
		}
		foreach (Turno t in turnos)
        {
            List<string> Dias = DiasTrabajo(t);
			bool seTrabaja = false;
			var HoyEs = dt.DayOfWeek.ToString();
            if (Dias.Contains(HoyEs)) { seTrabaja = true; }
			if (t.HoraEntrada < dt && t.HoraSalida < dt && t.Activo == true && seTrabaja == true)
			{
				ListaEntradas.Add(t.HoraEntrada);
				ListaSalidas.Add(t.HoraSalida);
			}
		}
		SetListViewDias();
		SelectorTareas.SelectedIndex = 0;
		BotonIniciarTarea.IsVisible = false;
	}

	private async void BotonCerrarSesion_Clicked(object sender, EventArgs e)
	{
		BotonCerrarSession.BackgroundColor = Color.FromRgba("#2B282D");
		bool answer = await DisplayAlert("Logout", "¿Deseas cerrar sesión?", "Si", "No");
		if (answer == true)
		{
			App.Current.MainPage = new NavigationPage(new MainPage());
		}
	}
	public void ComienzaElReloj()
    {
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
		presenciaContext.Logs.Add(new Bibliotec.Log("Logout", Username + " ha cerrado sesion - "+dt));
		presenciaContext.SaveChanges();

	}
	private async void BotonFichar_Clicked(object sender, EventArgs e)
	{
		var trabajador = presenciaContext.Trabajador.Where(x => x.usuario.Username == Username).Include(x => x.equipo).FirstOrDefault();
		Fichajes fich = new Fichajes(trabajador, dt, "Entrada");
		OperacionesDBContext.InsertaFichaje(fich);
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
		foreach (DateTime d in ListaEntradas)
		{
			if (dt < d)
			{
				var operacion = (d - dt);
				string motivo = await DisplayPromptAsync("Usted llega temprano.", "¿Cual es la razon?");
				OperacionesDBContext.InsertaLog(new Log("Temprano", "El trabajador " + trabajador.numero_tarjeta + " Ha llegado " + operacion + "m antes. - " + dt));
			}
			if (dt > d.AddMinutes(5))
			{
				var operacion = (dt - d);
				string motivo = await DisplayPromptAsync("Usted llega tarde.", "¿Cual es la razon?");
				OperacionesDBContext.InsertaLog(new Log("Retraso", "El trabajador " + trabajador.numero_tarjeta + " Ha llegado " + operacion + "m tarde debido a " + motivo + " - " + dt));
				OperacionesDBContext.InsertaIncidencia(new Incidencia(trabajador,"Retraso de"+operacion,dt,false),presenciaContext);
			}
			if (dt > d && dt <= d.AddMinutes(5))
			{
				OperacionesDBContext.InsertaLog(new Log("En hora", "El trabajador " + trabajador.numero_tarjeta + " Ha llegado a tiempo - " + dt));
			}
		}
	}
	private async void BotonPlegar_Clicked(object sender, EventArgs e)
		{
			var trabajador = presenciaContext.Trabajador.Where(x => x.usuario.Username == Username).Include(x => x.equipo).FirstOrDefault();
			OperacionesDBContext.InsertaFichaje(new Fichajes(trabajador, dt, "Entrada"));
			var TrabajadorEnTurno = presenciaContext.TrabajadorEnTurno.Where(x => x.trabajador == trabajador).FirstOrDefault();
			OperacionesDBContext.BorraTrabajadorEnTurno(TrabajadorEnTurno, presenciaContext);
			
		
				//presenciaContext.TrabajadorEnTurno.Remove(TrabajadorEnTurno);
				//presenciaContext.SaveChanges();
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
			foreach (DateTime d in ListaSalidas) {
				if (dt < d)
				{
					var operacion = (d - dt);
					string motivo = await DisplayPromptAsync("Esta saliendo antes de hora", "¿Cual es la razon?");
					OperacionesDBContext.InsertaLog(new Log("Temprano", "El trabajador " + trabajador.numero_tarjeta + " Ha salido " + operacion + "m antes. debido a " + motivo + " - " + dt));
				}
				if (dt > d.AddMinutes(5))
				{
					var operacion = (dt - d);
					string motivo = await DisplayPromptAsync("Usted Sale tarde.", "¿Cual es la razon?");
					OperacionesDBContext.InsertaLog(new Log("Retraso", "El trabajador " + trabajador.numero_tarjeta + " Ha salido " + operacion + "m tarde. - " + dt));
				}
				if (dt > d && dt <= d.AddMinutes(5))
				{
					OperacionesDBContext.InsertaLog(new Log("En hora", "El trabajador " + trabajador.numero_tarjeta + " Ha llegado a tiempo - " + dt));
				}
			}
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
				presenciaContext.Logs.Add(new Log("Tareas", Username + " ha iniciado tarea " + tarea.NombreTarea + " - " + dt));
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
			presenciaContext.Logs.Add(new Log("Tareas", Username + " ha finalizado tarea " + tarea.NombreTarea + " - " + dt));
			presenciaContext.SaveChanges();
		}
    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		Dia item = e.SelectedItem as Dia;
		dia = item;
	}
    private void BtnPedirVacaciones_Clicked(object sender, EventArgs e)
    {
		var trab = trabajador;
		App.Current.MainPage = new NavigationPage(new AnadeDiaCalendario(Username,trab));
    }
    private void BtnCalendario_Clicked(object sender, EventArgs e)
    {
			if(CalActivo== true) { 
				ListViewCalendar.IsVisible = false;
				CuerpoPrincipal.IsVisible = true;
				BtnCalendarioTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
				BtnPedirVacacionesTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
				CalActivo = false;
			}
			else 
			{ 
				ListViewCalendar.IsVisible = true;
				CuerpoPrincipal.IsVisible = false;;
				BtnCalendarioTrabajador.BackgroundColor = Color.FromRgba("#93778B");
				BtnPedirVacacionesTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
				CalActivo = true;
			}
	}
	
	private void SetListViewDias()
    {
		var ExisteCalendario = presenciaContext.Calendario.Where(x => x.Trabajador == trabajador).Include(x => x.DiasDelCalendario).FirstOrDefault();
		if (ExisteCalendario is not null)
		{
			var ListaDias = ExisteCalendario.DiasDelCalendario.ToList();
			ListViewCalendario.ItemsSource = ListaDias;
			if (ListaDias.Count > 0)
				ListViewCalendario.SelectedItem = ListaDias[0];
		}
	}
    private void ListViewCalendario_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
			
    }

    private void VolverATrabajadores_Clicked(object sender, EventArgs e)
    {
		switch (PrincipalActivo)
		{
			case true:
				ListViewCalendar.IsVisible = false;
				ListViewCalendar.IsEnabled = false;
				CuerpoPrincipal.IsVisible = true;
				CuerpoPrincipal.IsEnabled = true;
				PrincipalActivo = false;
				CalActivo = false;
				break;
			case false:
				ListViewCalendar.IsVisible = false;
				ListViewCalendar.IsEnabled = false;
				CuerpoPrincipal.IsVisible = true;
				CuerpoPrincipal.IsEnabled = true;
				PrincipalActivo = true;
				CalActivo = false;
				break;
		}
	}
	private string QueDiaEs()
    {
		string dia = dt.DayOfWeek.ToString();
		return dia;
    }
    private List<string> DiasTrabajo(Turno t)
    {
		List<string> dias = new List<string>();
		if (t.EsLunes == true) { dias.Add("Monday"); }
		if (t.EsMartes == true) { dias.Add("Tuesday"); }
		if (t.EsMiercoles == true) { dias.Add("Wednesday"); }
		if (t.EsJueves == true) { dias.Add("Thursday"); }
		if (t.EsViernes == true) { dias.Add("Friday"); }
		if (t.EsSabado == true) { dias.Add("Saturday"); }
		if (t.EsDomingo == true) { dias.Add("Sunday"); }
		return dias;
	}
}