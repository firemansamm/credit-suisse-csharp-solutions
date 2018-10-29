using System;
using System.Collections.Generic;
using C_Sharp_Challenge_Skeleton.Beans;

namespace C_Sharp_Challenge_Skeleton.Answers
{
    public class Question3
    {
        static unsafe int dfs(int n, bool* v, List<int>[] ad, int* order, int* c, int* nc)
        {
            if (v[n]) return 0;
            v[n] = true;
            /* 0: color this */
            /* 1: don't color this */
            int ret = 0, flag = 1, o;
            List<int> cx = ad[n];
            for (int i = 0; i < cx.Count; i++)
            {
                o = cx[i];
                if (v[o])
                {
                    if (order[o] - order[n] % 2 == 0) flag = 0;
                    continue;
                }
                order[o] = order[n] + 1;
                ret += dfs(o, v, ad, order, c, nc);
                c[n] += nc[o];
                if (c[o] > nc[o]) nc[n] += c[o];
                else nc[n] += nc[o];
            }
            c[n] += flag;
            return ret + 1;
        }

        public static unsafe int Answer(int numOfNodes, Edge[] edgeLists)
        {
            /* build adjlist */
            int vv = numOfNodes + 5;
            List<int>[] ad = new List<int>[vv];
            bool* v = stackalloc bool[vv];
            int* order = stackalloc int[vv], c = stackalloc int[vv], nc = stackalloc int[vv];
            int el = edgeLists.Length;
            /* tune the adjlist? maybe list access is slow */
            /* for n <= 32, use a bitmask */
            for (int i = 0; i <= numOfNodes; i++)
            {
                ad[i] = new List<int>();
            }
            for (int i = 0; i < el; i++)
            {
                Edge e = edgeLists[i];
                int x = e.EdgeA, y = e.EdgeB;
                ad[x].Add(y);
                ad[y].Add(x);
            }
            int ans = 0, f, g;
            for (int i = 1; i <= numOfNodes; i++)
            {
                if (v[i]) continue;
                f = dfs(i, v, ad, order, c, nc);
                g = c[i];
                if (nc[i] > g) g = nc[i];
                g = g + g - f;
                if (g > 0) ans += g;
            }
            return ans;
        }
    }
}