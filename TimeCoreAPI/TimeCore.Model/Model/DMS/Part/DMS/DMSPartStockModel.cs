using System;

namespace Model
{
    public class DMSPartStockModel : BasicModel, IDMSPartBasicModel
    {
        public int PartID { get; set; }
        public decimal Bestand { get; set; }
        public decimal BestandLager2 { get; set; }
        public string Bestandsfuehrung { get; set; }
        public string Bestandsfuehrung_Uebersetzt { get; set; }
        public decimal BestandStandardlager { get; set; }
        public int BestelleinheitID { get; set; }
        public decimal BestellMenge { get; set; }
        public decimal FilialBestand { get; set; }
        public decimal FilialBestellMenge { get; set; }
        public decimal FilialHoechstBestand { get; set; }
        public decimal FilialMeldeBestand { get; set; }
        public decimal FilialMindestBestand { get; set; }
        public decimal FilialOffeneBestellmenge { get; set; }
        public decimal FilialReservMenge { get; set; }
        public decimal FilialVerfuegBestand { get; set; }
        public decimal Hoechstbestand { get; set; }
        public decimal Mindestbestand { get; set; }
        public decimal OffeneBestellMenge { get; set; }
        public decimal RealerBestand { get; set; }
        public decimal RealerVerfuegBestand { get; set; }
        public decimal ReservMenge { get; set; }
        public decimal ReservMengeStandardlager { get; set; }
        public decimal ReservMengeZweitlager { get; set; }
        public decimal UebernahmeMenge { get; set; }
        public decimal Umschlagsmenge { get; set; }
        public decimal VerfuegBestand { get; set; }
        public decimal VerkaufteMengeFuerBestellvorschlagUmsatz { get; set; }
        public DateTime ZuletztBewegtAb { get; set; }
        public DateTime ZuletztBewegtZu { get; set; }
    }
}
