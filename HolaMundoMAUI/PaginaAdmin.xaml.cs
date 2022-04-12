namespace HolaMundoMAUI;

public partial class PaginaAdmin : ContentPage
{
    private string nombreUsuario;

    public PaginaAdmin()
    {
        InitializeComponent();
    }

    public PaginaAdmin(string nombreUsuario)
    {
        InitializeComponent();
        this.nombreUsuario = nombreUsuario;
    }
    public void NuevoUsuario(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaUsuarios());
    }
    public void NuevoTrabajador(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTrabajador());
    }
    public void NuevoGrupoTrabajo(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaGrupoTrabajo());
    }
    public void ModificaUsuario(object sender , EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new ModificarUsuario());
    }

    public void ModificaTrabajador(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new ModificarTrabajador());
    }

    public void ModificaGrupoTrabajo(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new ModificarGrupoTrabajo());
    }

    public void EliminaUsuario(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new EliminaUsuario());
    }

    public void EliminarTrabajador(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new EliminarTrabajador());
    }
    public void EliminarGrupoTrabajador(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new EliminarGrupoTrabajo());
    }

    public async void VolverAlMainAdmin(object sender, EventArgs e)
    {
        await DisplayAlert("Alert", "Hasta luego, "+nombreUsuario, "OK");
        App.Current.MainPage = new NavigationPage(new MainPage());
       
    }

    private void RegistrarNuevaTareaTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo());
    }

    private void ModificarTareaTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new ModificarTareas());
    }

    private void EliminaTareaTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new BorraTareas());
    }

    private void RegistrarNuevaZona_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona());
    }

    private void ModificarZona_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new ModificarZona());
    }

    private void EliminaZona_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new BorraZonas());
    }

    private void AnadeTareasGrupo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeTareasGrupoTrabajo());
    }

    private void AnadirZonaGrupo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirZonaGrupoTrabajo());
    }
}

