
/*
 * 掌管UI线程间跳转的问题
 * 可以缓存隐藏的窗体，关闭当前窗体后跳回原来窗体，方式有如下两种
 * 1、可以记录缓存窗体的名称，通过反射加载方式
 * 2、可以直接保存前一个窗体的实例
 * Author：Jiang.Qi.Kang
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace ManageApp.Common
{
    public class WindowsManager
    {
        public static List<string> FormNames;//窗体的完全限定名
        public static Queue<Window> FormsList;
        public static bool IsHide;
        public static Window GetInstanceByAllName(string name)
        {
            Type type = Type.GetType(name);
            if (type != null)
            {
                dynamic obj = type.Assembly.CreateInstance(type.FullName);
                return (Window)obj;
            }
            return null;
        }
        public static Window GetInstanceByClassName(string classNmae,string nameSpace = ConstSettings.FormNameSpace)
        {
            string fullName = string.Format("{0}.{1}", nameSpace, classNmae);
            Assembly assembly = Assembly.GetExecutingAssembly();
            Window window = assembly.CreateInstance(fullName) as Window;
            if (window != null)
            {
                return window;
            }
            
            return null;
        }
        public static object GetObjectInstanceByClassName(string classNmae, string nameSpace = ConstSettings.FormNameSpace)
        {
            string fullName = string.Format("{0}.{1}", nameSpace, classNmae);
            Assembly assembly = Assembly.GetExecutingAssembly();
            object window = assembly.CreateInstance(fullName) as Window;
            if (window != null)
            {
                return window;
            }

            return null;
        }
        public static void ShowWindowHideBefore(Window noWindow,string nextWindow)
        {
            if (FormsList == null)
            {
                FormsList = new Queue<Window>();
            }
            FormsList.Enqueue(noWindow);
            IsHide = true;
        }

    }
}
