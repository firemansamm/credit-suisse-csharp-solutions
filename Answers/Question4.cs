using System;
using System.Runtime.CompilerServices;

namespace C_Sharp_Challenge_Skeleton.Answers
{
    public class Question4
    {

        public static unsafe int Answer(string[,] machineToBeFixed, int numOfConsecutiveMachines)
        {
            int ans = 1 << 30, sm = 0, sz = 0, l1 = machineToBeFixed.GetLength(0), l2 = machineToBeFixed.GetLength(1), 
                lx = 0, vf = numOfConsecutiveMachines - 1, r = 0, brk = l2 - numOfConsecutiveMachines;
            int* vs = stackalloc int[l2];
            for (int i = 0; i < l1; i++)
            {
                for (int j = 0; j < l2; j++)
                {
                    /* manually inline it since the IL compiler wont */
                    /* only do the conversion if you can find k in a row */
                    r = 0;
                    fixed (char* g = machineToBeFixed[i, j])
                    {
                        char* t = g;
                        if (*t == 'X') {
                            if (j >= brk) break;
                            sz = 0;
                            sm = 0;
                            lx = j + 1;
                            continue;
                        }
                        while (*t != '\0')
                        {
                            r = (r * 10) + (*t - '0');
                            t++;
                        }
                        sm += (vs[j] = r);
                        while (sm > ans) {
                            sz--;
                            sm -= vs[lx++];
                        }
                        if (sz == vf)
                        {
                            if (sm < ans) ans = sm;
                            sm -= vs[lx++];
                        }
                        else sz++;
                    }
                }
                sz = 0;
                sm = 0;
                lx = 0;
            }
            if (ans == 1 << 30) return 0;
            else return ans;
        }
    }
}