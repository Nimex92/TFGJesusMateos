using Persistencia;
using Bibliotec;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class PaginaAdmin : ContentPage
{
    private string nombreUsuario;
    private string PerteneceATurnos;
    PresenciaContext presenciaContext = new PresenciaContext();
    DateTime dt = DateTime.Now;
    Usuarios us;
    Trabajador tr;
    Turno gr;
    Zonas zn;
    Tareas ta;
    EquipoTrabajo Et;
    Boolean UsActivo,TrActivo,GrActivo,ZnActivo,TaActivo,EtActivo;

    public PaginaAdmin(string nombreUsuario)
    {
        UsActivo = false;
        TrActivo = false;
        GrActivo = false;
        ZnActivo = false;
        TaActivo = false;
        EtActivo = false;
        
        InitializeComponent();
        SetListViewUsuarios();
        SetListViewTrabajadores();
        SetListViewGruposTrabajo();
        SetListViewTareas();
        SetListViewZonas();
        SetListViewEquiposTrabajo();

        this.nombreUsuario = nombreUsuario;
        Label_NameUser.Text = "Bienvenid@, " + Environment.NewLine + nombreUsuario;
    }
    public PaginaAdmin(string nombreUsuario,int interfaz)
    {
        UsActivo = false;
        TrActivo = false;
        GrActivo = false;
        ZnActivo = false;
        TaActivo = false;

        InitializeComponent();
        SetListViewUsuarios();
        SetListViewTrabajadores();
        SetListViewGruposTrabajo();
        SetListViewZonas();
        SetListViewTareas();
        SetListViewEquiposTrabajo();
        this.nombreUsuario = nombreUsuario;
        Label_NameUser.Text = "Bienvenid@,"+ Environment.NewLine +""+ nombreUsuario;
        switch (interfaz)
        {
            case 1:
                ListViewUsers.IsVisible = true;
                ListViewUsers.IsEnabled = true;
                UsActivo = true;
                LabelTitulo.Text = "Tech Talent" + Environment.NewLine + "gestíon de usuarios";
                break;
            case 2:
                ListViewWorkers.IsVisible = true;
                ListViewWorkers.IsEnabled = true;
                TrActivo = true;
                LabelTitulo2.Text = "Tech Talent" + Environment.NewLine + "gestión de trabajadores";
                break;
            case 3:
                ListViewGroups.IsVisible = true;
                ListViewGroups.IsEnabled = true;
                GrActivo = true;
                LabelTitulo3.Text = "Tech Talent" + Environment.NewLine + "gestión de equipos de trabajo";
                break;
            case 4:
                ListViewZones.IsVisible = true;
                ListViewZones.IsEnabled = true;
                ZnActivo = true;
                LabelTitulo4.Text = "Tech Talent" + Environment.NewLine + "gestión de Zonas de trabajo";
                break;
            case 5:
                ListViewTasks.IsVisible = true;
                ListViewTasks.IsEnabled = true;
                TaActivo = true;
                LabelTitulo5.Text = "Tech Talent" + Environment.NewLine + "gestión de tareas";
                break;
            case 6:
                ListViewTeams.IsVisible = true;
                ListViewTeams.IsEnabled = true;
                TaActivo = true;
                LabelTitulo5.Text = "Tech Talent" + Environment.NewLine + "gestión de turnos de trabajo";
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
        App.Current.MainPage = new NavigationPage(new AltaTrabajador(nombreUsuario,0));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Registrar trabajador' - " + dt));
        presenciaContext.SaveChanges();
    }
    public void NuevoGrupoTrabajo(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTurno(nombreUsuario,0));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Registrar grupo de trabajo' - " + dt));
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
    private void RegistrarNuevaZona_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Añadir zona de trabajo' - " + dt));
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
    public void SetListViewUsuarios()
    {
        PresenciaContext presenciaContext = new PresenciaContext();
        var users = presenciaContext.Usuarios.ToList();
        ListViewUsuarios.ItemsSource = users;
        if(users.Count > 0)
        ListViewUsuarios.SelectedItem = users[0];
    }
    public void SetListViewGruposTrabajo()
    {
        PresenciaContext presenciaContext = new PresenciaContext();
        var turnos = presenciaContext.Turno.ToList();
        ListViewGrupos.ItemsSource = turnos;
        if(turnos.Count > 0)
        ListViewGrupos.SelectedItem = turnos[0];
    }
    public void SetListViewTrabajadores()
    {
        PresenciaContext p = new PresenciaContext();
        var workers = p.Trabajador.Include(x=>x.usuario).ToList();
        ListViewTrabajadores.ItemsSource = workers;
        if(workers.Count > 0)
        ListViewTrabajadores.SelectedItem = workers[0];
    }
    public void SetListViewZonas()
    {
        PresenciaContext presenciaContext = new PresenciaContext();
        var zones = presenciaContext.Zonas.ToList();
        ListViewZonas.ItemsSource = zones;
        if(zones.Count > 0)
        ListViewZonas.SelectedItem = zones[0];
    }
    public void SetListViewTareas()
    {
        PresenciaContext presenciaContext = new PresenciaContext();
        var tareas = presenciaContext.Tareas.ToList();
        ListViewTareas.ItemsSource = tareas;
        if (tareas.Count > 0) 
        { 
            ListViewTareas.SelectedItem = tareas[0];
        }
    }
    public void SetListViewEquiposTrabajo()
    {
        PresenciaContext presenciaContext = new PresenciaContext();
        var equipos = presenciaContext.EquipoTrabajo.ToList();
        ListViewEquipos.ItemsSource = equipos;
        if (equipos.Count > 0)
        {
            ListViewEquipos.SelectedItem = equipos[0];
        }
    }
    public void OnItemSelectedUsuarios(object sender, SelectedItemChangedEventArgs e)
    {
        Usuarios item = e.SelectedItem as Usuarios;
        us = item;
    }
    public void OnItemSelectedTrabajadores(object sender, SelectedItemChangedEventArgs e)
    {
        Trabajador item = e.SelectedItem as Trabajador;
        tr = item;
    }
    public void OnItemSelectedGruposTrabajo(object sender, SelectedItemChangedEventArgs e)
    {
        Turno item = e.SelectedItem as Turno;
        gr = item;
    }
    public void OnItemSelectedEquiposTrabajo(object sender, SelectedItemChangedEventArgs e)
    {
        EquipoTrabajo item = e.SelectedItem as EquipoTrabajo;
        Et = item;
    }
    public void OnItemSelectedZonas(object sender, SelectedItemChangedEventArgs e)
    {
        Zonas item = e.SelectedItem as Zonas;
        zn = item;
    }
    public void OnItemSelectedTareas(object sender, SelectedItemChangedEventArgs e)
    {
        Tareas item = e.SelectedItem as Tareas;
        ta = item;
    }
    private void BtnUsuarios_Clicked(object sender, EventArgs e)
    {
        if (UsActivo == false)
        {
            ListViewUsers.IsVisible = true;
            ListViewUsers.IsEnabled = true;
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;

            BotonUser.BackgroundColor = Color.FromRgba("#ffa73b");
            BotonTrabajador.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTareas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonZonas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");

            UsActivo = true;
            TrActivo = false;
            GrActivo = false;
            ZnActivo = false;
            TaActivo = false;
            EtActivo = false;

            LabelTitulo.Text="Tech Talent"+ Environment.NewLine+ "gestión de usuarios";
        }
        else
        {
            ListViewUsers.IsVisible = false;
            ListViewUsers.IsEnabled = false;
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;


            BotonUser.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTrabajador.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTareas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonZonas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");


            UsActivo = false;
            TrActivo = false;
            GrActivo = false;
            ZnActivo = false;
            TaActivo = false;
            EtActivo = false;


            LabelTitulo.Text = "";
        }
    }
    private void BtnTrabajadores_Clicked(object sender, EventArgs e)
    {
        if (TrActivo == false)
        {
            ListViewWorkers.IsVisible = true;
            ListViewWorkers.IsEnabled = true;
            ListViewUsers.IsVisible = false;
            ListViewUsers.IsEnabled = false;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;


            BotonUser.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTrabajador.BackgroundColor = Color.FromRgba("#ffa73b");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTareas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonZonas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");


            TrActivo = true;
            UsActivo = false;
            GrActivo = false;
            ZnActivo = false;
            TaActivo = false;
            EtActivo = false;

            LabelTitulo.Text = "Tech Talent" + Environment.NewLine + "gestión de trabajadores";
        }
        else
        {
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewUsers.IsVisible = false;
            ListViewUsers.IsEnabled = false;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;

            BotonUser.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTrabajador.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTareas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonZonas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");


            TrActivo = false;
            UsActivo = false;
            GrActivo = false;
            ZnActivo = false;
            TaActivo = false;
            EtActivo = false;

            LabelTitulo.Text = "";
        }
    }
    private void BtnGruposTrabajo_Clicked(object sender, EventArgs e)
    {
        if (GrActivo == false)
        {
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewUsers.IsVisible = false;
            ListViewUsers.IsEnabled = false;
            ListViewGroups.IsVisible = true;
            ListViewGroups.IsEnabled = true;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;

            BotonUser.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTrabajador.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#ffa73b");
            BotonTareas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonZonas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");


            GrActivo = true;
            UsActivo = false;
            TrActivo = false;
            TaActivo = false;
            ZnActivo = false;
            EtActivo = false;

            LabelTitulo.Text = "Tech Talent" + Environment.NewLine + "gestión de trabajadores";
        }
        else
        {
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewUsers.IsVisible = false;
            ListViewUsers.IsEnabled = false;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;

            BotonUser.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTrabajador.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTareas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonZonas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");


            GrActivo = false;
            TrActivo = false;
            UsActivo = false;
            TaActivo = false;
            ZnActivo = false;
            EtActivo = false;

            LabelTitulo.Text = "";
        }
    }
    private void BtnEquiposTrabajo_Clicked(object sender, EventArgs e)
    {
        if (EtActivo == false)
        {
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewUsers.IsVisible = false;
            ListViewUsers.IsEnabled = false;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = true;
            ListViewTeams.IsEnabled = true;

            BotonUser.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTrabajador.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTareas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonZonas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#ffa73b");


            GrActivo = false;
            UsActivo = false;
            TrActivo = false;
            TaActivo = false;
            ZnActivo = false;
            EtActivo = true;


            LabelTitulo.Text = "Tech Talent" + Environment.NewLine + "gestión de trabajadores";
        }
        else
        {
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewUsers.IsVisible = false;
            ListViewUsers.IsEnabled = false;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;

            BotonUser.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTrabajador.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTareas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonZonas.BackgroundColor = Color.FromRgba("#ffa73b");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");


            GrActivo = false;
            TrActivo = false;
            UsActivo = false;
            TaActivo = false;
            ZnActivo = false;
            EtActivo = false;

            LabelTitulo.Text = "";
        }
    }
    private void BtnZonas_Clicked(object sender, EventArgs e)
    {
        if (ZnActivo == false)
        {
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewUsers.IsVisible = false;
            ListViewUsers.IsEnabled = false;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = true;
            ListViewZones.IsEnabled = true;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;

            BotonUser.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTrabajador.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTareas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonZonas.BackgroundColor = Color.FromRgba("#ffa73b");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");


            ZnActivo = true;
            GrActivo = false;
            UsActivo = false;
            TrActivo = false;
            TaActivo = false;
            EtActivo = false;

            LabelTitulo.Text = "Tech Talent" + Environment.NewLine + "gestión de zonas de trabajo";
        }
        else
        {
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewUsers.IsVisible = false;
            ListViewUsers.IsEnabled = false;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;

            BotonUser.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTrabajador.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTareas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonZonas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");

            ZnActivo = false;
            GrActivo = false;
            TrActivo = false;
            UsActivo = false;
            TaActivo = false;
            EtActivo = false;

            LabelTitulo.Text = "";
        }
    }
    private void BtnTareas_Clicked(object sender, EventArgs e)
    {
        if (TaActivo == false)
        {
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewUsers.IsVisible = false;
            ListViewUsers.IsEnabled = false;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = true;
            ListViewTasks.IsEnabled = true;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;

            BotonUser.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTrabajador.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTareas.BackgroundColor = Color.FromRgba("#ffa73b");
            BotonZonas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");

            ZnActivo = false;
            GrActivo = false;
            UsActivo = false;
            TrActivo = false;
            TaActivo = true;
            EtActivo = false;

            LabelTitulo.Text = "Tech Talent" + Environment.NewLine + "gestión de zonas de trabajo";
        }
        else
        {
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewUsers.IsVisible = false;
            ListViewUsers.IsEnabled = false;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;

            BotonUser.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTrabajador.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonTareas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonZonas.BackgroundColor = Color.FromRgba("#b9b6bf");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#b9b6bf");

            TaActivo = false;
            ZnActivo = false;
            GrActivo = false;
            TrActivo = false;
            UsActivo = false;
            EtActivo = false;

            LabelTitulo.Text = "";
        }
    }
    private async void BotonBorrarUsuarios_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Question?", "¿Estas seguro de eliminar el trabajador \""+nombreUsuario+"\"?", "Si", "No");
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
        App.Current.MainPage = new NavigationPage(new AltaUsuarios(us.Username,1));
    }
    private async void BotonBorrarTrabajadores_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Question?", "¿Estas seguro de borrar el trabajador \"" + tr.nombre + "\"?", "Si", "No");
        if (tr is not null && answer == true)
        {
            var NumeroTarjeta = tr.numero_tarjeta;
            OperacionesDBContext.borraTrabajador(NumeroTarjeta);
            presenciaContext.Logs.Add(new Log("Eliminar", nombreUsuario + " ha eliminado trabajador \"" + tr.nombre + "\" - " + dt));
            presenciaContext.SaveChanges();
            App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario,2));
        }
        else
        {
            await DisplayAlert("Alert", "Error!", "OK");
        }
    }
    private async void BotonBorrarGruposTrabajo_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Question?", "¿Estas seguro de borrar el grupo \"" + gr.Nombre + "\"?", "Si", "No");
        if (answer == true)
        {
            Turno turno = gr;
            presenciaContext.Remove(turno);
            presenciaContext.Logs.Add(new Log("Eliminar", nombreUsuario + " ha eliminado grupo de trabajo " + gr.Nombre + " - " + dt));
            presenciaContext.SaveChanges();
            App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario,3));
        }
    }
    private void BotonEditarZonas_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(nombreUsuario,zn.Nombre,1));
    }
    private async void BotonBorrarZonas_Clicked(object sender, EventArgs e)
    {
        if (zn is not null)
        {
            bool answer = await DisplayAlert("Question?", "¿Desea Borrar la zona \"" + zn.Nombre + "\"?", "Si", "No");
            if (answer == true)
            {
                await DisplayAlert("Alert", "Se ha borrado correctamente " + zn.Nombre, "OK");
                presenciaContext.Logs.Add(new Log("Eliminar", nombreUsuario + " ha eliminar " + zn.Nombre + " - " + dt));
                presenciaContext.SaveChanges();
                OperacionesDBContext.BorraZona(zn.IdZona);
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario,4));
            }
            else
            {
                await DisplayAlert("Alert", "No se han realizado cambios ", "OK");
            }
        }
        else
        {
            await DisplayAlert("Alert", "Por favor, Seleccione un elemento primero", "OK");
        }
    }
    private void BotonAnadeZona_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(nombreUsuario,"",0)); 
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Añadir zonas de trabajo "+ "-"+ dt));
        presenciaContext.SaveChanges();
        
        
    }
    private void BtnAnadeTarea_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Añadir tareas de trabajo" +  " - " + dt));
        presenciaContext.SaveChanges();
        
    }
    private void BotonCopiar_Clicked(object sender, EventArgs e)
    {
        if (us.Password != "")
            Clipboard.SetTextAsync(us.Password);
            presenciaContext.Logs.Add(new Log("Password", nombreUsuario + " ha copiado la contraseña del usuario:'" + tr.nombre + " - " + dt));
            presenciaContext.SaveChanges();
    }
    private void BtnAnadeTareaGrupoTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeTareasGrupoTrabajo(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Añadir tarea a grupo de trabajo:'" + ta.NombreTarea + " - " + dt));
        presenciaContext.SaveChanges();
    }
    private void AddZonasGrupos_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirZonaGrupoTrabajo(nombreUsuario));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Añadir zonasa grupo de trabajo:'" + zn.Nombre + " - " + dt));
        presenciaContext.SaveChanges();
    }

    private void BtnAnadeEquipos_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaGrupoTrabajo(nombreUsuario,"",0));
        presenciaContext.Logs.Add(new Log("Acceso",nombreUsuario+" ha accedido a \"Registrar nuevo equipo de trabajo\" - "+dt));
        presenciaContext.SaveChanges();
    }

    private void BtnAnadeTrabajadorAEquipo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeTrabajadorEquipoTrabajo(nombreUsuario));
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        BotonCerrarSession.BackgroundColor = Color.FromRgba("#b9b6bf");
        bool answer = await DisplayAlert("Question?","¿Deseas cerrar sesión?","Si","No");
        if(answer == true)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
    private async void BotonBorrarTareas_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Question?", "¿Estas seguro de borrar la tarea \"" + ta.NombreTarea + "\"?", "Si", "No");
        if (answer == true)
        {
            if (ta is not null)
            {
                presenciaContext.Tareas.Remove(ta);
                presenciaContext.Logs.Add(new Log("Eliminar", nombreUsuario + " ha eliminado la tarea " + ta.NombreTarea + " - " + dt));
                presenciaContext.SaveChanges();
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario, 5));
            }
            else
            {
                await DisplayAlert("Alert", "Error!", "OK");
            }
        }
    }
    private void BotonEditarTareas_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo(nombreUsuario,ta.NombreTarea,1));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Editar tarea de trabajo '+" + ta.NombreTarea + " - " + dt));
        presenciaContext.SaveChanges();
    }
    private void BotonEditarGruposTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTurno(nombreUsuario,gr.Nombre,1));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Editar grupo de trabajo' "+gr.Nombre+" - " + dt));
        presenciaContext.SaveChanges();
    }
    private void BotonEditarTrabajadores_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTrabajador(tr.nombre, 1));
        presenciaContext.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Editar trabajador '+" + tr.nombre + " - " + dt));
        presenciaContext.SaveChanges();
    }
}