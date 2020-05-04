using UnitOfWork.Abstractions.Entity;

namespace UnitOfWork.Example.Domains
{
	public class User : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }

		public override string ToString()
		{
			return $"User - {Id} - {Name} - {Age} -";
		}
	}
}
