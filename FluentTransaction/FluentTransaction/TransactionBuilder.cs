using System;
using System.Transactions;

namespace FluentTransaction
{
	public class TransactionBuilder
	{
		private Action _prepareAction;
		private Action _commitAction;
		private Action _rollbackAction;
		private Action _inDoubtAction;

		private TransactionBuilder()
		{
			_prepareAction = () => { };
			_commitAction = () => { };
			_rollbackAction = () => { };
			_inDoubtAction = () => { };
		}

		public static TransactionBuilder CreateNew()
		{
			return new TransactionBuilder();
		}

		public TransactionBuilder OnPrepare(Action action)
		{
			_prepareAction = action;

			return this;
		}

		public TransactionBuilder OnCommit(Action action)
		{
			_commitAction = action;

			return this;
		}

		public TransactionBuilder OnRollback(Action action)
		{
			_rollbackAction = action;

			return this;
		}

		public TransactionBuilder OnInDoubt(Action action)
		{
			_inDoubtAction = action;

			return this;
		}

		public void Register()
		{
			var localTransaction = new LocalTransaction(_prepareAction, _commitAction, _rollbackAction, _inDoubtAction);

			EnlistTransaction(localTransaction);
		}

		private void EnlistTransaction(LocalTransaction localTransaction)
		{
			if (Transaction.Current == null)
			{
				using (var transactionScope = new TransactionScope())
				{
					Transaction.Current.EnlistVolatile(localTransaction, EnlistmentOptions.None);

					transactionScope.Complete();
				}

				return;
			}

			Transaction.Current.EnlistVolatile(localTransaction, EnlistmentOptions.None);
		}
	}
}
