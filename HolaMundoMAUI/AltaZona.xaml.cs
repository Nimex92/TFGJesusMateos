namespace HolaMundoMAUI;
using Persistencia;
using ClassLibray;

public partial class AltaZona : ContentPage
{
    PresenciaContext p = new PresenciaContext();
    string Username;
    DateTime now = DateTime.Now;
    string Place;
    Db db = new Db();
    /// <summary>
    /// Constructor that receives the username to persist the data
    /// </summary>
    /// <param string="username"></param>
    public AltaZona(string username)
	{
		InitializeComponent();
        //Set the Username variable to a global local use
        Username= username;
    }
    /// <summary>
    /// Constructor that receives the username and placename to persist the data
    /// and Receive an option to enable create or update UI
    /// </summary>
    /// <param string="username"></param>
    /// <param string="place"></param>
    /// <param int="option"></param>
    public AltaZona(string username,string place,int option)
    {
        InitializeComponent();
        //Set the username and the place variable to a global local use
        Username = username;
        Place = place;
        switch (option)
        {
            //In this case enable the create UI
            case 0:
                RegisterButton.IsVisible = true;
                UpdateButton.IsVisible = false;
                NameField.Text = "";
                break;
            //In this case enable Update UI
            case 1:
                RegisterButton.IsVisible = false;
                UpdateButton.IsVisible = true;
                NameField.Text = place;
                break;
        }
    }
    /// <summary>
    /// Method to back to the MainPage
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void BackToMain(object sender,EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,4));
	}
    /// <summary>
    /// Method to add a new Place to DB
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        //Insert Method 
		bool inserta = db.InsertPlace(NameField.Text,p);
        //Insert Method
        db.InsertLog(new Log("Añadir", Username + " ha añadido grupo de trabajo " + NameField.Text + " - " + now),p);
        if (inserta == true)
        {
			await DisplayAlert ("Success","Zona unsertada correctamente","OK");
            App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,4));
        }
        else
        {
            await DisplayAlert("Error", "Error al insertar", "OK");
        }
		
    }
    /// <summary>
    /// Method to Update an Place from Db
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void UpdateButton_Clicked(object sender, EventArgs e)
    {
        var place = p.Places.Where(x => x.Name == Place).FirstOrDefault();
        db.UpdatePlace(place,NameField.Text, p);
        db.InsertLog(new Log("Añadir", Username + " ha modificado a " + place.Name + " - " + now), p);
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,4));
    }
}