using System;

namespace Model
{
    public class FirmModel : IFirmModel
    {
        public int ID { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
    }
}
