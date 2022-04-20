using Persistencia;
using Bibliotec;

namespace HolaMundoMAUI;

public partial class PaginaAdmin : ContentPage
{
    private string nombreUsuario;
    PresenciaContext presenciaContext = new PresenciaContext();
    DateTime dt = DateTime.Now;
    public PaginaAdmin(string nombreUsuario)
    {
        InitializeComponent();
        this.nombreUsuario = nombreUsuario;
        Label_NameUser.Text = "Bienvenid@ "+nombreUsuario;
    }
    public void NuevoUsuario(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaUsuarios(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Registrar usuario' - " + dt));
        presenciaContext.SaveChanges();
    }
    public void NuevoTrabajador(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTrabajador(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Registrar trabajador' - " + dt));
        presenciaContext.SaveChanges();
    }
    public void NuevoGrupoTrabajo(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaGrupoTrabajo(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Registrar grupo de trabajo' - " + dt));
        presenciaContext.SaveChanges();
    }
    public void ModificaUsuario(object sender , EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new ModificarUsuario(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Modificar usuario' - " + dt));
        presenciaContext.SaveChanges();
    }
    public void ModificaTrabajador(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new ModificarTrabajador(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Moridicar trabajador' - " + dt));
        presenciaContext.SaveChanges();
    }
    public void ModificaGrupoTrabajo(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new ModificarGrupoTrabajo(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Modificar grupo de trabajo' - " + dt));
        presenciaContext.SaveChanges();
    }
    public void EliminaUsuario(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new EliminaUsuario(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Eliminar usuario' - " + dt));
        presenciaContext.SaveChanges();
    }
    public void EliminarTrabajador(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new EliminarTrabajador(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Eliminar trabajador' - " + dt));
        presenciaContext.SaveChanges();
    }
    public void EliminarGrupoTrabajador(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new EliminarGrupoTrabajo(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Eliminar grupo de trabajo' - " + dt));
        presenciaContext.SaveChanges();
    }
    public async void VolverAlMainAdmin(object sender, EventArgs e)
    {
        await DisplayAlert("Alert", "Hasta luego, "+nombreUsuario, "OK");
        App.Current.MainPage = new NavigationPage(new MainPage());
        presenciaContext.Logs.Add(new Log("Logout", nombreUsuario + " ha cerrado sesion -"+dt ));
        presenciaContext.SaveChanges();

    }

    private void RegistrarNuevaTareaTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Añadir tareas de trabajo' - " + dt));
        presenciaContext.SaveChanges();
    }
    private void ModificarTareaTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new ModificarTareas(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Modificar tareas de trabajo' - " + dt));
        presenciaContext.SaveChanges();
    }
    private void EliminaTareaTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new BorraTareas(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Borrar tareas de trabajo' - " + dt));
        presenciaContext.SaveChanges();
    }
    private void RegistrarNuevaZona_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Añadir zona de trabajo' - " + dt));
        presenciaContext.SaveChanges();
    }
    private void ModificarZona_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new ModificarZona(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Modificar zona de trabajo' - " + dt));
        presenciaContext.SaveChanges();
    }
    private void EliminaZona_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new BorraZonas(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Borrar zona de trabajo' - " + dt));
        presenciaContext.SaveChanges();
    }
    private void AnadeTareasGrupo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeTareasGrupoTrabajo(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Añadir Tareas a grupo' - " + dt));
        presenciaContext.SaveChanges();
    }
    private void AnadirZonaGrupo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirZonaGrupoTrabajo(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Añadir zona de trabajo' - " + dt));
        presenciaContext.SaveChanges();
    }
    private void BotonSinNada_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new TrabajadoresEnTurno(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Trabajadores en turno' - " + dt));
        presenciaContext.SaveChanges();
    }

    private void Logout_png_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
}

