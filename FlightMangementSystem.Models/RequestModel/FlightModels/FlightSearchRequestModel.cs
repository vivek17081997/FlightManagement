using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightMangementSystem.Models.RequestModel.FlightModels
{
	/// <summary>
	/// Flight Search Request Model
	/// </summary>
	public class FlightSearchRequestModel
	{
		[Required]
		public int PageNo { get; set; }
		public string FlightNumber { get; set; }
		public string FlightCompany { get; set; }
		public DateTime DepartureDatetime { get; set; }
		public DateTime FlighTime { get; set; }
		public double Price { get; set; }

	}
}
