using Bibliotec;
using Persistencia;

namespace HolaMundoMAUI;

public partial class BorraTareas : ContentPage
{
	string NombreTarea;
	PresenciaContext presenciaContext = new PresenciaContext();
	public BorraTareas()
	{
		InitializeComponent();
		SetListView();
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

	private void BotonEditar_Clicked(object sender, EventArgs e)
    {
		var tarea = presenciaContext.Tareas.Where(x=>x.NombreTarea == NombreTarea).FirstOrDefault();
		presenciaContext.Remove(tarea);
		presenciaContext.SaveChanges();
		App.Current.MainPage = new NavigationPage(new BorraTareas());
	}
	private void VolverAlMain(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
	}

}