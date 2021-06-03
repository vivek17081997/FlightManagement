using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightMangementSystem.Models.RequestModel.FlightModels
{
	/// <summary>
	/// Request Model
	/// </summary>
	public class FlightDetailRequest
	{
		[Required]
		public string FlightNumber { get; set; }
		[Required]
		public string FlightCompany { get; set; }
		[Required]
		public int TotalSeats { get; set; }
		[Required]
		public double TicketPrice { get; set; }
	}
}
