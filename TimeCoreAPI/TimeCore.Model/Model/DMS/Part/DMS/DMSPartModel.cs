using System;

namespace Model
{
    public class DMSPartModel : BasicModel
    {
        public string ABC_Kennzeichen { get; set; }
        public bool Agenturware { get; set; }
        public DateTime Angelegt { get; set; }
        public string AnlageHerkunft { get; set; }
        public DateTime Artikel_Gueltig_Ab { get; set; }
        public bool ArtikelBezSchuetzen { get; set; }
        public bool ArtikelNrSchuetzen { get; set; }
        public string ArtikelnummerAlt { get; set; }
        public string ArtikelnummerNeu { get; set; }
        public int ArtikelPSAExtensionID { get; set; }
        public string AwnDid { get; set; }
        public string Barcode { get; set; }
        public string Bauteilschluessel { get; set; }
        public decimal Breite { get; set; }
        public string EigenesGaengigkeitszeichen { get; set; }
        public string EinspeiserNummer { get; set; }
        public bool Eisgriffigkeit { get; set; }
        public bool ErsteVerwendungWar { get; set; }
        public string ExterneRollgeraeuschKlasse { get; set; }
        public int ExternesRollgerausch { get; set; }
        public string Fahrzeugzuordnung { get; set; }
        public string Gaengigkeitszeichen { get; set; }
        public DateTime Geaendert { get; set; }
        public string Gebrauchsnummer { get; set; }
        public string GebrauchsNummernList { get; set; }
        public bool Gefahrgutkennzeichen { get; set; }
        public string Gefahrguttext { get; set; }
        public decimal Gewicht { get; set; }
        public string Grundeinheit { get; set; }
        public int GruppenKopfNr { get; set; }
        public bool GruppenKopfNrErzwingen { get; set; }
        public bool HaltbarkeitBeachten { get; set; }
        public int HaltbarkeitMonate { get; set; }
        public bool HatLagerdaten { get; set; }
        public bool? HatNachfolger { get; set; }
        public bool? HatVorgaenger { get; set; }
        public string Hinweis { get; set; }
        public string Hinweis2 { get; set; }
        public string Hinweis3 { get; set; }
        public decimal Hoehe { get; set; }
        public string ImageURL { get; set; }
        public int ImportTransaktion_ID { get; set; }
        public bool IsAlternativArtikel { get; set; }
        public bool IsPseudo { get; set; }
        public bool Kampagnenteil { get; set; }
        public bool Kommissionsware { get; set; }
        public string Kraftstoffeffizienzklasse { get; set; }
        public string KraftstoffeffizienzklasseAlt { get; set; }
        public decimal Laenge { get; set; }
        public bool LagerAnzeigen { get; set; }
        public DateTime LetzterHotasExport { get; set; }
        public string LL_TH_Herkunftsname { get; set; }
        public string LL_TH_Kuerzel { get; set; }
        public decimal MaxHaltbarkeit { get; set; }
        public int Mitarbeiter_ID { get; set; }
        public string Nasshaftungsklasse { get; set; }
        public string NasshaftungsklasseAlt { get; set; }
        public string NebenkostenCode { get; set; }
        public string NichtGelöschtWeil { get; set; }
        public bool NotLongerService { get; set; }
        public string RabattCode { get; set; }
        public bool RGSchuetzen { get; set; }
        public bool Ruecksendepflichtig_TIS { get; set; }
        public DateTime Saison1BisMonat { get; set; }
        public DateTime Saison1VonMonat { get; set; }
        public DateTime Saison2BisMonat { get; set; }
        public DateTime Saison2VonMonat { get; set; }
        public bool Schluesselschliessung { get; set; }
        public bool Schneegriffigkeit { get; set; }
        public string Sortimentskennzeichen { get; set; }
        public string VergleichsNr { get; set; }
        public string VergleichsNummernList { get; set; }
        public string Vertriebsweg_TIS { get; set; }
        public decimal Vorschlagsmenge { get; set; }
        public string VorzugsartikelNr { get; set; }
    }
}
