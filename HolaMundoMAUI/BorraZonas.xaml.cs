using Persistencia;
using Bibliotec;

namespace HolaMundoMAUI;

public partial class BorraZonas : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	Zonas zone;
	string NombreUsuario;
	DateTime dt = DateTime.Now;
	public BorraZonas(string user)
	{
		InitializeComponent();
		SetListView();
		NombreUsuario = user;
	}
	public void SetListView()
	{
		var Zonas = presenciaContext.Zonas.ToList();
		ListViewUsuarios.ItemsSource = Zonas;
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		Zonas item = e.SelectedItem as Zonas;
		zone = item;
	}
	public void VolverAlMain(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario));
	}
	private async void BotonBorrar_Clicked(object sender, EventArgs e)
    {
		if(zone is not null) { 
		bool answer = await DisplayAlert("Question?", "¿Desea Borrar la zona "+zone.Nombre+"?", "Si", "No");
        if (answer==true)
        {
			await DisplayAlert("Alert", "Se ha borrado correctamente " + zone.Nombre, "OK");
			presenciaContext.Logs.Add(new Log("Eliminar", NombreUsuario + " ha eliminar " + zone.Nombre + " - " + dt));
			presenciaContext.SaveChanges();
			OperacionesDBContext.BorraZona(zone.IdZona);
			App.Current.MainPage = new NavigationPage(new BorraZonas(NombreUsuario));
			
        }
        else
        {
			await DisplayAlert("Alert", "No se han realizado cambios ", "OK");
		}
        }
        else
        {
			await DisplayAlert("Alert", "Por favor, Seleccione un elemento primero", "OK");
        }
	}
}