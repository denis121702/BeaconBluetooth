namespace AltBeaconLibrary.Sample
{
    using System;
    using System.Linq;

    using AltBeaconLibrary.ContractAndroid;
    using AltBeaconLibrary.WCF;

    using Java.Lang;

    using Xamarin.Forms;

    /// <summary>
    /// The main page.
    /// </summary>
    public class MainPage : ContentPage
    {
        private ListView listView;
        private BeaconViewModel viewModel;

        public MainPage()
        {            
            this.viewModel = new BeaconViewModel();
            this.viewModel.ListChanged += (sender, e) =>
            {
                if (viewModel.Data.Count == 0)
                {
                    return;
                }

                viewModel.Data.ForEach(x =>
                {
                    var item = GlobalSharedBeaconCollection.observableItems.FirstOrDefault(i => i.Bssid == x.Bssid);
                    if (item != null)
                    {
                        int i = GlobalSharedBeaconCollection.observableItems.IndexOf(item);
                        GlobalSharedBeaconCollection.observableItems[i] = x;
                    }
                    else
                    {
                        GlobalSharedBeaconCollection.observableItems.Add(x);
                    }
                });
            };

            this.viewModel.Init();

            this.BackgroundColor = Color.White;
            this.Title = "Beacon Searcher";            
            this.BindingContext = viewModel;
            this.Content = BuildContent();
        }

        protected override bool OnBackButtonPressed()
        {
            var beaconService = Xamarin.Forms.DependencyService.Get<IAltBeaconService>();
            beaconService.StopMonitoringAndRanging();    

            GC.Collect();

            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());

            // iOS
            //Thread.CurrentThread.Abort();
            
            return true;
        }

        private StackLayout BuildContent()
        {
            var layout = new StackLayout();

            var setMeasurePointButton = new Button { Text = "2. Set Measure Point" };
            setMeasurePointButton.Clicked += (sender, e) =>
                {
                    var todoListPage = new TodoListPage(new SharedBeacon(), BeaconType.MeasurePoint);
                    Navigation.PushAsync(todoListPage);
                };

            var startButton = new Button { Text = "3. Start" };
            startButton.Clicked += (sender, e) =>
                {
                    var todoListPage = new TodoListPage(new SharedBeacon(), BeaconType.StartProcess);
                    Navigation.PushAsync(todoListPage);
                };

            var deleteAllButton = new Button { Text = "4. Delete Points" };
            deleteAllButton.Clicked += (sender, e) =>
                {
                   ProxyServiceAgent.DeleteAllData();
                };


            this.listView = new ListView { ItemTemplate = new DataTemplate(typeof(ListItemView)), RowHeight = 90, };
            this.listView.ItemSelected += (sender, e) =>
                {
                    var sharedBeaconItem = (SharedBeacon)e.SelectedItem;
                    var todoListPage = new TodoListPage(sharedBeaconItem, BeaconType.AccessPoint);
                    Navigation.PushAsync(todoListPage);
                };

            this.listView.SetBinding(ListView.ItemsSourceProperty, "Data");

            layout.Children.Add(setMeasurePointButton);
            layout.Children.Add(startButton);
            layout.Children.Add(deleteAllButton);
            layout.Children.Add(this.listView);
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.BackgroundColor = Color.Yellow;

            return layout;
        }

        //void startButton_Clicked(object sender, EventArgs e)
        //{
        //    var beaconService = Xamarin.Forms.DependencyService.Get<IAltBeaconService>();
        //    beaconService.StartMonitoring();
        //    beaconService.StartRanging();
        //}

        //void stopButton_Clicked(object sender, EventArgs e)
        //{
        //    //var beaconService = Xamarin.Forms.DependencyService.Get<IAltBeaconService>();
        //    //beaconService.StopMonitoringAndRanging();    
        //}       

        protected override void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = GlobalSharedBeaconCollection.observableItems;
        }
    }
}