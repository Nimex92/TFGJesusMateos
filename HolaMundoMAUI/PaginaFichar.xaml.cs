
using Microsoft.Maui.Dispatching;
using System.Diagnostics;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using ClassLibray;

namespace HolaMundoMAUI;

public partial class PaginaFichar : ContentPage
{
	string Username;
	Worker worker;
	bool CalActivo,PrincipalActivo;
	public IDispatcherTimer MyTimer { get; set; }
	PresenciaContext presenciaContext = new PresenciaContext();
	List<DateTime> ListaEntradas = new List<DateTime>();
	List<DateTime> ListaSalidas = new List<DateTime>();
	DateTime dt = DateTime.Now;
	DateTime HoraEntrada;
	Day _day;
	public PaginaFichar(string username)
	{
		InitializeComponent();
		CompruebaFichajes(username);
		CompruebaTareas(username);
		ComienzaElReloj();
		
		Username = username;
		var user = presenciaContext.Users.Where(x=>x.Username.ToLower()==username.ToLower()).FirstOrDefault();
		LabelNameUser.Text = user.Username;
		CalActivo = false;
		if (SelectorTareas.IsVisible == true)
		{
			BotonIniciarTarea.IsVisible = false;
			BotonIniciarTarea.IsEnabled = false;
			BotonAcabarTarea.IsVisible = false;
			BotonAcabarTarea.IsEnabled = false;
		}

<<<<<<< HEAD
		var trab = presenciaContext.Workers
			.Where(x => x.User.Username == username)
			.Include(x => x.WorkGroup)
			.Include(x => x.User)
=======
		var trab = presenciaContext.Trabajador
			.Where(x => x.Usuario.Username == username)
			.Include(x => x.Equipo)
			.Include(x => x.Usuario)
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
			.FirstOrDefault();
		worker = trab;

		var EquipoTrabajo = presenciaContext.WorkGroups
			.Where(x => x.Workers.Contains(trab))
			.Include(x=>x.WorkShifts)
			.Include(x => x.Tasks)
			.FirstOrDefault();
		var tareas = EquipoTrabajo.Tasks.ToList(); 
		var turnos = EquipoTrabajo.WorkShifts.ToList();
		WorkShift workShiftActual = new WorkShift();
		foreach (WorkTask t in tareas)
		{
			SelectorTareas.Items.Add(t.Name);
		}
		foreach (WorkShift t in turnos)
        {
            List<string> Dias = DiasTrabajo(t);
			bool seTrabaja = false;
			var HoyEs = dt.DayOfWeek.ToString();
            if (Dias.Contains(HoyEs)) { seTrabaja = true; }
			if (t.CheckIn < dt && t.CheckOut < dt && t.Enabled == true && seTrabaja == true)
			{
				ListaEntradas.Add(t.CheckIn);
				ListaSalidas.Add(t.CheckOut);
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
<<<<<<< HEAD
		var Trabajador = presenciaContext.SignedWorkers.Where(x => x.Worker.User.Username == user).FirstOrDefault();
=======
		var Trabajador = presenciaContext.TrabajadorEnTurno.Where(x => x.trabajador.Usuario.Username == user).FirstOrDefault();
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
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
<<<<<<< HEAD
		var trabajador = presenciaContext.StartedTasks.Where(x => x.Worker.User.Username == user).Include(x => x.Worker).Include(x => x.Task).FirstOrDefault();
=======
		var trabajador = presenciaContext.TareasComenzadas.Where(x => x.trabajador.Usuario.Username == user).Include(x => x.trabajador).Include(x => x.tarea).FirstOrDefault();
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
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
		presenciaContext.Logs.Add(new Log("Logout", Username + " ha cerrado sesion - "+dt));
		presenciaContext.SaveChanges();

	}
	private async void BotonFichar_Clicked(object sender, EventArgs e)
	{
<<<<<<< HEAD
		var trabajador = presenciaContext.Workers.Where(x => x.User.Username == Username).Include(x => x.WorkGroup).FirstOrDefault();
		Signing fich = new Signing(trabajador, dt, "Entrada");
		DbInsert.InsertSigning(fich, presenciaContext);
		
		
        presenciaContext.SignedWorkers.Add(new SignedWorker(trabajador, fich));
=======
		var trabajador = presenciaContext.Trabajador.Where(x => x.Usuario.Username == Username).Include(x => x.Equipo).FirstOrDefault();
		Fichajes fich = new Fichajes(trabajador, dt, "Entrada");
		OperacionesDBContext.InsertaFichaje(fich);
        presenciaContext.TrabajadorEnTurno.Add(new TrabajadorEnTurno(trabajador, fich));
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
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
<<<<<<< HEAD
				DbInsert.InsertLog(new Log("Temprano", "El trabajador " + trabajador.CardNumber + " Ha llegado " + operacion + "m antes. - " + dt), presenciaContext);
=======
				OperacionesDBContext.InsertaLog(new Log("Temprano", "El trabajador " + trabajador.NumeroTarjeta + " Ha llegado " + operacion + "m antes. - " + dt));
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
			}
			if (dt > d.AddMinutes(5))
			{
				var operacion = (dt - d);
				string motivo = await DisplayPromptAsync("Usted llega tarde.", "¿Cual es la razon?");
<<<<<<< HEAD
				DbInsert.InsertLog(new Log("Retraso", "El trabajador " + trabajador.CardNumber + " Ha llegado " + operacion + "m tarde debido a " + motivo + " - " + dt), presenciaContext);
				DbInsert.InsertIssue(new Issue(trabajador,"Retraso de"+operacion,dt,false),presenciaContext);
			}
			if (dt > d && dt <= d.AddMinutes(5))
			{
				DbInsert.InsertLog(new Log("En hora", "El trabajador " + trabajador.CardNumber + " Ha llegado a tiempo - " + dt), presenciaContext);
=======
				OperacionesDBContext.InsertaLog(new Log("Retraso", "El trabajador " + trabajador.NumeroTarjeta + " Ha llegado " + operacion + "m tarde debido a " + motivo + " - " + dt));
				OperacionesDBContext.InsertaIncidencia(new Incidencia(trabajador,"Retraso de"+operacion,dt,false),presenciaContext);
			}
			if (dt > d && dt <= d.AddMinutes(5))
			{
				OperacionesDBContext.InsertaLog(new Log("En hora", "El trabajador " + trabajador.NumeroTarjeta + " Ha llegado a tiempo - " + dt));
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
			}
		}
	}
	private async void BotonPlegar_Clicked(object sender, EventArgs e)
		{
<<<<<<< HEAD
			var trabajador = presenciaContext.Workers.Where(x => x.User.Username == Username).Include(x => x.WorkGroup).FirstOrDefault();
			DbInsert.InsertSigning(new Signing(trabajador, dt, "Entrada"), presenciaContext);
			var TrabajadorEnTurno = presenciaContext.SignedWorkers.Where(x => x.Worker == trabajador).FirstOrDefault();
			DbDelete.DeleteSignedWorker(TrabajadorEnTurno, presenciaContext);
=======
			var trabajador = presenciaContext.Trabajador.Where(x => x.Usuario.Username == Username).Include(x => x.Equipo).FirstOrDefault();
			OperacionesDBContext.InsertaFichaje(new Fichajes(trabajador, dt, "Entrada"));
			var TrabajadorEnTurno = presenciaContext.TrabajadorEnTurno.Where(x => x.trabajador == trabajador).FirstOrDefault();
			OperacionesDBContext.BorraTrabajadorEnTurno(TrabajadorEnTurno, presenciaContext);
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
			
		
				//presenciaContext.SignedWorkers.Remove(SignedWorkers);
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
<<<<<<< HEAD
					DbInsert.InsertLog(new Log("Temprano", "El trabajador " + trabajador.CardNumber + " Ha salido " + operacion + "m antes. debido a " + motivo + " - " + dt), presenciaContext);
=======
					OperacionesDBContext.InsertaLog(new Log("Temprano", "El trabajador " + trabajador.NumeroTarjeta + " Ha salido " + operacion + "m antes. debido a " + motivo + " - " + dt));
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
				}
				if (dt > d.AddMinutes(5))
				{
					var operacion = (dt - d);
					string motivo = await DisplayPromptAsync("Usted Sale tarde.", "¿Cual es la razon?");
<<<<<<< HEAD
					DbInsert.InsertLog(new Log("Retraso", "El trabajador " + trabajador.CardNumber + " Ha salido " + operacion + "m tarde. - " + dt),presenciaContext);
				}
				if (dt > d && dt <= d.AddMinutes(5))
				{
					DbInsert.InsertLog(new Log("En hora", "El trabajador " + trabajador.CardNumber + " Ha llegado a tiempo - " + dt),presenciaContext);
=======
					OperacionesDBContext.InsertaLog(new Log("Retraso", "El trabajador " + trabajador.NumeroTarjeta + " Ha salido " + operacion + "m tarde. - " + dt));
				}
				if (dt > d && dt <= d.AddMinutes(5))
				{
					OperacionesDBContext.InsertaLog(new Log("En hora", "El trabajador " + trabajador.NumeroTarjeta + " Ha llegado a tiempo - " + dt));
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
				}
			}
	}
	private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = SelectorTareas.SelectedIndex;
			var tareaActual = SelectorTareas.SelectedItem.ToString();
			var tarea = presenciaContext.WorkTasks.Where(x => x.Name == tareaActual).FirstOrDefault();
			var TareaIniciada = presenciaContext.StartedTasks.Where(x => x.Task.Name == tarea.Name && x.TastStart.Date == dt.Date).OrderBy(x => x.TastStart).LastOrDefault();

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
				var tarea = presenciaContext.WorkTasks.Where(x => x.Name == tareaActual).FirstOrDefault();
				presenciaContext.Add(new StartedTask(tarea, worker, dt));
				DbInsert.InsertLog(new Log("Tareas", Username + " ha iniciado tarea " + tarea.Name + " - " + dt),presenciaContext);
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
			var tarea = presenciaContext.WorkTasks.Where(x => x.Name == tareaActual).FirstOrDefault();
			var TareaIniciada = presenciaContext.StartedTasks.Where(x => x.Task.Name == tarea.Name).Where(x => x.TastStart.Date == dt.Date).OrderBy(x => x.TastStart).Last();
			var HorasUsadas = (DateTime.Now - TareaIniciada.TastStart).TotalHours;
			bool EnHora = false;
			if (HorasUsadas <= tarea.ElapsedTime)
			{
				EnHora = true;
			}

			presenciaContext.EndedTasks.Add(new EndedTask(tarea, worker, TareaIniciada.TastStart, dt, HorasUsadas, EnHora));
			presenciaContext.StartedTasks.Remove(TareaIniciada);
			DbInsert.InsertLog(new Log("Tareas", Username + " ha finalizado tarea " + tarea.Name + " - " + dt), presenciaContext);
			presenciaContext.SaveChanges();
		}
    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		Day item = e.SelectedItem as Day;
		_day = item;
	}
    private void BtnPedirVacaciones_Clicked(object sender, EventArgs e)
    {
		var trab = worker;
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
		var ExisteCalendario = presenciaContext.Calendars.Where(x => x.Worker == worker).Include(x => x.DaysOnCalendar).FirstOrDefault();
		if (ExisteCalendario is not null)
		{
			var ListaDias = ExisteCalendario.DaysOnCalendar.ToList();
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
    private List<string> DiasTrabajo(WorkShift t)
    {
		List<string> dias = new List<string>();
		if (t.Monday == true) { dias.Add("Monday"); }
		if (t.Tuesday == true) { dias.Add("Tuesday"); }
		if (t.Wednesday == true) { dias.Add("Wednesday"); }
		if (t.Thursday == true) { dias.Add("Thursday"); }
		if (t.Friday == true) { dias.Add("Friday"); }
		if (t.Saturday == true) { dias.Add("Saturday"); }
		if (t.Domingo == true) { dias.Add("Sunday"); }
		return dias;
	}
}