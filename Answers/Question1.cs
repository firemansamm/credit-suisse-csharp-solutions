
namespace C_Sharp_Challenge_Skeleton.Answers
{
    public class Question1
    {
        public static int Answer(int[] portfolios)
        {
            /* use trie to go faster for n>16? */
            int a = 0;
            for(int i = 0; i < portfolios.Length; i++)
            {
                int v1 = portfolios[i];
                for(int j = i + 1; j < portfolios.Length; j++)
                {
                    int v2 = v1 ^ portfolios[j];
                    if (v2 > a) a = v2;
                }
            }
            return a;
        }
    }
}
