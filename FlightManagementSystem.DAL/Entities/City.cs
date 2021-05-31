using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManagementSystem.DAL.Entities
{
	public class City:BaseEntity
	{
		[Key]
		public long CityId { get; set; }
		public long StateId { get; set; }
		public string CityName { get; set; }
	}
}
