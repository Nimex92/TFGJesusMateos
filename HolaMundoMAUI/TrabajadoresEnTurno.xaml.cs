using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace HolaMundoMAUI;


public partial class TrabajadoresEnTurno : ContentPage
{
	PresenciaContext p = new PresenciaContext();
	string Username;

	public TrabajadoresEnTurno(string username)
	{
		InitializeComponent();
		SetListView();
		Username = username;
	}
	private void actualiza()
	{
		App.Current.MainPage = new NavigationPage(new TrabajadoresEnTurno(Username));
	}
	public void SetListView()
	{
		var signedWorkers = p.SignedWorkers.Include(x => x.Worker).Include(x=>x.Worker.WorkGroup).Include(x=>x.Worker.User).ToList();
		ListViewUsuarios.ItemsSource = signedWorkers;
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		
	}

    private void BotonVolver_Clicked(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username));
    }
}