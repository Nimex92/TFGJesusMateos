using Persistencia;
using Bibliotec;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class AltaGrupoTrabajo : ContentPage
{
	string NombreUsuario;
	DateTime dt = DateTime.Now;
	public AltaGrupoTrabajo(string user)
	{
		InitializeComponent();
		
		SelectorHoraEntrada.ItemsSource = GeneraHoras();
		SelectorHoraSalida.ItemsSource = GeneraHoras();
		SelectorMinutoEntrada.ItemsSource = GeneraMinutos();
		SelectorMinutoSalida.ItemsSource = GeneraMinutos();
		SelectorHoraEntrada.SelectedIndex = 0;
		SelectorMinutoEntrada.SelectedIndex = 0;
		SelectorHoraSalida.SelectedIndex = 0;
		SelectorMinutoSalida.SelectedIndex = 0;
	}
	public async void RegistraNuevoGrupoTrabajo(object sender , EventArgs e)
    {
		PresenciaContext presenciaContext = new PresenciaContext();
		string NombreGrupo = CampoNombre.Text;
		if(NombreGrupo is not null)
        {
			string HoraEntrada = SelectorHoraEntrada.SelectedItem.ToString();
			string MinutoEntrada = SelectorMinutoEntrada.SelectedItem.ToString();
			string TiempoEntrada = HoraEntrada + ":" + MinutoEntrada;
			string HoraSalida = SelectorHoraSalida.SelectedItem.ToString();
			string MinutoSalida = SelectorMinutoSalida.SelectedItem.ToString();
			string TiempoSalida = HoraSalida + ":" + MinutoSalida;

			var GrupoExiste = presenciaContext.Grupo_Trabajo.Where(x => x.Turno == NombreGrupo).FirstOrDefault();
			if (GrupoExiste is not null)
			{
				await DisplayAlert("Alert", "El grupo de trabajo ya existe.", "OK");
			}
			else
			{
				OperacionesDBContext.insertarGrupoTrabajo(NombreGrupo, TiempoEntrada, TiempoSalida);
				presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha añadido grupo de trabajo " + NombreGrupo+" - "+dt));
				presenciaContext.SaveChanges();
				await DisplayAlert("Alert", "El grupo de trabajo se ha añadido correctamente.", "OK");
			}
        }
        else
        {
			await DisplayAlert("Alert", "Debes introducir el nombre de turno", "OK");
        }
		

	}
	public List<string> GeneraHoras()
    {
		var ListaHoras = new List<string>();
		for (int i = 00; i < 24; i++)
		{
			if (i < 10)
			{
				ListaHoras.Add("0" + i.ToString());
			}
			else
			{
				ListaHoras.Add(i.ToString());
			}
		}
		return ListaHoras;
	}
	public List<string> GeneraMinutos()
	{
		var ListaMinutos = new List<string>();
		for (int i = 00; i < 60; i++)
		{
			if (i < 10)
			{
				ListaMinutos.Add("0" + i.ToString());
			}
			else
			{
				ListaMinutos.Add(i.ToString());
			}
		}
		return ListaMinutos;
	}

	public void VolverAlMain(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario));
    }
}