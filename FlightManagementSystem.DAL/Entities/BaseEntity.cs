using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.DAL.Entities
{
	public  class BaseEntity
	{
		public int CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }

		public int? ModifyBy { get; set; }
		public DateTime? ModifiedDate { get; set; }

		public int? DeletedBy { get; set; }
		public DateTime? DeletedDate { get; set; }

		public bool IsActive { get; set; }
	}
}
