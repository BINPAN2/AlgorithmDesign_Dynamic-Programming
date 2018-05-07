using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 测试类
 */

public class Test
{
    static void Main(string[] args)
    {
        Knapsack knapsack = new Knapsack();
        for (int i = 1; i < 6; i++)
        {
            Console.WriteLine("第" + i + "组数据计算结果为：" + knapsack.getItem("data/input_assign02_0" + i + ".txt"));
            knapsack.clear();
        }
        Console.ReadKey();
    }
}