using System;

namespace Model
{
    public class DMSPartProductGroupModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public string WG { get; set; }
        public bool WGSchuetzen { get; set; }

    }
}
