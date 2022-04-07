using Bibliotec;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class EliminarTrabajador
{

	Trabajador tr = new();
	PresenciaContext presenciaContext = new PresenciaContext();
	public EliminarTrabajador()
	{
		InitializeComponent();
		SetListView();
	}
	public void VolverAlMain(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
	}
	public void SetListView()
	{
		var trabajador = presenciaContext.Trabajador.Include(x => x.grupo).Include(x => x.usuario).ToList();
		ListViewUsuarios.ItemsSource = trabajador;
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		Trabajador item = e.SelectedItem as Trabajador;
		tr = item;
	}
	public async void BorrarTrabajador(object sender, EventArgs e)
	{
		if(tr is not null) { 
		var NumeroTarjeta = tr.numero_tarjeta;
		OperacionesDBContext.borraTrabajador(NumeroTarjeta);
		await Task.Delay(1000);
		App.Current.MainPage = new NavigationPage(new EliminarTrabajador());
		}
        else
        {
			LabelPruebas.Text = "ERROR!";
			LabelPruebas.TextColor = Colors.Red;
        }
	}
}
