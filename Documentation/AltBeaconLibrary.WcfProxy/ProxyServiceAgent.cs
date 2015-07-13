using System;
using System.ServiceModel;
using WirelessCalculationContract;

namespace AltBeaconLibrary.WCF
{
    using System.Collections.Generic;

    using AltBeaconLibrary.ContractAndroid;

    using WirelessCalculationContract.DataContract;

    public class ProxyServiceAgent
    {        
        //private static EndpointAddress endPoint = new EndpointAddress("http://172.17.100.169:63437/WiFi.svc");

        private static EndpointAddress endPoint = new EndpointAddress("http://dennis-gladun.de/WiFi.svc");
        
        private static BasicHttpBinding binding;

        static ProxyServiceAgent()
        {
            binding = CreateBasicHttpBinding();
        }

        private static BasicHttpBinding CreateBasicHttpBinding()
        {
            BasicHttpBinding binding = new BasicHttpBinding
            {
                Name = "basicHttpBinding",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };

            TimeSpan timeout = new TimeSpan(0, 0, 30);
            binding.SendTimeout = timeout;
            binding.OpenTimeout = timeout;
            binding.ReceiveTimeout = timeout;
            return binding;
        }
    
        public static void SetAccessPoint(SharedBeacon sharedBeaconItem)
        {
            //if (sharedBeaconItem == null)
            //{
            //    return;
            //}

            WirelessCalculationClient _client = new WirelessCalculationClient(binding, endPoint);

            AccessPoint accessPoint = new AccessPoint
            {
                Bssid = sharedBeaconItem.Bssid,
                X = (int)sharedBeaconItem.PosX,
                Y = (int)sharedBeaconItem.PosY,
                XReal = (int)sharedBeaconItem.PosX,
                YReal = (int)sharedBeaconItem.PosY,
                Ssid = sharedBeaconItem.RssiLevel,
            };
                     
            _client.SetAccessPointCompleted += _client_SetAccessPointCompleted;

            _client.SetAccessPointAsync(accessPoint);
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


        public static void SetMeasurePoint(SharedBeacon sharedBeaconItem)
        {
            //if (sharedBeaconItem == null)
            //{
            //    return;
            //}

            WirelessCalculationClient _client = new WirelessCalculationClient(binding, endPoint);

            List<MeasurePoint> measurePointList = new List<MeasurePoint>
            {
                new MeasurePoint
                    {                        
                        Bssid = "00-1C-28-1A-53-FF",                     
                        Ssid = "NETCOLOGNE-6003",                        
                        RssiLevel = -50,                        
                        CenterFrequency = 2437000,                        
                        Quality = 0
                    },
                new MeasurePoint
                    {                        
                        Bssid = "BC-05-43-16-35-9C",                     
                        Ssid = "Hallen der Erleuchtung",                        
                        RssiLevel = -60,                        
                        CenterFrequency = 2437000,                        
                        Quality = 0
                    },
                new MeasurePoint
                    {                        
                        Bssid = "54-88-0E-1A-51-48",                        
                        Ssid = "Shantos 2.4GHz",                        
                        RssiLevel = -70,                        
                        CenterFrequency = 2437000,                        
                        Quality = 0
                    }
            };

            MeasureSector measureSector = new MeasureSector();
            measureSector.ApList = measurePointList.ToArray();
            measureSector.X = 1;
            measureSector.Y = 2;
            measureSector.XReal = 3;
            measureSector.YReal = 4;

            _client.SetMeasurePointAsync(measureSector);
            _client.SetMeasurePointCompleted += _client_SetMeasurePointCompleted;
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