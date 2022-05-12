using Persistencia;
using Bibliotec;
using Microsoft.EntityFrameworkCore;
using ClassLibrary1;

namespace HolaMundoMAUI;

public partial class PaginaAdmin : ContentPage
{
    private string nombreUsuario;
    PresenciaContext p = new PresenciaContext();
    DateTime dt = DateTime.Now;
    Usuarios us;
    Trabajador tr;
    Turno gr;
    Zonas zn;
    Tareas ta;
    EquipoTrabajo Et;
    Calendario cal;
    Dia dia;
    Boolean UsActivo,TrActivo,GrActivo,ZnActivo,TaActivo,EtActivo,DiActivo;

    public PaginaAdmin(string nombreUsuario)
    {
        UsActivo = false;
        TrActivo = false;
        GrActivo = false;
        ZnActivo = false;
        TaActivo = false;
        EtActivo = false;
        
        InitializeComponent();
        SetListViewTrabajadores();
        SetListViewGruposTrabajo();
        SetListViewTareas();
        SetListViewZonas();
        SetListViewEquiposTrabajo();
        

        this.nombreUsuario = nombreUsuario;
        LabelNameUser.Text = nombreUsuario;
    }
    public PaginaAdmin(string nombreUsuario,int interfaz)
    {
        UsActivo = false;
        TrActivo = false;
        GrActivo = false;
        ZnActivo = false;
        TaActivo = false;

        InitializeComponent();
        SetListViewTrabajadores();
        SetListViewGruposTrabajo();
        SetListViewZonas();
        SetListViewTareas();
        SetListViewEquiposTrabajo();

        this.nombreUsuario = nombreUsuario;
        LabelNameUser.Text = nombreUsuario;
        switch (interfaz)
        {
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
                EtActivo = true;
                LabelTitulo5.Text = "Tech Talent" + Environment.NewLine + "gestión de turnos de trabajo";
                break;
            case 7:
                ListViewCalendar.IsVisible = true;
                ListViewCalendar.IsEnabled = true;
                DiActivo = true;
                LabelTitulo7.Text = "Tech Talent" + Environment.NewLine + "gestión de dias libres";
                break;
        }
    }



    //Settear ListViews de la ventana /////////////////////////////
    public void SetListViewGruposTrabajo()
    {
        PresenciaContext presenciaContext = new PresenciaContext();
        var turnos = presenciaContext.Turno.Where(x=>x.Eliminado==false).OrderBy(x=>x.Nombre).ToList();
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
    //////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////// Lo que sucede cuando pulsamos en cada uno de los list view /////////////////////////
    public  void OnItemSelectedTrabajadores(object sender, SelectedItemChangedEventArgs e)
    {
        Trabajador item = e.SelectedItem as Trabajador;
        tr = item;
        cal = p.Calendario.Where(x => x.Trabajador == item).Include(x=>x.Trabajador).FirstOrDefault();
        
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
    public void OnItemSelectedCalendario(object sender, SelectedItemChangedEventArgs e)
    {
        Dia item = e.SelectedItem as Dia;
        dia = item;
    }
    public void OnItemSelectedTareas(object sender, SelectedItemChangedEventArgs e)
    {
        Tareas item = e.SelectedItem as Tareas;
        ta = item;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////
    
    /////////// Funciones Menu lateral ///////////////////////////////////////////////////////////
    private void BtnTrabajadores_Clicked(object sender, EventArgs e)
    {
        if (TrActivo == false)
        {
            ListViewWorkers.IsVisible = true;
            ListViewWorkers.IsEnabled = true;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;
            ListViewCalendar.IsVisible = false;
            ListViewCalendar.IsEnabled = false;


            
            BotonTrabajador.BackgroundColor = Color.FromRgba("#84677D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");


            TrActivo = true;
            UsActivo = false;
            GrActivo = false;
            ZnActivo = false;
            TaActivo = false;
            EtActivo = false;

            LabelTitulo2.Text = "Tech Talent" + Environment.NewLine + "gestión de trabajadores";
        }
        else
        {
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

            
            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");


            TrActivo = false;
            UsActivo = false;
            GrActivo = false;
            ZnActivo = false;
            TaActivo = false;
            EtActivo = false;
        }
    }
    private void BtnGruposTrabajo_Clicked(object sender, EventArgs e)
    {
        if (GrActivo == false)
        {
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewGroups.IsVisible = true;
            ListViewGroups.IsEnabled = true;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = false;
            ListViewTeams.IsEnabled = false;
            ListViewCalendar.IsVisible = false;
            ListViewCalendar.IsEnabled = false;

            
            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#84677D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");


            GrActivo = true;
            UsActivo = false;
            TrActivo = false;
            TaActivo = false;
            ZnActivo = false;
            EtActivo = false;
        }
        else
        {
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

            
            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");


            GrActivo = false;
            TrActivo = false;
            UsActivo = false;
            TaActivo = false;
            ZnActivo = false;
            EtActivo = false;
        }
    }
    private void BtnEquiposTrabajo_Clicked(object sender, EventArgs e)
    {
        if (EtActivo == false)
        {
            ListViewWorkers.IsVisible = false;
            ListViewWorkers.IsEnabled = false;
            ListViewGroups.IsVisible = false;
            ListViewGroups.IsEnabled = false;
            ListViewZones.IsVisible = false;
            ListViewZones.IsEnabled = false;
            ListViewTasks.IsVisible = false;
            ListViewTasks.IsEnabled = false;
            ListViewTeams.IsVisible = true;
            ListViewTeams.IsEnabled = true;
            ListViewCalendar.IsVisible = false;
            ListViewCalendar.IsEnabled = false;


            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#84677D");


            GrActivo = false;
            UsActivo = false;
            TrActivo = false;
            TaActivo = false;
            ZnActivo = false;
            EtActivo = true;

        }
        else
        {
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


            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");


            GrActivo = false;
            TrActivo = false;
            UsActivo = false;
            TaActivo = false;
            ZnActivo = false;
            EtActivo = false;

        }
        }
        private void BtnZonas_Clicked(object sender, EventArgs e)
        {
            if (ZnActivo == false)
            {
                ListViewWorkers.IsVisible = false;
                ListViewWorkers.IsEnabled = false;
                ListViewGroups.IsVisible = false;
                ListViewGroups.IsEnabled = false;
                ListViewZones.IsVisible = true;
                ListViewZones.IsEnabled = true;
                ListViewTasks.IsVisible = false;
                ListViewTasks.IsEnabled = false;
                ListViewTeams.IsVisible = false;
                ListViewTeams.IsEnabled = false;
                ListViewCalendar.IsVisible = false;
                ListViewCalendar.IsEnabled = false;


                BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
                BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
                BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
                BotonZonas.BackgroundColor = Color.FromRgba("#84677D");
                BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");


                ZnActivo = true;
                GrActivo = false;
                UsActivo = false;
                TrActivo = false;
                TaActivo = false;
                EtActivo = false;
            }
            else
            {
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


                BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
                BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
                BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
                BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
                BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");

                ZnActivo = false;
                GrActivo = false;
                TrActivo = false;
                UsActivo = false;
                TaActivo = false;
                EtActivo = false;
            }
        }
        private void BtnTareas_Clicked(object sender, EventArgs e)
        {
            if (TaActivo == false)
            {
                ListViewWorkers.IsVisible = false;
                ListViewWorkers.IsEnabled = false;
                ListViewGroups.IsVisible = false;
                ListViewGroups.IsEnabled = false;
                ListViewZones.IsVisible = false;
                ListViewZones.IsEnabled = false;
                ListViewTasks.IsVisible = true;
                ListViewTasks.IsEnabled = true;
                ListViewTeams.IsVisible = false;
                ListViewTeams.IsEnabled = false;
                ListViewCalendar.IsVisible = false;
                ListViewCalendar.IsEnabled = false;


                BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
                BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
                BotonTareas.BackgroundColor = Color.FromRgba("#84677D");
                BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
                BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");

                ZnActivo = false;
                GrActivo = false;
                UsActivo = false;
                TrActivo = false;
                TaActivo = true;
                EtActivo = false;
            }
            else
            {
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


                BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
                BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
                BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
                BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
                BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");

                TaActivo = false;
                ZnActivo = false;
                GrActivo = false;
                TrActivo = false;
                UsActivo = false;
                EtActivo = false;
            }
        }
    

    ///////////////////////////////////////////////////////////////////////////////////////////////

    /////// Metodos de las funciones de añadir ////////////////////////////////////////////////////
    private void BotonAnadeZona_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(nombreUsuario, "", 0));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Añadir zonas de trabajo " + "-" + dt));
    }
    private void BtnAnadeTarea_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo(nombreUsuario));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Añadir tareas de trabajo" + " - " + dt));
    }
    private void BtnAnadeDias_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeDiaCalendario(nombreUsuario, cal, 0, dia));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Añadir dias al calendario" + " - " + dt));

    }
    private void BtnAnadeTareaGrupoTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeTareasGrupoTrabajo(nombreUsuario));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Añadir tarea a grupo de trabajo:'" + ta.NombreTarea + " - " + dt));
    }
    private void AddZonasGrupos_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirZonaGrupoTrabajo(nombreUsuario));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Añadir zonasa grupo de trabajo:'" + zn.Nombre + " - " + dt));
    }
    private void BtnAnadeEquipos_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaGrupoTrabajo(nombreUsuario, "", 0));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " ha accedido a \"Registrar nuevo equipo de trabajo\" - " + dt));
    }
    private void BtnAnadeTrabajadorAEquipo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeTrabajadorEquipoTrabajo(nombreUsuario));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Añadir trabajador a un grupo de trabajo" + " - " + dt));

    }
    private void AddGruposEquipoTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirTurnoEquipoTrabajo(nombreUsuario, gr.Nombre, 0));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Añadir turno a equipo de trabajo' - " + dt));
    }
    public void NuevoUsuario(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaUsuarios(nombreUsuario));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Registrar usuario' - " + dt));
    }
    public void NuevoTrabajador(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTrabajador(nombreUsuario, 0));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Registrar trabajador' - " + dt));
    }
    public void NuevoGrupoTrabajo(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTurno(nombreUsuario, 0));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Registrar grupo de trabajo' - " + dt));
    }
    public async void VolverAlMainAdmin(object sender, EventArgs e)
    {
        await DisplayAlert("Alert", "Hasta luego, " + nombreUsuario, "OK");
        App.Current.MainPage = new NavigationPage(new MainPage());
        OperacionesDBContext.InsertaLog(new Log("Logout", nombreUsuario + " ha cerrado sesion -" + dt));

    }
    private void RegistrarNuevaTareaTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo(nombreUsuario));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Añadir tareas de trabajo' - " + dt));
    }
    private void RegistrarNuevaZona_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(nombreUsuario));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Añadir zona de trabajo' - " + dt));
    }
    private void AnadeTareasGrupo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeTareasGrupoTrabajo(nombreUsuario));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Añadir Tareas a grupo' - " + dt));
    }
    private void AnadirZonaGrupo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirZonaGrupoTrabajo(nombreUsuario));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Añadir zona de trabajo' - " + dt));
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////

    /////// Metodos de las funciones de modificar /////////////////////////////////////////////////
    private void BotonEditarUsuarios_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaUsuarios(us.Username,1));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Editar usuarios" + " - " + dt));

    }
    private void BotonEditarZonas_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(nombreUsuario, zn.Nombre, 1));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Editar zonas de trabajo" + " - " + dt));

    }
    private void BotonEditarEquiposTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaGrupoTrabajo(nombreUsuario, Et.Nombre, 1));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Editar equipo de trabajo '+" + ta.NombreTarea + " - " + dt));
    }
    private void BotonActualizaDias_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeDiaCalendario(nombreUsuario, cal, 1, dia));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Añadir dias al calendario" + " - " + dt));

    }
    private void BotonEditarTareas_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo(nombreUsuario, ta.NombreTarea, 1));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Editar tarea de trabajo '+" + ta.NombreTarea + " - " + dt));
    }
    private void BotonEditarGruposTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTurno(nombreUsuario, gr.Nombre, 1));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Editar grupo de trabajo' " + gr.Nombre + " - " + dt));
    }
    private void BotonEditarTrabajadores_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTrabajador(tr.nombre, 1));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Editar trabajador '+" + tr.nombre + " - " + dt));
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /////// Metodos de las funciones de borrado ///////////////////////////////////////////////////
    private async void BotonBorrarUsuarios_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool answer = await DisplayAlert("Question?", "¿Estas seguro de eliminar el trabajador \"" + nombreUsuario + "\"?", "Si", "No");
            if (answer == true)
            {
                bool inserta = OperacionesDBContext.BorraUsuario(us);
                OperacionesDBContext.InsertaLog(new Log("Eliminar", nombreUsuario + " ha eliminado tarea " + us.Username + " - " + dt));
                if (inserta == true)
                {
                    await DisplayAlert("Alert", "Se ha borrado correctamente.", "Vale");
                    App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario, 1));
                }
                else
                {
                    await DisplayAlert("Alert", "Debe seleccionar un usuario", "OK");
                }
            }
        }catch(Exception ex)
        {
            await DisplayAlert("Alert", "Error inesperado " + ex.StackTrace, "Vale");
        }
    }
    private async void BotonBorrarTrabajadores_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool answer = await DisplayAlert("Question?", "¿Estas seguro de borrar el trabajador \"" + tr.nombre + "\"?", "Si", "No");
                if (tr is not null && answer == true)
                {
                    OperacionesDBContext.BorraTrabajador(tr);
                    OperacionesDBContext.InsertaLog(new Log("Eliminar", nombreUsuario + " ha eliminado trabajador \"" + tr.nombre + "\" - " + dt));
                    App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario, 2));
                }
                else
                {
                    await DisplayAlert("Alert", "No se han producido cambios.", "Vale");
                }
            }catch(Exception ex)
            {
                await DisplayAlert("Alert", "Error inesperado "+ex.StackTrace, "Vale");
            }
        }
    private async void BotonBorrarGruposTrabajo_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool answer = await DisplayAlert("Question?", "¿Estas seguro de borrar el grupo \"" + gr.Nombre + "\"?", "Si", "No");
                if (answer == true)
                {
                    Turno turno = gr;
                    OperacionesDBContext.BorraTurno(turno);
                    OperacionesDBContext.InsertaLog(new Log("Eliminar", nombreUsuario + " ha eliminado grupo de trabajo " + gr.Nombre + " - " + dt));
                    await DisplayAlert("Alert", "Se ha eliminado correctamente.", "Vale");

                    App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario, 3));

                }
            }catch(Exception ex)
            {
                await DisplayAlert("Alert", "Error inesperado " + ex.Message,"Vale");
            }
        }
    private async void BotonBorrarZonas_Clicked(object sender, EventArgs e)
    {
        if (zn is not null)
        {
            bool answer = await DisplayAlert("Question?", "¿Desea Borrar la zona \"" + zn.Nombre + "\"?", "Si", "No");
            if (answer == true)
            {
                await DisplayAlert("Alert", "Se ha borrado correctamente " + zn.Nombre, "OK");
                OperacionesDBContext.BorraZona(zn);
                OperacionesDBContext.InsertaLog(new Log("Eliminar", nombreUsuario + " ha eliminar " + zn.Nombre + " - " + dt));
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario, 4));
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
    private void BotonQuitarTrabajadorDeGrupo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new BorraTrabajadorDeGrupo(nombreUsuario, Et));
    }
    private void RemoveGruposEquipoTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirTurnoEquipoTrabajo(nombreUsuario, gr.Nombre, 1));
        p.Logs.Add(new Log("Acceso", nombreUsuario + " Accede a 'Añadir turno a equipo de trabajo' - " + dt));
        p.SaveChanges();
    }
    private async void BotonBorrarDias_Clicked(object sender, EventArgs e)
    {

        bool respuesta = await DisplayAlert("Question?", "¿Deseas eliminar este dia?", "Si", "No");
        if (respuesta == true)
        {
            p.DiaLibre.Remove(dia);
            p.SaveChanges();
            await DisplayAlert("Alert", "Se ha eliminado correctamente", "Vale");
            App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario, 2));
        }
        else
        {
            await DisplayAlert("Alert", "No se realizaron cambios.", "Vale");
        }
    }
    private async void BotonBorrarTareas_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Question?", "¿Estas seguro de borrar la tarea \"" + ta.NombreTarea + "\"?", "Si", "No");
        if (answer == true)
        {
            if (ta is not null)
            {
                OperacionesDBContext.BorraTarea(ta);
                OperacionesDBContext.InsertaLog(new Log("Eliminar", nombreUsuario + " ha eliminado la tarea " + ta.NombreTarea + " - " + dt));
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario, 5));
            }
            else
            {
                await DisplayAlert("Alert", "Error!", "OK");
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////

    /////////// Otras funciones //////////////////////////////////////////////////////////////////
    private void BotonCopiar_Clicked(object sender, EventArgs e)
    {
        if (us.Password != "")
            Clipboard.SetTextAsync(us.Password);
            p.Logs.Add(new Log("Password", nombreUsuario + " ha copiado la contraseña del usuario:'" + tr.nombre + " - " + dt));
            p.SaveChanges();
    }
    private async void BotonCalendario_Clicked(object sender, EventArgs e)
    {
        ListViewCalendar.IsVisible = true;
        ListViewCalendar.IsEnabled = true;
        ListViewWorkers.IsVisible = false;
        ListViewWorkers.IsEnabled = false;

        var ExisteCalendario = p.Calendario.Where(x => x.Trabajador == tr).Include(x=>x.DiasDelCalendario).FirstOrDefault();
        if(ExisteCalendario is not null)
        {
            var ListaDias = ExisteCalendario.DiasDelCalendario.ToList();
            ListViewCalendario.ItemsSource = ListaDias;
            if(ListaDias.Count > 0)
            ListViewCalendario.SelectedItem = ListaDias[0];
        }
        else
        {
            await DisplayAlert("Alert", "El trabajador no dispone de calendario, se procede a crearlo", "Vale");
            var trabajador = p.Trabajador.Where(x => x.numero_tarjeta == tr.numero_tarjeta).FirstOrDefault();
            var calendario = new Calendario(trabajador);
            p.Calendario.Add(calendario);
            p.SaveChanges();
            await DisplayAlert("Alert", "Calendario creado con exito", "Vale");
        }
        CompruebaDias(tr);
    }
    private void VolverATrabajadores_Clicked(object sender, EventArgs e)
    {
        ListViewCalendar.IsVisible = false;
        ListViewCalendar.IsEnabled = false;
        ListViewWorkers.IsVisible = true;
        ListViewWorkers.IsEnabled = true;
    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        BotonCerrarSession.BackgroundColor = Color.FromRgba("#2B282D");
        bool answer = await DisplayAlert("Question?","¿Deseas cerrar sesión?","Si","No");
        if(answer == true)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
    private async void CompruebaDias(Trabajador tr)
    {
        var ListaFichajes = p.Fichajes.Where(x => x.Trabajador == tr).ToList();
        var CalendarioTrabajador = p.Calendario.Where(x => x.Trabajador == tr).Include(x => x.DiasDelCalendario).FirstOrDefault();
        var ListaDiasLibres = CalendarioTrabajador.DiasDelCalendario.ToList();
        var FechasFichajes = Fichajes.GetFechas(ListaFichajes);
        var FechasFestivos = Dia.GetFechas(ListaDiasLibres);
        foreach (DateTime d in FechasFestivos)
        {
            if (FechasFichajes.Contains(d))
            {

                var ListaDias = p.DiaLibre.ToList();
                foreach (Dia di in ListaDias)
                {
                    if (di.CalendarioPertenece == CalendarioTrabajador && di.Fecha == d)
                    {
                        di.Disfrutado = true;
                        p.DiaLibre.Update(di);
                        p.SaveChanges();
                    }
                    else
                    {
                        di.Disfrutado = false;
                        p.DiaLibre.Update(di);
                    }
                }


            }

        }
    }
    private void TrabajadoresEnTurno_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new TrabajadoresEnTurno(nombreUsuario));
        OperacionesDBContext.InsertaLog(new Log("Acceso", nombreUsuario + " Accede a 'Trabajadores en turno' - " + dt));
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
}