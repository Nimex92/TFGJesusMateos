using Bibliotec;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class EliminaUsuario : ContentPage
{
	Usuarios us = new();
	public EliminaUsuario()
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
		PresenciaContext presenciaContext = new PresenciaContext();
		var users = presenciaContext.Usuarios.ToList();
		ListViewUsuarios.ItemsSource = users;
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		Usuarios item = e.SelectedItem as Usuarios;
		us = item;
	}
	public async void BorrarUsuario(object sender, EventArgs e)
    {
		bool answer = await DisplayAlert("Question?", "¿Estas seguro?", "Si", "No");
		var IdUser = us.IdUser;
        if (answer ==true) 
		{
			bool inserta = OperacionesDBContext.borraUsuario(IdUser);
			if (inserta == true)
			{
				await Task.Delay(1000);
				App.Current.MainPage = new NavigationPage(new EliminaUsuario());
			}
			else
			{
				await DisplayAlert("Alert","Debe seleccionar un usuario","OK");
			}
        }
        else
        {
			//
        }

	}


}