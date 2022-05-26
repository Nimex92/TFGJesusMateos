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
        IssueCheck();
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
        IssueCheck();

        this.Username = username;
        LabelNameUser.Text = username;
        switch (option)
        {
            case 2:
                ListViewWorkers.IsVisible = true;
                activeWorker = true;
                LabelTitulo2.Text = "Gesti�n de trabajadores";
                break;
            case 3:
                ListViewGroups.IsVisible = true;
                activeWorkShift = true;
                LabelTitulo3.Text = "Gesti�n de turnos de trabajo";
                break;
            case 4:
                ListViewZones.IsVisible = true;
                activePlace = true;
                LabelTitulo4.Text = "Gesti�n de lugares de trabajo";
                break;
            case 5:
                ListViewTasks.IsVisible = true;
                activeWorkTask = true;
                LabelTitulo5.Text = "Gesti�n de tareas de trabajo";
                break;
            case 6:
                ListViewTeams.IsVisible = true;
                activeWorkGroup = true;
                LabelTitulo5.Text = "Gesti�n de equipos de trabajo";
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
    /// <summary>
    /// Method to fill the ListView of the UI
    /// </summary>
    public void SetListViewWorkShifts()
    {
        var workShifts = p.WorkShifts.Where(x => x.Deleted == false).OrderBy(x => x.Name).ToList();
        ListViewGrupos.ItemsSource = workShifts;
        if (workShifts.Count > 0)
            ListViewGrupos.SelectedItem = workShifts[0];
    }
    /// <summary>
    /// Method to fill the ListView of the UI
    /// </summary>
    public void SetListViewWorkers()
    {
        var workers = p.Workers.Include(x => x.User).ToList();
        ListViewTrabajadores.ItemsSource = workers;
        if (workers.Count > 0)
            ListViewTrabajadores.SelectedItem = workers[0];
    }
    /// <summary>
    /// Method to fill the ListView of the UI
    /// </summary>
    public void SetListViewPlaces()
    {
        var zones = p.Places.ToList();
        ListViewZonas.ItemsSource = zones;
        if (zones.Count > 0)
            ListViewZonas.SelectedItem = zones[0];
    }
    /// <summary>
    /// Method to fill the ListView of the UI
    /// </summary>
    public void SetListViewWorkTasks()
    {
        var tareas = p.WorkTasks.ToList();
        ListViewTareas.ItemsSource = tareas;
        if (tareas.Count > 0)
            ListViewTareas.SelectedItem = tareas[0];
    }
    /// <summary>
    /// Method to fill the ListView of the UI
    /// </summary>
    public void SetListViewWorkGroups()
    {
        var equipos = p.WorkGroups.ToList();
        ListViewEquipos.ItemsSource = equipos;
        if (equipos.Count > 0)
            ListViewEquipos.SelectedItem = equipos[0];
    }
    /// <summary>
    /// Method to set an action when select an ListView Item
    /// </summary>
    /// <param object="sender"></param>
    /// <param SelectedItemChangedEventArgs="e"></param>
    public void OnItemSelectedWorkers(object sender, SelectedItemChangedEventArgs e)
    {
        Worker item = e.SelectedItem as Worker;
        Worker = item;
        Calendar = p.Calendars.Where(x => x.Worker == item).Include(x => x.Worker).FirstOrDefault();
    }
    /// <summary>
    /// Method to set an action when select an ListView Item
    /// </summary>
    /// <param object="sender"></param>
    /// <param SelectedItemChangedEventArgs="e"></param>
    public void OnItemSelectedWorkShifts(object sender, SelectedItemChangedEventArgs e)
    {
        WorkShift item = e.SelectedItem as WorkShift;
        WorkShift = item;
    }
    /// <summary>
    /// Method to set an action when select an ListView Item
    /// </summary>
    /// <param object="sender"></param>
    /// <param SelectedItemChangedEventArgs="e"></param>
    public void OnItemSelectedWorkGroups(object sender, SelectedItemChangedEventArgs e)
    { 
        WorkGroup item = e.SelectedItem as WorkGroup;
        WorkGroup = item;
    }
    /// <summary>
    /// Method to set an action when select an ListView Item
    /// </summary>
    /// <param object="sender"></param>
    /// <param SelectedItemChangedEventArgs="e"></param>
    public void OnItemSelectedPlaces(object sender, SelectedItemChangedEventArgs e)
    {
        Places item = e.SelectedItem as Places;
        Place = item;
    }
    /// <summary>
    /// Method to set an action when select an ListView Item
    /// </summary>
    /// <param object="sender"></param>
    /// <param SelectedItemChangedEventArgs="e"></param>
    public void OnItemSelectedCalendar(object sender, SelectedItemChangedEventArgs e)
    {
        Day item = e.SelectedItem as Day;
        Day = item;
    }
    /// <summary>
    /// Method to set an action when select an ListView Item
    /// </summary>
    /// <param object="sender"></param>
    /// <param SelectedItemChangedEventArgs="e"></param>
    public void OnItemSelectedWorkTasks(object sender, SelectedItemChangedEventArgs e)
    {
        WorkTask item = e.SelectedItem as WorkTask;
        WorkTask = item;
    }
    /// <summary>
    /// Method to enable the workers UI of the admin section
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void WorkersButton_Clicked(object sender, EventArgs e)
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
            WorkersButton.BackgroundColor = Color.FromRgba("#84677D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#2B282D");
            activeWorker = true;
            activeWorkShift = false;
            activePlace = false;
            activeWorkTask = false;
            activeWorkGroup = false;
            IssueCheck();
            LabelTitulo2.Text = "Gesti�n de trabajadores";
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
            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#2B282D");
            activeWorker = false;
            activeWorkShift = false;
            activePlace = false;
            activeWorkTask = false;
            activeWorkGroup = false;
            IssueCheck();
        }
    }
    /// <summary>
    /// Method to enable the workers UI of the admin section
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void WorkShiftsButton_Clicked(object sender, EventArgs e)
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
            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#84677D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#2B282D");
            activeWorkShift = true;
            activeWorker = false;
            activeWorkTask = false;
            activePlace = false;
            activeWorkGroup = false;
            IssueCheck();
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
            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#2B282D");
            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = false;
            activePlace = false;
            activeWorkGroup = false;
            IssueCheck();
        }
    }
    /// <summary>
    /// Method to enable the WorkGroups UI of the admin section
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void WorkGroupsButton_Clicked(object sender, EventArgs e)
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

            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#84677D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#2B282D");

            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = false;
            activePlace = false;
            activeWorkGroup = true;
            activePayroll = false;
            IssueCheck();
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


            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#2B282D");


            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = false;
            activePlace = false;
            activeWorkGroup = false;
            activePayroll = false;
            IssueCheck();

        }
    }
    /// <summary>
    /// Method to enable the Places UI of the admin section
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void PlacesButton_Clicked(object sender, EventArgs e)
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


            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#84677D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#2B282D");


            activePlace = true;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = false;
            activeWorkGroup = false;
            activePayroll = false;
            IssueCheck();
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

            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#2B282D");

            activePlace = false;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = false;
            activeWorkGroup = false;
            activePayroll = false;
            IssueCheck();
        }
    }
    /// <summary>
    /// Method to enable the WorkTasks UI of the admin section
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void WorkTasksButton_Clicked(object sender, EventArgs e)
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

            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#84677D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#2B282D");

            activePlace = false;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkTask = true;
            activeWorkGroup = false;
            activePayroll = false;
            IssueCheck();
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

            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#2B282D");

            activeWorkTask = false;
            activePlace = false;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkGroup = false;
            activePayroll = false;
            IssueCheck();
        }
    }
    /// <summary>
    /// Method to enable the Payroll UI of the admin section
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void PayrollButton_Clicked(object sender, EventArgs e)
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

            
            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#84677D"); ;

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

            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#2B282D");

            activeWorkTask = false;
            activePlace = false;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkGroup = false;
            activePayroll = false;
            IssueCheck();
        }
    }
    /// <summary>
    /// Method to enable the Issues UI of the admin section
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void IssueButton_Clicked(object sender, EventArgs e)
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

            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#84677D"); ;

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

            WorkersButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkShiftsButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkTasksButton.BackgroundColor = Color.FromRgba("#2B282D");
            PlacesButton.BackgroundColor = Color.FromRgba("#2B282D");
            WorkGroupsButton.BackgroundColor = Color.FromRgba("#2B282D");
            PayrollsButton.BackgroundColor = Color.FromRgba("#2B282D");

            activeWorkTask = false;
            activePlace = false;
            activeWorkShift = false;
            activeWorker = false;
            activeWorkGroup = false;
            activePayroll = false;
            IssueCheck();
        }
    }
    /// <summary>
    /// Method to enable the generate Payroll Method of the admin section
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void WorkerPayrollButton_Clicked(object sender, EventArgs e)
    {
        GeneratePayrollToPDF(Worker);
    }
    /// <summary>
    /// Method to add a new Place
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void AddPlaceButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(Username, "", 0));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'A�adir zonas de trabajo " + "-" + dt),p);
    }
    /// <summary>
    /// Method to add a new WorkTasks
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void AddWorkTasksButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'A�adir tareas de trabajo" + " - " + dt), p);
    }
    /// <summary>
    /// Method to add a new Day
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void AddDaysButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeDiaCalendario(Username, Calendar, 0, Day));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'A�adir dias al calendario" + " - " + dt), p);

    }
    /// <summary>
    /// Method to add a Place to WorkGroup
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void AddPlacesTOWorkGroupButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirZonaGrupoTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Añadir zonasa grupo de trabajo:'" + Place.Name + " - " + dt), p);
    }
    /// <summary>
    /// Method to add a new WorkGroup
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void AddWorkGroupsButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaGrupoTrabajo(Username, "", 0));
        db.InsertLog(new Log("Acceso", Username + " ha accedido a \"Registrar nuevo equipo de trabajo\" - " + dt), p);
    }
    /// <summary>
    /// Method to add a Worker to WorkGroup
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void AddWorkerTOWorkGroupButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeTrabajadorEquipoTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Añadir trabajador a un grupo de trabajo" + " - " + dt), p);

    }
    /// <summary>
    /// Method to add a WorkShift to WorkGroup
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void AddWorkShiftTOWorkGroupButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirTurnoEquipoTrabajo(Username, WorkShift.Name, 0));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Añadir turno a equipo de trabajo' - " + dt), p);
    }
    /// <summary>
    /// Method to add a new User
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    public void AddUserButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaUsuarios(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Registrar usuario' - " + dt), p);
    }
    /// <summary>
    /// Method to add a new Worker
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    public void AddWorkerButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTrabajador(Username, 0));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Registrar trabajador' - " + dt), p);
    }
    /// <summary>
    /// Method to add a new WorkShift
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    public void AddWorkGroupButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTurno(Username, 0));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Registrar grupo de trabajo' - " + dt), p);
    }
    /// <summary>
    /// Method to Logout session
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    public async void LogoutSessionButton_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Logout", "Hasta luego, " + Username, "OK");
        App.Current.MainPage = new NavigationPage(new MainPage());
        db.InsertLog(new Log("Logout", Username + " ha cerrado sesion -" + dt), p);

    }
    /// <summary>
    /// Method to add a new WorkTask
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void AddWorkTaskButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Añadir tareas de trabajo' - " + dt), p);
    }
    /// <summary>
    /// Method to add a new Place
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void AddPlacesButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Añadir zona de trabajo' - " + dt), p);
    }
    /// <summary>
    /// Method to add a WorkTask to Workgroup
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void AddWorkTasksTOWorkGroupButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeTareasGrupoTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Añadir Tareas a grupo' - " + dt), p);
    }
    /// <summary>
    /// Method to add a Place to WorkGroup
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void AddPlaceTOWorkGroupButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirZonaGrupoTrabajo(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'A�adir zona de trabajo' - " + dt), p);
    }
    /// <summary>
    /// Method to edit a Place
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void ModifyPlacesButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaZona(Username, Place.Name, 1));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Editar zonas de trabajo" + " - " + dt), p);

    }
    /// <summary>
    /// Method to edit a WorkShift
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void ModifyWorkShiftButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaGrupoTrabajo(Username, WorkGroup.Name, 1));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Editar equipo de trabajo '+" + WorkTask.Name + " - " + dt), p);
    }
    /// <summary>
    /// Method to edit a Day
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void ModifyDaysButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadeDiaCalendario(Username, Calendar, 1, Day));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Añadir dias al calendario" + " - " + dt),p);

    }
    /// <summary>
    /// Method to edit a WorkTask
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void ModifyWorkTaskButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTareaTrabajo(Username, WorkTask.Name, 1));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Editar tarea de trabajo '+" + WorkTask.Name + " - " + dt), p);
    }
    /// <summary>
    /// Method to edit WorkShift
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void ModifyWorkGroupButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTurno(Username, WorkShift.Name, 1));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Editar grupo de trabajo' " + WorkShift.Name + " - " + dt), p);
    }
    /// <summary>
    /// Method to edit a Worker
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void ModifyWorkersButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AltaTrabajador(Worker.Name, 1));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Editar trabajador '+" + Worker.Name + " - " + dt), p);
    }
    /// <summary>
    /// Method to delete a worker
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private async void DeleteWorkerButton_Clicked(object sender, EventArgs e)
    {
        try
        {
                bool answer = await DisplayAlert("Question?", "¿Estas seguro de borrar el trabajador \"" + Worker.Name + "\"?", "Si", "No");
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
    /// <summary>
    /// Method to delete a WorkShift
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private async void DeleteWorkShiftButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool answer = await DisplayAlert("Question?", "¿Estas seguro de borrar el grupo \"" + WorkShift.Name + "\"?", "Si", "No");
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
    /// <summary>
    /// Method to delete a Place
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private async void DeletePlaceButton_Clicked(object sender, EventArgs e)
    {
        if (Place is not null)
        {
            bool answer = await DisplayAlert("Question?", "¿Desea Borrar la zona \"" + Place.Name + "\"?", "Si", "No");
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
    /// <summary>
    /// Method to remove a Worker from WorkGroup
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void RemoveWorkerFROMWorkGroup(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new BorraTrabajadorDeGrupo(Username, WorkGroup));
    }
    /// <summary>
    /// Method to remove a WorkShift from a WorkGroup
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void RemoveWorkShiftFROMWorkGroup(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AnadirTurnoEquipoTrabajo(Username, WorkShift.Name, 1));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Añadir turno a equipo de trabajo' - " + dt), p);
    }
    /// <summary>
    /// Method to delete a Day
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private async void DeleteDaysButton_Clicked(object sender, EventArgs e)
    {

        bool respuesta = await DisplayAlert("¿Estás segur@?", "¿Deseas eliminar este dia?", "Si", "No");
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
    /// <summary>
    /// Method to delete a WorkTask
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private async void DeleteWorkTaskButton_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("¿Estás segur@?", "¿Estas seguro de borrar la tarea \"" + WorkTask.Name + "\"?", "Si", "No");
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
    /// <summary>
    /// Method to enable the calendar UI
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private async void CalendarButton_Clicked(object sender, EventArgs e)
    {
        ListViewCalendar.IsVisible = true;
        ListViewCalendar.IsEnabled = true;
        ListViewWorkers.IsVisible = false;
        ListViewWorkers.IsEnabled = false;

        var ExistsOnCalendar = p.Calendars.Where(x => x.Worker == Worker).Include(x=>x.DaysOnCalendar).FirstOrDefault();
        if(ExistsOnCalendar is not null)
        {
            var DayList = ExistsOnCalendar.DaysOnCalendar.ToList();
            ListViewCalendario.ItemsSource = DayList;
            if (DayList.Count > 0)
                ListViewCalendario.SelectedItem = DayList[0];
        }
        else
        {
            await DisplayAlert("Error", "El trabajador no dispone de calendario, se procede a crearlo", "Vale");
            var worker = p.Workers.Where(x => x.CardNumber == Worker.CardNumber).FirstOrDefault();
            var calendar = new Calendar(worker);
            p.Calendars.Add(calendar);
            p.SaveChanges();
            await DisplayAlert("Alert", "Calendario creado con exito", "Vale");
        }
        DaysCheck(Worker);
    }
    /// <summary>
    /// Method to back to the workers screen
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void BackToWorkersWindowButton_Clicked(object sender, EventArgs e)
    {
        ListViewCalendar.IsVisible = false;
        ListViewCalendar.IsEnabled = false;
        ListViewWorkers.IsVisible = true;
        ListViewWorkers.IsEnabled = true;
    }
    /// <summary>
    /// Method to Logout session
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private async void LogoutButton_Clicked(object sender, EventArgs e)
    {
        //BotonCerrarSession.BackgroundColor = Color.FromRgba("#2B282D");
        bool answer = await DisplayAlert("Logout", "�Deseas cerrar sesi�n?", "Si", "No");
        if (answer == true)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
    /// <summary>
    /// Method to check the worker free days
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private async void DaysCheck(Worker tr)
    {
        var signingList = p.Signings.Where(x => x.Worker == tr).ToList();
        var workerCalendar = p.Calendars.Where(x => x.Worker == tr).Include(x => x.DaysOnCalendar).FirstOrDefault();
        var freeDaysList = workerCalendar.DaysOnCalendar.ToList();
        var signingDates = Signing.GetFechas(signingList);
        var holidaysDate = Day.GetFechas(freeDaysList);
        foreach (DateTime datetime in holidaysDate)
        {
            if (signingDates.Contains(datetime))
            {

                var dayList = p.DayOff.ToList();
                foreach (Day day in dayList)
                {
                    if (day.BelongCalendar == workerCalendar && day.Date == datetime)
                    {
                        day.Enjoyed = true;
                        p.DayOff.Update(day);
                        p.SaveChanges();
                    }
                    else
                    {
                        day.Enjoyed = false;
                        p.DayOff.Update(day);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Method to check the signed workers
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void SignedWorkersButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new TrabajadoresEnTurno(Username));
        db.InsertLog(new Log("Acceso", Username + " Accede a 'Trabajadores en turno' - " + dt),p);
    }
    public async void IssueCheck()
    {
        try
        {
            var IssuesList = p.Issues.Where(x=>x.Justified==false).Include(x=>x.Worker).ToList();
            ListViewIncidencias.ItemsSource = IssuesList;
            if (IssuesList.Count > 0)
            {
                IssueButton.Source = "problemasactivo.png";
                ListViewIncidencias.SelectedItem = IssuesList[0];
            }
            else
            {
                IssueButton.Source = "problemas.png";
            }
        }
        catch (NullReferenceException ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }
    /// <summary>
    /// Method to generate a worker's payroll
    /// </summary>
    /// <param Worker="worker"></param>
    public void GeneratePayrollToPDF(Worker worker)
    {
        try
        {
            PdfWriter writer = new PdfWriter("C:\\Users\\jesus\\Desktop\\HolaMundoMAUI\\Nominas\\demo2.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("Nomina mensual")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
               .SetFontSize(20);
            BetterTable DatosEmpresa = NewTable(3, 2);
            DatosEmpresa.SetColorHeader(ColorConstants.LIGHT_GRAY);
            DatosEmpresa.ChangeTableFontSize(8);
            DatosEmpresa.SetNextText("Empresa");
            DatosEmpresa.SetNextText("Direccion");
            DatosEmpresa.SetNextText("CIF");
            DatosEmpresa.SetNextText("TTI Ventures");
            DatosEmpresa.SetNextText(" C. Paletillas,6,Calahorra");
            DatosEmpresa.SetNextText("B26557397");
            BetterTable DatosTrabajador = NewTable(5, 2);
            DatosTrabajador.SetColorHeader(ColorConstants.LIGHT_GRAY);
            DatosTrabajador.ChangeTableFontSize(8);
            DatosTrabajador.SetNextText("Trabajador");
            DatosTrabajador.SetNextText("Categoria");
            DatosTrabajador.SetNextText("N� Matricula");
            DatosTrabajador.SetNextText("Antiguedad");
            DatosTrabajador.SetNextText("D.N.I");
            DatosTrabajador.SetNextText(worker.Name);
            DatosTrabajador.SetNextText(worker.Category);
            DatosTrabajador.SetNextText("");
            DatosTrabajador.SetNextText((dt - worker.HiringDate).ToString());
            DatosTrabajador.SetNextText(worker.Nif);
            BetterTable OtrosDatos = NewTable(7, 2);
            OtrosDatos.SetColorHeader(ColorConstants.LIGHT_GRAY);
            OtrosDatos.ChangeTableFontSize(8);
            OtrosDatos.SetNextText("N� Afiliaci�n S.S");
            OtrosDatos.SetNextText("Tarifa");
            OtrosDatos.SetNextText("Cod C.T.");
            OtrosDatos.SetNextText("Secci�n");
            OtrosDatos.SetNextText("Nro.");
            OtrosDatos.SetNextText("Periodo");
            OtrosDatos.SetNextText("D�as");
            OtrosDatos.SetNextText(worker.SocialSecurityCard);
            var diasTrabajador = p.Signings.Where(x => x.Worker == worker).Where(x => x.CheckInCheckOut == "Entrada").OrderBy(x => x.SigningDate).ToList();
            var diastotal = diasTrabajador.Count;
            var lapso = diasTrabajador.First().SigningDate.Date + "-" + diasTrabajador.Last().SigningDate.Date;
            OtrosDatos[1,6].SetText(lapso.ToString());
            OtrosDatos[1,5].SetText(diastotal.ToString());
            BetterTable CuerpoNomina = NewTable(5, 8);
            CuerpoNomina.SetColorHeader(ColorConstants.LIGHT_GRAY);
            CuerpoNomina.ChangeTableFontSize(10);
            CuerpoNomina.RemoveBorder(1);
            CuerpoNomina.AddTableBorder(1);
            CuerpoNomina.SetNextText("Cuant�a");
            CuerpoNomina.SetNextText("Precio");
            CuerpoNomina.SetNextText("Concepto");
            CuerpoNomina.SetNextText("Devengos");
            CuerpoNomina.SetNextText("Deducciones");
            BetterTable PieNomina1 = NewTable(7, 2);
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
            BetterTable PieNomina2 = NewTable(4, 5);
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
            PieNomina2[2, 3].SetText("1245.74�");
            PieNomina2[2, 3].AddAllBorders();
            PieNomina2.AddTableBorder(1);
            BetterTable CabeceraPieNomina3 = NewTable(1, 1);
            CabeceraPieNomina3.ChangeTableFontSize(8);
            CabeceraPieNomina3.RemoveBorder(2);
            CabeceraPieNomina3.TableConjunctionDown();
            CabeceraPieNomina3.SetNextText("DETERMINACION DE LA B. DE COTIZACION A LA S.S. Y CONCEPTOS DE RECAUCACION CONJUNTA Y APORTACION A LA EMPRESA");
            BetterTable PieNomina3 = NewTable(5, 8);
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
            PieNomina3[6, 0].SetText("3. Cotizaci�n horas extraordinarias");
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
    /// <summary>
    /// 
    /// </summary>
    /// <param int="numeroColumnas"></param>
    /// <param int="numeroFilas"></param>
    /// <returns></returns>
    public BetterTable NewTable(int numeroColumnas, int numeroFilas)
    {
        BetterTable b = new BetterTable(numeroColumnas, numeroFilas);
        return b;
    }
}