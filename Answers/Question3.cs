using System;
using System.Collections.Generic;
using C_Sharp_Challenge_Skeleton.Beans;

namespace C_Sharp_Challenge_Skeleton.Answers
{
    public class Question3
    {
        static unsafe int dfs(int n, bool* v, int* ad, int* order, int* c, int* nc, int tn)
        {
            if (v[n]) return 0;
            v[n] = true;
            /* 0: color this */
            /* 1: don't color this */
            int ret = 0, flag = 1, o, adj = ad[n];
            for (int i = 1; i <= tn; i++)
            {
                if ((adj & (1 << i)) == 0) continue;
                if (v[i])
                {
                    if (order[i] - order[n] % 2 == 0) flag = 0;
                    continue;
                }
                order[i] = order[n] + 1;
                ret += dfs(i, v, ad, order, c, nc, tn);
                c[n] += nc[i];
                nc[n] += Math.Max(nc[i], c[i]);
            }
            c[n] += flag;
            return ret + 1;
        }

        public static unsafe int Answer(int numOfNodes, Edge[] edgeLists)
        {
            /* build adjlist */
            int vv = numOfNodes + 5;
            bool* v = stackalloc bool[vv];
            int* order = stackalloc int[vv], c = stackalloc int[vv], nc = stackalloc int[vv], ad = stackalloc int[vv];
            int el = edgeLists.Length;
            /* for n <= 32, use a bitmask */
            for (int i = 0; i < el; i++)
            {
                Edge e = edgeLists[i];
                int x = e.EdgeA, y = e.EdgeB;
                ad[x] |= 1<<y;
                ad[y] |= 1<<x;
            }
            int ans = 0, f, g;
            for (int i = 1; i <= numOfNodes; i++)
            {
                if (v[i]) continue;
                f = dfs(i, v, ad, order, c, nc, numOfNodes);
                g = c[i];
                if (nc[i] > g) g = nc[i];
                g = g + g - f;
                if (g > 0) ans += g;
            }
            return ans;
        }
    }
}