using Bibliotec;
using Persistencia;

namespace HolaMundoMAUI;

public partial class BorraTareas : ContentPage
{
	string NombreTarea;
	PresenciaContext presenciaContext = new PresenciaContext();
	string NombreUsuario;
	DateTime dt = DateTime.Now;
	public BorraTareas(string user)
	{
		InitializeComponent();
		SetListView();
		NombreUsuario = user;
	}
	public void SetListView()
	{
		var Tareas = presenciaContext.Tareas.ToList();
		ListViewUsuarios.ItemsSource = Tareas;
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		Tareas item = e.SelectedItem as Tareas;
		NombreTarea = item.NombreTarea;
	}

	private async void BotonBorrar_Clicked(object sender, EventArgs e)
    {
		
		var tarea = presenciaContext.Tareas.Where(x=>x.NombreTarea == NombreTarea).FirstOrDefault();
		bool answer = await DisplayAlert("Question?", "¿Estas seguro de borrar la tarea " + tarea.NombreTarea + "\"?", "Si", "No");
		presenciaContext.Remove(tarea);
		presenciaContext.Logs.Add(new Log("Eliminar", NombreUsuario + " ha eliminado tarea " + tarea.NombreTarea + " - " + dt));
		presenciaContext.SaveChanges();
		App.Current.MainPage = new NavigationPage(new BorraTareas(NombreUsuario));
	}
	private void VolverAlMain(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario));
	}

}