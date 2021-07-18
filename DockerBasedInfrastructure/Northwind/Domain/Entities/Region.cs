using System.Collections.Generic;

namespace Northwind.Domain.Entities
{
	public class Region
	{
		public int RegionId { get; set; }
		public string RegionDescription { get; set; }

		public ICollection<Territory> Territories { get; private set; } = new HashSet<Territory>();
	}
}
