using System.ComponentModel.DataAnnotations;

namespace CarHireV2.Models
{
    public enum CarRecommendationLevel
    {
        Highest, //主页幻灯片、主页车型、预约幻灯片、预约车型
        Higher, //主页车型、预约幻灯片、预约车型
        High, //预约幻灯片、预约车型
        Normal, //预约车型
        Low
    }

    public enum RouteRecommendationLevel
    {
        High, //主页幻灯片、主页路线
        Normal //主页路线
    }

    public abstract class ObjectBase : IDataType
    {
        public string Name { get; private set; }
        public string ImagePath { get; private set; }
        public string BasicInfo { get; private set; }

        [Key]
        public int ID { get; private set; }
    }

    public class Car : ObjectBase, IDataType
    {
        public bool Enabled { get; private set; }
        public CarRecommendationLevel CarRecommendation { get; private set; }
        public string Detail1 { get; private set; }
        public string DetailImagePath1 { get; private set; }
        public string Detail2 { get; private set; }
        public string DetailImagePath2 { get; private set; }
        public int Speed { get; private set; }
        public int OilCost { get; private set; }
        public double Price { get; private set; }
        public double Deposit { get; private set; }
    }

    public class Route : ObjectBase, IDataType
    {
        public RouteRecommendationLevel RouteRecommendation { get; private set; }
        public string Detail1 { get; private set; }
        public string DetailImagePath1 { get; private set; }
        public string Detail2 { get; private set; }
        public string DetailImagePath2 { get; private set; }
    }

    public class Activity : ObjectBase, IDataType
    {
    }

    public class Store : ObjectBase, IDataType
    {
    }

    public class Airport : ObjectBase, IDataType
    {
    }
}