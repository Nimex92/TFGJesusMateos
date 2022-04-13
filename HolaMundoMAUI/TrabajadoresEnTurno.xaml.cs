using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace HolaMundoMAUI;


public partial class TrabajadoresEnTurno : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	public TrabajadoresEnTurno()
	{
		InitializeComponent();
		SetListView();
	}
	private static void actualiza()
	{
		App.Current.MainPage = new NavigationPage(new TrabajadoresEnTurno());
	}
	public void SetListView()
	{
		var trabajador = presenciaContext.TrabajadorEnTurno.Include(x => x.trabajador).Include(x=>x.trabajador.grupo).Include(x=>x.trabajador.usuario).ToList();
		ListViewUsuarios.ItemsSource = trabajador;
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		//Trabajador item = e.SelectedItem as Trabajador;
		//tr = item;
	}

    private void BotonVolver_Clicked(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
		actualiza = false;
    }
}