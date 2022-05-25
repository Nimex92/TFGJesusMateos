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

    public AltaZona(string username)
	{
		InitializeComponent();
        Username= username;
    }
    public AltaZona(string username,string place,int option)
    {
        InitializeComponent();
        Username = username;
        Place = place;
        switch (option)
        {
            case 0:
                RegisterButton.IsVisible = true;
                UpdateButton.IsVisible = false;
                NameField.Text = "";
                break;
            case 1:
                RegisterButton.IsVisible = false;
                UpdateButton.IsVisible = true;
                NameField.Text = place;
                break;
        }
    }

    private void BackToMain(object sender,EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,4));
	}
    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
		bool inserta = db.InsertPlace(NameField.Text,p);
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

    private void UpdateButton_Clicked(object sender, EventArgs e)
    {
        var place = p.Places.Where(x => x.Name == Place).FirstOrDefault();
        db.UpdatePlace(place,NameField.Text, p);
        db.InsertLog(new Log("Añadir", Username + " ha modificado a " + place.Name + " - " + now), p);
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,4));
    }
}