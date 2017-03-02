using System;
using System.ComponentModel.DataAnnotations;

namespace CarHireV2.Models
{
    public enum CommentType
    {
        Good,
        Middle,
        Bad
    }

    public class Comment : IDataType
    {
        public Comment()
        {
            // Reserved for DataContext
        }

        public Comment(User user, Car car, int rating, string content)
        {
            User = user;
            Car = car;
            UpdateComment(rating, content);
        }

        public DateTime DateCreated { get; private set; }
        public User User { get; private set; }
        public Car Car { get; private set; }
        public CommentType Type { get; private set; }
        public int Rating { get; private set; }
        public string Content { get; private set; }

        [Key]
        public int ID { get; private set; }

        public void UpdateComment(int rating, string content)
        {
            DateCreated = DateTime.Now;
            Rating = rating;
            switch (rating)
            {
                case 5:
                case 4:
                    Type = CommentType.Good;
                    break;
                case 3:
                    Type = CommentType.Middle;
                    break;
                default:
                    Type = CommentType.Bad;
                    break;
            }
            Content = content;
        }
    }
}