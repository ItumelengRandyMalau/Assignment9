using Assignment9.Models;
namespace Assignment9.Views;

public partial class ShoppingCartPage : ContentPage
{
    private int _profileId;

    public ShoppingCartPage(int profileId)
    {
        InitializeComponent();
        _profileId = profileId;
        LoadShoppingCart();
    }

    private async void LoadShoppingCart()
    {
        var cartItems = await App.Database.GetShoppingCartAsync(_profileId);
        var items = new List<ShoppingItem>();

        foreach (var cartItem in cartItems)
        {
            var item = await App.Database.GetShoppingItemAsync(cartItem.ItemId);
            if (item != null)
            {
                items.Add(item);
            }
        }

        cartListView.ItemsSource = items;
    }

    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return; // Prevent re-selecting

        var selectedItem = (ShoppingItem)e.SelectedItem;
        DisplayAlert("Selected Item", $"{selectedItem.Name} - ${selectedItem.Price}", "OK");

        // Clear selection
        ((ListView)sender).SelectedItem = null;
    }

    private async void OnRemoveFromCartClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var item = (ShoppingItem)button.CommandParameter;

        if (item == null)
            return;

        var cartItem = await App.Database.GetCartItemAsync(_profileId, item.Id);
        if (cartItem != null)
        {
            await App.Database.RemoveFromCartAsync(cartItem);
            await DisplayAlert("Success", "Item removed from cart", "OK");
            LoadShoppingCart(); // Refresh cart view
        }
    }
}