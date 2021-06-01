using System;
using System.Collections.Generic;
using System.Text;

namespace FlightMangementSystem.Models.ResponseModel.FlightModels
{
	/// <summary>
	/// Response Model
	/// </summary>
	public class FlightAddResponseModel
	{
		public long FlightId { get; set; }
		public string FlightNumber { get; set; }
		public string FlightCompany { get; set; }
		public int TotalSeats { get; set; }
		public double TicketPrice { get; set; }
	}
}
