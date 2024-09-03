using System;

public static class Extensions
{
	public static int RoundToInterval(int interger, int interval)
	{
		if (interval == 0)
		{
			throw new ArgumentException("The specified interval cannot be 0.", nameof(interval));
		}
		return ((int)Math.Round((double)interger / (double)interval)) * interval;
	}

}
