using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using imady.Domain.Weixin;
using imady.WxService;
using Newtonsoft.Json;


namespace ExploreRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new b();
            Console.WriteLine(a.getstr());
            Console.ReadLine();
        }

    }

    public class a
    {
        public virtual string str ()
        {
            return "a";
        }

        public virtual string getstr()
        {
            return str();
        }
    }

    public class b:a
    {
        public new string str()
        {
            return "b";
        }
    }

}
