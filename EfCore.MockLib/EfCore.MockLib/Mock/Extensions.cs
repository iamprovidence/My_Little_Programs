using System.Collections.Generic;
using System.Linq;

namespace EfCore.MockLib.Mock
{
	internal static class Extensions
	{
		public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out T third)
		{
			first = list.Skip(0).Take(1).Single();
			second = list.Skip(1).Take(1).Single();
			third = list.Skip(2).Take(1).Single();
		}
	}
}
