using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.DAL.Entities
{
	public class Country : BaseEntity
	{
		public long CountryId { get; set; }
		public string CountryName { get; set; }
	}
}
