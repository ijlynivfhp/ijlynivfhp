using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FW.Common
{
    public class DynamicClassHelper
    {
        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        private static string Propertystring(string propertyname)
        {
            StringBuilder sbproperty = new StringBuilder();
            sbproperty.Append(" private   string   _" + propertyname + "   =  null;\n");
            sbproperty.Append(" public   string  " + "" + propertyname + "\n");
            sbproperty.Append(" {\n");
            sbproperty.Append(" get{   return   _" + propertyname + ";}   \n");
            sbproperty.Append(" set{   _" + propertyname + "   =   value;   }\n");
            sbproperty.Append(" }");
            return sbproperty.ToString();
        }
        /// <summary>
        /// 创建动态类
        /// </summary>
        /// <param name="listMnProject">属性列表</param>
        /// <returns></returns>
        public static Assembly Newassembly(List<string> propertyList)
        {
            //创建编译器实例。   
            CSharpCodeProvider provider = new CSharpCodeProvider();
            //设置编译参数。   
            CompilerParameters paras = new CompilerParameters();
            paras.GenerateExecutable = false;
            paras.GenerateInMemory = true;

            //创建动态代码。   
            StringBuilder classsource = new StringBuilder();
            classsource.Append("public   class   dynamicclass \n");
            classsource.Append("{\n");

            //创建属性。   
            for (int i = 0; i < propertyList.Count; i++)
            {
                classsource.Append(Propertystring(propertyList[i]));
            }
            classsource.Append("}");
            System.Diagnostics.Debug.WriteLine(classsource.ToString());
            //编译代码。   
            CompilerResults result = provider.CompileAssemblyFromSource(paras, classsource.ToString());
            //获取编译后的程序集。   
            Assembly assembly = result.CompiledAssembly;

            return assembly;
        }
        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="objclass"></param>
        /// <param name="propertyname"></param>
        /// <param name="value"></param>
        public static void Reflectionsetproperty(object objclass, string propertyname, object value)
        {
            PropertyInfo[] infos = objclass.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                if (info.Name == propertyname && info.CanWrite)
                {
                    info.SetValue(objclass, value, null);
                }
            }
        }
        /// <summary>
        /// 得到属性值
        /// </summary>
        /// <param name="objclass"></param>
        /// <param name="propertyname"></param>
        public static void Reflectiongetproperty(object objclass, string propertyname)
        {
            PropertyInfo[] infos = objclass.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                if (info.Name == propertyname && info.CanRead)
                {
                    System.Console.WriteLine(info.GetValue(objclass, null));
                }
            }
        }
    }
}
