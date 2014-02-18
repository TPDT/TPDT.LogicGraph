//#include <stdafx.h>
#include<stdio.h>
#include<string.h>

struct product
{
	char name[20];
	char num[7];
	char size[4];
	long int date;
	long int max;
	long int min;
	long int real;
}p1[100], p4[100];

struct pro
{
	char name[20];
	long int date;
	long int total;
}p2, p3[100];

struct pro_2
{
	char name[20];
	long int sum;
}p5[100];
//**************************************信息输入模块******************************************

int input()//建立物资数据库
{
	FILE *fp;
	int i, n;
	printf("\n请输入物资总数[n=?]\n");
	scanf("%d", &n);
	if ((fp = fopen("pro.txt", "w+")) == NULL)
	{
		printf("\n cannot open the file! \n");
		return(0);
	}
	printf("\n 依次输入物资信息：\n");
	for (i = 0; i<n; i++)
	{
		printf("\n 第%d种物资\n", i + 1);
		printf("\n物资名称   编号 规格 年 月 日 最大库存 最小库存 实际库存\n");
		scanf("%s %s %s %ld %ld %ld %ld", p1[i].name, p1[i].num, p1[i].size, &p1[i].date, &p1[i].max, &p1[i].min, &p1[i].real);
	}
	for (i = 0; i<n; i++)
		fprintf(fp, "%s %s %s %ld %ld %ld %ld\n", p1[i].name, p1[i].num, p1[i].size, p1[i].date,
		p1[i].max, p1[i].min, p1[i].real);
	fclose(fp);
	return 0;
}

int load()
{
	int i;
	FILE *fp;
	if ((fp = fopen("pro.txt", "r+")) == NULL)
	{
		printf("\ncannot open the file!\n");
		return(0);
	}
	for (i = 0; !feof(fp); i++)
		fscanf(fp, "%s %s %s %ld %ld %ld %ld", p1[i].name, p1[i].num, p1[i].size, &p1[i].date, &p1[i].max, &p1[i].min, &p1[i].real);
	fclose(fp);
	return(i);
}
//*********************************信息查找模块******************************************

void search_1()//按产品名称查询
{
	int i, m, j = 0;
	char str1[20];
	printf("\n输入查询物资名称：\n");
	scanf("%s", str1);
	m = load();
	for (i = 0; i<m; i++)
	{
		if (strcmp(str1, p1[i].name) == 0)
		{
			printf("\n物资名称     编号 规格 年 月 日 最大库存 最小库存 实际库存\n");
			printf("%s %s %s %ld %ld %ld %ld", p1[i].name, p1[i].num, p1[i].size, p1[i].date, p1[i].max, p1[i].min, p1[i].real);	          j++;
		}
	}
	if (j == 0)  printf("\n没有发现\n");
}

void search_2()//按进货日期查询
{
	int i, j, l, n, m, q;
	long int k, y;
	l = 0;
	n = 0;
	j = 0;
	k = 0;
	printf("\n输入年 月:\n");
	scanf("%ld", &y);
	m = load();
	for (i = 0; i<m; i++)
	{
		if (y * 100 <= p1[i].date&&y >= p1[i].date / 100)
		{
			printf("\n物资名称    编号 规格 年 月 日 最大库存 最小库存 实际库存\n");
			printf("%s %s %s %ld %ld %ld %ld", p1[i].name, p1[i].num, p1[i].size, p1[i].date,
				p1[i].max, p1[i].min, p1[i].real);
			strcpy(p5[j].name, p1[i].name);
			p5[j].sum = p1[i].real;
			j++;
		}
	}
	printf("\n该查询月份产品种类:%d", j);
	q = j;
	printf("\n产品名     该月进的数量");//统计产品的种类与数量
	for (i = 0; i <= q; i++)
		printf("\n%s            %ld", p5[i].name, p5[i].sum);
	printf("\n");
}

void search_3()//按产品名称和库存量查询
{
	int i, m;
	long int k;
	char str2[7];
	printf("\n输入查询物资编号：\n");
	scanf("%s", str2);
	m = load();
	printf("\n物资名称   编号 规格 年 月 日 最大库存 最小库存 实际库存\n");
	k = 0;
	for (i = 0; i<m; i++)
	{
		if (strcmp(str2, p1[i].num) == 0)
		{
			printf("%s %s %s %ld %ld %ld %ld\n", p1[i].name, p1[i].num, p1[i].size, p1[i].date, p1[i].max, p1[i].min, p1[i].real);
			k = k + p1[i].real;
		}
	}
	if (k) printf("统计物资库存:%ld", k);
	else  printf("\n not found! \n");
	printf("\n");
}
//****************************************新物资入库模块**************************************
int insert()  //新物资统计
{
	int i, n;
	FILE *fp;
	if ((fp = fopen("pro.txt", "a")) == NULL)
	{
		printf("\n cannot find the file! \n");
		return(0);
	}
	printf("\n请输入增加物资种数[n=?]\n");//增加新物资信息
	scanf("%d", &n);
	printf("\n 依次输入新物资信息：\n");
	printf("\n物资名称   编号 规格 年 月 日 最大库存 最小库存量 实际库存\n");
	for (i = 0; i<n; i++)
	{
		scanf("%s %s %s %ld %ld %ld %ld", p1[i].name, p1[i].num, p1[i].size, &p1[i].date, &p1[i].max, &p1[i].min, &p1[i].real);
	}
	for (i = 0; i<n; i++)
		fprintf(fp, "%s %s %s %ld %ld %ld %ld", p1[i].name, p1[i].num, p1[i].size, p1[i].date, p1[i].max, p1[i].min, p1[i].real);
	fclose(fp);
	return 0;

}
//********************************************领料模块**************************************
int lend()//查找所领取物资的信息
{
	int i, m;
	printf("\n输入查询名称 领料数量\n");
	scanf("%s %ld", p2.name, &p2.total);
	m = load();
	for (i = 0; i<m; i++)
	{
		if (strcmp(p2.name, p1[i].name) == 0)
		if (p2.total <= p1[i].real)  { i--; break; }
		else  printf("\n查询到该物资，不满足领料要求\n");
	}
	if (i == m)  printf("\n not found ！ \n");
	else
	{
		FILE *fp;
		printf("\n输入今天日期:\n");
		scanf("%ld", p2.date);
		fp = fopen("领料单.txt", "a");
		fprintf(fp, "%s %ld %ld", p2.name, p2.date, p2.total);
		fclose(fp);
		i++;
		p1[i].real = p1[i].real - p2.total;
		fp = fopen("pro.txt", "a");
		for (i = 0; i<m; i++)
			fprintf(fp, "%s %s %s %ld %ld %ld %ld\n", p1[i].name, p1[i].num, p1[i].size, p1[i].date, p1[i].max, p1[i].min, p1[i].real);
		fclose(fp);
	}
	return 0;
}
//*****************************************打印模块****************************************
int print_1()//打印领料单
{
	int i, j;
	FILE *fp;
	if ((fp = fopen("领料单.txt", "r")) == NULL)
	{
		printf("\n cannot open the file! \n");
		return(0);
	}
	for (i = 0; !feof(fp); i++)
		fscanf(fp, "%s %ld %ld", p3[i].name, &p3[i].date, &p3[i].total);
	fclose(fp);
	printf("物资名称  领料日期  领料数量");
	for (j = 0; j <= i; j++)
		printf("\n%s   %ld %ld", p3[j].name, p3[j].date, p3[j].total);
	return 0;
}

void print_2()//打印物资库存清单
{
	int m, i;
	m = load();
	printf("\n物资名称  实际库存\n");
	for (i = 0; i<m; i++)
		printf("\n%s         %ld", p1[i].name, p1[i].real);
}

int menu()
{
	int n, w;
	printf("\n                              欢迎进入物资管理系统\n");
	printf(" 1. 输入(建立物资数据库)\n");
	printf(" 2. 按名称查询\n");
	printf(" 3. 按进货日期查询并统计\n");
	printf(" 4. 按产品名称和规模并统计\n");
	printf(" 5. 领料\n");
	printf(" 6. 新物资入库\n");
	printf(" 7. 打印领料单\n");
	printf(" 8. 打印库存物资\n");
	printf(" 9. 退出\n");
	printf(" choose the number to execute（1~9）\n");
	do
	{
		scanf("%d", &n);
		if (n<1 || n>9)
		{
			printf("             error! inpute again !\n");
			printf("  choose the number again!（1~9）\n");
			w = 1;
		}
		else w = 0;
	} while (w == 1);
	switch (n)
	{
	case 1: input(); break;
	case 2: search_1(); break;
	case 3: search_2(); break;
	case 4: search_3(); break;
	case 5: lend(); break;
	case 6: insert(); break;
	case 7: print_1(); break;
	case 8: print_2(); break;
	case 9: return(0);
		break;
	}
	return 1;

}

int main()
{
	int i;
	do
	{
		i = menu();
	} while (i);
}
