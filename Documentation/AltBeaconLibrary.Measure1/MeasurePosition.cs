using System;
using System.Collections.Generic;
using System.Linq;


namespace AltBeaconLibrary.Measure1
{
    using AltBeaconLibrary.Measure1.DataContract;

    public class MeasurePosition
    {
        private int IterationCount { get; set; }

        private double PixelToCm { get; set; }

        private Guid DeviceId { get; set; }

        public MeasurePosition(Guid deviceId)
        {
            IterationCount = 10;
            PixelToCm = 1;
            DeviceId = deviceId;
        }

        public void OneRun(List<MeasurePoint> currentScanList)
        {
            List<AccessPoint> accessPointList = DBOperation.GetAccessPointList();

            List<MeasurePoint> measurePointList = DBOperation.GetMeasurePointList();

            //bool isCheck = false;
            //PointF LastMeasurePoint = new PointF();

            for (int j = 0; j < IterationCount; j++)
            {
                Region findRegion = null;

                foreach (MeasurePoint item in currentScanList)
                {
                    int Frequency = int.Parse(item.CenterFrequency.ToString().Substring(0, 4));
                    double Level = -item.RssiLevel;

                    double measurePointConstant = this.MeasureConstant(item, measurePointList);

                    double distanceToRouterinRealWithConstant = CalculateDistance(Level, Frequency, measurePointConstant);

                    double distanceToRouterinReal = CalculateDistance(Level, Frequency);

                    double distanceToRouterinInPixel = this.ConvertDistanceToPixel(distanceToRouterinRealWithConstant * 100); // in Meter

                    distanceToRouterinInPixel = distanceToRouterinInPixel + (j * (distanceToRouterinInPixel / 100) * 10);

                    AccessPoint findAccessPoint = accessPointList.Single<AccessPoint>(m => m.Bssid == item.Bssid);

                    Region reg = DrawCircleInMemory(new Point(findAccessPoint.X, findAccessPoint.Y), distanceToRouterinInPixel);
                    if (findRegion == null)
                    {
                        findRegion = reg;
                    }
                    else
                    {
                        findRegion.Intersect(reg);
                    }
                }

                Bitmap bitmap = new Bitmap(1, 1);
                Graphics graphics = Graphics.FromImage(bitmap);

                PointF lastMeasurePoint = RegionCentroid(findRegion, new Matrix());
                if (!float.IsNaN(lastMeasurePoint.X) && !float.IsNaN(lastMeasurePoint.Y))
                {
                    if ((findRegion.GetBounds(graphics).Size.Width < 250 && findRegion.GetBounds(graphics).Size.Height < 250))
                    {
                        //PositionMeasureList.GetList.Add(LastMeasurePoint);
                        DBOperation.SetPosition(new Position
                        {
                            X = (int)lastMeasurePoint.X,
                            Y = (int)lastMeasurePoint.Y,
                            DeviceId = this.DeviceId
                        });
                    }
                    //else
                    //{
                    //    LastMeasurePoint = PositionMeasureList.GetLast;
                    //}                                       

                    //isCheck = true;

                    break;
                }
            }

            //if (isCheck)
            //{
            //    return new Position
            //    {
            //        X = (int)LastMeasurePoint.X,
            //        Y = (int)LastMeasurePoint.Y
            //    };
            //}

            //return new Position()
            //{
            //    X = (int)PositionMeasureList.GetLast.X,
            //    Y = (int)PositionMeasureList.GetLast.Y
            //};
        }

        private double MeasureConstant(MeasurePoint item, List<MeasurePoint> measurePointList)
        {
            List<MeasurePoint> measurePointConstantList = measurePointList.Where(m => m.Bssid == item.Bssid)
                                                                          .OrderBy(m => m.RssiLevel)
                                                                          .ToList();
            double measurePointConstant;
            if (measurePointConstantList.Count == 0)
            {
                measurePointConstant = -27.55;
            }
            else if (measurePointConstantList.Count == 1)
            {
                measurePointConstant = measurePointConstantList.First().Constant;
            }
            else
            {
                //that this value will range between -10 and -100 dBm                
                double min = -100;
                double max = -10;

                List<ConstantClass> ConstantList = new List<ConstantClass>();
                List<ConstantClass> ConstantTempList = measurePointConstantList.Select(m =>
                {
                    var nextItem = measurePointConstantList.GetNext(m);
                    var previousItem = measurePointConstantList.GetPrevious(m);

                    double maxItem;
                    double minItem;

                    if (measurePointConstantList.IsFirst(m))
                    {
                        double teilLevel = ((m.RssiLevel - nextItem.RssiLevel) / 3);

                        minItem = min;
                        maxItem = m.RssiLevel - teilLevel;
                    }
                    else if (measurePointConstantList.IsLast(m))
                    {
                        double teilLevel = ((previousItem.RssiLevel - m.RssiLevel) / 3);
                        double middleConstant = ((previousItem.Constant + m.Constant) / 2);

                        minItem = m.RssiLevel + teilLevel;
                        maxItem = max;

                        ConstantList.Add(new ConstantClass
                        {
                            MaxLevel = minItem,
                            MinLevel = minItem + teilLevel,
                            Constant = middleConstant
                        });
                    }
                    else
                    {
                        double teilLevelMax = ((m.RssiLevel - nextItem.RssiLevel) / 3);
                        double teilLevelMin = ((previousItem.RssiLevel - m.RssiLevel) / 3);
                        double middleConstant = ((previousItem.Constant + m.Constant) / 2);

                        minItem = m.RssiLevel + teilLevelMin;
                        maxItem = m.RssiLevel - teilLevelMax;

                        ConstantList.Add(new ConstantClass
                        {
                            MaxLevel = minItem,
                            MinLevel = minItem + teilLevelMin,
                            Constant = middleConstant
                        });
                    }

                    return new ConstantClass { MaxLevel = maxItem, MinLevel = minItem, Constant = m.Constant };

                }).ToList();

                ConstantTempList.AddRange(ConstantList);

                double constant = ConstantTempList.First(m => m.MinLevel < item.RssiLevel && item.RssiLevel <= m.MaxLevel).Constant;

                measurePointConstant = constant;
            }

            return measurePointConstant;
        }

        public class ConstantClass
        {
            public double MaxLevel { get; set; }

            public double MinLevel { get; set; }

            public double Constant { get; set; }
        }

        public double CalculateDistance(double levelInDb, double freqInMHz)
        {
            double exp = (27.55 - (20 * Math.Log10(freqInMHz)) + levelInDb) / 20.0;
            return Math.Pow(10.0, exp);
        }

        public double CalculateDistance(double levelInDb, double freqInMHz, double constant)
        {
            double exp = (-constant - (20 * Math.Log10(freqInMHz)) + levelInDb) / 20.0;
            return Math.Pow(10.0, exp);
        }

        private double ConvertDistanceToPixel(double distanceInReal)
        {
            return distanceInReal * PixelToCm;
        }

        private double ConvertDistanceToReal(double distanceInPixel)
        {
            return distanceInPixel / PixelToCm;
        }

        private Region DrawCircleInMemory(Point pt1, double radRect)
        {
            GraphicsPath graphics_rect = new GraphicsPath();

            Rectangle ellipse_rect = new Rectangle(pt1.X - (int)radRect, pt1.Y - (int)radRect,
                                                    (int)radRect * 2, (int)radRect * 2);
            graphics_rect.AddEllipse(ellipse_rect);

            return new Region(graphics_rect);
        }

        private PointF RegionCentroid(Region region, Matrix transform)
        {
            float mx = 0;
            float my = 0;
            float total_weight = 0;
            foreach (RectangleF rect in region.GetRegionScans(transform))
            {
                float rect_weight = rect.Width * rect.Height;
                mx += rect_weight * (rect.Left + rect.Width / 2f);
                my += rect_weight * (rect.Top + rect.Height / 2f);
                total_weight += rect_weight;
            }

            return new PointF(mx / total_weight, my / total_weight);
        }
    }
}
