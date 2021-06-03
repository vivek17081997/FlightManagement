using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManagementSystem.DAL.Entities
{
    public class TransactionDetail : BaseEntity
	{
		[Key]
		public long TransactionId { get; set; }
		public long OrderId { get; set; }
		public long CustomerId { get; set; }
		public int Passenger { get; set; }
		public string TransactionStatus { get; set; }
		public double Amount { get; set; }

	}
}
