using System;
using System.Transactions;

namespace FluentTransaction
{
	internal class LocalTransaction : IEnlistmentNotification
	{
		private readonly Action _prepareAction;
		private readonly Action _commitAction;
		private readonly Action _rollbackAction;
		private readonly Action _inDoubtAction;

		internal LocalTransaction(Action prepare, Action commit, Action rollback, Action inDoubt)
		{
			_prepareAction = prepare;
			_commitAction = commit;
			_rollbackAction = rollback;
			_inDoubtAction = inDoubt;
		}

		public void Prepare(PreparingEnlistment preparingEnlistment)
		{
			try
			{
				_prepareAction?.Invoke();
				preparingEnlistment.Prepared();
			}
			catch
			{
				preparingEnlistment.ForceRollback();
			}
		}
		public void Commit(Enlistment enlistment)
		{
			_commitAction?.Invoke();
			enlistment.Done();
		}

		public void Rollback(Enlistment enlistment)
		{
			_rollbackAction?.Invoke();
			enlistment.Done();
		}

		public void InDoubt(Enlistment enlistment)
		{
			_inDoubtAction?.Invoke();
			enlistment.Done();
		}
	}
}
