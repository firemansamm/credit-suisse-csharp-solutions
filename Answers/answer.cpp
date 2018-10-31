#include <algorithm>
#include <cstring>
#include <cmath>
using namespace std;

/* compiled on windows 10 with: g++ -Ofast -fPIC -frename-registers -shared -o answer.dll answer.cpp */
/* compiled on ubntu 14.06 with g++ -Ofast -fPIC -frename-registers -shared -o answer.so answer.cpp */

extern "C" int ans1(int* ptr, int len) {
	int ans = 0;
	for(int i=0;i<len;i++){
		int f = ptr[i];
		for(int j=0;j<len;j++){
			ans = max(ans, f^ptr[j]);
		}
	}
	return ans;
}

extern "C" int ans2(int* ptr1, int* ptr2, int len1, int len2) {
	int sm1 = 0, sm2 = 0;
	for(int i=0;i<len1;i++) sm1 += ptr1[i];
	for(int i=0;i<len2;i++) sm2 += ptr2[i];
	int lm = sm1 + sm2 + 10, g;
	if (sm1 < sm2) {
		bool co[lm], *c = co + sm1 + 5;
		memset(co, 0, sizeof co);
		for(int i=0;i<len1;i++){
			g = ptr1[i];
			for(int j=sm1;j>g;j--) c[j] |= c[j-g];
			c[g] = 1;
			if (c[0]) return 0;
		}
		for(int i=0;i<len2;i++){
			g = -ptr2[i];
			c[g] = 1;
			for(int j=g+1;j<=sm1+g;j++) c[j] |= c[j-g];
			if(c[0]) return 0;
		}
		for(int i=0;i<=sm1;i++) if(c[i] || c[-i]) return i;
		return 0;
	} else {
		bool co[lm], *c = co + sm2 + 5;
		memset(co, 0, sizeof co);
		for(int i=0;i<len2;i++){
			g = ptr2[i];
			for(int j=sm2;j>g;j--) c[j] |= c[j-g];
			c[g] = 1;
			if (c[0]) return 0;
		}
		for(int i=0;i<len1;i++){
			g = -ptr1[i];
			c[g] = 1;
			for(int j=g+1;j<=sm2+g;j++) c[j] |= c[j-g];
			if(c[0]) return 0;
		}
		for(int i=0;i<=sm2;i++) if(c[i] || c[-i]) return i;
		return 0;
	}
}

extern "C" int ans5(int* ptr, int val, int len) {
	if (val <= 0) return 0;
	int ans[val + 5];
	for(int i=1;i<=val;i++) ans[i] = 1<<30;
	ans[0] = 0;
	for(int i=0;i<len;i++){
		int g = ptr[i];
		for(int j=g;j<=val;j++){
			ans[j] = min(ans[j], ans[j-g] + 1);
		}
	}
	return (ans[val] < 1<<30) ? ans[val] : 0;
}

extern "C" int ans6(int n, int t, int* f){
	
}