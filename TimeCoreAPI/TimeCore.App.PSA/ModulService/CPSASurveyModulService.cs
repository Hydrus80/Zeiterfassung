using BO_Basis;
using BO_Remote.PSA.ASR.ModelService.Interface;
using BO_Serial;
using Connect.InfoSysteme;
using DA_Basis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tools;
using Wag.Dev.Common.Models;

namespace BO_Remote.PSA.ASR.ModulService
{
    public class CPSASurveyModulService : ICPSASurveyModulService
    {
        //Felder
        private ICPSAASRModelService m_PSAASRModelService = null;
        private CFVFirmaHSList m_CFVFirmaHSList = null;
        private int m_FirmNr = 0;
        private int m_WorkShopNr = 0;

        /// <summary>
        /// Konstruktor mit Parameter
        /// </summary>
        /// <param name="selCPSAASRModelService">CPSAASRModelService</param>
        /// <param name="selCFVFirmaHSList">Firmenliste</param>
        /// <param name="selFirmNr">Aktuelle Firmennummer</param>
        /// <param name="selWorkShopNr">Aktuelle Filialnummer</param>
        public CPSASurveyModulService(ICPSAASRModelService selCPSAASRModelService, CFVFirmaHSList selCFVFirmaHSList, int selFirmNr, int selWorkShopNr)
        {
            m_PSAASRModelService = selCPSAASRModelService;
            m_CFVFirmaHSList = selCFVFirmaHSList;
            m_FirmNr = selFirmNr;
            m_WorkShopNr = selWorkShopNr;
        }

        /// <inheritdoc/>
        public CSuche GetOpelBrandForWorkOrderSearch()
        {
            //Suche aufbauen
            CSuche opelBrandSearch = new CSuche(typeof(CFzMarkeList));
            opelBrandSearch.SetBedingung("FzMarkePSA.Uid", SuchOption.gleich, "02");
            opelBrandSearch.SelectSingleProperty("ID");
            return opelBrandSearch;
        }

        /// <inheritdoc/>
        public CSuche GetOpelVehicleForWorkOrderSearch(CSuche opelBrandSearch)
        {
            //Opel Fahrzeuge holen
            CSuche opelVehiclesSearch = new CSuche(typeof(CFzList));
            opelVehiclesSearch.SetBedingung("Marke.ID", SuchOption.IN, opelBrandSearch);
            opelVehiclesSearch.SelectSingleProperty("ID");
            return opelVehiclesSearch;
        }

        /// <inheritdoc/>
        public CSuche GetWorkOrderInvoiceForWorkOrderSearch(DateTime selStartDate, DateTime selEndDate)
        {
            //Auftragsrechnung mit Datum holen
            CSuche invoiceSearch = new CSuche(typeof(CAuftrRechnungList));
            invoiceSearch.SelectSingleProperty("AuftrRechnung.AKopf.ID");
            invoiceSearch.SetBedingung("FilialNr", SuchOption.gleich, m_WorkShopNr);
            invoiceSearch.SetBedingung("RechnDatum", SuchOption.BETWEEN, new DateTime[] { selStartDate.Date, selEndDate.Date });
            return invoiceSearch;
        }

        /// <inheritdoc/>
        public CSuche GetExcludedWorkOrderTypForWorkOrderSearch()
        {
            //Interne Auftragsarten holen
            CSuche internalWorkOrdertypSearch = new CSuche(typeof(CAuftrArtList));
            internalWorkOrdertypSearch.SetBedingung("AuftrArt.IsIntern", SuchOption.gleich, true);
            internalWorkOrdertypSearch.SelectSingleProperty("ID");
            return internalWorkOrdertypSearch;
        }

        /// <inheritdoc/>
        public async Task<WerbasResult> GetInvoicedOpelWorkOrdersForSurvey(DataAccess DA, DateTime selStartDate, DateTime selEndDate)
        {
            //Init
            CAuftrKopfList returnValue = null;

            //Datenzugriff
            if (DA == null) DA = new DataAccess();

            try
            {
                //Opel Fahrzeugsuche holen
                CSuche opelVehiclesSearch = GetOpelVehicleForWorkOrderSearch(GetOpelBrandForWorkOrderSearch());

                //Interne Auftragsarten holen
                CSuche internalWorkOrdertypSearch = GetExcludedWorkOrderTypForWorkOrderSearch();

                //Auftragsrechnung mit Datum holen
                CSuche invoiceSearch = GetWorkOrderInvoiceForWorkOrderSearch(selStartDate, selEndDate);

                //Auftragssuche zusammenbauen
                CSuche workorderSearch = new CSuche(typeof(CAuftrKopfList));
                workorderSearch.SetBedingung("AuftrZustand.AZ", SuchOption.gleich, CAuftrZustandList.AZs[(int)enumAZ.RechnungGedruckt]);
                workorderSearch.SetBedingung("FilialNr", SuchOption.gleich, m_WorkShopNr);
                workorderSearch.SetBedingung("AuftrArt.ID", SuchOption.NOTIN, internalWorkOrdertypSearch);
                workorderSearch.SetBedingung("Fz_ID", SuchOption.IN, opelVehiclesSearch);
                workorderSearch.SetBedingung("ID", SuchOption.IN, invoiceSearch);
                workorderSearch.SetSortProperty("ID", 0);

                //Artikelliste holen
                WerbasResult foundWorkOrderListResult = await m_PSAASRModelService.GenericDBSearch<CAuftrKopfList>(workorderSearch, FilialeGetMode.FilialeGenauUndNull, m_CFVFirmaHSList, DA).ConfigureAwait(false);
                if (!foundWorkOrderListResult.IsSuccessAndObjectIsType<CAuftrKopfList>()) return foundWorkOrderListResult;
                returnValue = foundWorkOrderListResult.ResultObject.MapObjectToT<CAuftrKopfList>();

                //Zurück
                return new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = returnValue };
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "9987578", "CPSASurveyModulService", "GetInvoicedOpelWorkOrdersForSurvey");
            }
        }

        /// <inheritdoc/>
        public async Task<WerbasResult> GetWorkOrderVerhicles(DataAccess DA, CAuftrKopfList selWorkOrderList, string[] selProperties)
        {
            //INit
            CFzList vehicleList = new CFzList();
            //new string[] { "ID", "IdentNr", "Typ", "ModellCode" }

            try
            {
                //Übergabe ok?
                if (selWorkOrderList == null) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "4779161", "CPSASurveyModulService", "GetWorkOrderVerhicles");

                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Aufträge durchlaufen
                if (selWorkOrderList.Count > 0)
                {
                    //IDs holen
                    List<int> FoundIDs = (from CAuftrKopf a1 in selWorkOrderList select a1.Fz_ID).ToList();

                    //Fahrzeuge suchen
                    CSuche vehicleSearch = new CSuche(typeof(CFzList));
                    vehicleSearch.SetBedingung("ID", SuchOption.IN, FoundIDs.ToArray());
                    vehicleSearch.SelectProperties(selProperties);

                    //Artikelliste holen
                    WerbasResult foundWorkOrderListResult = await m_PSAASRModelService.GenericDBSearch<CFzList>(vehicleSearch, FilialeGetMode.FilialeEgal, m_CFVFirmaHSList, DA).ConfigureAwait(false);
                    if (!foundWorkOrderListResult.IsSuccessAndObjectIsType<CFzList>()) return foundWorkOrderListResult;
                    vehicleList = foundWorkOrderListResult.ResultObject.MapObjectToT<CFzList>();
                }

                //Zurück
                return new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = vehicleList };
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "4110660", "CPSASurveyModulService", "GetWorkOrderVerhicles");
            }
        }

        /// <inheritdoc/>
        public async Task<WerbasResult> GetWorkOrderCustomers(DataAccess DA, CAuftrKopfList selWorkOrderList, string[] selProperties)
        {
            //INit
            CKdList customerList = new CKdList();
            //new string[] { "ID","Kd.Person.ID", "Kd.Person.Adresse.PLZ", "Kd.Person.Adresse.Strasse", "Kd.Person.Adresse.Ort","Kd.Person.Email" }

            try
            {
                //Übergabe ok?
                if (selWorkOrderList == null) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "9387921", "CPSASurveyModulService", "GetWorkOrderCustomers");

                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Aufträge durchlaufen
                if (selWorkOrderList.Count > 0)
                {
                    //IDs holen
                    List<int> FoundIDs = (from CAuftrKopf a1 in selWorkOrderList select a1.Kd_ID).ToList();

                    //Kunden suchen
                    CSuche vehicleSearch = new CSuche(typeof(CKdList));
                    vehicleSearch.SetBedingung("ID", SuchOption.IN, FoundIDs.ToArray());
                    vehicleSearch.SelectProperties(selProperties);

                    //Kundenliste holen
                    WerbasResult foundWorkOrderListResult = await m_PSAASRModelService.GenericDBSearch<CKdList>(vehicleSearch, FilialeGetMode.FilialeEgal, m_CFVFirmaHSList, DA).ConfigureAwait(false);
                    if (!foundWorkOrderListResult.IsSuccessAndObjectIsType<CKdList>()) return foundWorkOrderListResult;
                    customerList = foundWorkOrderListResult.ResultObject.MapObjectToT<CKdList>();
                }

                //Zurück
                return new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = customerList };
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "4853352", "CPSASurveyModulService", "GetWorkOrderCustomers");
            }
        }

        /// <inheritdoc/>
        public async Task<WerbasResult> GetWorkOrderInvoices(DataAccess DA, CAuftrKopfList selWorkOrderList)
        {
            //INit
            CAuftrRechnungSList invoiceList = new CAuftrRechnungSList();
            //new string[] { "ID, "Gesamt", "GesamtohneMwSt", "AKopf_ID" }

            try
            {
                //Übergabe ok?
                if (selWorkOrderList == null) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "9387921", "CPSASurveyModulService", "GetWorkOrderCustomers");

                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Aufträge durchlaufen
                if (selWorkOrderList.Count > 0)
                {
                    //IDs holen
                    List<int> FoundIDs = (from CAuftrKopf a1 in selWorkOrderList select a1.ID).ToList();

                    //Kunden suchen
                    CSuche invoiceSearch = new CSuche(typeof(CAuftrRechnungSList));
                    invoiceSearch.SetBedingung("AKopfID", SuchOption.IN, FoundIDs.ToArray());

                    //Kundenliste holen
                    WerbasResult foundWorkOrderListResult = await m_PSAASRModelService.GenericDBSearch<CAuftrRechnungSList>(invoiceSearch, FilialeGetMode.FilialeEgal, m_CFVFirmaHSList, DA).ConfigureAwait(false);
                    if (!foundWorkOrderListResult.IsSuccessAndObjectIsType<CAuftrRechnungSList>()) return foundWorkOrderListResult;
                    invoiceList = foundWorkOrderListResult.ResultObject.MapObjectToT<CAuftrRechnungSList>();
                }

                //Zurück
                return new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = invoiceList };
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "4853352", "CPSASurveyModulService", "GetWorkOrderCustomers");
            }
        }

        /// <inheritdoc/>
        public async Task<WerbasResult> GetASRExportPath(DataAccess DA)
        {
            //Init
            string returnExportPath = string.Empty;

            //Datenzugriff
            if (DA == null) DA = new DataAccess();

            try
            {
                //Einstellungen holen
                CSuche searchPath = new CSuche(typeof(CSchmodderList));
                searchPath.SetBedingung("SchmodderKey", SuchOption.gleich, (int)CSchmodder.enumKey.PSA_ASR_Path);
                searchPath.SetBedingung("FilialNr", SuchOption.gleich, m_WorkShopNr);

                //Suche ausführen
                WerbasResult SchmodderSettingsResult = await m_PSAASRModelService.GenericDBSearch<CSchmodderList>(searchPath, m_CFVFirmaHSList, DA).ConfigureAwait(false);
                if ((!SchmodderSettingsResult.Result) || (SchmodderSettingsResult.ResultObject is null)) return SchmodderSettingsResult;
                CSchmodderList GetSettingsList = (CSchmodderList)SchmodderSettingsResult.ResultObject;

                //Voreinstellungen vorhanden?
                if (GetSettingsList is CSchmodderList)
                {
                    //Pfad Export
                    returnExportPath = (from CSchmodder a in GetSettingsList
                                        where a.SchmodderKey == (int)CSchmodder.enumKey.PSA_ASR_Path
                                        select a.Text).FirstOrDefault();
                    if (string.IsNullOrEmpty(returnExportPath))
                        return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(-1, "In den PSA ASR Einstellungen ist kein Exportpfad gesetzt"), WerbasResultLevel.SchwerwiegenderFehler, "6998411", "CPSASurveyModulService", "GetASRExportPath");
                }

                //Zurück
                return new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = returnExportPath };

            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "3846026	", "CPSASurveyModulService", "GetASRExportPath");
            }
        }

        /// <inheritdoc/>
        public async Task<WerbasResult> GetASRRRDICode(DataAccess DA, CFzMarke selPSABrand)
        {
            //Init
            string returnRRDICode = string.Empty;

            //Datenzugriff
            if (selPSABrand == null) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "4135726", "CPSASurveyModulService", "GetASRRRDICode");
            if (DA == null) DA = new DataAccess();

            try
            {
                //Einstellungen holen
                CSuche searchRRDI = new CSuche(typeof(CPSARRDICodesList));
                searchRRDI.SetBedingung("FzMarke_ID", SuchOption.gleich, selPSABrand.ID);
                searchRRDI.SetBedingung("FilialNr", SuchOption.gleich, m_WorkShopNr);

                //Suche ausführen
                WerbasResult SchmodderSettingsResult = await m_PSAASRModelService.GenericDBSearch<CPSARRDICodesList>(searchRRDI, m_CFVFirmaHSList, DA).ConfigureAwait(false);
                if ((!SchmodderSettingsResult.Result) || (SchmodderSettingsResult.ResultObject is null)) return SchmodderSettingsResult;
                CPSARRDICodesList GetSettingsList = (CPSARRDICodesList)SchmodderSettingsResult.ResultObject;

                //Voreinstellungen vorhanden?
                if ((GetSettingsList is CPSARRDICodesList) && (GetSettingsList.Count > 0))
                {
                    //Pfad Export
                    returnRRDICode = GetSettingsList[0].RRDICode;
                    if (string.IsNullOrEmpty(returnRRDICode))
                        return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(-1, "In den PSA ASR Einstellungen ist kein RRDI-Code gesetzt"), WerbasResultLevel.SchwerwiegenderFehler, "8615712", "CPSASurveyModulService", "GetASRRRDICode");
                }
                else
                    return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(-1, "In den PSA ASR Einstellungen ist kein RRDI-Code gesetzt"), WerbasResultLevel.SchwerwiegenderFehler, "7418317", "CPSASurveyModulService", "GetASRRRDICode");

                //Zurück
                return new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = returnRRDICode };

            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "4023425	", "CPSASurveyModulService", "GetASRRRDICode");
            }
        }

        /// <inheritdoc/>
        public async Task<WerbasResult> GetOpelWERBASBrand(DataAccess DA, string selPSAUid)
        {
            //Übergabe
            if (string.IsNullOrEmpty(selPSAUid)) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "4481821", "CPSASurveyModulService", "GetOpelWERBASBrand");
            if (DA == null) DA = new DataAccess();

            try
            {
                //Suche aufbauen
                CSuche opelBrandSearch = new CSuche(typeof(CFzMarkePSAList));
                opelBrandSearch.SetBedingung("Uid", SuchOption.gleich, selPSAUid);
                opelBrandSearch.SelectSingleProperty("ID");

                CSuche werbasBrandSearch = new CSuche(typeof(CFzMarkeList));
                werbasBrandSearch.SetBedingung("FzMarkePSA.ID", SuchOption.IN, opelBrandSearch);

                //Markenliste holen
                return await m_PSAASRModelService.GenericDBSearch<CFzMarkeList>(werbasBrandSearch, m_CFVFirmaHSList, DA).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "9000939	", "CPSASurveyModulService", "GetOpelWERBASBrand");
            }
        }

        /// <inheritdoc/>
        public async Task<WerbasResult> GetDiscountScheme(DataAccess DA, string selSchemeName)
        {
            //INit
            CTHSourceList foundSchemaList = null;
            List<Tuple<int, string, string>> opelSurveyScheme = new List<Tuple<int, string, string>>();

            //Datenzugriff
            if (DA == null) DA = new DataAccess();

            try
            {
                //Suchen
                CSuche schemeSearch = new CSuche(typeof(CTHSourceList));
                schemeSearch.SetBedingung("Import_Art", SuchOption.gleich, CTHSource._InternalCategorie);
                schemeSearch.SetBedingung("Name", SuchOption.gleich, selSchemeName);

                //Artikelliste holen
                WerbasResult foundWorkOrderListResult = await m_PSAASRModelService.GenericDBSearch<CTHSourceList>(schemeSearch, FilialeGetMode.FilialeEgal, m_CFVFirmaHSList, DA).ConfigureAwait(false);
                if (!foundWorkOrderListResult.IsSuccessAndObjectIsType<CTHSourceList>()) return foundWorkOrderListResult;
                foundSchemaList = foundWorkOrderListResult.ResultObject.MapObjectToT<CTHSourceList>();

                //Gefunden?
                if (foundSchemaList?.Count > 0)
                {
                    XmlDocument profileXml = new XmlDocument();
                    profileXml.LoadXml(foundSchemaList[0].Struktur);

                    XmlNode FileNode = profileXml.SelectSingleNode("File");
                    if (FileNode != null)
                    {
                        //Items durchlaufen
                        foreach (XmlNode itemnode in FileNode.SelectNodes("Item"))
                        {
                            //Init
                            string propName = string.Empty;
                            string propDesignation = string.Empty;
                            int propPos = -1;

                            //property_name
                            XmlNode tempnode = itemnode;
                            XmlNode xattr = tempnode.Attributes.GetNamedItem("property_name");
                            if (xattr == null || xattr.Value.Length == 0)
                                return new WerbasResult() { ErrorCode = "5301218", Result = false, ErrorMessage = CMeldung.sRes(-1, "Kein gültiges Schema für 'Opel Kundenzufriedenheit'"), Level = WerbasResultLevel.SchwerwiegenderFehler, ResultObject = null };
                            propName = xattr.Value.Trim();

                            //designation
                            xattr = tempnode.Attributes.GetNamedItem("designation");
                            if (xattr == null || xattr.Value.Length == 0)
                                return new WerbasResult() { ErrorCode = "5301218", Result = false, ErrorMessage = CMeldung.sRes(-1, "Kein gültiges Schema für 'Opel Kundenzufriedenheit'"), Level = WerbasResultLevel.SchwerwiegenderFehler, ResultObject = null };
                            propDesignation = xattr.Value.Trim();

                            //ID holen
                            xattr = tempnode.Attributes.GetNamedItem("pos");
                            if ((xattr != null) && (xattr.Value.Length == 0))
                                return new WerbasResult() { ErrorCode = "5004492", Result = false, ErrorMessage = CMeldung.sRes(-1, "Kein gültiges Schema für 'Opel Kundenzufriedenheit'"), Level = WerbasResultLevel.SchwerwiegenderFehler, ResultObject = null };
                            if (int.TryParse(xattr.Value.Trim(), out propPos))
                            {
                                opelSurveyScheme.Add(new Tuple<int, string, string>(propPos, propName, propDesignation));
                            }
                        }
                    }
                }
                else
                {
                    //Schema wurde nicht installiert
                    return new WerbasResult() { ErrorCode = "1405547", Result = false, ErrorMessage = CMeldung.sRes(-1, "Kein gültiges Schema für 'Opel Kundenzufriedenheit'"), Level = WerbasResultLevel.SchwerwiegenderFehler, ResultObject = null };
                }

                //Zurück
                return new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = opelSurveyScheme };
            }
            catch (Exception ex)
            {
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "4605936	", "CPSASurveyModulService", "GetDiscountScheme");
            }
        }

        /// <inheritdoc/>
        public string BuildSurveyFileName(string selRRDICode, string selMonth, string selYear)
        {
            //Init
            string returnFileName = "KD_";

            //Übergabe ok?
            if (string.IsNullOrEmpty(selRRDICode)) return string.Empty;
            if (string.IsNullOrEmpty(selMonth)) return string.Empty;
            if (selMonth.Length != 2) selMonth = selMonth.PadLeft(2, '0');
            if (string.IsNullOrEmpty(selYear)) return string.Empty;
            if (selYear.Length != 4) return string.Empty;

            return returnFileName + selRRDICode + "_" + selMonth + selYear + ".xls";
        }

        /// <inheritdoc/>
        public async Task<WerbasResult> GetTaskSettings(DataAccess DA, CSchedule SelScheduler)
        {
            //Init
            CVoreinstellungScheduleList SelTaskSettingsList = null;

            try
            {
                //Übergabe ok?
                if (SelScheduler == null) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "8502880", "CPSASurveyModulService", "GetTaskSettings");
                if (DA == null) DA = new DataAccess();

                //Voreinstellungen holen
                CSuche sucheVE = new CSuche(typeof(CVoreinstellungScheduleList));
                sucheVE.SetBedingung("ScheduleID", SuchOption.gleich, SelScheduler.ID);
                WerbasResult TaskSettingResult = await m_PSAASRModelService.GenericDBSearch<CVoreinstellungScheduleList>(sucheVE, FilialeGetMode.FilialeGenauUndNull, m_CFVFirmaHSList, DA).ConfigureAwait(false);
                if ((!TaskSettingResult.Result) || (TaskSettingResult.ResultObject == null)) return TaskSettingResult;
                SelTaskSettingsList = (CVoreinstellungScheduleList)TaskSettingResult.ResultObject;

                //zurück
                return new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = SelTaskSettingsList };
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "4443670", "CPSASurveyModulService", "GetTaskSettings");
            }
        }

        /// <summary>
        /// Ermittelt das Start- und Enddatum fuer die Kundenzufriedeheitsumfrage
        /// - Startdatum 5. des Vormonats
        /// - Enddatum 4. des aktuellen Monats
        /// </summary>
        /// <param name="selectedBasisDate"></param>
        /// <returns></returns>
        public Tuple<DateTime, DateTime> GetDatesForOpelCustomerSurvey(DateTime selectedBasisDate)
        {
            //Init
            DateTime selStartDate = DateTime.MinValue;
            DateTime selEndDate = DateTime.MinValue;

            if (selectedBasisDate.Month == 1)
            {
                //Startdatum 5. des Vormonats
                selStartDate = new DateTime(selectedBasisDate.AddYears(-1).Year, selectedBasisDate.AddMonths(-1).Month, 5, 0, 0, 0);
            }
            else
            {
                //Startdatum 5. des Vormonats
                selStartDate = new DateTime(selectedBasisDate.Year, selectedBasisDate.AddMonths(-1).Month, 5, 0, 0, 0);
            }

            //Enddatum 4. des aktuellen Monats
            selEndDate = new DateTime(selectedBasisDate.Year, selectedBasisDate.Month, 4, 23, 59, 59);

            //Zurück
            return new Tuple<DateTime, DateTime>(selStartDate, selEndDate);
        }

        /// <inheritdoc/>
        public async Task<WerbasResult> ExportOpelCustomerSurvey(DataAccess DA, CSchedule selScheduler, string selFirmName)
        {
            //Init
            CAuftrKopfList workorderList = null;
            CFzList workorderVehicleList = null;
            CKdList selWorkOrderCustomerList = null;
            CAuftrRechnungSList selWorkOrderInvoiceList = null;
            CVoreinstellungScheduleList selTaskSettingsList = null;
            string formatDate = "dd.MM.yyyy";
            string formatNumeric = "0,##";
            DateTime selStartDate = DateTime.MinValue;
            DateTime selEndDate = DateTime.MinValue;


            try
            {
                //Task Einstellungen holen
                WerbasResult TaskSettingResult = await GetTaskSettings(DA, selScheduler).ConfigureAwait(false);
                if ((!TaskSettingResult.Result) || (TaskSettingResult.ResultObject == null)) return TaskSettingResult;
                selTaskSettingsList = (CVoreinstellungScheduleList)TaskSettingResult.ResultObject;
                if ((selTaskSettingsList is CVoreinstellungScheduleList) && (selTaskSettingsList.Count > 0))
                {
                    //Jahresformatierung holen
                    CVoreinstellungSchedule v_FormatYear = selTaskSettingsList.FindeVoreinstellung(selScheduler, (int)CVoreinstellungSchedule.EnumKennung.PSA_Kundenzufriedenheit_FormatDate);
                    if ((v_FormatYear != null) && (!string.IsNullOrEmpty(v_FormatYear.Wert)))
                        formatDate = v_FormatYear.Wert;

                    //Zahlenformatierung holen
                    CVoreinstellungSchedule v_FormatNumeric = selTaskSettingsList.FindeVoreinstellung(selScheduler, (int)CVoreinstellungSchedule.EnumKennung.PSA_Kundenzufriedenheit_FormatNumeric);
                    if ((v_FormatNumeric != null) && (!string.IsNullOrEmpty(v_FormatNumeric.Wert)))
                        formatNumeric = v_FormatNumeric.Wert;
                }

                //Zeitbereich holen
                Tuple<DateTime, DateTime> selectedRange = GetDatesForOpelCustomerSurvey(DateTime.Now);
                if (selectedRange is Tuple<DateTime, DateTime>)
                {
                    selStartDate = selectedRange.Item1;
                    selEndDate = selectedRange.Item2;
                }
                else
                    return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(-1,"Für die Auswertung konnte kein gültiger Zeitbereich ermittelt werden"), WerbasResultLevel.SchwerwiegenderFehler, "4898846", "CPSASurveyModulService", "ExportOpelCustomerSurvey");

                //Auftragsliste holen
                WerbasResult workorderListResult = await GetInvoicedOpelWorkOrdersForSurvey(DA, selStartDate, selEndDate).ConfigureAwait(false);
                if (!workorderListResult.IsSuccessAndObjectIsType<CAuftrKopfList>()) return workorderListResult;
                workorderList = workorderListResult.ResultObject.MapObjectToT<CAuftrKopfList>();

                //Kundenfahrzeuge holen
                WerbasResult workorderVehicleResult = await GetWorkOrderVerhicles(DA, workorderList, new string[] { "ID", "IdentNr", "Typ", "ModellCode" }).ConfigureAwait(false);
                if (!workorderVehicleResult.IsSuccessAndObjectIsType<CFzList>()) return workorderVehicleResult;
                workorderVehicleList = workorderVehicleResult.ResultObject.MapObjectToT<CFzList>();

                //Kunden holen
                WerbasResult workorderCustomerResult = await GetWorkOrderCustomers(DA, workorderList, new string[] { "ID", "Kd.Person.ID", "Kd..Person.Name", "Kd.Person.Vorname", "Kd.Person.Mobil", "Kd.Person.Telefon1", "Kd.Person.Adresse.PLZ", "Kd.Person.Adresse.Strasse", "Kd.Person.Adresse.Ort", "Kd.Person.Email" }).ConfigureAwait(false);
                if (!workorderCustomerResult.IsSuccessAndObjectIsType<CKdList>()) return workorderCustomerResult;
                selWorkOrderCustomerList = workorderCustomerResult.ResultObject.MapObjectToT<CKdList>();

                //Rechnungen holen
                WerbasResult workorderInvoicesResult = await GetWorkOrderInvoices(DA, workorderList).ConfigureAwait(false);
                if (!workorderInvoicesResult.IsSuccessAndObjectIsType<CAuftrRechnungSList>()) return workorderInvoicesResult;
                selWorkOrderInvoiceList = workorderInvoicesResult.ResultObject.MapObjectToT<CAuftrRechnungSList>();

                //Datei bauen und exportieren
                WerbasResult exportResult = await CreateExcelWithWorkOrderListData(DA, workorderList, workorderVehicleList, selWorkOrderCustomerList, selWorkOrderInvoiceList,
                    selStartDate, formatDate, formatNumeric, selFirmName).ConfigureAwait(false);
                if (!exportResult.IsSuccess()) return exportResult;

                //Zurück
                return new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = new List<object>(new object[] { workorderList.Count.ToString(), selStartDate, selEndDate }) };

         
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "7863723", "CPSASurveyModulService", "ExportOpelCustomerSurvey");
            }
        }

        /// <inheritdoc/>
        public string FormatAmountOfInvoice(decimal selInvoiceAmount, string selFormatNumeric)
        {
            //Init
            string returnValue = string.Empty;
            decimal twoDigitsAwayFromZero = decimal.Round(selInvoiceAmount, 2, MidpointRounding.AwayFromZero);

            if (string.IsNullOrEmpty(selFormatNumeric))
            {
                //Fallback mit zwei Nachkommastellen in aktuellen Landeskultur
                returnValue = twoDigitsAwayFromZero.ToString("F2", CultureInfo.CurrentCulture);
            }
            else
            {
                //Hart auf Komma setzen als Fallback-Option
                if (selFormatNumeric.ToUpper() == "KOMMA")
                {
                    returnValue = twoDigitsAwayFromZero.ToString("F2", CultureInfo.CreateSpecificCulture("de-DE"));
                }
                //Hart auf Komma setzen als Fallback-Option
                else if (selFormatNumeric.ToUpper() == "PUNKT")
                {
                    returnValue = twoDigitsAwayFromZero.ToString("F2", CultureInfo.CreateSpecificCulture("de-CH"));
                }
                //Formatangabe aus Taskeinstellungen beachten mit generischer Kultur
                else
                {
                    returnValue = twoDigitsAwayFromZero.ToString(selFormatNumeric, CultureInfo.InvariantCulture);
                }
            }

            return returnValue;
        }

        /// <inheritdoc/>
        public async Task<WerbasResult> CreateExcelWithWorkOrderListData(DataAccess DA, CAuftrKopfList selWorkOrderList,
            CFzList selWorkOrderVehicleList, CKdList selWorkOrderCustomerList, CAuftrRechnungSList selWorkOrderInvoiceList,
            DateTime selStartDate, string selFormatDate, string selFormatNumeric, string selFirmName)
        {
            //Init
            string fileExportPath = string.Empty;
            string fileRRDICode = string.Empty;
            string fileExportFileName = string.Empty;
            CFzMarke selOpelBrand = null;
            object[,] data = null;
            List<Tuple<int, string, string>> tupleSurveyScheme = new List<Tuple<int, string, string>>();

            try
            {
                //Übergabe ok?
                if (selWorkOrderList == null) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "3620337", "CPSASurveyModulService", "CreateExcelWithWorkOrderListData");
                if (selWorkOrderVehicleList == null) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "1904892", "CPSASurveyModulService", "CreateExcelWithWorkOrderListData");
                if (selWorkOrderCustomerList == null) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "5529366", "CPSASurveyModulService", "CreateExcelWithWorkOrderListData");
                if (selWorkOrderInvoiceList == null) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "6439320", "CPSASurveyModulService", "CreateExcelWithWorkOrderListData");

                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Hole den Exportpfad
                WerbasResult GetExportPathResult = await GetASRExportPath(DA);
                if (!GetExportPathResult.IsSuccessAndObjectIsType<string>()) return GetExportPathResult;
                fileExportPath = GetExportPathResult.ResultObject.MapObjectToT<string>();

                //Holt die Opelmarke
                WerbasResult getOpelBrand = await GetOpelWERBASBrand(DA, "02");
                if (!getOpelBrand.IsSuccessAndObjectIsType<CFzMarkeList>()) return getOpelBrand;
                CFzMarkeList foundBrandList = getOpelBrand.ResultObject.MapObjectToT<CFzMarkeList>();
                if (foundBrandList.Count > 0)
                    selOpelBrand = foundBrandList[0];
                else
                    return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(-1, "Es gibt keinen Hersteller für die PSA-Marke 'Opel'."), WerbasResultLevel.SchwerwiegenderFehler, "2057517", "CPSASurveyModulService", "CreateExcelWithWorkOrderListData");

                //Hole den RRDI-Code
                WerbasResult GetERRDIResult = await GetASRRRDICode(DA, selOpelBrand);
                if (!GetERRDIResult.IsSuccessAndObjectIsType<string>()) return GetERRDIResult;
                fileRRDICode = GetERRDIResult.ResultObject.MapObjectToT<string>();

                //Dateiname bauen
                fileExportFileName = BuildSurveyFileName(fileRRDICode, selStartDate.Month.ToString("00"), selStartDate.Year.ToString("0000"));
                if (string.IsNullOrEmpty(fileExportFileName))
                    return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(-1, "Es konnte kein Dateiname für den Export der Opel Kundenzufriedenheitsumfrage erzeugt werden. Bitte prüfen Sie ob der RRDI-Code im Firmenstamm ausgefüllt ist"), WerbasResultLevel.SchwerwiegenderFehler, "8236817", "CPSASurveyModulService", "CreateExcelWithWorkOrderListData");
                if (!fileExportPath.EndsWith("\\")) fileExportPath += "\\";

                //hat es die Datei schon?
                if (File.Exists(fileExportPath + fileExportFileName))
                    return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(-1, "Die Exportdatei '#' existiert schon im Exportverzeichnis", fileExportPath + fileExportFileName), WerbasResultLevel.SchwerwiegenderFehler, "8236817", "CPSASurveyModulService", "CreateExcelWithWorkOrderListData");

                //Excel öffnen
                ExcelWrapper excel = new ExcelWrapper(fileExportPath + fileExportFileName, CMeldung.sRes(-1, "OPEL"));
                excel.SetActiveSheet(0);

                //Schema Opel Kundenzufriedenheit aus THSource auslesen
                WerbasResult schemaResult = await GetDiscountScheme(DA, "Opel Kundenzufriedenheit").ConfigureAwait(false);
                if (!schemaResult.IsSuccessAndObjectIsType<List<Tuple<int, string, string>>>()) return schemaResult;
                tupleSurveyScheme = schemaResult.ResultObject.MapObjectToT<List<Tuple<int, string, string>>>();
                if (tupleSurveyScheme.Count == 0)
                    return new WerbasResult() { ErrorCode = "2312274", Result = false, ErrorMessage = CMeldung.sRes(-1, "Kein gültiges Schema für 'Opel Kundenzufriedenheit'"), Level = WerbasResultLevel.SchwerwiegenderFehler, ResultObject = null };

                //Spalten/Reihenanzahl für Excel setzen
                data = new object[selWorkOrderList.Count + 1, tupleSurveyScheme.Count];

                //Überschriften aus designation setzen
                for (int j = 0; j < tupleSurveyScheme.Count; j++)
                    data[0, j] = tupleSurveyScheme[j].Item3;

                //Daten setzen
                for (int x = 1; x <= selWorkOrderList.Count; x++)
                {
                    //Eintrag holen
                    CAuftrKopf runnerWorkOrderEntry = selWorkOrderList[x - 1];

                    //Auftragsfahrzeug holen
                    CFz workorderVehicle = (from CFz a1 in selWorkOrderVehicleList where a1.ID == runnerWorkOrderEntry.Fz_ID select a1).FirstOrDefault() ?? new CFz();

                    //Auftragskunde holen
                    CKd workorderCustomer = (from CKd a1 in selWorkOrderCustomerList where a1.ID == runnerWorkOrderEntry.Kd_ID select a1).FirstOrDefault() ?? new CKd();

                    //Auftragsrechnung holen
                    CAuftrRechnungS workorderInvoice = (from CAuftrRechnungS a1 in selWorkOrderInvoiceList where a1.AKopfID == runnerWorkOrderEntry.ID select a1).FirstOrDefault() ?? new CAuftrRechnungS();

                    //Zeile schreiben und Exceldaten aktualisieren
                    WerbasResult updateEntry = await CreateExcelEntryWithWorkOrderData(data, tupleSurveyScheme, runnerWorkOrderEntry, workorderVehicle,
                        workorderCustomer, workorderInvoice, fileRRDICode, selFirmName, selFormatDate, selFormatNumeric, x).ConfigureAwait(false);
                    if (updateEntry.IsSuccessAndObjectIsType<object[,]>())
                        data = updateEntry.ResultObject.MapObjectToT<object[,]>();
                    else
                        return updateEntry;
                }

                //Datei schreiben
                excel.WriteRangeValues("A1", data);
                excel.AutoSizeColumns();
                excel.SaveFile();

                //Zurück mit Anzahl von Aufträgen
                return new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = selWorkOrderList.Count.ToString() };
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "9014948", "CPSASurveyModulService", "CreateExcelWithWorkOrderListData");
            }
        }

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
        public Task<WerbasResult> CreateExcelEntryWithWorkOrderData(object[,] dataExcel, List<Tuple<int, string, string>> tupleSurveyScheme, CAuftrKopf selWorkOrder, CFz selWorkorderVehicle,
            CKd selWorkorderCustomer, CAuftrRechnungS selWorkorderInvoice, string fileRRDICode, string selFirmName, string selFormatDate, string selFormatNumeric, int selWorkOrderIndex)
        {
            //Übergabe ok?
            if (dataExcel == null) return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "3545612", "CPSASurveyModulService", "CreateExcelEntryWithWorkOrderData"));
            if (tupleSurveyScheme == null) return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "5409118", "CPSASurveyModulService", "CreateExcelEntryWithWorkOrderData"));
            if (selWorkOrder == null) return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "6760597", "CPSASurveyModulService", "CreateExcelEntryWithWorkOrderData"));
            if (selWorkorderVehicle == null) return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "6920942", "CPSASurveyModulService", "CreateExcelEntryWithWorkOrderData"));
            if (selWorkorderCustomer == null) return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "8361007", "CPSASurveyModulService", "CreateExcelEntryWithWorkOrderData"));
            if (selWorkorderInvoice == null) return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "6783942", "CPSASurveyModulService", "CreateExcelEntryWithWorkOrderData"));
            if (string.IsNullOrEmpty(fileRRDICode)) return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "3287630", "CPSASurveyModulService", "CreateExcelEntryWithWorkOrderData"));
            if (string.IsNullOrEmpty(selFormatDate)) return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "3817198", "CPSASurveyModulService", "CreateExcelEntryWithWorkOrderData"));

            try
            {
                //Spalten durchlaufen
                for (int j = 0; j < tupleSurveyScheme.Count; j++)
                {
                    //Spalte korrekt beschriftet?
                    if (string.IsNullOrEmpty(tupleSurveyScheme[j].Item2))
                        continue;

                    //Wert zuweisen
                    switch (tupleSurveyScheme[j].Item2)
                    {
                        case "DEALERCODE":
                            dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = fileRRDICode;
                            break;
                        case "POSNAME": //Eigene Firmenname
                            dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selFirmName;
                            break;
                        case "CLIENTNAME":
                            if (!string.IsNullOrEmpty(selWorkorderCustomer.Person?.Name))
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkorderCustomer.Person.Name;
                            else
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = string.Empty;
                            break;
                        case "CLIENTSURNAME":
                            if (!string.IsNullOrEmpty(selWorkorderCustomer.Person?.Vorname))
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkorderCustomer.Person.Vorname;
                            else
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = string.Empty;
                            break;
                        case "CLIENTPRIVATEMOBILE":
                            if (!string.IsNullOrEmpty(selWorkorderCustomer.Person?.Mobil))
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkorderCustomer.Person.Mobil;
                            else
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = string.Empty;
                            break;
                        case "CLIENTPRIVATEPHONE":
                            if (!string.IsNullOrEmpty(selWorkorderCustomer.Person?.Telefon1))
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkorderCustomer.Person.Telefon1;
                            else
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = string.Empty;
                            break;
                        case "CLIENTFIXPHONE":
                            if (!string.IsNullOrEmpty(selWorkorderCustomer.Person?.Telefon2))
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkorderCustomer.Person.Telefon2;
                            else
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = string.Empty;
                            break;
                        case "CLIENTADRESS":
                            if (!string.IsNullOrEmpty(selWorkorderCustomer.Person?.Adresse?.Strasse))
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkorderCustomer.Person.Adresse.Strasse;
                            else
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = string.Empty;
                            break;
                        case "CLIENTCITY":
                            if (!string.IsNullOrEmpty(selWorkorderCustomer.Person?.Adresse?.Ort))
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkorderCustomer.Person.Adresse.Ort;
                            else
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = string.Empty;
                            break;
                        case "CLIENTPOSTCODE":
                            if (!string.IsNullOrEmpty(selWorkorderCustomer.Person?.Adresse?.PLZ))
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkorderCustomer.Person.Adresse.PLZ;
                            else
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = string.Empty;
                            break;
                        case "WORKORDERNUMBER":
                            dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkOrder.AuftragsNr.ToString();
                            break;
                        case "INVOICEDATE":
                            if (selWorkorderInvoice.RechnDatum > DateTime.MinValue)
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkorderInvoice.RechnDatum.ToString(selFormatDate, System.Globalization.CultureInfo.InvariantCulture);
                            else
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = string.Empty;
                            break;
                        case "VEHICLEPICKUPDATE":
                            dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkOrder.BringenAm.ToString(selFormatDate, System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "VEHICLEMODEL":
                            if (!string.IsNullOrEmpty(selWorkorderVehicle.Typ))
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkorderVehicle.Typ;
                            else
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = string.Empty;
                            break;
                        case "CLIENTMAIL":
                            if (!string.IsNullOrEmpty(selWorkorderCustomer.Person?.Email))
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkorderCustomer.Person.Email;
                            else
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = string.Empty;
                            break;
                        case "VIN":
                            if (!string.IsNullOrEmpty(selWorkorderVehicle.IdentNr))
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = selWorkorderVehicle.IdentNr;
                            else
                                dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = string.Empty;
                            break;
                        case "AMOUNTOFINVOICE":
                            dataExcel[selWorkOrderIndex, tupleSurveyScheme[j].Item1] = FormatAmountOfInvoice(selWorkorderInvoice.Gesamt, selFormatNumeric);
                            break;
                    }
                }

                //Zurück mit Anzahl von Aufträgen
                return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = dataExcel });
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "2978118", "CPSASurveyModulService", "CreateExcelEntryWithWorkOrderData"));
            }
        }
    }
}
