using System;
namespace C_Sharp_Challenge_Skeleton.Answers
{
    public class Question1
    {
        [System.Runtime.InteropServices.DllImport("answer", EntryPoint = "ans1")]
        public static extern int ans1(int[] ptr, int len);
        public static int Answer(int[] portfolios)
        {
            if (portfolios.Length > 5) return ans1(portfolios, portfolios.Length);
            /* use trie to go faster for n>16? */
            int a = 0;
            for(int i = 0; i < portfolios.Length; i++)
            {
                int v1 = portfolios[i];
                for(int j = i + 1; j < portfolios.Length; j++)
                {
                    a = Math.Max(a, v1 ^ portfolios[j]);
                }
            }
            return a;
        }
    }
}
