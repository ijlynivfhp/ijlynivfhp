using FW.Common;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FW.Console
{
    internal class Program
    {
        static volatile int x, y, a, b;
        static void Main(string[] args)
        {


            while (true)
            {
                var t1 = Task.Run(Test1);
                var t2 = Task.Run(Test2);
                Task.WaitAll(t1, t2);
                if (a == 0 && b == 0)
                {
                    System.Console.WriteLine("{0}, {1}", a, b);
                }
                else
                {
                    System.Console.WriteLine("{0}, {1}", a, b);
                }
                x = y = a = b = 0;
            }


            //创建编译器实例
            CSharpCodeProvider provider = new CSharpCodeProvider();
            //设置编译参数
            CompilerParameters paras = new CompilerParameters { GenerateExecutable = false, GenerateInMemory = true };
            //创建动态代码
            StringBuilder classSource = new StringBuilder();
            classSource.Append("public class DynamicClass \n");
            classSource.Append("{\n");
            classSource.Append(" public int Id { set; get; }  \n");
            classSource.Append(" public string Name { set; get; }  \n");
            classSource.Append(" public int Age { set; get; }  \n");
            classSource.Append(" public int context1 { set; get; }  \n");
            classSource.Append("}");
            System.Console.WriteLine(classSource.ToString());

            //编译代码 
            CompilerResults result = provider.CompileAssemblyFromSource(paras, classSource.ToString());
            //获取编译后的程序集
            Assembly assembly = result.CompiledAssembly;

            object obclass = assembly.CreateInstance("DynamicClass");

            Type dynamicType = obclass.GetType();
            var newObject = Activator.CreateInstance(dynamicType);
        }
        static void Test1()
        {
            x = 1;
            //方案一，只用一个 Interlocked.MemoryBarrierProcessWide();test2不需要添加内存屏障。问题就可以解决
            //方案二 Interlocked.MemoryBarrier(); 为什么不用这个内存屏障，即使添加了也还是会出现，必须同时在test、和test2中同时添加
            a = y;
        }

        static void Test2()
        {
            y = 1;

            //方案二 Interlocked.MemoryBarrier(); 
            b = x;
        }
    }
}
