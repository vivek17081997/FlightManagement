using System;
using System.Collections.Generic;
using System.Text;

namespace FlightMangementSystem.Models.CommonResponse.FlightModels
{
	public class Flight
	{
		public long FlightId { get; set; }
		public string FlightNumber { get; set; }
		public string FlightCompany { get; set; }
		public int TotalSeats { get; set; }
		public double TicketPrice { get; set; }
	}
}
