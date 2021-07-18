using System;

namespace Northwind.DbProcessor.Configuration
{
	[Flags]
	internal enum RunModes
	{
		ListChanges = 1,
		ApplyMigration = 2,
	}
}
