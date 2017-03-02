using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CarHireV2.Models
{
    public class DataContext : DbContext
    {
        public DbSet<GlobalProperty> GlobalProperties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PlaneOrder> PlaneOrders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }

    public class DataLists
    {
        public DataLists()
        {
            DataContext = new DataContext();
            GlobalProperties = DataContext.GlobalProperties.ToList();
            Users = DataContext.Users.ToList();
            Orders = DataContext.Orders.ToList();
            PlaneOrders = DataContext.PlaneOrders.ToList();
            Comments = DataContext.Comments.ToList();
            AllCars = DataContext.Cars.ToList();
            EnabledCars = DataContext.Cars.Where(car => car.Enabled).ToList();
            Routes = DataContext.Routes.ToList();
            Activities = DataContext.Activities.ToList();
            Stores = DataContext.Stores.ToList();
            Airports = DataContext.Airports.ToList();
        }

        public DataContext DataContext { get; set; }
        public List<GlobalProperty> GlobalProperties { get; set; }
        public List<User> Users { get; set; }
        public List<Order> Orders { get; set; }
        public List<PlaneOrder> PlaneOrders { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Car> AllCars { get; set; }
        public List<Car> EnabledCars { get; set; }
        public List<Route> Routes { get; set; }
        public List<Activity> Activities { get; set; }
        public List<Store> Stores { get; set; }
        public List<Airport> Airports { get; set; }
    }

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            SelectList();
            SelectCarousel();
        }

        public List<Activity> ActivityList { get; private set; }
        public List<Car> CarCarousel { get; private set; }
        public List<Car> CarList { get; private set; }
        public List<Route> RouteCarousel { get; private set; }
        public List<Route> RouteList { get; private set; }

        private void SelectCarousel()
        {
            CarCarousel = CarList.Where
                (car => car.CarRecommendation == CarRecommendationLevel.Highest).ToList();
            RouteCarousel = RouteList.Where
                (route => route.RouteRecommendation == RouteRecommendationLevel.High).ToList();
        }

        private void SelectList()
        {
            ActivityList = DataRuntime.RuntimeData.Activities.ToList();
            CarList = DataRuntime.RuntimeData.EnabledCars.Where
                (car => car.CarRecommendation == CarRecommendationLevel.Highest ||
                        car.CarRecommendation == CarRecommendationLevel.Higher).ToList();
            RouteList = DataRuntime.RuntimeData.Routes.ToList();
        }
    }

    public class SelectViewModel
    {
        public SelectViewModel(int? selectedCarIndex)
        {
            SelectList(selectedCarIndex);
            SelectCarousel();
        }

        public List<Car> CarCarousel { get; private set; }
        public List<Car> CarList { get; private set; }
        public List<Store> StoreList { get; private set; }

        private void SelectCarousel()
        {
            CarCarousel = DataRuntime.RuntimeData.EnabledCars.Where
                (car => car.CarRecommendation != CarRecommendationLevel.Low &&
                        car.CarRecommendation != CarRecommendationLevel.Normal).ToList();
        }

        private void SelectList(int? selectedCarIndex)
        {
            if (selectedCarIndex == null)
            {
                CarList = CommonHelpers.RandomCarList(DataRuntime.RuntimeData.EnabledCars.Where
                    (car => car.CarRecommendation != CarRecommendationLevel.Low).ToList(), null, 3);
            }
            else
            {
                CarList = CommonHelpers.RandomCarList(DataRuntime.RuntimeData.EnabledCars.Where
                    (car => car.CarRecommendation != CarRecommendationLevel.Low).ToList(), selectedCarIndex, 2);
                CarList.Insert(0, DataRuntime.RuntimeData.EnabledCars.First(car => car.ID == selectedCarIndex));
            }
            StoreList = DataRuntime.RuntimeData.Stores.ToList();
        }
    }

    public class PlaneViewModel
    {
        public PlaneViewModel()
        {
            Property = DataRuntime.RuntimeProperty;
            Airports = DataRuntime.RuntimeData.Airports.ToList();
        }

        public GlobalProperty Property { get; private set; }
        public List<Airport> Airports { get; private set; }
    }

    public class UserPanelViewModel
    {
        public UserPanelViewModel(int userID)
        {
            Property = DataRuntime.RuntimeProperty;
            User = DataRuntime.RuntimeData.Users.First(user => user.ID == userID);
            var userOrders = DataRuntime.RuntimeData.Orders.Where(order => order.User.ID == userID).ToList();
            CurrentOrders = userOrders.Where(order => order.Condition < OrderCondition.ReturnSuccess).ToList();
            HistoryOrders = userOrders.Where(order => order.Condition == OrderCondition.ReturnSuccess).ToList();
            PlaneOrders = DataRuntime.RuntimeData.PlaneOrders.Where(
                order => order.User.ID == userID && order.Condition != PlaneOrderCondition.Cancelled).ToList();
        }

        public GlobalProperty Property { get; private set; }
        public User User { get; private set; }
        public List<Order> CurrentOrders { get; private set; }
        public List<PlaneOrder> PlaneOrders { get; private set; }
        public List<Order> HistoryOrders { get; private set; }
    }

    public class MaxValues
    {
        public MaxValues()
        {
            var anyCars = DataRuntime.RuntimeData.EnabledCars.Any();
            MaxSpeed = anyCars ?
                DataRuntime.RuntimeData.EnabledCars.Max(car => car.Speed)
                : 0;
            MaxOilCost = anyCars ?
                DataRuntime.RuntimeData.EnabledCars.Max(car => car.OilCost)
                : 0;
            MaxPrice = anyCars ? 
                DataRuntime.RuntimeData.EnabledCars.Max(car => car.Price) 
                : 0;
        }

        public int MaxSpeed { get; private set; }
        public int MaxOilCost { get; private set; }
        public double MaxPrice { get; private set; }
    }

    public class CarAPIModel
    {
        public CarAPIModel(int carID)
        {
            Car = DataRuntime.RuntimeData.EnabledCars.First(car => car.ID == carID);
            SpeedPercentage = (int) Math.Round(Car.Speed*100.0/DataRuntime.RuntimeMaxValues.MaxSpeed);
            OilCostPercentage = (int) Math.Round(Car.OilCost*100.0/DataRuntime.RuntimeMaxValues.MaxOilCost);
            PricePercentage = (int) Math.Round(Car.Price*100.0/DataRuntime.RuntimeMaxValues.MaxPrice);
        }

        public Car Car { get; private set; }
        public int SpeedPercentage { get; private set; }
        public int OilCostPercentage { get; private set; }
        public int PricePercentage { get; private set; }
    }
}