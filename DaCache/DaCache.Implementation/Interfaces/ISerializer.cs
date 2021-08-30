using System.Threading.Tasks;

namespace DaCache.Implementation.Interfaces
{
	internal interface ISerializer
	{
		Task<byte[]> Serialize<T>(T item);
		Task<T> Deserialize<T>(byte[] serializedObject);
	}
}
