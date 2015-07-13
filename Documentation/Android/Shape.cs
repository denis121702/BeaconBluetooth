namespace AltBeaconLibrary.Sample.Droid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AltBeaconLibrary.ContractAndroid;
    using AltBeaconLibrary.WCF;

    using Android.App;
    using Android.Content;
    using Android.Graphics;
    using Android.Views;

    using WirelessCalculationContract;

    /// <summary>
    /// This is our class responsible for drawing our shapes
    /// </summary>
    public class Shape : View
    {        
        public ShapeView ShapeView { get; set; }

        protected override void OnAttachedToWindow()
        {
            if (ShapeView.BeaconType == BeaconType.StartProcess)
            {
                ProxyServiceAgent.GetAccessPointList();

                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 3000;
                timer.Enabled = true;
                timer.Elapsed += (sender, e) => Task.Run(() =>
                    {
                        List<SharedBeacon> list =
                            new List<SharedBeacon>(GlobalSharedBeaconCollection.observableItems);

                        List<SharedBeacon> sortQualityAccessPointList = new List<SharedBeacon>();
                        if (GlobalSharedBeaconCollection.accessPointListServer.Count > 0)
                        {
                            sortQualityAccessPointList
                                = list.Where(kvp => 
                                             GlobalSharedBeaconCollection.accessPointListServer.Exists(m => m.Bssid == kvp.Bssid)).ToList();
                        }

                        ProxyServiceAgent.GetMeasurePosition(sortQualityAccessPointList);

                    });                

                ProxyServiceAgent.AddPosition += (sender, e) =>
                {
                    List<Position> positionList = e.Data;
                    foreach (Position position in positionList)
                    {
                        if (position.DeviceId == ProxyServiceAgent.DeviceId)
                        {
                            this._posX = position.X;
                            this._posY = position.Y;
                        }
                    }

                    ((Activity)Xamarin.Forms.Forms.Context).RunOnUiThread(() =>
                    {                        
                        this.Invalidate();
                    });
                };
            }
        }
      
        public Shape(Context context) : base(context)
        {
            this.SetBackgroundColor(Color.Blue);            
        }            

        private float _posX = 0;
        private float _posY = 0;

        public override bool OnTouchEvent(MotionEvent ev)
        {
            if (ShapeView.BeaconType == BeaconType.StartProcess)
            {
                return true;
            }

            MotionEventActions action = ev.Action & MotionEventActions.Mask;

            if (action == MotionEventActions.Down)
            {
                this._posX = ev.GetX();
                this._posY = ev.GetY();

                GlobalSharedBeaconCollection.PosX = this._posX;
                GlobalSharedBeaconCollection.PosY = this._posY;

                this.Invalidate();
            }

            return true;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            canvas.Save();

            //canvas.Translate(_posX, _posY);

            string textPoint = string.Empty;
            if (this.ShapeView.BeaconType == BeaconType.AccessPoint)
            {
                textPoint = this.ShapeView.sharedBeaconItem.Bssid;
            }
            else if (this.ShapeView.BeaconType == BeaconType.MeasurePoint)
            {
                textPoint = string.Format("X={0},Y={1}", this._posX, this._posY);
            }
            //else if (this.ShapeView.BeaconType == BeaconType.StartProcess)
            //{
            //    textPoint = "I love you.";
            //}

            this.HandleShapeDraw(
                    canvas,
                    this._posX,
                    this._posY,
                    textPoint,
                    this.ShapeView.BeaconType);

            foreach (var item in GlobalSharedBeaconCollection.currentAccessPoint)
            {
                this.HandleShapeDraw(canvas, item.PosX, item.PosY, item.Bssid, BeaconType.AccessPoint);
            }

            foreach (var item in GlobalSharedBeaconCollection.currentMeasurePoint)
            {
                this.HandleShapeDraw(canvas, item.PosX, item.PosY, string.Format("X={0},Y={1}", item.PosX, item.PosY), BeaconType.MeasurePoint);
            }

            canvas.Restore();
        }

        private void HandleShapeDraw(Canvas canvas, float x, float y, string text, BeaconType beaconType)
        {            
            //case ShapeType.Box:
            //    HandleStandardDraw (canvas, p => {
            //        var rect = new RectF (x, y, x + this.Width, y + this.Height);
            //        if (ShapeView.CornerRadius > 0) {
            //            var cr = Resize (ShapeView.CornerRadius);
            //            canvas.DrawRoundRect (rect, cr, cr, p);
            //        } else {
            //            canvas.DrawRect (rect, p);
            //        }
            //    });
            //    break;
            // case ShapeType.Circle:

            int radius = beaconType == BeaconType.StartProcess ? 45 : 20;

            this.HandleStandardDraw(canvas, p => canvas.DrawCircle(x, y, radius, p), beaconType);
            this.HandleStandardDraw(canvas, p => { p.TextSize = 50; canvas.DrawText(text, x, y + 40, p); }, beaconType);                    
        }

        /// <summary>
        /// A simple method that handles drawing our shape with the various colours we need
        /// </summary>
        /// <param name="canvas">
        /// Canvas.
        /// </param>
        /// <param name="drawShape">
        /// Draw shape.
        /// </param>
        /// <param name="beaconType">
        /// The beacon Type.
        /// </param>
        private void HandleStandardDraw(Canvas canvas, Action<Paint> drawShape, BeaconType beaconType)
        {
            var strokePaint = new Paint(PaintFlags.AntiAlias);
            strokePaint.SetStyle(Paint.Style.FillAndStroke);            
            
            strokePaint.StrokeCap = Paint.Cap.Round;
            strokePaint.Color = beaconType == BeaconType.MeasurePoint ? Color.Yellow : Color.Black;
            
            drawShape(strokePaint);
        }     
    }
}