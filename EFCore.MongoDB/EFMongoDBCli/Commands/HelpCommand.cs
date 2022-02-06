class HelpCommand : ICommand
{
	public void Execute()
	{
		var text = @"
﻿
                     _/\__
               ---==/    \\
         ___  ___   |.    \|\
        | __|| __|  |  )   \\\
        | _| | _|   \_/ |  //|\\
        |___||_|       /   \\\/\\

--help				- Get help

--add-migration     - Create a new migration. Specify Migration name and folder

--update-database   - Updates the database with missing migrations

";

		Console.WriteLine(text);
	}
}
