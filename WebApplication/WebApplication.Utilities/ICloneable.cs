namespace WebApplication.Utilities
{
	public interface ICloneable<out T>
	{
		T Clone();
	}
}
