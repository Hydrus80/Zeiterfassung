using System;

namespace Model
{
    public class DMSPartStorageModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public string EinStandardLagerortFuer12186 { get; set; }
        public string Lagerort2 { get; set; }
        public string Lagerort2Lager2 { get; set; }
        public string Lagerort2LagerX { get; set; }
        public string Lagerort3 { get; set; }
        public string Lagerort3Lager2 { get; set; }
        public string Lagerort3LagerX { get; set; }
        public string LagerorteToolTip { get; set; }
        public decimal Lagerwert { get; set; }
        public DateTime LetzteAenderungEigenesGz { get; set; }
        public DateTime LetzterAbgang { get; set; }
        public DateTime LetzterZugang { get; set; }
        public string StandardLagerort { get; set; }
        public string StandardLagerortLager2 { get; set; }
        public string StandardlagerortLagerX { get; set; }

    }
}
