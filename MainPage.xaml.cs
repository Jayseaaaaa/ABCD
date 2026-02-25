namespace Page_2
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
        private void OnSubmitClicked(object sender, EventArgs e)
        {
            string userName = foodEntry.Text;


            if (!string.IsNullOrEmpty(userName))
            {

                DisplayAlert("There are no such thing like that ", $"{userName}!", "OK");

            }
            else
            {
                DisplayAlert("Error", "Please ENTER SOMETHING.", "OK");
            }

        }
        private void OnImageButtonClicked(object sender, EventArgs e)
        {

            DisplayAlert(
                "Fried Chicken", "Calories: 140 calories for a wing " +
                "\n340 calories for a large breast. " +
                "\nProtein: 30 grams of protein per serving (200 grams). " +
                "\nFat: 25 grams of fat per serving. " +
                "\nCarbohydrates: 20 grams of carbohydrates per serving. ", "OK");
        }
    }

}
