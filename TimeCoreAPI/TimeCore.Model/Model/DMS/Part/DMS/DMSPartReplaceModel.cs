using System;

namespace Model
{
    public class DMSPartReplaceModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public bool ErsetzungBedingung { get; set; }
        public bool ErsetzungEinzel { get; set; }
        public bool ErsetzungMehrfach { get; set; }
        public bool ErsterBestandWar { get; set; }
        public string Folgeartikel { get; set; }
        public int FolgeartikelLieferant { get; set; }

    }
}
