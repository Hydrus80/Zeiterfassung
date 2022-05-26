
namespace Model
{
    public interface IWorkshopModel : IBasicModel
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public FirmModel Firm { get; set; }
    }
}
