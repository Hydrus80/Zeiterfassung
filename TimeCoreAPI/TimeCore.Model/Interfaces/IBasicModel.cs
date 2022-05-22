using System;

namespace Model
{
    public interface IBasicModel
    {
        public int ID { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
