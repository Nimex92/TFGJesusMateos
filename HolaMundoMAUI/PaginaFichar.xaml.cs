
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
	DateTime dt = DateTime.Now;
	DateTime HoraEntrada;
	public PaginaFichar(string username)
    {
		InitializeComponent();

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
		var tareas = presenciaContext.Tareas;
		foreach (Tareas t in tareas)
		{
			SelectorTareas.Items.Add(t.NombreTarea);
		}
	}
    public PaginaFichar()
	{
		InitializeComponent();

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
		var tareas = presenciaContext.Tareas;
		foreach (Tareas t in tareas)
		{
			SelectorTareas.Items.Add(t.NombreTarea);
		}


	}


    public void irMainPage(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new MainPage());

	}
	private void BotonFichar_Clicked(object sender, EventArgs e)
	{
		var trabajador = presenciaContext.Trabajador.Where(x => x.usuario.Username == username).Include(x=>x.grupo).FirstOrDefault();
		OperacionesDBContext.insertaFichaje(trabajador.numero_tarjeta, trabajador.grupo.IdGrupo, "Entrada");
		BotonFichar.IsVisible = false;
		BotonFichar.IsEnabled = false;
		BotonPlegar.IsVisible = true;
		BotonPlegar.IsEnabled = true;
		SelectorTareas.IsEnabled = true;
		SelectorTareas.IsVisible = true;
	}
	private void BotonPlegar_Clicked(object sender, EventArgs e)
	{
		var trabajador = presenciaContext.Trabajador.Where(x => x.usuario.Username == username).Include(x => x.grupo).FirstOrDefault();
		OperacionesDBContext.insertaFichaje(trabajador.numero_tarjeta, trabajador.grupo.IdGrupo, "Salida");
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
	}
	void OnPickerSelectedIndexChanged(object sender, EventArgs e)
	{
		int selectedIndex = SelectorTareas.SelectedIndex;

		if (selectedIndex != -1)
		{
			BotonIniciarTarea.IsVisible = true;
			BotonIniciarTarea.IsEnabled = true;
		}
	}

    private void BotonIniciarTarea_Clicked(object sender, EventArgs e)
    {
		BotonAcabarTarea.IsEnabled = true;
		BotonAcabarTarea.IsVisible = true;
		BotonIniciarTarea.IsEnabled = false;
		BotonIniciarTarea.IsVisible = false;

		HoraEntrada = DateTime.Now;
	}

    private void BotonAcabarTarea_Clicked(object sender, EventArgs e)
    {
		BotonAcabarTarea.IsEnabled = false;
		BotonAcabarTarea.IsVisible = false;
		BotonIniciarTarea.IsEnabled = true;
		BotonIniciarTarea.IsVisible = true;
		var tareaActual = SelectorTareas.SelectedItem.ToString();
		var tarea = presenciaContext.Tareas.Where(x=>x.NombreTarea == tareaActual).FirstOrDefault();
		var grupo = presenciaContext.Grupo_Trabajo.Find(trabajador.grupo.IdGrupo);
		OperacionesDBContext.insertaTareaRealizada(tarea.NombreTarea,trabajador.numero_tarjeta, grupo.IdGrupo ,HoraEntrada,DateTime.Now);
		
	}


}

