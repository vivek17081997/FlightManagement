using System;
using System.Collections.Generic;
using System.Text;

namespace FlightMangementSystem.Models.ResponseModel.FlightModels
{
	public class SearchedFlightDetailsResponseModel
	{
		public long FlightId { get; set; }
		public string FlightNumber { get; set; }
		public string FlightCompany { get; set; }
		public int TotalSeats { get; set; }
		public double TicketPrice { get; set; }
		public long DepartureId { get; set; }
		public long From { get; set; }
		public long To { get; set; }
		public DateTime DepatureTime { get; set; }
		public DateTime DepatureDate { get; set; }
		public long AirportId { get; set; }
		public string AirportName { get; set; }
	}
}
