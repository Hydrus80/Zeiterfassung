using System;

namespace Model
{
    public class DMSPartStellantisModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public string EPPP_CountryCode_TIS { get; set; }
        public string EPPP_FranchiseCode_TIS { get; set; }
        public string EPPP_Katalogseite_TIS { get; set; }
        public string EPPP_Material_TIS { get; set; }
        public string EPPP_PfandteileNr1 { get; set; }
        public string EPPP_PfandteileNr2 { get; set; }
        public DateTime EPPP_Preismaster_TIS { get; set; }
        public DateTime EPPP_Preisupdate_TIS { get; set; }
        public string EPPP_Versionsnummer_TIS { get; set; }
        public int FiatDLPPartInformation_ID { get; set; }
        public bool FIATPrim2Unterdruecken { get; set; }
        public decimal HUB_Rabatt { get; set; }
        public string HUB_Rabattcode { get; set; }
        public string KatalogNr_TIS { get; set; }
        public DateTime LetzterExportOpel { get; set; }
        public DateTime PRIM2Datum { get; set; }
        public decimal PSA_Rabatt { get; set; }
        public string PSA_Rabattcode { get; set; }
        public bool TISUpdateDeaktiviert { get; set; }
        public string Verkaufsorganisation_TIS { get; set; }
        public string Waehrung_TIS { get; set; }

    }
}
