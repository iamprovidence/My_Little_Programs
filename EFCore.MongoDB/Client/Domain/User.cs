using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Domain
{
	[Table("Users")]
	internal class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Guid Code { get; set; }
	}
}
