using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AltBeaconLibrary.Sample
{
	public class TodoListPage : ContentPage
	{
		public TodoListPage()
		{	
			// XAML sample
			//return new SamplePage ();

			//var screenSize = DependencyService.Get<IScreen>();

			//double width = 200;
			//double height = 500;

			/*
            Button button1 = new Button
            {
                Text = " Go to Label Demo Page ",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1
            };
            button1.Clicked += (sender, args) =>
            {
                Navigation.PushAsync(new LabelDemoPage());
            };
			*/

			Title = "denis test";
			Content = new StackLayout()
			{
				Children = {

					new Label
					{
						Text = "Map",
						FontSize = 20,
						FontAttributes = FontAttributes.Bold,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						BackgroundColor = Color.Green,

					},
					//button1,
					//new Label () {
					//    Text = "Hello world"
					//},
					//new BoxView () {
					//    Color = Color.Red
					//},

					/*new ShapeView () {
						ShapeType = ShapeType.Circle,
						StrokeColor = Color.Blue,
						Color = Color.Yellow,
						StrokeWidth = 5f
					}
					*/
				},

				//TranslationY = 80,
				//        TranslationX = 20
				//Orientation = StackOrientation.Horizontal,
				BackgroundColor = Color.White,
				//Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.Fill,
				Orientation = StackOrientation.Vertical,
				Spacing = 0 
					//HorizontalOptions = LayoutOptions.FillAndExpand,
					//VerticalOptions = LayoutOptions.FillAndExpand,
					//TranslationX = 150,
					//TranslationY = 100
			};

		}

	}
}

