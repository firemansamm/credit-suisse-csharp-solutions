using System;
using System.Collections.Generic;
namespace C_Sharp_Challenge_Skeleton.Answers
{
    public class Question5
    {
        public static unsafe int Answer(int[] numOfShares, int totalValueOfShares)
        {
            if (totalValueOfShares < 0) return 0;
            int nx, i, j, t, len = numOfShares.Length;
            int[] ans = new int[totalValueOfShares + 1];
            ans[0] = 0;
            for (i = 1; i <= totalValueOfShares; i++) ans[i] = 1 << 30;

            for (i = 0; i < len; i++)
            {
                for (j = numOfShares[i], t = 0; j <= totalValueOfShares; j++, t++)
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