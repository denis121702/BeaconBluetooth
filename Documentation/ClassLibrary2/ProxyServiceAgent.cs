namespace AltBeaconLibrary.WCF
{
    using System;
    using System.Linq;
    using System.ServiceModel;

    using WirelessCalculationContract;
    using System.Collections.Generic;

    using AltBeaconLibrary.ContractAndroid;

    using ClassLibrary2;

    using WirelessCalculationContract.DataContract;

    public class ProxyServiceAgent
    {
        public static Guid DeviceId { get; set; }

        private static EndpointAddress endPoint = new EndpointAddress("http://dennis-gladun.de/WiFi.svc");

        private static BasicHttpBinding binding;

        public static event EventHandler<PositionEventArgs> AddPosition;
        
        static ProxyServiceAgent()
        {
            DeviceId = Guid.NewGuid();
            binding = CreateBasicHttpBinding();
        }

        private static BasicHttpBinding CreateBasicHttpBinding()
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding
            {
                Name = "basicHttpBinding",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };

            TimeSpan timeout = new TimeSpan(0, 0, 30);
            basicHttpBinding.SendTimeout = timeout;
            basicHttpBinding.OpenTimeout = timeout;
            basicHttpBinding.ReceiveTimeout = timeout;

            return basicHttpBinding;
        }

        public static void GetAccessPointList()
        {
            WirelessCalculationClient client = new WirelessCalculationClient(binding, endPoint);
            client.GetAccessPointListCompleted += client_GetAccessPointListCompleted;

            client.GetAccessPointListAsync();
        }

        static void client_GetAccessPointListCompleted(object sender, GetAccessPointListCompletedEventArgs e)
        {            
            if (e.Error == null && e.Cancelled == false)
            {
                List<SharedBeacon> resultList = e.Result.Select(m => 
                    new SharedBeacon()
                        {                            
                            Bssid = m.Bssid                          
                        }).ToList();

                GlobalSharedBeaconCollection.accessPointListServer.Clear();
                GlobalSharedBeaconCollection.accessPointListServer.AddRange(resultList);
            }

            var clientObject = sender as WirelessCalculationClient;
            if (clientObject != null &&
                clientObject.State == System.ServiceModel.CommunicationState.Opened)
            {
                clientObject.CloseAsync();
            }

            GC.Collect();
        }

        public static void DeleteAllData()
        {          
            WirelessCalculationClient client = new WirelessCalculationClient(binding, endPoint);
            client.DeleteAllDataCompleted += client_DeleteAllDataCompleted;

            client.DeleteAllDataAsync();
        }

        static void client_DeleteAllDataCompleted(object sender, DeleteAllDataCompletedEventArgs e)
        {
            var clientObject = sender as WirelessCalculationClient;
            if (clientObject != null &&
                clientObject.State == System.ServiceModel.CommunicationState.Opened)
            {
                clientObject.CloseAsync();
            }

            GC.Collect();
        }

        public static void GetMeasurePosition(List<SharedBeacon> accessPointList)
        {
            if (accessPointList == null || accessPointList.Count == 0)
            {
                return;
            }

            List<MeasurePoint> measurePointList = accessPointList.Select(x => new MeasurePoint
            {
                Bssid = x.Bssid,
                Ssid = x.Bssid,
                RssiLevel = x.RssiLevel,
                CenterFrequency = x.CenterFrequency,
                Quality = 0,
                Constant = 0
            }).ToList();

            WirelessCalculationClient client = new WirelessCalculationClient(binding, endPoint);
            client.GetMeasurePositionCompleted += _client_GetMeasurePositionCompleted;

            client.GetMeasurePositionAsync(DeviceId, measurePointList.ToArray());
        }

        static void _client_GetMeasurePositionCompleted(object sender, GetMeasurePositionCompletedEventArgs e)
        {
            if (e.Error == null && e.Cancelled == false)
            {
                var handler = AddPosition;
                if (handler != null)
                {
                    List<Position> positionList = e.Result.ToList();
                    handler(null, new PositionEventArgs(positionList));
                }
            }

            var clientObject = sender as WirelessCalculationClient;
            if (clientObject != null &&
                clientObject.State == System.ServiceModel.CommunicationState.Opened)
            {
                clientObject.CloseAsync();
            }

            GC.Collect();
        }

        public static void SetAccessPoint(SharedBeacon sharedBeaconItem)
        {
            if (sharedBeaconItem == null)
            {
                return;
            }

            AccessPoint accessPoint = new AccessPoint
            {
                Bssid = sharedBeaconItem.Bssid,
                X = (int)sharedBeaconItem.PosX,
                Y = (int)sharedBeaconItem.PosY,
                XReal = (int)sharedBeaconItem.PosX,
                YReal = (int)sharedBeaconItem.PosY,
                Ssid = sharedBeaconItem.RssiLevel.ToString(),
            };

            WirelessCalculationClient client = new WirelessCalculationClient(binding, endPoint);
            client.SetAccessPointCompleted += _client_SetAccessPointCompleted;

            client.SetAccessPointAsync(accessPoint);
            //var res = Task<int>.Factory.FromAsync (_client.SetAccessPointAsync, _client.SetAccessPointCompleted, value1, null, null);                           
        }

        static void _client_SetAccessPointCompleted(object sender, SetAccessPointCompletedEventArgs e)
        {
            //string msg = null;

            //if (e.Error != null)
            //{
            //    msg = e.Error.Message;
            //}
            //else if (e.Cancelled)
            //{
            //    msg = "Request was cancelled.";
            //}
            //else
            //{
            //    msg = e.Result.ToString();
            //}

            //RunOnUiThread(() => MobileDetailsTXT.Text = msg);

            var clientObject = sender as WirelessCalculationClient;
            if (clientObject != null &&
                clientObject.State == System.ServiceModel.CommunicationState.Opened)
            {
                clientObject.CloseAsync();
            }

            GC.Collect();
        }


        public static void SetMeasurePoint(List<SharedBeacon> list, System.Drawing.Point position)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }

            List<MeasurePoint> measurePointList = list.Select(x => new MeasurePoint
            {
                Bssid = x.Bssid,
                Ssid = x.Bssid,
                RssiLevel = x.RssiLevel,
                CenterFrequency = x.CenterFrequency,
                Quality = 0,
                Constant = 0
            }).ToList();           

            MeasureSector measureSector = new MeasureSector();
            measureSector.ApList = measurePointList.ToArray();
            measureSector.X = 1;
            measureSector.Y = 2;
            measureSector.XReal = 3;
            measureSector.YReal = 4;

            WirelessCalculationClient client = new WirelessCalculationClient(binding, endPoint);
            client.SetMeasurePointCompleted += _client_SetMeasurePointCompleted;
            client.SetMeasurePointAsync(measureSector);
        }

        static void _client_SetMeasurePointCompleted(object sender, SetMeasurePointCompletedEventArgs e)
        {
            var clientObject = sender as WirelessCalculationClient;
            if (clientObject != null &&
                clientObject.State == System.ServiceModel.CommunicationState.Opened)
            {
                clientObject.CloseAsync();
            }

            GC.Collect();
        }      
    }
}