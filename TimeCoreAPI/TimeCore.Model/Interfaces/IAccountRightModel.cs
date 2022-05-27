
namespace Model
{
    public interface IAccountRightModel : IBasicModel
    {
        public int RightID { get; set; }
        public AccountModel Account { get; set; }
    }
}
