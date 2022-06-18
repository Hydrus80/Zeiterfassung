using System;

namespace Model
{
    public class DMSPartClassicModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public int IdAusWerbasClassic { get; set; }
        public int IdAusWerbasClassic1 { get; set; }
        public int IdAusWerbasClassic2 { get; set; }
        public int IdAusWerbasClassic3 { get; set; }
        public int IdAusWerbasClassic4 { get; set; }

    }
}
