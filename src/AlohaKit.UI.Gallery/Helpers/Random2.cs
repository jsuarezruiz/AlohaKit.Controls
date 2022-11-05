using System;

namespace AlohaKit.UI.Gallery.Helpers
{
	// ==++==
	// 
	//   Copyright (c) Microsoft Corporation.  All rights reserved.
	// 
	// ==--==
	/*============================================================
    **
    ** Class:  Random
    **
    **
    ** Purpose: A random number generator.
    **
    ** 
    ===========================================================*/

	/// <summary>
	/// To have consistent behaviour of random generator across Runtimes (Mono, Core) and avoid changes to it in future releases src from Github repo is used instead of stock BCL class. 
	/// </summary>
	public class Random2
	{
		//
		// Private Constants 
		//
		private const int MBIG = 2000000000;
		private const int MSEED = 161803398;

		//
		// Member Variables
		//
		private int inext;
		private int inextp;
		private int[] SeedArray = new int[56];

		//
		// Public Constants
		//

		//
		// Native Declarations
		//

		//
		// Constructors
		//

		public Random2(int Seed)
		{
			int ii;
			int mj, mk;

			//Initialize our Seed array.
			//This algorithm comes from Numerical Recipes in C (2nd Ed.)
			int subtraction = (Seed == Int32.MinValue) ? Int32.MaxValue : Math.Abs(Seed);
			mj = MSEED - subtraction;
			SeedArray[55] = mj;
			mk = 1;
			for (int i = 1; i < 55; i++)
			{  //Apparently the range [1..55] is special (Knuth) and so we're wasting the 0'th position.
				ii = (21 * i) % 55;
				SeedArray[ii] = mk;
				mk = mj - mk;
				if (mk < 0) mk += MBIG;
				mj = SeedArray[ii];
			}
			for (int k = 1; k < 5; k++)
			{
				for (int i = 1; i < 56; i++)
				{
					SeedArray[i] -= SeedArray[1 + (i + 30) % 55];
					if (SeedArray[i] < 0) SeedArray[i] += MBIG;
				}
			}
			inext = 0;
			inextp = 21;
		}

		//
		// Package Private Methods
		//

		/*====================================Sample====================================
        **Action: Return a new random number [0..1) and reSeed the Seed array.
        **Returns: A double [0..1)
        **Arguments: None
        **Exceptions: None
        ==============================================================================*/
		protected virtual double Sample()
		{
			//Including this division at the end gives us significantly improved
			//random number distribution.
			return (InternalSample() * (1.0 / MBIG));
		}

		private int InternalSample()
		{
			int retVal;
			int locINext = inext;
			int locINextp = inextp;

			if (++locINext >= 56) locINext = 1;
			if (++locINextp >= 56) locINextp = 1;

			retVal = SeedArray[locINext] - SeedArray[locINextp];

			if (retVal == MBIG) retVal--;
			if (retVal < 0) retVal += MBIG;

			SeedArray[locINext] = retVal;

			inext = locINext;
			inextp = locINextp;

			return retVal;
		}

		//
		// Public Instance Methods
		// 

		/*=====================================Next=====================================
        **Returns: A double [0..1)
        **Arguments: None
        **Exceptions: None
        ==============================================================================*/
		public virtual double NextDouble()
		{
			return Sample();
		}
	}
}