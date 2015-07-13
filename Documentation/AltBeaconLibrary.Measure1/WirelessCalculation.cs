using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltBeaconLibrary.Measure1
{
    using AltBeaconLibrary.Measure1.DataContract;

    public class WirelessCalculation
    {
        public List<Position> GetMeasurePosition(Guid deviceId, List<MeasurePoint> measurePointList)
        {
            MeasurePosition measurePosition = new MeasurePosition(deviceId);

            measurePosition.OneRun(measurePointList);

            return DBOperation.GetLastPositionOfAllDevice();
        }

        public List<AccessPoint> GetAccessPointList()
        {
            return DBOperation.GetAccessPointList();
        }

        public bool SetAccessPoint(AccessPoint accessPoint)
        {

            return DBOperation.SetAccessPoint(accessPoint);
        }

        public bool SetMeasurePoint(MeasurePoint measurePoint)
        {
            return DBOperation.SetMeasurePoint(measurePoint);
        }

        public bool DeleteAllData()
        {
            return DBOperation.DeleteAllData();
        }

        public string GetVersion()
        {
            //Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            return "1111";
        }
    }
}
