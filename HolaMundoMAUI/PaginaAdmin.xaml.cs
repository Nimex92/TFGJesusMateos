using ClassLibray;
using HolaMundoMAUI;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;
using Color = Microsoft.Maui.Graphics.Color;

namespace HolaMundoMAUI;

public partial class PaginaAdmin : ContentPage
{
    private string Username;
    PresenciaContext p = new PresenciaContext();
    DateTime dt = DateTime.Now;
    Worker Worker;
    WorkShift WorkShift;
    Places Place;
    WorkTask WorkTask;
    WorkGroup WorkGroup;
    Calendar Calendar;
    Day Day;
    bool activeWorker, activeWorkShift, activePlace, activeWorkTask, activeWorkGroup, activeDay, activePayroll, activeIssue, activeVacationsRequest;
    Db db = new Db();
    public PaginaAdmin(string username)
    {
        activeWorker = false;
        activeWorkShift = false;
        activePlace = false;
        activeWorkTask = false;
        activeWorkGroup = false;
        activePayroll = false;
        activeIssue = false;
        activeVacationsRequest = false;

        InitializeComponent();
        SetListViewWorkers();
        SetListViewWorkShifts();
        SetListViewWorkTasks();
        SetListViewPlaces();
        SetListViewWorkGroups();
        CompruebaIncidencias();
        Username = username;
        LabelNameUser.Text = username;
    }
    public PaginaAdmin(string username, int option)
    {
        activeWorker = false;
        activeWorkShift = false;
        activePlace = false;
        activeWorkTask = false;
        activeWorkGroup = false;
        activePayroll = false;
        activeIssue = false;
        activeVacationsRequest = false;

        InitializeComponent();
        SetListViewWorkers();
        SetListViewWorkShifts();
        SetListViewPlaces();
        SetListViewWorkTasks();
        SetListViewWorkGroups();
        CompruebaIncidencias();

        this.Username = username;
        LabelNameUser.Text = username;
        switch (option)
        {
            case 2:
                ListViewWorkers.IsVisible = true;
                activeWorker = true;
                LabelTitulo2.Text = "Gestión de trabajadores";
                break;
            case 3:
                ListViewGroups.IsVisible = true;
                activeWorkShift = true;
                LabelTitulo3.Text = "Gestión de turnos de trabajo";
                break;
            case 4:
                ListViewZones.IsVisible = true;
                activePlace = true;
                LabelTitulo4.Text = "Gestión de lugares de trabajo";
                break;
            case 5:
                ListViewTasks.IsVisible = true;
                activeWorkTask = true;
                LabelTitulo5.Text = "Gestión de tareas de trabajo";
                break;
            case 6:
                ListViewTeams.IsVisible = true;
                activeWorkGroup = true;
                LabelTitulo5.Text = "Gestión de equipos de trabajo";
                break;
            case 7:
                ListViewCalendar.IsVisible = true;
                activeDay = true;
                LabelTitulo7.Text = "Listado de dias libres";
                break;
            case 8:

                break;
            case 9:

                break;
        }

    }
    //Settear ListViews de la ventana ////////////////////////////////////////////////////////////
    public void SetListViewWorkShifts()
    {
        var workShifts = p.WorkShifts.Where(x => x.Deleted == false).OrderBy(x => x.Name).ToList();
        ListViewGrupos.ItemsSource = workShifts;
        if (workShifts.Count > 0)
            ListViewGrupos.SelectedItem = workShifts[0];
    }
    public void SetListViewWorkers()
    {
        var workers = p.Workers.Include(x => x.User).ToList();
        ListViewTrabajadores.ItemsSource = workers;
        if (workers.Count > 0)
            ListViewTrabajadores.SelectedItem = workers[0];
    }
    public void SetListViewPlaces()
    {
        var zones = p.Places.ToList();
        ListViewZonas.ItemsSource = zones;
        if (zones.Count > 0)
            ListViewZonas.SelectedItem = zones[0];
    }
    public void SetListViewWorkTasks()
    {
        var tareas = p.WorkTasks.ToList();
        ListViewTareas.ItemsSource = tareas;
        if (tareas.Count > 0)
            ListViewTareas.SelectedItem = tareas[0];

    }
    public void SetListViewWorkGroups()
    {
        var equipos = p.WorkGroups.ToList();
        ListViewEquipos.ItemsSource = equipos;
        if (equipos.Count > 0)
            ListViewEquipos.SelectedItem = equipos[0];
    }
    //////////////////////////////////////////////////////////////////////////////////////////////

    ///////// Lo que sucede cuando pulsamos en cada uno de los list view /////////////////////////
    public void OnItemSelectedWorkers(object sender, SelectedItemChangedEventArgs e)
    {
        Worker item = e.SelectedItem as Worker;
        Worker = item;
        Calendar = p.Calendars.Where(x => x.Worker == item).Include(x => x.Worker).FirstOrDefault();
    }
    public void OnItemSelectedWorkShifts(object sender, SelectedItemChangedEventArgs e)
    {
        WorkShift item = e.SelectedItem as WorkShift;
        WorkShift = item;
    }
    public void OnItemSelectedWorkGroups(object sender, SelectedItemChangedEventArgs e)
    { 
        WorkGroup item = e.SelectedItem as WorkGroup;
        WorkGroup = item;
    }
    public void OnItemSelectedPlaces(object sender, SelectedItemChangedEventArgs e)
    {
        Places item = e.SelectedItem as Places;
        Place = item;
    }
    public void OnItemSelectedCalendar(object sender, SelectedItemChangedEventArgs e)
    {
        Day item = e.SelectedItem as Day;
        Day = item;
    }
    public void OnItemSelectedWorkTasks(object sender, SelectedItemChangedEventArgs e)
    {
        WorkTask item = e.SelectedItem as WorkTask;
        WorkTask = item;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////

    /////////// Funciones Menu lateral ///////////////////////////////////////////////////////////
    private void BtnTrabajadores_Clicked(object sender, EventArgs e)
    {
        if (activeWorker == false)
        {
            ListViewWorkers.IsVisible = true;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = false;
            ListViewCalendar.IsVisible = false;
            ListViewNominas.IsVisible = false;
            ListViewIssues.IsVisible = false;
            BotonTrabajador.BackgroundColor = Color.FromRgba("#84677D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#2B282D");
            activeWorker = true;
            activeWorkShift = false;
            activePlace = false;
            activeWorkTask = false;
            activeWorkGroup = false;
            CompruebaIncidencias();
            LabelTitulo2.Text = "Gestión de trabajadores";
        }
        else
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = false;
            ListViewNominas.IsVisible = false;
            ListViewIssues.IsVisible = false;
            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#2B282D");
            activeWorker = false;
            activeWorkShift = false;
            activePlace = false;
            activeWorkTask = false;
            activeWorkGroup = false;
            CompruebaIncidencias();
        }
    }
    private void BtnGruposTrabajo_Clicked(object sender, EventArgs e)
    {
        if (activeWorkShift == false)
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = true;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = false;
            ListViewCalendar.IsVisible = false;
            ListViewNominas.IsVisible = false;
            ListViewIssues.IsVisible = false;
            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#84677D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#2B282D");
            activeWorkShift = true;
            activeWorker = false;
            activeWorkTask = false;
            activePlace = false;
            activeWorkGroup = false;
            CompruebaIncidencias();
        }
        else
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = false;
            ListViewNominas.IsVisible = false;
            ListViewIssues.IsVisible = false;
            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#2B282D");
            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = false;
            activePlace = false;
            activeWorkGroup = false;
            CompruebaIncidencias();
        }
    }
    private void BtnEquiposTrabajo_Clicked(object sender, EventArgs e)
    {
        if (activeWorkGroup == false)
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = true;
            ListViewCalendar.IsVisible = false;
            ListViewNominas.IsVisible = false;
            ListViewIssues.IsVisible = false;

            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#84677D");
            BotonNominas.BackgroundColor = Color.FromRgba("#2B282D");

            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = false;
            activePlace = false;
            activeWorkGroup = true;
            activePayroll = false;
            CompruebaIncidencias();
        }
        else
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = false;
            ListViewNominas.IsVisible = false;
            ListViewIssues.IsVisible = false;


            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#2B282D");


            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = false;
            activePlace = false;
            activeWorkGroup = false;
            activePayroll = false;
            CompruebaIncidencias();

        }
    }
    private void BtnZonas_Clicked(object sender, EventArgs e)
    {
        if (activePlace == false)
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = true;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = false;
            ListViewCalendar.IsVisible = false;
            ListViewNominas.IsVisible = false;
            ListViewIssues.IsVisible = false;


            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#84677D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#2B282D");


            activePlace = true;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = false;
            activeWorkGroup = false;
            activePayroll = false;
            CompruebaIncidencias();
        }
        else
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = false;
            ListViewNominas.IsVisible = false;
            ListViewIssues.IsVisible = false;

            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#2B282D");

            activePlace = false;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = false;
            activeWorkGroup = false;
            activePayroll = false;
            CompruebaIncidencias();
        }
    }
    private void BtnTareas_Clicked(object sender, EventArgs e)
    {
        if (activeWorkTask == false)
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = true;
            ListViewTeams.IsVisible = false;
            ListViewCalendar.IsVisible = false;
            ListViewNominas.IsVisible = false;
            ListViewIssues.IsVisible = false;

            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#84677D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#2B282D");

            activePlace = false;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = true;
            activeWorkGroup = false;
            activePayroll = false;
            CompruebaIncidencias();
        }
        else
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = false;
            ListViewNominas.IsVisible = false;
            ListViewIssues.IsVisible = false;

            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#2B282D");

            activeWorkTask = false;
            activePlace = false;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkGroup = false;
            activePayroll = false;
            CompruebaIncidencias();
        }
    }
    private void BtnNominas_Clicked(object sender, EventArgs e)
    {
        if (activePayroll == false)
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = false;
            ListViewCalendar.IsVisible = false;
            ListViewNominas.IsVisible = true;
            ListViewIssues.IsVisible = false;

            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#84677D"); ;

            activePlace = false;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = false;
            activeWorkGroup = false;
            activePayroll = true;
        }
        else
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = false;
            ListViewIssues.IsVisible = false;

            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#2B282D");

            activeWorkTask = false;
            activePlace = false;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkGroup = false;
            activePayroll = false;
            CompruebaIncidencias();
        }
    }
    private void BtnProblemas_Clicked(object sender, EventArgs e)
    {
        if (activePayroll == false)
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = false;
            ListViewCalendar.IsVisible = false;
            ListViewNominas.IsVisible = false;
            ListViewIssues.IsVisible = true;

            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#84677D"); ;

            activePlace = false;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = false;
            activeWorkGroup = false;
            activePayroll = true;
        }
        else
        {
            ListViewWorkers.IsVisible = false;
            ListViewGroups.IsVisible = false;
            ListViewZones.IsVisible = false;
            ListViewTasks.IsVisible = false;
            ListViewTeams.IsVisible = false;
            ListViewIssues.IsVisible = false;

            BotonTrabajador.BackgroundColor = Color.FromRgba("#2B282D");
            BotonGrupoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonTareas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonZonas.BackgroundColor = Color.FromRgba("#2B282D");
            BotonEquipoTrabajo.BackgroundColor = Color.FromRgba("#2B282D");
            BotonNominas.BackgroundColor = Color.FromRgba("#2B282D");

            activeWorkTask = false;
            activePlace = false;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkGroup = false;
            activePayroll = false;
            CompruebaIncidencias();
        }
    }
    private void BotonNominas_Clicked(object sender, EventArgs e)
    {
        GeneraNominaPdf(Worker);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////

    /////// Metodos de las funciones de aÃ±adir ////////////////////////////////////////////////////
    private void BotonAnadeZona_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(Username, "", 0));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Añadir zonas de trabajo " + "-" + dt),p);
    }
    private void BtnAnadeTarea_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Añadir tareas de trabajo" + " - " + dt), p);
    }
    private void BtnAnadeDias_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeDiaCalendario(Username, Calendar, 0, Day));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Añadir dias al calendario" + " - " + dt), p);

    }
    private void BtnAnadeTareaGrupoTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeTareasGrupoTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'AÃ±adir tarea a grupo de trabajo:'" + WorkTask.Name + " - " + dt),p);
    }
    private void AddZonasGrupos_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirZonaGrupoTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'AÃ±adir zonasa grupo de trabajo:'" + Place.Name + " - " + dt), p);
    }
    private void BtnAnadeEquipos_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaGrupoTrabajo(Username, "", 0));
        db.InsertLog(new Log("Acceso", Username + " ha accedido a \"Registrar nuevo equipo de trabajo\" - " + dt), p);
    }
    private void BtnAnadeTrabajadorAEquipo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeTrabajadorEquipoTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'AÃ±adir trabajador a un grupo de trabajo" + " - " + dt), p);

    }
    private void AddGruposEquipoTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirTurnoEquipoTrabajo(Username, WorkShift.Name, 0));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'AÃ±adir turno a equipo de trabajo' - " + dt), p);
    }
    public void NuevoUsuario(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaUsuarios(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Registrar usuario' - " + dt), p);
    }
    public void NuevoTrabajador(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTrabajador(Username, 0));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Registrar trabajador' - " + dt), p);
    }
    public void NuevoGrupoTrabajo(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTurno(Username, 0));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Registrar grupo de trabajo' - " + dt), p);
    }
    public async void VolverAlMainAdmin(object sender, EventArgs e)
    {
        await DisplayAlert("Alert", "Hasta luego, " + Username, "OK");
        App.Current.MainPage = new NavigationPage(new MainPage());
        db.InsertLog(new Log("Logout", Username + " ha cerrado sesion -" + dt), p);

    }
    private void RegistrarNuevaTareaTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'AÃ±adir tareas de trabajo' - " + dt), p);
    }
    private void RegistrarNuevaZona_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'AÃ±adir zona de trabajo' - " + dt), p);
    }
    private void AnadeTareasGrupo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeTareasGrupoTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'AÃ±adir Tareas a grupo' - " + dt), p);
    }
    private void AnadirZonaGrupo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirZonaGrupoTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'AÃ±adir zona de trabajo' - " + dt), p);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////

    /////// Metodos de las funciones de modificar /////////////////////////////////////////////////
    private void BotonEditarZonas_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(Username, Place.Name, 1));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Editar zonas de trabajo" + " - " + dt), p);

    }
    private void BotonEditarEquiposTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaGrupoTrabajo(Username, WorkGroup.Name, 1));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Editar equipo de trabajo '+" + WorkTask.Name + " - " + dt), p);
    }
    private void BotonActualizaDias_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeDiaCalendario(Username, Calendar, 1, Day));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'AÃ±adir dias al calendario" + " - " + dt),p);

    }
    private void BotonEditarTareas_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo(Username, WorkTask.Name, 1));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Editar tarea de trabajo '+" + WorkTask.Name + " - " + dt), p);
    }
    private void BotonEditarGruposTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTurno(Username, WorkShift.Name, 1));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Editar grupo de trabajo' " + WorkShift.Name + " - " + dt), p);
    }
    private void BotonEditarTrabajadores_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTrabajador(Worker.Name, 1));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Editar trabajador '+" + Worker.Name + " - " + dt), p);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /////// Metodos de las funciones de borrado ///////////////////////////////////////////////////
    private async void BotonBorrarTrabajadores_Clicked(object sender, EventArgs e)
    {
        try
        {
                bool answer = await DisplayAlert("Question?", "Â¿Estas seguro de borrar el trabajador \"" + Worker.Name + "\"?", "Si", "No");
                if (Worker is not null && answer == true)
                {
                    db.DeleteWorker(Worker, p);
                    db.InsertLog(new Log("Eliminar", Username + " ha eliminado trabajador \"" + Worker.Name + "\" - " + dt), p);
                    App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 2));
                }
                else
                {
                    await DisplayAlert("Error", "No se han producido cambios.", "Vale");
                }
        }catch(Exception ex)
        {
            await DisplayAlert("Error", ex.ToString(), "Vale");
        }
    }
    private async void BotonBorrarGruposTrabajo_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool answer = await DisplayAlert("Question?", "Â¿Estas seguro de borrar el grupo \"" + WorkShift.Name + "\"?", "Si", "No");
            if (answer == true)
            {
                WorkShift workShift = WorkShift;
                db.DeleteWorkShift(workShift, p);
                db.InsertLog(new Log("Eliminar", Username + " ha eliminado grupo de trabajo " + WorkShift.Name + " - " + dt), p);
                await DisplayAlert("Alert", "Se ha eliminado correctamente.", "Vale");
                WorkShift turno = WorkShift;
                db.DeleteWorkShift(turno, p);
                db.InsertLog(new Log("Eliminar", Username + " ha eliminado grupo de trabajo " + WorkShift.Name + " - " + dt), p);
                await DisplayAlert("Alert", "Se ha eliminado correctamente.", "Vale");
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 3));
            }
        }catch(Exception ex)
        {
             await DisplayAlert("Alert", "Error inesperado " + ex.ToString,"Vale");
        }
        
    }
    private async void BotonBorrarZonas_Clicked(object sender, EventArgs e)
    {
        if (Place is not null)
        {
            bool answer = await DisplayAlert("Question?", "Â¿Desea Borrar la zona \"" + Place.Name + "\"?", "Si", "No");
            if (answer == true)
            {
                await DisplayAlert("Alert", "Se ha borrado correctamente " + Place.Name, "OK");
                db.DeletePlace(Place, p);
                db.InsertLog(new Log("Eliminar", Username + " ha eliminar " + Place.Name + " - " + dt), p);
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 4));
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
        App.Current.MainPage = new NavigationPage(new BorraTrabajadorDeGrupo(Username, WorkGroup));
    }
    private void RemoveGruposEquipoTrabajo_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirTurnoEquipoTrabajo(Username, WorkShift.Name, 1));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'AÃ±adir turno a equipo de trabajo' - " + dt), p);
    }
    private async void BotonBorrarDias_Clicked(object sender, EventArgs e)
    {

        bool respuesta = await DisplayAlert("Â¿EstÃ¡s segur@?", "Â¿Deseas eliminar este dia?", "Si", "No");
        if (respuesta == true)
        {
            p.DayOff.Remove(Day);
            p.SaveChanges();
            await DisplayAlert("Eliminado", "Se ha eliminado correctamente", "Vale");
            App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 2));
        }
        else
        {
            await DisplayAlert("Error", "No se realizaron cambios.", "Vale");
        }
    }
    private async void BotonBorrarTareas_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Â¿EstÃ¡s segur@?", "Â¿Estas seguro de borrar la tarea \"" + WorkTask.Name + "\"?", "Si", "No");
        if (answer == true)
        {
            if (WorkTask is not null)
            {
                db.DeleteWorkTask(WorkTask,p);
                db.InsertLog(new Log("Eliminar", Username + " ha eliminado la tarea " + WorkTask.Name + " - " + dt),p);
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 5));
            }
            else
            {
                await DisplayAlert("Alert", "Error!", "OK");
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////

    /////////// Otras funciones //////////////////////////////////////////////////////////////////
    private async void BotonCalendario_Clicked(object sender, EventArgs e)
    {
        ListViewCalendar.IsVisible = true;
        ListViewCalendar.IsEnabled = true;
        ListViewWorkers.IsVisible = false;
        ListViewWorkers.IsEnabled = false;

        var ExisteCalendario = p.Calendars.Where(x => x.Worker == Worker).Include(x=>x.DaysOnCalendar).FirstOrDefault();
        if(ExisteCalendario is not null)
        {
            var ListaDias = ExisteCalendario.DaysOnCalendar.ToList();
            ListViewCalendario.ItemsSource = ListaDias;
            if (ListaDias.Count > 0)
                ListViewCalendario.SelectedItem = ListaDias[0];
        }
        else
        {
            await DisplayAlert("Alert", "El trabajador no dispone de calendario, se procede a crearlo", "Vale");
            var trabajador = p.Workers.Where(x => x.CardNumber == Worker.CardNumber).FirstOrDefault();
            var calendario = new Calendar(trabajador);
            p.Calendars.Add(calendario);
            p.SaveChanges();
            await DisplayAlert("Alert", "Calendario creado con exito", "Vale");
        }
        CompruebaDias(Worker);
    }
    private void VolverATrabajadores_Clicked(object sender, EventArgs e)
    {
        ListViewCalendar.IsVisible = false;
        ListViewCalendar.IsEnabled = false;
        ListViewWorkers.IsVisible = true;
        ListViewWorkers.IsEnabled = true;
    }
    private async void BotonCerrarSesion_Clicked(object sender, EventArgs e)
    {
        //BotonCerrarSession.BackgroundColor = Color.FromRgba("#2B282D");
        bool answer = await DisplayAlert("Logout", "¿Deseas cerrar sesión?", "Si", "No");
        if (answer == true)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
    private async void CompruebaDias(Worker tr)
    {
        var ListaFichajes = p.Signings.Where(x => x.Worker == tr).ToList();
        var CalendarioTrabajador = p.Calendars.Where(x => x.Worker == tr).Include(x => x.DaysOnCalendar).FirstOrDefault();
        var ListaDiasLibres = CalendarioTrabajador.DaysOnCalendar.ToList();
        var FechasFichajes = Signing.GetFechas(ListaFichajes);
        var FechasFestivos = Day.GetFechas(ListaDiasLibres);
        foreach (DateTime d in FechasFestivos)
        {
            if (FechasFichajes.Contains(d))
            {

                var ListaDias = p.DayOff.ToList();
                foreach (Day di in ListaDias)
                {
                    if (di.BelongCalendar == CalendarioTrabajador && di.Date == d)
                    {
                        di.Enjoyed = true;
                        p.DayOff.Update(di);
                        p.SaveChanges();
                    }
                    else
                    {
                        di.Enjoyed = false;
                        p.DayOff.Update(di);
                    }
                }


            }

        }
    }
    private void TrabajadoresEnTurno_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new TrabajadoresEnTurno(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Trabajadores en turno' - " + dt),p);
    }
    public async void CompruebaIncidencias()
    {
        try
        {
            var ListaIncidencias = p.Issues.Where(x=>x.Justified==false).Include(x=>x.Worker).ToList();
            ListViewIncidencias.ItemsSource = ListaIncidencias;
            if (ListaIncidencias.Count > 0)
            {
                BtnProblemas.Source = "problemasactivo.png";
                ListViewIncidencias.SelectedItem = ListaIncidencias[0];
            }
            else
            {
                BtnProblemas.Source = "problemas.png";
            }
        }
        catch (NullReferenceException ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }
    public void GeneraNominaPdf(Worker tr)
    {
        try
        {
            PdfWriter writer = new PdfWriter("C:\\Users\\jesus\\Desktop\\HolaMundoMAUI\\Nominas\\demo2.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("Nomina mensual")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
               .SetFontSize(20);
            BetterTable DatosEmpresa = GeneraTabla(3, 2);
            DatosEmpresa.SetColorHeader(ColorConstants.LIGHT_GRAY);
            DatosEmpresa.ChangeTableFontSize(8);
            DatosEmpresa.SetNextText("Empresa");
            DatosEmpresa.SetNextText("Direccion");
            DatosEmpresa.SetNextText("CIF");
            DatosEmpresa.SetNextText("TTI Ventures");
            DatosEmpresa.SetNextText(" C. Paletillas,6,Calahorra");
            DatosEmpresa.SetNextText("B26557397");
            BetterTable DatosTrabajador = GeneraTabla(5, 2);
            DatosTrabajador.SetColorHeader(ColorConstants.LIGHT_GRAY);
            DatosTrabajador.ChangeTableFontSize(8);
            DatosTrabajador.SetNextText("Trabajador");
            DatosTrabajador.SetNextText("Categoria");
            DatosTrabajador.SetNextText("Nº Matricula");
            DatosTrabajador.SetNextText("Antiguedad");
            DatosTrabajador.SetNextText("D.N.I");
            DatosTrabajador.SetNextText(tr.Name);
            DatosTrabajador.SetNextText(tr.Category);
            DatosTrabajador.SetNextText("");
            DatosTrabajador.SetNextText((dt - tr.HiringDate).Days.ToString());
            DatosTrabajador.SetNextText(tr.Nif);
            BetterTable OtrosDatos = GeneraTabla(7, 2);
            OtrosDatos.SetColorHeader(ColorConstants.LIGHT_GRAY);
            OtrosDatos.ChangeTableFontSize(8);
            OtrosDatos.SetNextText("Nº Afiliación S.S");
            OtrosDatos.SetNextText("Tarifa");
            OtrosDatos.SetNextText("Cod C.T.");
            OtrosDatos.SetNextText("Sección");
            OtrosDatos.SetNextText("Nro.");
            OtrosDatos.SetNextText("Periodo");
            OtrosDatos.SetNextText("Días");
            OtrosDatos.SetNextText(tr.SocialSecurityCard);
            var diasTrabajador = p.Signings.Where(x => x.Worker == tr).Where(x => x.CheckInCheckOut == "Entrada").OrderBy(x => x.SigningDate).ToList();
            var diastotal = diasTrabajador.Count;
            var lapso = diasTrabajador.First().SigningDate.Date + "-" + diasTrabajador.Last().SigningDate.Date;
            OtrosDatos[1,6].SetText(lapso.ToString());
            OtrosDatos[1,5].SetText(diastotal.ToString());
            BetterTable CuerpoNomina = GeneraTabla(5, 8);
            CuerpoNomina.SetColorHeader(ColorConstants.LIGHT_GRAY);
            CuerpoNomina.ChangeTableFontSize(10);
            CuerpoNomina.RemoveBorder(1);
            CuerpoNomina.AddTableBorder(1);
            CuerpoNomina.SetNextText("Cuantía");
            CuerpoNomina.SetNextText("Precio");
            CuerpoNomina.SetNextText("Concepto");
            CuerpoNomina.SetNextText("Devengos");
            CuerpoNomina.SetNextText("Deducciones");
            BetterTable PieNomina1 = GeneraTabla(7, 2);
            PieNomina1.SetColorHeader(ColorConstants.LIGHT_GRAY);
            PieNomina1.ChangeTableFontSize(8);
            PieNomina1.SetNextText("Rem. Total");
            PieNomina1.SetNextText("P.P. Extras");
            PieNomina1.SetNextText("Base S.S.");
            PieNomina1.SetNextText("Base A.T y DES");
            PieNomina1.SetNextText("Base I.R.P.F");
            PieNomina1.SetNextText("T. Devengo");
            PieNomina1.SetNextText("T. a deducir");
            Paragraph SubPieNomina1 = new Paragraph("* Percepciones salariales sujetas a Cot. S.S.\t\t\t\t\t\t\t\t *Percepciones no salariales excluidas Cot. S.S.")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
               .SetFontSize(6);
            BetterTable PieNomina2 = GeneraTabla(4, 5);
            PieNomina2.ChangeTableFontSize(6);
            PieNomina2.RemoveBorder(1);
            PieNomina2.AddTableBorder(1);
            PieNomina2.SetNextText("Fecha");
            PieNomina2.SetNextText("Sello empresa");
            PieNomina2.SetNextText("Recibi");
            PieNomina2[0, 0].SetText("16 de Mayo de 2022");
            PieNomina2[4, 0].SetText("SWIFT/BIC: 789456258");
            PieNomina2[4, 0].SetText("IBAN: ES45678912324");
            PieNomina2[4, 0].SetTextAlign("START");
            PieNomina2[1, 3].SetText("Total a percibir");
            PieNomina2[1, 3].AddAllBorders();
            PieNomina2[2, 3].SetText("1245.74€");
            PieNomina2[2, 3].AddAllBorders();
            PieNomina2.AddTableBorder(1);
            BetterTable CabeceraPieNomina3 = GeneraTabla(1, 1);
            CabeceraPieNomina3.ChangeTableFontSize(8);
            CabeceraPieNomina3.RemoveBorder(2);
            CabeceraPieNomina3.TableConjunctionDown();
            CabeceraPieNomina3.SetNextText("DETERMINACION DE LA B. DE COTIZACION A LA S.S. Y CONCEPTOS DE RECAUCACION CONJUNTA Y APORTACION A LA EMPRESA");
            BetterTable PieNomina3 = GeneraTabla(5, 8);
            PieNomina3.TableConjunctionUp();
            PieNomina3.ChangeTableFontSize(8);
            PieNomina3.RemoveBorder(1);
            PieNomina3.AddTableBorder(1);
            PieNomina3[0, 0].SetText("Concepto");
            PieNomina3[0, 0].SetTextAlign("END");
            PieNomina3[0, 2].SetText("Base");
            PieNomina3[0, 3].SetText("Tipo");
            PieNomina3[0, 4].SetText("Aportacion Empresarial");
            PieNomina3[1, 0].SetText("1. Contingencias comunes");
            PieNomina3[1, 1].SetText("......................");
            PieNomina3[1, 2].SetText("1475.00");
            PieNomina3[1, 3].SetText("23.60");
            PieNomina3[1, 4].SetText("348.10");
            PieNomina3[2, 0].SetText("2. Contingencias profesionales");
            PieNomina3[2, 1].SetText("AT Y EP................");
            PieNomina3[2, 2].SetText("1475.00");
            PieNomina3[2, 3].SetText("1.00");
            PieNomina3[2, 4].SetText("14.75");
            PieNomina3[3, 0].SetText(" y conceptos de reaudacion");
            PieNomina3[3, 1].SetText("Desempleo--............");
            PieNomina3[3, 2].SetText("1475.00");
            PieNomina3[3, 3].SetText("5.50");
            PieNomina3[3, 4].SetText("81.13");
            PieNomina3[4, 0].SetText("Conjunta");
            PieNomina3[4, 1].SetText("Formacion profesional..");
            PieNomina3[4, 2].SetText("1475.00");
            PieNomina3[4, 3].SetText("5.50");
            PieNomina3[4, 4].SetText("71.13");
            PieNomina3[5, 1].SetText("Fondo garantia salarial");
            PieNomina3[5, 2].SetText("1475.00");
            PieNomina3[5, 3].SetText("0.62");
            PieNomina3[5, 4].SetText("5.85");
            PieNomina3[6, 0].SetText("3. Cotización horas extraordinarias");
            PieNomina3[6, 1].SetText(".................");
            PieNomina3[6, 2].SetText(".................");
            PieNomina3[6, 3].SetText(".................");
            PieNomina3[6, 4].SetText(".................");

            document.Add(header);
            document.Add(DatosEmpresa.Table);
            document.Add(DatosTrabajador.Table);
            document.Add(OtrosDatos.Table);
            document.Add(CuerpoNomina.Table);
            document.Add(PieNomina1.Table);
            document.Add(SubPieNomina1);
            document.Add(PieNomina2.Table);
            document.Add(CabeceraPieNomina3.Table);
            document.Add(PieNomina3.Table);
            document.Close();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }
    public BetterTable GeneraTabla(int numeroColumnas, int numeroFilas)
    {
        BetterTable b = new BetterTable(numeroColumnas, numeroFilas);
        return b;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
}