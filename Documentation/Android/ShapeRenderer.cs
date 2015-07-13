using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;
using AltBeaconLibrary.Sample;
using AltBeaconLibrary.Sample.Droid;

[assembly: ExportRenderer(typeof(ShapeView), typeof(ShapeRenderer))]
namespace AltBeaconLibrary.Sample.Droid
{
    public class ShapeRenderer : ViewRenderer<ShapeView, Shape>
    {
        public ShapeRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ShapeView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            Element.VerticalOptions = LayoutOptions.FillAndExpand;


            SetNativeControl(new Shape(Context)
            {

                ShapeView = Element
            });
        }
    }
}