
namespace Model
{
    public interface IAccountModel : IBasicModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
