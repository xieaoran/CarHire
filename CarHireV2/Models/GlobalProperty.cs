using System.ComponentModel.DataAnnotations;

namespace CarHireV2.Models
{
    public class GlobalProperty : IDataType
    {
        public string PlaneImagePath1 { get; private set; }
        public string PlaneTitle1 { get; private set; }
        public string PlaneDescription1 { get; private set; }
        public string PlaneImagePath2 { get; private set; }
        public string PlaneTitle2 { get; private set; }
        public string PlaneDescription2 { get; private set; }

        [Key]
        public int ID { get; private set; }
    }
}