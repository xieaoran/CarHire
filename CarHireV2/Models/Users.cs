using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;

namespace CarHireV2.Models
{
    //此处未添加初始化id操作
    public abstract class UserBase : IDataType
    {
        protected UserBase()
        {
            // Reserved for DataContext
        }

        protected UserBase(string email, string password)
        {
            Email = email.ToLower();
            Password = CommonHelpers.RSAEncrypt(password);
        }

        [RegularExpression(@"^[a-z0-9|\-|_]+@[a-z0-9]+(\.[a-z]+)+$")]
        public string Email { get; private set; }

        public string Password { get; private set; }

        [Key]
        public int ID { get; private set; }

        public virtual bool CheckPassword(string password)
        {
            return password == CommonHelpers.RSADecrypt(Password);
        }

        public virtual void ChangePassword(string newPassword)
        {
            Password = CommonHelpers.RSAEncrypt(newPassword);
        }
    }

    public class User : UserBase, IDataType
    {
        public User()
        {
            // Reserved for DataContext
        }

        public User(string email, string password, string name, long cellPhoneNumber)
            : base(email, password)
        {
            Name = name;
            CellPhoneNumber = cellPhoneNumber;
        }

        [StringLength(30)]
        public string Name { get; private set; }

        [RegularExpression(@"^1\d{10}$")]
        public long CellPhoneNumber { get; private set; }

        public void ChangeInfo(string name, string cellPhoneNumberStr)
        {
            if (name != "") Name = name;
            if (cellPhoneNumberStr != "")
            {
                long cellPhoneNumber;
                if (!long.TryParse(cellPhoneNumberStr, out cellPhoneNumber))
                    throw new DbEntityValidationException();
                CellPhoneNumber = cellPhoneNumber;
            }
        }
    }
}