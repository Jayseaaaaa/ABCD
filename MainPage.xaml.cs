namespace Page_3;

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnOpenMapClicked(object sender, EventArgs e)
        {
            var location = new Location(37.7749, -122.4194);
            var options = new MapLaunchOptions { Name = "San Francisco" };

            await Map.Default.OpenAsync(location, options);
        }
    }
}

}
