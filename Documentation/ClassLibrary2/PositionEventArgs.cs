using System;
using System.Collections.Generic;

using WirelessCalculationContract;

namespace ClassLibrary2
{ 
    public class PositionEventArgs : EventArgs
	{
		public List<Position> Data { get; protected set; }

        public PositionEventArgs(List<Position> data)
		{
			Data = data;
		}
	}
}