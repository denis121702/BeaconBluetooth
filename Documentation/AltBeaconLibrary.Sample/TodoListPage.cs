namespace AltBeaconLibrary.Sample
{
    using System.Collections.Generic;

    using AltBeaconLibrary.ContractAndroid;
    using AltBeaconLibrary.WCF;

    using Xamarin.Forms;

    /// <summary>
    /// The todo list page.
    /// </summary>
    public class TodoListPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TodoListPage"/> class.
        /// </summary>
        /// <param name="sharedBeaconItem">
        /// The shared beacon item.
        /// </param>
        /// <param name="beaconType">
        /// The beacon type.
        /// </param>
        public TodoListPage(SharedBeacon sharedBeaconItem, BeaconType beaconType) 
        {
            Button button1 = new Button
            {
                Text = "1. Save Position",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 0
            };

            button1.Clicked += (sender, args) =>
            {
                sharedBeaconItem.PosX = GlobalSharedBeaconCollection.PosX;
                sharedBeaconItem.PosY = GlobalSharedBeaconCollection.PosY;

                if (beaconType == BeaconType.AccessPoint)
                {
                    GlobalSharedBeaconCollection.currentAccessPoint.Add(sharedBeaconItem);
                    
                    ProxyServiceAgent.SetAccessPoint(sharedBeaconItem);                
                }
                else if (beaconType == BeaconType.MeasurePoint)
                {
                    GlobalSharedBeaconCollection.currentMeasurePoint.Add(sharedBeaconItem);

                    List<SharedBeacon> list = new List<SharedBeacon>(GlobalSharedBeaconCollection.observableItems);

                    ProxyServiceAgent.SetMeasurePoint(
                        list,
                        new System.Drawing.Point(
                            (int)GlobalSharedBeaconCollection.PosX,
                            (int)GlobalSharedBeaconCollection.PosY));
                }                

                GlobalSharedBeaconCollection.PosX = 0;
                GlobalSharedBeaconCollection.PosY = 0;
                               
                Navigation.PopAsync();
            };

            if (beaconType == BeaconType.StartProcess)
            {
                button1.IsEnabled = false;
            }            

            var parent = new StackLayout();            
            parent.Children.Add(button1);
            parent.Children.Add(new ShapeView(sharedBeaconItem)
            {
                   BeaconType = beaconType
            });
            
            parent.BackgroundColor = Color.White;
            parent.VerticalOptions = LayoutOptions.Fill;
            parent.Orientation = StackOrientation.Vertical;
            parent.Spacing = 0;

            this.Content = parent;
            this.Title = "Beacon Map";
        }
    }
}
