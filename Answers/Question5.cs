using System;
using System.Collections.Generic;
namespace C_Sharp_Challenge_Skeleton.Answers
{
    public class Question5
    {
         /* how much faster is recursion */
            public static unsafe int rec(int* coins, int idx, int val, int[] memo, int rs)
            {
                if (val < 0) return 1 << 30;
                if (val == 0) return 0;
                int ix = (idx * rs) + val;
                if (memo[ix] != 0) return memo[ix];
                int cc = coins[idx], ct = val / cc;
                if (val % cc == 0) return ct;
                if (idx == 0) return 1 << 30;
                int ans = 1<<30, nv = coins[idx-1], rem;
                if (nv == 0) return 1 << 30;
                for (int i = ct; i >= 0; i--)
                {
                    rem = val - (i * cc);
                    if (i + ((rem + nv - 1) / nv) >= ans) return memo[ix] = ans;
                    ans = Math.Min(ans, i + rec(coins, idx - 1, rem, memo, rs));
                }
                return memo[ix] = ans;
            }

            public static unsafe int Answer(int[] numOfShares, int totalValueOfShares)
            {
                if (totalValueOfShares < 0) return 0;
                Array.Sort(numOfShares);
                int len = numOfShares.Length;
                int[] mem = new int[len * (totalValueOfShares + 1)];
                fixed (int* shares = numOfShares)
                {
                    int ans = rec(shares, len - 1, totalValueOfShares, mem, totalValueOfShares);
                    if (ans < 1 << 30) return ans;
                    else return 0;
                }
            }
    }
}