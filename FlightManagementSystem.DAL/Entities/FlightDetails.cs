using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManagementSystem.DAL.Entities
{
	public class FlightDetail: BaseEntity
	{
		[Key]
		public long FlightId { get; set; }
		public string FlightNumber { get; set; }
		public string FlightCompany { get; set; }
		public int TotalSeats { get; set; }
		public double TicketPrice { get; set; }
		
	}
}
