using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Knapsack
{
    public int[] item;//物品标记（是否被选中）
    public string[] name;//物品名称
    public int number;//物品数量
    public int capacity;//背包总容量
    public int[,] V;//推理表
    public int[] v;//物品价值
    public int[] w;//物品重量

    public void FindMax()//动态规划
    {
        int i, j;
        //初始化
        V = new int[number+1,capacity+1];
        for (i = 0; i <= number; i++)
        {
            V[i,0] = 0;
        }
        for (j = 0; j <= capacity; j++)
        {
            V[0,j] = 0;
        }
        //填表
        for (i = 1; i <= number; i++)
        {
            for (j = 1; j <= capacity; j++)
            {
                if (j < w[i])//包装不进
                {
                    V[i,j] = V[i - 1,j];
                }
                else//能装
                {
                    if (V[i - 1,j] > V[i - 1,j - w[i]] + v[i])//不装价值大
                    {
                        V[i,j] = V[i - 1,j];
                    }
                    else//前i-1个物品的最优解与第i个物品的价值之和更大
                    {
                        V[i,j] = V[i - 1,j - w[i]] + v[i];
                    }
                }
            }
        }
    }


    public void FindWhat(int i, int j)//寻找解的组成方式
    {
        if (i > 0)
        {
            if (V[i,j] == V[i - 1,j])//相等说明没装
            {
                item[i] = 0;//全局变量，标记未被选中
                FindWhat(i - 1, j);
            }
            else if (j - w[i] >= 0 && V[i,j] == V[i - 1,j - w[i]] + v[i])
            {
                item[i] = 1;//标记已被选中
                FindWhat(i - 1, j - w[i]);//回到装包之前的位置
            }
        }
    }

    public void InitData(string path)//初始化数据
    {
        string str;
        str = DataReader.readDat(path);
        String[] Substring = str.Split(' ');
        capacity = int.Parse( Substring[0]);
        string[] tempSub = new string [Substring.Length-1];
        number = tempSub.Length;
        w = new int[number+1];
        v = new int[number + 1];
        name = new string[number+1];
        item = new int[number + 1];
        for (int i = 1; i < Substring.Length; i++)
        {
            tempSub[i - 1] = Substring[i];
        }

        for (int i = 0; i < tempSub.Length; i++)
        {
            string StrTemp = tempSub[i].Substring(1, tempSub[i].Length - 2);
            string[] unit = StrTemp.Split(',');
            name[i + 1] = unit[0];
            w[i + 1] = int.Parse(unit[1]);
            v[i + 1] = int.Parse(unit[2]);
        }
    }

    public string getItem(string path)//输出结果
    {
        InitData(path);
        FindMax();
        FindWhat(number, capacity);
        return display();
    }

    public string display()
    {
        return "背包容量：" + capacity + ",最大价值：" + V[number,capacity] + "，所选物品：" + result();
    }

    public string result()//返回被选中的物品名称
    {
        string result="";
        for (int i = 1; i <=number; i++)
        {
            if (item[i]==1)
            {
                result += name[i]+" "; 
            }
        }
        return result;
    }

    public void clear()//清除数据
    {
        number = 0;
        capacity = 0;
        item = null;
        name = null;
        V = null;
        v = null;
        w = null;
    }

}