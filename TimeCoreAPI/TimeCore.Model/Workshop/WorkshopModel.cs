using System;

namespace Model
{
    public class WorkshopModel : IBasicModel, IWorkshopModel
    {
        public int ID { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public IFirmModel Firm { get; set; }
    }
}
