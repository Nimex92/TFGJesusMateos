using Persistencia;
using Bibliotec;

namespace HolaMundoMAUI;

public partial class PaginaAdmin : ContentPage
{
    private string nombreUsuario;
    PresenciaContext presenciaContext = new PresenciaContext();
    DateTime dt = DateTime.Now;
    Usuarios us;
    Boolean UsActivo;
    public PaginaAdmin(string nombreUsuario)
    {
        UsActivo = false;
        InitializeComponent();
        SetListViewUsuarios();
        this.nombreUsuario = nombreUsuario;
        Label_NameUser.Text = "Bienvenid@, " + Environment.NewLine + nombreUsuario;
    }
    public PaginaAdmin(string nombreUsuario,int interfaz)
    {
        UsActivo = false;
        InitializeComponent();
        SetListViewUsuarios();
        this.nombreUsuario = nombreUsuario;
        Label_NameUser.Text = "Bienvenid@,"+ Environment.NewLine +""+ nombreUsuario;
        switch (interfaz)
        {
            case 1:
                ListViewUsers.IsVisible = true;
                ListViewUsers.IsEnabled = true;
                UsActivo = true;
                LabelTitulo.Text = "Tech Talent" + Environment.NewLine + "Página de administracion";
                break;
        }
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

    private void BtnAñadir_Clicked(object sender, EventArgs e)
    {
        GrupoBotonesPrincipal.IsVisible = false;
        GrupoBotonesPrincipal.IsEnabled = false;
        GrupoAnadir.IsEnabled = true;
        GrupoAnadir.IsVisible = true;
        GrupoModificar.IsEnabled = false;
        GrupoModificar.IsVisible = false;
        GrupoEliminar.IsEnabled = false;
        GrupoEliminar.IsVisible = false;
        GrupoAlterno.IsEnabled = false;
        GrupoAlterno.IsVisible = false;
    }
    private void BtnModificar_Clicked(object sender, EventArgs e)
    {
        GrupoBotonesPrincipal.IsVisible = false;
        GrupoBotonesPrincipal.IsEnabled = false;
        GrupoAnadir.IsEnabled = false;
        GrupoAnadir.IsVisible = false;
        GrupoModificar.IsEnabled = true;
        GrupoModificar.IsVisible = true;
        GrupoEliminar.IsEnabled = false;
        GrupoEliminar.IsVisible = false;
        GrupoAlterno.IsEnabled = false;
        GrupoAlterno.IsVisible = false;
    }
    private void BtnEliminar_Clicked(object sender, EventArgs e)
    {
        GrupoBotonesPrincipal.IsVisible = false;
        GrupoBotonesPrincipal.IsEnabled = false;
        GrupoAnadir.IsEnabled = false;
        GrupoAnadir.IsVisible = false;
        GrupoModificar.IsEnabled = false;
        GrupoModificar.IsVisible = false;
        GrupoEliminar.IsEnabled = true;
        GrupoEliminar.IsVisible = true;
        GrupoAlterno.IsEnabled = false;
        GrupoAlterno.IsVisible = false;
    }
    private void VolverGrupoBotonesPrincipales_Clicked(object sender, EventArgs e)
    {
        GrupoBotonesPrincipal.IsVisible = true;
        GrupoBotonesPrincipal.IsEnabled = true;
        GrupoAnadir.IsEnabled = false;
        GrupoAnadir.IsVisible = false;
        GrupoModificar.IsEnabled = false;
        GrupoModificar.IsVisible = false;
        GrupoEliminar.IsEnabled = false;
        GrupoEliminar.IsVisible = false;
        GrupoAlterno.IsEnabled = false;
        GrupoAlterno.IsVisible = false;
    }
    private void BtnAlterno_Clicked(object sender, EventArgs e)
    {
        GrupoBotonesPrincipal.IsVisible = false;
        GrupoBotonesPrincipal.IsEnabled = false;
        GrupoAnadir.IsEnabled = false;
        GrupoAnadir.IsVisible = false;
        GrupoModificar.IsEnabled = false;
        GrupoModificar.IsVisible = false;
        GrupoEliminar.IsEnabled = false;
        GrupoEliminar.IsVisible = false;
        GrupoAlterno.IsEnabled = true;
        GrupoAlterno.IsVisible = true;
    }
    public void SetListViewUsuarios()
    {
        PresenciaContext presenciaContext = new PresenciaContext();
        var users = presenciaContext.Usuarios.ToList();
        ListViewUsuarios.ItemsSource = users;
    }
    public void OnItemSelectedUsuarios(object sender, SelectedItemChangedEventArgs e)
    {
        Usuarios item = e.SelectedItem as Usuarios;
        us = item;
    }
    private void BtnUsuarios_Clicked(object sender, EventArgs e)
    {
        if (UsActivo == false)
        {
            ListViewUsers.IsVisible = true;
            
            UsActivo = true;
            LabelTitulo.Text="Tech Talent"+ Environment.NewLine+"Página de administracion";

        }
        else
        {
            ListViewUsers.IsVisible = false;
            
            UsActivo = false;
            LabelTitulo.Text = "";
        }
    }

    private async void BotonBorrarUsuarios_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Question?", "¿Estas seguro?", "Si", "No");
        var IdUser = us.IdUser; 
        
        
        if (answer == true)
        {
            bool inserta = OperacionesDBContext.borraUsuario(IdUser);
            presenciaContext.Logs.Add(new Log("Eliminar", nombreUsuario + " ha eliminado tarea " + us.Username + " - " + dt));
            presenciaContext.SaveChanges();
            if (inserta == true)
            {
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario,1));
            }
            else
            {
                await DisplayAlert("Alert", "Debe seleccionar un usuario", "OK");
            }
        }
    }

    private void BotonEditarUsuarios_Clicked(object sender, EventArgs e)
    {

    }
}

