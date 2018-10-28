using System;
using System.Collections.Generic;
namespace C_Sharp_Challenge_Skeleton.Answers
{
    public class Question5
    {
        public static unsafe int Answer(int[] numOfShares, int totalValueOfShares)
        {
            if (totalValueOfShares < 0) return 0;
            int nx, g;
            int* ans = stackalloc int[totalValueOfShares + 1];
            int inf = (1 << 30) + 1, len = numOfShares.Length;
            ans[0] = 0;
            for (int i = 1; i <= totalValueOfShares; i++) ans[i] = 1 << 30;
        
            for (int i = 0; i < len; i++)
            {
                g = numOfShares[i];
                for (int j = g; j <= totalValueOfShares; j++)
                {
                    nx = ans[j - g] + 1;
                    if (nx != inf && nx < ans[j]) ans[j] = nx;
                }
            }
            if (ans[totalValueOfShares] != 1 << 30) return ans[totalValueOfShares];
            return 0;
        }
    }
}