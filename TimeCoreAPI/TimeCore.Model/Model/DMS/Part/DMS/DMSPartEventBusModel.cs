using System;

namespace Model
{
    public class DMSPartEventBusModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public string EventBusContext { get; set; }
        public bool EventBusTracking { get; set; }

    }
}
