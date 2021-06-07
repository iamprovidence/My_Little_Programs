using System;
using System.Transactions;

namespace FluentTransaction.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			Commit();
			Console.WriteLine("============");
			Rollback();
			Console.WriteLine("============");
			CommitWithoutScope();
		}

		private static void Commit()
		{
			using (var transactionScope = new TransactionScope())
			{
				TransactionBuilder
					.CreateNew()
					.OnPrepare(() =>
					{
						Console.WriteLine("Prepare data [1]");
					})
					.OnCommit(() =>
					{
						Console.WriteLine("Commit data [1]");
					})
					.OnRollback(() =>
					{
						Console.WriteLine("Rollback data [1]");
					}).Register();

				TransactionBuilder
					.CreateNew()
					.OnPrepare(() =>
					{
						Console.WriteLine("Prepare data [2]");
					})
					.OnCommit(() =>
					{
						Console.WriteLine("Commit data [2]");
					})
					.OnRollback(() =>
					{
						Console.WriteLine("Rollback data [2]");
					}).Register();

				transactionScope.Complete();
			}
		}

		private static void Rollback()
		{
			using (var transactionScope = new TransactionScope())
			{
				TransactionBuilder
					.CreateNew()
					.OnPrepare(() =>
					{
						Console.WriteLine("Prepare data [1]");
					})
					.OnCommit(() =>
					{
						Console.WriteLine("Commit data [1]");
					})
					.OnRollback(() =>
					{
						Console.WriteLine("Rollback data [1]");
					}).Register();

				TransactionBuilder
					.CreateNew()
					.OnPrepare(() =>
					{
						Console.WriteLine("Prepare data [2]");
					})
					.OnCommit(() =>
					{
						Console.WriteLine("Commit data [2]");
					})
					.OnRollback(() =>
					{
						Console.WriteLine("Rollback data [2]");
					}).Register();

				// transactionScope.Complete();
			}
		}

		private static void CommitWithoutScope()
		{
			TransactionBuilder
				.CreateNew()
				.OnPrepare(() =>
				{
					Console.WriteLine("Prepare data [1]");
				})
				.OnCommit(() =>
				{
					Console.WriteLine("Commit data [1]");
				})
				.OnRollback(() =>
				{
					Console.WriteLine("Rollback data [1]");
				}).Register();

		}
	}
}
