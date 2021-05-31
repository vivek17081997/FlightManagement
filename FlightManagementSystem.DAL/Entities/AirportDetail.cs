using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManagementSystem.DAL.Entities
{
	class AirportDetail : BaseEntity
	{
		[Key]
		public long AirportId { get; set; }
		public string AirportName { get; set; }
		public string State { get; set; }
		public string City { get; set; }
	}
}
