using System;
using System.Linq;
using System.Collections.Generic;

namespace C_Sharp_Challenge_Skeleton.Answers
{
    public class Question2
    {
        public static unsafe int Answer(int[] cashflowIn, int[] cashflowOut)
        {
            int sm1 = 0, sm2 = 0, g, i, j, f;
            for (i = 0; i < cashflowIn.Length; i++) sm1 += cashflowIn[i];
            for (i = 0; i < cashflowOut.Length; i++) sm2 += cashflowOut[i];

            int lm = sm1 + sm2 + 10;
            /* clamp to sumA+sumB - don't overallocate for coherence */
            if (sm1 < sm2)
            {
                bool* co = stackalloc bool[lm], c = co + sm1 + 5;
                for (i = 0; i < cashflowIn.Length; i++)
                {
                    g = cashflowIn[i];
                    for (j = sm1, f = j - g; j > g; j--, f--) c[j] |= c[f];
                    c[g] = true;
                    if (c[0]) return 0;
                }
                for (i = 0; i < cashflowOut.Length; i++)
                {
                    g = -cashflowOut[i];
                    c[g] = true;
                    for (j = g + 1, f = j - g; j <= sm1 + g; j++, f++) c[j] |= c[f];
                    if (c[0]) return 0;
                }
                for (i = 0; i <= sm1; i++) if (c[i] || c[-i]) return i;
                return 0;
            }
            else
            {
                bool* co = stackalloc bool[lm], c = co + sm2 + 5;
                for (i = 0; i < cashflowOut.Length; i++)
                {
                    g = cashflowOut[i];
                    for (j = sm2, f = j - g; j > g; j--, f--) c[j] |= c[f];
                    c[g] = true;
                    if (c[0]) return 0;
                }
                for (i = 0; i < cashflowIn.Length; i++)
                {
                    g = -cashflowIn[i];
                    c[g] = true;
                    for (j = g + 1, f = j - g; j <= sm2 + g; j++, f++) c[j] |= c[f];
                    if (c[0]) return 0;
                }
                for (i = 0; i <= sm2; i++) if (c[i] || c[-i]) return i;
                return 0;
            }
        }
    }
}