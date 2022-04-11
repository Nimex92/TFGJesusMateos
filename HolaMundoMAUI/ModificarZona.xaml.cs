using Bibliotec;
using Persistencia;

namespace HolaMundoMAUI;

public partial class ModificarZona : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	string NombreZona;
	public ModificarZona()
	{
		InitializeComponent();
		SetListView();
	}
	public void SetListView()
	{
		var Zonas = presenciaContext.Zonas.ToList();
		ListViewUsuarios.ItemsSource = Zonas;
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		LayoutCampos.IsEnabled = false;
		LayoutCampos.IsVisible = false;
		Zonas item = e.SelectedItem as Zonas;
		NombreZona = item.Nombre;
	}

	public void MostrarEditar(object sender, EventArgs e)
    {
		LayoutCampos.IsEnabled=true;
		LayoutCampos.IsVisible=true;
		var zona = presenciaContext.Zonas.Where(x => x.Nombre == NombreZona).FirstOrDefault();
		CampoNombre.Text = zona.Nombre;

	}
	public void VolverAlMain(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
    }
	public async void RegistrANuevaZona(object sender, EventArgs e)
	{
		bool inserta = OperacionesDBContext.ActualizaZona(NombreZona, CampoNombre.Text);
		if(inserta == true)
        {
			await DisplayAlert("Alert", "Se ha insertado nueva zona.", "OK");
			App.Current.MainPage = new NavigationPage(new ModificarZona());
		}
        else
        {
			await DisplayAlert("Alert", "Error al insertar.", "OK");
		}
	}
}