#include <iostream>
#include <stdio.h>

using namespace std;

int main()
{
	int n;

	cin >> n;

	int a[1000000];
	int b[1000000] = {0};

	for(int i = 0; i < n ; i++)
	{
		cin >> a[i];
	}

	for(int i = 0; i < n ; i++)
	{
		b[a[i]] = 1;
	}

	for(int i = 0; i <= 1000000 ; i++)
	{
		if(b[i] != 0)
		{
			printf("%d\n", i);
		}
	}

	return 0;

}