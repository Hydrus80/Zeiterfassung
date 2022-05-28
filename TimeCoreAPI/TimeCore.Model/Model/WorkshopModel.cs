using System;

namespace Model
{
    public class WorkshopModel : BasicModel
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public virtual FirmModel Firm { get; set; }
    }
}
