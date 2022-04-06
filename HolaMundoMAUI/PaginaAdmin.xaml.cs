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

    public void VolverAlMainAdmin(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
}

