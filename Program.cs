using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map();

            //map.path = map.DataMapText;
            //map.floorMap = File.ReadAllLines(map.path);
            //map.mapLayout = new char[map.floorMap.Length, map.floorMap[0].Length];
            map.MapTxt();
       
        }
    }
}
