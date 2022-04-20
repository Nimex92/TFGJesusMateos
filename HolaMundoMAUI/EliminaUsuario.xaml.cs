using Bibliotec;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class EliminaUsuario : ContentPage
{
	Usuarios us = new();
	string NombreUsuario;
	DateTime dt = DateTime.Now;
	PresenciaContext presenciaContext = new PresenciaContext();
	public EliminaUsuario(string user)
	{
		InitializeComponent();
		SetListView();
		NombreUsuario = user;

	}
	public void VolverAlMain(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario));
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
			presenciaContext.Logs.Add(new Log("Eliminar", NombreUsuario + " ha eliminado tarea " + us.Username + " - " + dt));
			presenciaContext.SaveChanges();
			if (inserta == true)
			{
				await Task.Delay(1000);
				App.Current.MainPage = new NavigationPage(new EliminaUsuario(NombreUsuario));
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