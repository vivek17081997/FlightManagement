using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManagementSystem.DAL.Entities
{
	public class AirportDetail : BaseEntity
	{
		[Key]
		public long AirportId { get; set; }
		public string AirportName { get; set; }
		public long CountryId { get; set; }
		public long StateId { get; set; }
		public long CityId { get; set; }
	}
}
