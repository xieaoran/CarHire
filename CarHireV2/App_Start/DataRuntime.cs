using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarHireV2.Models;

namespace CarHireV2
{
    public class DataRuntime
    {
        public static DataLists RuntimeData { get; set; }
        public static GlobalProperty RuntimeProperty { get; set; }
        public static IndexViewModel RuntimeIndex { get; set; }
        public static PlaneViewModel RuntimePlane { get; set; }
        public static MaxValues RuntimeMaxValues { get; set; }

        public static void Load()
        {
            RuntimeData = new DataLists();
            RuntimeProperty = RuntimeData.GlobalProperties.First();
            RuntimeIndex = new IndexViewModel();
            RuntimePlane = new PlaneViewModel();
            RuntimeMaxValues = new MaxValues();
        }
    }
}