using BO_Basis;
using BO_Serial;
using DA_Basis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wag.Dev.Common.Models;

namespace BO_Remote.PSA.ASR.ModulService
{
    public interface ICPSASurveyModulService
    {
        /// <summary>
        /// Holt folgende Auftraege aus der DB
        /// - Die keine interne Auftragsart haben
        /// - Die den Zustand Rechnung gedruckt haben
        /// - Die ein Opel Fahrzeug haben
        /// - Die der aktuellen Filiale zugeordnet sind
        /// - Aus dem gesuchten Zeitraum
        /// </summary>
        /// <param name="DA">Data Access</param>
        /// <param name="selStartDate">Startdatum</param>
        /// <param name="selEndDate">Enddatum</param>
        /// <returns></returns>
        Task<WerbasResult> GetInvoicedOpelWorkOrdersForSurvey(DataAccess DA, DateTime selStartDate, DateTime selEndDate);

        /// <summary>
        /// FzMarke Opel anhand FzMarkePSA.Uid = 02 holen
        /// Hat als SelectSingleProperty ID gesetzt
        /// </summary>
        /// <returns></returns>
        CSuche GetOpelBrandForWorkOrderSearch();

        /// <summary>
        /// FzListe Opel mit der uebergebenen FzMarke-Suche
        /// Hat als SelectSingleProperty ID gesetzt
        /// </summary>
        /// <param name="opelBrandSearch">Aufgebaute CFzMarkeList-Suche fuer Opel</param>
        /// <returns></returns>
        CSuche GetOpelVehicleForWorkOrderSearch(CSuche opelBrandSearch);

        /// <summary>
        /// Auftragsrechnungen in dem gesuchten Zeitbereich
        /// holen
        /// </summary>
        /// <param name="selStartDate">Startdatum</param>
        /// <param name="selEndDate">Enddatum</param>
        /// <returns></returns>
        CSuche GetWorkOrderInvoiceForWorkOrderSearch(DateTime selStartDate, DateTime selEndDate);

        /// <summary>
        /// Holt die auszuschliessenden Auftragsarten fuer die Suche
        /// Im Moment nur interne Auftragsarten.
        /// Hat als SelectSingleProperty ID gesetzt.
        /// </summary>
        /// <returns></returns>
        CSuche GetExcludedWorkOrderTypForWorkOrderSearch();

        /// <summary>
        /// Holt den in den ASR-Einstellungen hinterlegten Exportpfad.
        /// Dieser ist in der Schmoddertabelle hinterlegt. Ist
        /// kein Pfad ermitteltbar, so wird ein Fehler zurueck gegeben
        /// </summary>
        /// <param name="DA">Data Access</param>
        /// <returns></returns>
        Task<WerbasResult> GetASRExportPath(DataAccess DA);

        /// <summary>
        /// Holt den in den ASR-Einstellungen hinterlegten RRDI-Code.
        /// Dieser ist in der CPSARRDICode-Tabelle hinterlegt. Ist
        /// kein Code ermitteltbar, so wird ein Fehler zurueck gegeben
        /// </summary>
        /// <param name="DA">Data Access</param>
        /// <param name="selPSABrand">WERBAS Fahrzeugmarke<param>
        /// <returns></returns>
        Task<WerbasResult> GetASRRRDICode(DataAccess DA, CFzMarke selPSABrand);

        /// <summary>
        /// Holt die FzMarke, die der PSA-Marke Opel
        /// zugewiesen ist
        /// </summary>
        /// <param name="DA">Data Access</param>
        /// <param name="selPSAUid">Uid</param>
        /// <returns></returns>
        Task<WerbasResult> GetOpelWERBASBrand(DataAccess DA, string selPSAUid);

        /// <summary>
        /// Baut mit den uebergebenen Werten den Dateinamen auf. Prueft ob
        /// die uebergebenen Werte gefuellt sind, und von der Laenge passen.
        /// Im Fehlerfall wird ein leerer String zurueck gegeben.
        /// </summary>
        /// <param name="selRRDICode">RRDI Code Kunde</param>
        /// <param name="selMonth">Monat mit zwei Stellen</param>
        /// <param name="selYear">Jahr mir vier Stellen</param>
        /// <returns></returns>
        string BuildSurveyFileName(string selRRDICode, string selMonth, string selYear);

        /// <summary>
        /// Ermittelt aus der Liste der Auftraege die Fz_ID, und holt damit
        /// die passenden Fahrzeuge zu den Auftraegen aus der Datenbank.
        /// Abgerufen werden die Werte aus dem uebergebenen Propertiesstring
        /// </summary>
        /// <param name="DA">Data Access</param>
        /// <param name="selWorkOrderList">Liste der Auftraege</param>
        /// <param name="selProperties">Gewuenschte Properties</param>
        /// <returns></returns>
        Task<WerbasResult> GetWorkOrderVerhicles(DataAccess DA, CAuftrKopfList selWorkOrderList, string[] selProperties);

        /// <summary>
        /// Ermittelt aus der Liste der Auftraege die Kd_ID, und holt damit
        /// die passenden Kunden zu den Auftraegen aus der Datenbank.
        /// Abgerufen werden die Werte aus dem uebergebenen Propertiesstring
        /// </summary>
        /// <param name="DA">Data Access</param>
        /// <param name="selWorkOrderList">Liste der Auftraege</param>
        /// <param name="selProperties">Gewuenschte Properties</param>
        /// <returns></returns>
        Task<WerbasResult> GetWorkOrderCustomers(DataAccess DA, CAuftrKopfList selWorkOrderList, string[] selProperties);

        /// <summary>
        /// Ermittelt aus der Liste der Auftraege die ID, und holt damit
        /// die passenden Rechnungen zu den Auftraegen aus der Datenbank.
        /// </summary>
        /// <param name="DA">Data Access</param>
        /// <param name="selWorkOrderList">Liste der Auftraege</param>
        /// <returns></returns>
        Task<WerbasResult> GetWorkOrderInvoices(DataAccess DA, CAuftrKopfList selWorkOrderList);

        /// <summary>
        /// Holt die Schemadefinition zu dem Opel Kundenzufriedenheit aus der DB. Liest
        /// dann die hinterlegten Felder aus, und gibt dieses als  List<Tuple<int, string, string>> zurueck
        /// </summary>
        /// <param name="DA">Data Access</param>
        /// <param name="cFVFirmaHSList">Firmenliste</param>
        /// <returns></returns>
        Task<WerbasResult> GetDiscountScheme(DataAccess DA, string selSchemeName);

        /// <summary>
        /// Erstellt aus den gelieferten Werten eine Excel und legt diese
        /// im PSA-Exportverzeichnis ab. Ist die Datei in dem Verzeichnis
        /// schon vorhanden, dann wir mit einem Fehler abgebrochen.
        /// </summary>
        /// <param name="DA">Data Access</param>
        /// <param name="selWorkOrderList">Liste der Auftraege</param>
        /// <param name="selWorkOrderVehicleList">Liste der Auftragsfahrzeuge</param>
        /// <param name="selWorkOrderCustomerList">Liste der Auftragskunden</param>
        /// <param name="selWorkOrderInvoiceList">Liste der Auftragsrechnungen</param>
        /// <param name="selStartDate">Startdatum des Bereichs fuer den Dateinamen</param>
        /// <param name="selFormatDate">Format fuer stringausgabe Datum</param>
        /// <param name="selFormatNumeric">Format fuer stringausgabe Zahlenwerte</param>
        /// <param name="selEndDate">Eigener Firmenname</param>
        /// <returns></returns>
        Task<WerbasResult> CreateExcelWithWorkOrderListData(DataAccess DA, CAuftrKopfList selWorkOrderList,
            CFzList selWorkOrderVehicleList, CKdList selWorkOrderCustomerList, CAuftrRechnungSList selWorkOrderInvoiceList,
            DateTime selStartDate, string selFormatDate, string selFormatNumeric, string selFirmName);

        // <summary>
        /// Erstellt die Opel Kundenzufriedenheitsumfrage, und exportiert diese
        /// als Excel im PSA Exportverzeichnis
        /// </summary>
        /// <param name="DA">Data Access</param>
        /// <param name="selScheduler">Scheduler</param>
        /// <param name="selFirmName">Eigener Firmenname</param>
        /// <returns></returns>
        Task<WerbasResult> ExportOpelCustomerSurvey(DataAccess DA, CSchedule selScheduler, string selFirmName);

        /// <summary>
        /// Ruft die Taskeinstellungen zu dem uebergebenen Scheduler ab
        /// </summary>
        /// <param name="DA">DataAccess</param>
        /// <param name="SelScheduler">Scheduler</param>
        /// <returns></returns>
        Task<WerbasResult> GetTaskSettings(DataAccess DA, CSchedule SelScheduler);

        /// <summary>
        /// Formatierungsmethode fuer die Summe zu String. Setzt zuerst die decimalsumme auf zwei Nachkommastellen,
        /// falls dies nicht der Fall sein sollte. Danach wird der Formatierungsstring ausgewertet.
        /// Leer - es wird als Formatierung F2 ausgewaehlt mit der aktuellen Culture
        /// KOMMA - es wird hart mit de-DE und der Formatierung F2 auf Komma als Dezimaltrenner gesetzt
        /// PUNKT - es wird hart mit de-CH und der Formatierung F2 auf Punkt als Dezimaltrenner gesetzt
        /// Angabe aus Taskteinstellung - wird mit InvariantCulture formatiert
        /// Ueber diesem Wege sollte es moeglich sein, fast alle gewuenschten Formate zu setzen
        /// </summary>
        /// <param name="selInvoiceAmount">Rechnungssumme Auftrag</param>
        /// <param name="selFormatNumeric">Formatangabe fuer String</param>
        /// <returns></returns>
        string FormatAmountOfInvoice(decimal selInvoiceAmount, string selFormatNumeric);

        /// <summary>
        /// Erzeugt aus dem uebergebenen Werten einen Eintrag im Excel, und gibt
        /// das aktualisierte Objekt zurueck.
        /// </summary>
        /// <param name="dataExcel">Excel-Inhalt</param>
        /// <param name="tupleSurveyScheme">Das ausgelesene Schema fuer das Excel</param>
        /// <param name="selWorkOrder">Auftrag</param>
        /// <param name="selWorkorderVehicle">Auftragsfahrzeug</param>
        /// <param name="selWorkorderCustomer">Auftragskunde</param>
        /// <param name="selWorkorderInvoice">Rechnung</param>
        /// <param name="fileRRDICode">RRDI-Code Haendler</param>
        /// <param name="selFirmName">Aktueller Firmenname</param>
        /// <param name="selFormatDate">Format Datum</param>
        /// <param name="selFormatNumeric">Format Summe</param>
        /// <param name="selWorkOrderIndex">Index Auftrag</param>
        /// <returns></returns>
        Task<WerbasResult> CreateExcelEntryWithWorkOrderData(object[,] dataExcel, List<Tuple<int, string, string>> tupleSurveyScheme, CAuftrKopf selWorkOrder, CFz selWorkorderVehicle,
            CKd selWorkorderCustomer, CAuftrRechnungS selWorkorderInvoice, string fileRRDICode, string selFirmName, string selFormatDate, string selFormatNumeric, int selWorkOrderIndex);

        /// <summary>
        /// Ermittelt das Start- und Enddatum fuer die Kundenzufriedeheitsumfrage
        /// - Startdatum 5. des Vormonats
        /// - Enddatum 4. des aktuellen Monats
        /// Rueckgabe Tuple mit Start und Enddatum
        /// </summary>
        /// <param name="selectedBasisDate">Aktuelles Datum</param>
        /// <returns></returns>
        Tuple<DateTime, DateTime> GetDatesForOpelCustomerSurvey(DateTime selectedBasisDate);
    }
}