using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManagementSystem.DAL.Entities
{
	public class OrderDetail: BaseEntity
	{
		[Key]
		public long OrderId { get; set; }
		public long FlightId { get; set; }
		public string TransactionType { get; set; }
		public string Orgin { get; set; }
		public DateTime DepartureDate { get; set; }
		public DateTime? ReturnDate { get; set; }
		public string Destination { get; set; }
		public int NumberOfSeats { get; set; }

	}
}
