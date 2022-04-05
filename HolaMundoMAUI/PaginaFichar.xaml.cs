
using Microsoft.Maui.Dispatching;
using System.Diagnostics;
using Persistencia;
using ClassLibrary1;
using Microsoft.EntityFrameworkCore;

namespace HolaMundoMAUI;

public partial class PaginaFichar : ContentPage
{
	bool FicharActivado=false;
	string username;
    public IDispatcherTimer MyTimer { get; set; }
	PresenciaContext presenciaContext = new PresenciaContext();
	public PaginaFichar(string username)
    {
		InitializeComponent();

		this.username = username;
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
		
	}


    public void irMainPage(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new MainPage());

	}
	private void ComienzaJornada(object sendeer, EventArgs e)
    {
		if (FicharActivado == false)
		{
			FicharActivado = true;
			BotonFichar.Background = new SolidColorBrush(Colors.Green);
			//var query = presenciaContext.Trabajador //(REFERENCIA A LA TABLA) SELECT DE SQL (Lista de trabajadores)
			//	.Where(x => x.numero_tarjeta == user) // WHERE DE SQL (Argumentos)
			//	.Include(x => x.grupo)				//Lo que quieres incluir (JOIN DE SQL)
			//	.OrderBy(x => x.numero_tarjeta);	// ORDER BY DE SQL
			//	//.Select(x=>x.numero_tarjeta);		// CON LO QUE ME QUIERO QUEDAR EN LA VARIABLE
			//var trabajador = query.First();			//(Accionar la consulta)Como darle al enter en la consola de mysql
			var trabajador = presenciaContext.Trabajador.Where(x => x.usuario.Username == username).Include(x=>x.grupo).FirstOrDefault();
			OperacionesDBContext.insertaFichaje(trabajador.numero_tarjeta, trabajador.grupo.IdGrupo, "Entrada");

		}
		else
        {
			FicharActivado = false;
			BotonFichar.Background = new SolidColorBrush(Colors.Red);
			var trabajador = presenciaContext.Trabajador.Where(x=>x.usuario.Username == username).FirstOrDefault();
			OperacionesDBContext.insertaFichaje(trabajador.numero_tarjeta, trabajador.grupo.IdGrupo, "Salida");
			
		}
	}
}

