using Bibliotec;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class EliminarGrupoTrabajo
{
	bool activado;
	Grupo_Trabajo gr = new();
	PresenciaContext presenciaContext = new PresenciaContext();
	string NombreUsuario;
	DateTime dt = DateTime.Now;
	public EliminarGrupoTrabajo(string user)
	{
		InitializeComponent();
		activado = false;
		SetListView();
		NombreUsuario = user;
	}
	public void VolverAlMain(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario));
	}
	public void SetListView()
    {
		var GrupoTrabajo = presenciaContext.Grupo_Trabajo.ToList();
		ListViewUsuarios.ItemsSource = GrupoTrabajo;
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		Grupo_Trabajo item = e.SelectedItem as Grupo_Trabajo;
		gr = item;

	}
	public async void BorrarGrupoTrabajo(object sender, EventArgs e)
    {
		OperacionesDBContext.borraGrupoTrabajo(gr.IdGrupo);
		presenciaContext.Logs.Add(new Log("Eliminar", NombreUsuario + " ha eliminado grupo de trabajo " + gr.Turno + " - " + dt));
		presenciaContext.SaveChanges();
		await Task.Delay(1000);
		App.Current.MainPage = new NavigationPage(new EliminarGrupoTrabajo(NombreUsuario));
	}
}