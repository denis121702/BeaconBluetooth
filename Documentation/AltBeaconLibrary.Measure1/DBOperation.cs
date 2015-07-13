using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltBeaconLibrary.Measure1
{
    using AltBeaconLibrary.Measure1.DataContract;

    public class DBOperation
    {
        public static List<Position> GetLastPositionOfAllDevice()
        {
            //using (DataClassesDataContext dataContex = new DataClassesDataContext())
            //{
            //    IEnumerable<PositionData> duplicates = from arc in dataContex.PositionDatas
            //                                           group arc by arc.DeviceId into g
            //                                           select g.OrderByDescending(m => m.Time).First();

            //    //dataContex.Log = new System.IO.StreamWriter(@"g:\linq-to-sql.log") { AutoFlush = true };
            //    return duplicates.Select(position => new Position()
            //    {
            //        DeviceId = position.DeviceId,
            //        X = position.X,
            //        Y = position.Y,
            //        Time = position.Time
            //    }).ToList();
            //}

            return new List<Position>();
        }

        public static bool SetPosition(Position positionMeasure)
        {
            //PositionData positionData = new PositionData
            //{
            //    RecordId = Guid.NewGuid(),
            //    DeviceId = positionMeasure.DeviceId,
            //    X = positionMeasure.X,
            //    Y = positionMeasure.Y,
            //    Time = DateTime.Now
            //};

            //using (DataClassesDataContext dataContex = new DataClassesDataContext())
            //{
            //    dataContex.PositionDatas.InsertOnSubmit(positionData);

            //    dataContex.SubmitChanges();
            //}

            return true;
        }

        public static bool DeleteAllData()
        {
            //using (DataClassesDataContext dataContex = new DataClassesDataContext())
            //{
            //    //    dataContex.ExecuteCommand("DELETE FROM Entity");            
            //    dataContex.ExecuteCommand("TRUNCATE TABLE [dbo].[Position]");
            //    dataContex.ExecuteCommand("TRUNCATE TABLE [dbo].[MeasurePoint]");
            //    dataContex.ExecuteCommand("TRUNCATE TABLE [dbo].[AccessPoint]");

            //    dataContex.SubmitChanges();
            //}

            return true;
        }

        public static bool SetAccessPoint(AccessPoint accessPoint)
        {
            //AccessPointData accessPointData = new AccessPointData
            //{
            //    RecordId = Guid.NewGuid(),
            //    Bssid = accessPoint.Bssid,
            //    CenterFrequency = accessPoint.CenterFrequency,
            //    Quality = accessPoint.Quality,
            //    RssiLevel = accessPoint.RssiLevel,
            //    Ssid = accessPoint.Ssid,
            //    X = accessPoint.X,
            //    XReal = accessPoint.XReal,
            //    Y = accessPoint.Y,
            //    YReal = accessPoint.YReal
            //};

            //using (DataClassesDataContext dataContex = new DataClassesDataContext())
            //{
            //    dataContex.AccessPointDatas.InsertOnSubmit(accessPointData);

            //    dataContex.SubmitChanges();
            //}

            return true;
        }

        public static bool SetMeasurePoint(MeasurePoint measurePoint)
        {
            //MeasurePointData measurePointData = new MeasurePointData
            //{
            //    RecordId = Guid.NewGuid(),
            //    Bssid = measurePoint.Bssid,
            //    CenterFrequency = measurePoint.CenterFrequency,
            //    Constant = measurePoint.Constant,
            //    Quality = measurePoint.Quality,
            //    RssiLevel = measurePoint.RssiLevel,
            //    Ssid = measurePoint.Ssid
            //};

            //using (DataClassesDataContext dataContex = new DataClassesDataContext())
            //{
            //    dataContex.MeasurePointDatas.InsertOnSubmit(measurePointData);

            //    dataContex.SubmitChanges();
            //}

            return true;
        }

        public static List<AccessPoint> GetAccessPointList()
        {
            //using (DataClassesDataContext dataContex = new DataClassesDataContext())
            //{
            //    return dataContex.AccessPointDatas.Select(accessPoint => new AccessPoint()
            //    {
            //        Bssid = accessPoint.Bssid,
            //        CenterFrequency = (uint)accessPoint.CenterFrequency,
            //        Quality = (uint)accessPoint.Quality,
            //        RssiLevel = accessPoint.RssiLevel,
            //        Ssid = accessPoint.Ssid,
            //        X = accessPoint.X,
            //        XReal = accessPoint.XReal,
            //        Y = accessPoint.Y,
            //        YReal = accessPoint.YReal

            //    }).ToList();
            //}

            return new List<AccessPoint>();
        }

        public static List<MeasurePoint> GetMeasurePointList()
        {
            //using (DataClassesDataContext dataContex = new DataClassesDataContext())
            //{
            //    return dataContex.MeasurePointDatas.Select(measurePoint => new MeasurePoint()
            //    {
            //        Bssid = measurePoint.Bssid,
            //        CenterFrequency = (uint)measurePoint.CenterFrequency,
            //        Constant = measurePoint.Constant,
            //        Quality = (uint)measurePoint.Quality,
            //        RssiLevel = measurePoint.RssiLevel,
            //        Ssid = measurePoint.Ssid

            //    }).ToList();
            //}

            return new List<MeasurePoint>();
        }
    }
}
