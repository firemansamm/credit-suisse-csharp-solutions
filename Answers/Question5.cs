using System;
using System.Collections.Generic;
namespace C_Sharp_Challenge_Skeleton.Answers
{
    public class Question5
    {
        public static unsafe int Answer(int[] numOfShares, int totalValueOfShares)
        {
            if (totalValueOfShares < 0) return 0;

            /* clamp this to go faster, use recursion to clamp */
            fixed (int* shares = numOfShares)
            {
                int nx, g, i, j, t, len = numOfShares.Length;
                int* ans = stackalloc int[totalValueOfShares + 5];
                ans[0] = 0;
                for (i = 1; i <= totalValueOfShares; i++) ans[i] = 1 << 30;
            
                for (i = 0; i < len; i++)
                {
                    g = shares[i];
                    for (j = g, t = 0; j <= totalValueOfShares; j++, t++)
                    {
                        nx = ans[t] + 1;
                        if (nx < ans[j]) ans[j] = nx;
                    }
                }
                if (ans[totalValueOfShares] < 1 << 30) return ans[totalValueOfShares];
                return 0;
            }
        }
    }
}