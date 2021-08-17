using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EfCore.MockLib.Mock
{
	internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
	{
		private readonly IEnumerator<T> _enumerator;

		public TestAsyncEnumerator(IEnumerator<T> enumerator)
		{
			_enumerator = enumerator ?? throw new ArgumentNullException();
		}

		public T Current => _enumerator.Current;

		public void Dispose()
		{
		}

		public ValueTask DisposeAsync()
		{
			return ValueTask.CompletedTask;
		}

		public Task<bool> MoveNext(CancellationToken cancellationToken)
		{
			return Task.FromResult(_enumerator.MoveNext());
		}

		public ValueTask<bool> MoveNextAsync()
		{
			return ValueTask.FromResult(_enumerator.MoveNext());
		}
	}
}
