using Assignment9.Models;
namespace Assignment9.Views;

public partial class ShoppingListPage : ContentPage
{
	public ShoppingListPage()
	{
		InitializeComponent();
        LoadShoppingItems();
    }

    private async void LoadShoppingItems()
    {
        shoppingListView.ItemsSource = await App.Database.GetShoppingItemsAsync();
    }
    // ? FIX: Add the missing OnItemSelected method
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is ShoppingItem)
        {
            var profile = await App.Database.GetProfileAsync();
            if (profile != null)
            {
                await Navigation.PushAsync(new ShoppingCartPage(profile.Id)); // ? Pass profileId
            }
            else
            {
                await DisplayAlert("Error", "Profile not found", "OK");
            }
        }
    }



    private async void OnAddToCartClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var selectedItem = (ShoppingItem)button.CommandParameter;

        if (selectedItem == null)
        {
            await DisplayAlert("Error", "Item not found", "OK");
            return;
        }

        var profile = await App.Database.GetProfileAsync();
        if (profile == null)
        {
            await DisplayAlert("Error", "Profile not found", "OK");
            return;
        }

        var cartItem = new ShoppingCart
        {
            ProfileId = profile.Id,
            ItemId = selectedItem.Id,
            Quantity = 1 // Default quantity is 1
        };

        await App.Database.AddToCartAsync(cartItem);
        await DisplayAlert("Success", $"{selectedItem.Name} added to cart!", "OK");
    }
}