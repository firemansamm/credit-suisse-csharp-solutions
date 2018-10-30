using System;
using System.Collections.Generic;
using C_Sharp_Challenge_Skeleton.Beans;

namespace C_Sharp_Challenge_Skeleton.Answers
{
    public class Question3
    {
        /** list adjlist fallback for n > 31 **/

        static unsafe int dfs2(int n, bool* v, List<int>[] ad, int* order, int* c, int* nc)
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
                ret += dfs2(o, v, ad, order, c, nc);
                c[n] += nc[o];
                nc[n] += Math.Max(nc[i], c[i]);
            }
            c[n] += flag;
            return ret + 1;
        }

        public static unsafe int Answer2(int numOfNodes, Edge[] edgeLists)
        {
            /* build adjlist */
            int vv = numOfNodes + 5;
            List<int>[] ad = new List<int>[vv];
            bool* v = stackalloc bool[vv];
            int* order = stackalloc int[vv], c = stackalloc int[vv], nc = stackalloc int[vv];
            int el = edgeLists.Length;
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
                f = dfs2(i, v, ad, order, c, nc);
                g = Math.Max(nc[i], c[i]);
                g = g + g - f;
                if (g > 0) ans += g;
            }
            return ans;
        }

        /** end fallback for n>31 **/


        static unsafe int dfs(int n, bool* v, int* ad, int* order, int* c, int* nc, int tn)
        {
            if (v[n]) return 0;
            v[n] = true;
            /* 0: color this */
            /* 1: don't color this */
            int ret = 0, flag = 1, adj = ad[n];
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
            /* for n <= 32, use a bitmask */
            if (numOfNodes >= 32) return Answer2(numOfNodes, edgeLists);
            int vv = numOfNodes + 5;
            bool* v = stackalloc bool[vv];
            int* order = stackalloc int[vv], c = stackalloc int[vv], nc = stackalloc int[vv], ad = stackalloc int[vv];
            int el = edgeLists.Length;
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
                g = Math.Max(nc[i], c[i]);
                g = g + g - f;
                if (g > 0) ans += g;
            }
            return ans;
        }
    }
}