﻿namespace WebArchitecture.Utilities
{
	public interface ICloneable<out T>
	{
		T Clone();
	}
}
