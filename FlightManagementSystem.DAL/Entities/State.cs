using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManagementSystem.DAL.Entities
{
	public class State:BaseEntity
	{
		[Key]
		public long StateId { get; set; }
		public long CountryId { get; set; }
		public string StateName { get; set; }
	}
}
