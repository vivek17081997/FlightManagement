using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.DAL.Entities
{

	public class DepartureDetails
	{
		public long  DepartureId { get; set; }
		public long  FlightId { get; set; }
		public long  AirPortIdFrom { get; set; }
		public long  AirPortIdTo { get; set; }
		public DateTime  DepatureTime { get; set; }
		public DateTime  DepatureDate { get; set; }
	}
}
