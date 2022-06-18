using System;
using System.ComponentModel;
using System.Reflection;

namespace Model
{
    public interface ICArtikel
    {
        public string ABC_Kennzeichen { get; set; }
        public bool Agenturware { get; set; }
        public decimal Akt_EK { get; set; }
        public DateTime Akt_EK_Datum { get; set; }
        public bool AngebotMitMWST { get; set; }
        public bool AngebotsPreiseRechnen { get; set; }
        public decimal AngebotsVK { get; set; }
        public DateTime AngebotsVK_Bis { get; set; }
        public decimal AngebotsVK_Incl { get; set; }
        public decimal AngebotsVK_Marge { get; set; }
        public decimal AngebotsVK_Mwst { get; set; }
        public DateTime AngebotsVK_Von { get; set; }
        public DateTime Angelegt { get; set; }
        public string AnlageHerkunft { get; set; }
        public decimal AnzahlVerkauftM { get; set; }
        public decimal AnzahlVerkauftMM1 { get; set; }
        public decimal AnzahlVerkauftMM10 { get; set; }
        public decimal AnzahlVerkauftMM11 { get; set; }
        public decimal AnzahlVerkauftMM12 { get; set; }
        public decimal AnzahlVerkauftMM13 { get; set; }
        public decimal AnzahlVerkauftMM14 { get; set; }
        public decimal AnzahlVerkauftMM15 { get; set; }
        public decimal AnzahlVerkauftMM16 { get; set; }
        public decimal AnzahlVerkauftMM17 { get; set; }
        public decimal AnzahlVerkauftMM18 { get; set; }
        public decimal AnzahlVerkauftMM19 { get; set; }
        public decimal AnzahlVerkauftMM2 { get; set; }
        public decimal AnzahlVerkauftMM20 { get; set; }
        public decimal AnzahlVerkauftMM21 { get; set; }
        public decimal AnzahlVerkauftMM22 { get; set; }
        public decimal AnzahlVerkauftMM23 { get; set; }
        public decimal AnzahlVerkauftMM24 { get; set; }
        public decimal AnzahlVerkauftMM3 { get; set; }
        public decimal AnzahlVerkauftMM4 { get; set; }
        public decimal AnzahlVerkauftMM5 { get; set; }
        public decimal AnzahlVerkauftMM6 { get; set; }
        public decimal AnzahlVerkauftMM7 { get; set; }
        public decimal AnzahlVerkauftMM8 { get; set; }
        public decimal AnzahlVerkauftMM9 { get; set; }
        public int AnzMonateZumDurchschnittsEK { get; set; }
        public DateTime Artikel_Gueltig_Ab { get; set; }
        public bool ArtikelBezSchuetzen { get; set; }
        public bool ArtikelNrSchuetzen { get; set; }
        public string ArtikelnummerAlt { get; set; }
        public string ArtikelnummerNeu { get; set; }
        public int ArtikelPSAExtensionID { get; set; }
        public decimal AufAbschlag1 { get; set; }
        public decimal AufAbschlag2 { get; set; }
        public decimal AufAbschlag3 { get; set; }
        public decimal AufAbschlag4 { get; set; }
        public bool Auszeichnunggilt { get; set; }
        public decimal Auszeichnungspreis { get; set; }
        public string AwnDid { get; set; }
        public string Barcode { get; }
        public string Bauteilschluessel { get; set; }
        public decimal Bestand { get; set; }
        public decimal BestandLager2 { get; set; }
        public string Bestandsfuehrung { get; set; }
        public string Bestandsfuehrung_Uebersetzt { get; set; }
        public decimal BestandStandardlager { get; set; }
        public int BestelleinheitID { get; set; }
        public decimal BestellMenge { get; set; }
        public decimal Bonus_EK { get; set; }
        public string Bonuskennung { get; set; }
        public decimal Breite { get; set; }
        public bool DAFMdiUnterdruecken { get; set; }
        public decimal Durchschn_EK { get; set; }
        public decimal Durchschn_EK_Fix { get; set; }
        public string EigenesGaengigkeitszeichen { get; set; }
        public string EinspeiserNummer { get; set; }
        public string EinStandardLagerortFuer12186 { get; }
        public bool Eisgriffigkeit { get; set; }
        public decimal EK_Vorschau { get; set; }
        public bool EKfix { get; set; }
        public int EKRabGruppe_ID { get; set; }
        public string EPPP_CountryCode_TIS { get; set; }
        public string EPPP_FranchiseCode_TIS { get; set; }
        public string EPPP_Katalogseite_TIS { get; set; }
        public string EPPP_Material_TIS { get; set; }
        public string EPPP_PfandteileNr1 { get; set; }
        public string EPPP_PfandteileNr2 { get; set; }
        public DateTime EPPP_Preismaster_TIS { get; set; }
        public DateTime EPPP_Preisupdate_TIS { get; set; }
        public string EPPP_Versionsnummer_TIS { get; set; }
        public bool ErsetzungBedingung { get; set; }
        public bool ErsetzungEinzel { get; set; }
        public bool ErsetzungMehrfach { get; set; }
        public bool ErsterBestandWar { get; set; }
        public bool ErsteVerwendungWar { get; set; }
        public string EventBusContext { get; }
        public bool EventBusTracking { get; }
        public string ExterneRollgeraeuschKlasse { get; set; }
        public int ExternesRollgerausch { get; set; }
        public string Fahrzeugzuordnung { get; set; }
        public int FiatDLPPartInformation_ID { get; set; }
        public bool FIATPrim2Unterdruecken { get; set; }
        public decimal FilialBestand { get; set; }
        public decimal FilialBestellMenge { get; set; }
        public decimal FilialHoechstBestand { get; set; }
        public decimal FilialMeldeBestand { get; set; }
        public decimal FilialMindestBestand { get; set; }
        public decimal FilialOffeneBestellmenge { get; set; }
        public decimal FilialReservMenge { get; set; }
        public decimal FilialVerfuegBestand { get; set; }
        public string Folgeartikel { get; set; }
        public int FolgeartikelLieferant { get; set; }
        public string Gaengigkeitszeichen { get; set; }
        public decimal Garantiepreis { get; set; }
        public DateTime Geaendert { get; set; }
        public string Gebrauchsnummer { get; set; }
        public string GebrauchsNummernList { get; set; }
        public bool Gefahrgutkennzeichen { get; set; }
        public string Gefahrguttext { get; set; }
        public decimal Gewicht { get; set; }
        public string Grundeinheit { get; set; }
        public int GruppenKopfNr { get; set; }
        public bool GruppenKopfNrErzwingen { get; set; }
        public bool HaltbarkeitBeachten { get; }
        public int HaltbarkeitMonate { get; set; }
        public bool HasSubBranch { get; set; }
        public bool HatLagerdaten { get; set; }
        public bool? HatNachfolger { get; set; }
        public bool? HatVorgaenger { get; set; }
        public string Hinweis { get; set; }
        public string Hinweis2 { get; set; }
        public string Hinweis3 { get; set; }
        public decimal Hoechstbestand { get; set; }
        public decimal Hoehe { get; set; }
        public decimal HUB_Rabatt { get; set; }
        public string HUB_Rabattcode { get; set; }
        public int IdAusWerbasClassic { get; set; }
        public int IdAusWerbasClassic1 { get; set; }
        public int IdAusWerbasClassic2 { get; set; }
        public int IdAusWerbasClassic3 { get; set; }
        public int IdAusWerbasClassic4 { get; set; }
        public string ImageURL { get; set; }
        public int ImportTransaktion_ID { get; set; }
        public decimal IndividuellerEKRabatt { get; set; }
        public PropertyInfo[] InnerClassPropinfs { get; set; }
        public PropertyInfo[] InnerClassPropinfsEingelagerte { get; }
        public decimal Internerpreis { get; set; }
        public bool IsAlternativArtikel { get; }
        public bool IsPseudo { get; set; }
        public bool IvecoRamsesUnterdruecken { get; set; }
        public bool Kampagnenteil { get; set; }
        public string KatalogNr_TIS { get; set; }
        public bool KeinAuszeichnungspreisErmitteln { get; set; }
        public bool Kommissionsware { get; set; }
        public string Kraftstoffeffizienzklasse { get; set; }
        public string KraftstoffeffizienzklasseAlt { get; set; }
        public decimal Laenge { get; set; }
        public bool LagerAnzeigen { get; set; }
        public string Lagerort2 { get; set; }
        public string Lagerort2Lager2 { get; set; }
        public string Lagerort2LagerX { get; set; }
        public string Lagerort3 { get; set; }
        public string Lagerort3Lager2 { get; set; }
        public string Lagerort3LagerX { get; set; }
        public string LagerorteToolTip { get; }
        public decimal Lagerwert { get; }
        public DateTime LetzteAenderungEigenesGz { get; set; }
        public decimal Letzter_EK { get; set; }
        public DateTime LetzterAbgang { get; set; }
        public DateTime LetzterExportOpel { get; set; }
        public DateTime LetzterHotasExport { get; set; }
        public DateTime LetzterZugang { get; set; }
        public DateTime Letztes_EK_Datum { get; set; }
        public int LieferantID { get; set; }
        public string LieferantME { get; set; }
        public string LieferantMwSt { get; set; }
        public string LieferantWG { get; set; }
        public int LiRabattgruppeID { get; set; }
        public decimal Listen_EK { get; set; }
        public decimal Listen_VK { get; set; }
        public decimal ListenVK_Incl { get; set; }
        public decimal ListenVK_Marge { get; set; }
        public decimal ListenVK_Mwst { get; set; }
        public string LL_TH_Herkunftsname { get; }
        public string LL_TH_Kuerzel { get; }
        public bool MANFremdteil { get; set; }
        public bool ManuelleErfassungVKs { get; set; }
        public int Marke { get; set; }
        public decimal MaxHaltbarkeit { get; set; }
        public int MengeneinheitID { get; set; }
        public decimal MengeProVerpackungseinheit { get; set; }
        public decimal Mindestbestand { get; set; }
        public decimal Mischpreis { get; set; }
        public int Mitarbeiter_ID { get; set; }
        public string MwstKennzeichen { get; set; }
        public string Nasshaftungsklasse { get; set; }
        public string NasshaftungsklasseAlt { get; set; }
        public string NebenkostenCode { get; set; }
        public DateTime NeuePreiseAb { get; set; }
        public decimal NeuerListenEK { get; set; }
        public decimal NeuerListenVK { get; set; }
        public string NichtGelöschtWeil { get; set; }
        public bool NotLongerService { get; set; }
        public decimal OffeneBestellMenge { get; set; }
        public DateTime PreiseGeaendert { get; set; }
        public int Preiseinheit { get; set; }
        public decimal PreisProGrundeinheit { get; set; }
        public string Preistyp { get; set; }
        public DateTime PRIM2Datum { get; set; }
        public string Produkttypengruppe { get; set; }
        public PropertyDescriptorCollection PropDescCollection { get; set; }
        public decimal PSA_Rabatt { get; set; }
        public string PSA_Rabattcode { get; set; }
        public decimal Rabatt_auf_ListenVK { get; set; }
        public bool Rabattausschluss { get; set; }
        public string RabattCode { get; set; }
        public decimal RealerBestand { get; }
        public decimal RealerVerfuegBestand { get; }
        public decimal ReservMenge { get; set; }
        public decimal ReservMengeStandardlager { get; set; }
        public decimal ReservMengeZweitlager { get; set; }
        public bool RGSchuetzen { get; set; }
        public bool Ruecksendepflichtig_TIS { get; set; }
        public DateTime Saison1BisMonat { get; set; }
        public DateTime Saison1VonMonat { get; set; }
        public DateTime Saison2BisMonat { get; set; }
        public DateTime Saison2VonMonat { get; set; }
        public bool Schluesselschliessung { get; set; }
        public bool Schneegriffigkeit { get; set; }
        public bool SEATAutopartDabeiIn310 { get; set; }
        public bool SEATAutopartUnterdruecken { get; set; }
        public decimal ShowAktEK { get; }
        public decimal ShowAngebotsVK { get; }
        public decimal ShowAngebotsVK_Incl { get; }
        public decimal ShowDurchschn_EK { get; }
        public decimal ShowDurchschn_EK_Fix { get; }
        public decimal ShowVK { get; }
        public decimal ShowVK_Incl { get; }
        public decimal ShowVK2 { get; }
        public decimal ShowVK2_Incl { get; }
        public decimal ShowVK3 { get; }
        public decimal ShowVK3_Incl { get; }
        public decimal ShowVK4 { get; }
        public decimal ShowVK4_Incl { get; }
        public string Sortimentskennzeichen { get; set; }
        public bool Staffelpreise { get; set; }
        public string StandardLagerort { get; set; }
        public string StandardLagerortLager2 { get; set; }
        public string StandardlagerortLagerX { get; set; }
        public bool TISUpdateDeaktiviert { get; set; }
        public decimal UebernahmeMenge { get; set; }
        public decimal Umschlagsmenge { get; set; }
        public decimal VerfuegBestand { get; set; }
        public string VergleichsNr { get; set; }
        public string VergleichsNummernList { get; set; }
        public string Verkaufsorganisation_TIS { get; set; }
        public decimal VerkaufteMengeFuerBestellvorschlagUmsatz { get; set; }
        public int VerpackungseinheitID { get; set; }
        public string Vertriebsweg_TIS { get; set; }
        public decimal VK { get; set; }
        public decimal VK_Incl { get; set; }
        public decimal VK_Incl_LL { get; }
        public decimal VK_Marge { get; set; }
        public decimal VK_Mwst { get; set; }
        public decimal VK2 { get; set; }
        public decimal VK2_Incl { get; set; }
        public decimal VK2_Marge { get; set; }
        public decimal VK2_Mwst { get; set; }
        public decimal VK3 { get; set; }
        public decimal VK3_Incl { get; set; }
        public decimal VK3_Marge { get; set; }
        public decimal VK3_Mwst { get; set; }
        public decimal VK4 { get; set; }
        public decimal VK4_Incl { get; set; }
        public decimal VK4_Marge { get; set; }
        public decimal VK4_Mwst { get; set; }
        public int VKBasis1 { get; set; }
        public int VKBasis2 { get; set; }
        public int VKBasis3 { get; set; }
        public int VKBasis4 { get; set; }
        public int VKRabGruppeID { get; set; }
        public decimal Vorschlagsmenge { get; set; }
        public string VorzugsartikelNr { get; set; }
        public string Waehrung_TIS { get; set; }
        public string WG { get; set; }
        public bool WGSchuetzen { get; set; }
        public DateTime ZuletztBewegtAb { get; set; }
        public DateTime ZuletztBewegtZu { get; set; }
        public string Zusatzfeld1 { get; set; }
        public string Zusatzfeld10 { get; set; }
        public string Zusatzfeld11 { get; set; }
        public string Zusatzfeld12 { get; set; }
        public string Zusatzfeld13 { get; set; }
        public string Zusatzfeld14 { get; set; }
        public string Zusatzfeld15 { get; set; }
        public string Zusatzfeld16 { get; set; }
        public string Zusatzfeld2 { get; set; }
        public string Zusatzfeld3 { get; set; }
        public string Zusatzfeld4 { get; set; }
        public string Zusatzfeld5 { get; set; }
        public string Zusatzfeld6 { get; set; }
        public string Zusatzfeld7 { get; set; }
        public string Zusatzfeld8 { get; set; }
        public string Zusatzfeld9 { get; set; }
    }
}