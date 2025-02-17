using Assignment9.Models;
using System;
namespace Assignment9.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
		LoadProfile();
	}

    private async void LoadProfile()
    {
        var profile = await App.Database.GetProfileAsync();
        if (profile != null)
        {
            nameEntry.Text = profile.Name;
            surnameEntry.Text = profile.Surname;
            emailEntry.Text = profile.Email;
            bioEditor.Text = profile.Bio;
        }
    }

    private async void OnSaveProfileClicked(object sender, EventArgs e)
    {
        var profile = new Profile
        {
            Name = nameEntry.Text,
            Surname = surnameEntry.Text,
            Email = emailEntry.Text,
            Bio = bioEditor.Text
        };

        await App.Database.SaveProfileAsync(profile);
        await DisplayAlert("Success", "Profile saved!", "OK");
    }

    private async void OnGoToShoppingListClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ShoppingListPage");
    }


}   
