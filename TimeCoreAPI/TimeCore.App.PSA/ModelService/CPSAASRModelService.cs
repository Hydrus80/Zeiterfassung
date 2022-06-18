using BO_Basis;
using BO_Remote.PSA.ASR.ModelService.Interface;
using BO_Serial;
using Connect.InfoSysteme;
using DA_Basis;
using System;
using System.Collections;
using System.Threading.Tasks;
using Tools;
using Wag.Dev.Common.Models;
using Wag.Dev.Connector.Common;
using Wag.Dev.Workorder.Common.Interfaces;

namespace BO_Remote.PSA.ASR.ModelService
{
    /// <summary>
    /// Schnittstellenklasse fuer PSA ASR
    /// Die Klasse ist einzig dazu da, Datenbankanfragen zur Verfügung zur stellen.
    /// 
    /// Originalklasse aus DMS. Erste Eindruecke
    /// - Probblem mit CSuche, da diese domainuebergreifend ist. Sprich über EFC muss ich direkt über die Modelle
    /// die Operationen vornehmen. In CSuche kann ich quasi alles reinknallen, was ich hier kaum abbilden kann
    /// - Die Modell aus dem DMS fehlen komplett, und die haben kein Interface. Sprich ich kann hier keinen Wrapper erstellen
    /// - Der Datenbankzugriff ist uber DataAccess hier nicht abbildbar, da das DMS weitaus komplexer ist. Dazu gehoert auch die
    /// Firmenliste, die aktuell nicht exisitiert
    /// - Kein Zugriff auf die DMS-Datebank, und diese ist auch nicht über EFC abgebildet. Man koennte jetzt versuchen den betroffenen Teil
    /// der Datenbank nachzubilden, und dann ueber EFC den Zugriff aufzubauen. Oder man macht es sich einfach, und laesst sich durch das CSuche
    /// immer einen SQL-Befehl uebergeben, den man ausfuehrt. Das wird aber beim zurueckmappen der Werte ein echtes Problem. 
    /// Man koennte auch das XML der CSuche aufdroeseln, und versuchen das ueber EFC dann abzubilden. Aber ich will weg von dem Domainuebergreifenden
    /// Zugriffen. 
    /// - Daher bleibt eigentlich nur als Schritt diesen generellen ModelService quasi in die Tonne zu treten, und in dem drueberliegenden ModulService
    /// wirklich zu extraieren, welche Tabellen/Modelle ich eigentlich wirklich nutze. Die Business-Schicht ist einigermaßen gekapselt, aber es gibt halt noch
    /// zu viele Abhaengigkeiten zum DMS-Framework, um das ohne Aufwand zu portieren. 
    /// - Das DMS hat keine Domaintrennung, daher sind auch in den PSA-Modulen Zugriffe sehr verteilt über verschiedene Bereiche. Die Module sind nur nach
    /// groben Bereichen getrennt. Sprich wenn ich z.B ein Preismodul uebernehmen wollte, dann greift dieses in Artikel, Kunden, Voreinstellungen, Warengruppen etc.
    /// rein. Sprich um den Funktionsumfang dieses relativ kleinen Moduls hier nachbilden zu koennen, muesste ich diese Domains hier aufbauen. 
    /// - ich muesste die Original Module wahrscheinlich erstmal im DMS weiter auf Domainnutzung runterbrechen, und diese koennte ich dann Stueck- fuer Stueck hier 
    /// einbinden, und die Datenbankzugriffe schaffen.
    /// - Die Modelle moechte ich hier nich 1:1 reinkopieren oder als Library einfuegen, das macht meines achtens nach keinen großen Sinn. Da auch hier Domainuebergreifend
    /// implementiert wurde, und diese Modell teilweise auch noch Funktionen drin haben. Entweder man muesste Interfaces schaffen, mit denen ich auch hier ueber ein ViewModel
    /// arbeiten koennte, oder ich arbeite z.B mit JSON. Sprich die Modelle werden serializiert bzw. deserialisiert. Dann koennte ich hier die Felder fuellen, die ich benoetige.
    /// Aber ob das so funktioniert oder pragmatisch ist, keine Ahnung
    /// - Eine schnelle Suche fuer komplexe Suchen in EFC hat einmal das hier ergeben:
    /// https://stackoverflow.com/questions/60583511/how-make-query-for-getting-some-objects-from-many-to-many-model-using-ef-core
    /// Ich muesste mich tiefergehend mit EFC auseinandersetzen, wobei ich mich wie gesagt gegen diese Logik der CSuche wehre. Sie ermoeglicht ein Programmieren ueber
    /// fast alle Bereiche des DMS, und das ist eines der Hauptprobleme, die wir dort haben.  
    /// 
    /// </summary>
    public class CPSAASRModelService : ICPSAASRModelService
    {
        //Init
        private CAuftrRTFHelper m_rtfHelper = null;

        /// <summary>
        /// Generische Suche
        /// </summary>
        /// <typeparam name="T">CBAsisListe</typeparam>
        /// <param name="SelParameter">Suchparameter</param>
        /// <param name="cFVFirmaHSList">Firmenliste</param>
        /// <param name="DA">DataAccess</param>
        /// <returns></returns>
        public Task<WerbasResult> GenericDBSearch<T>(CSuche SelParameter, CFVFirmaHSList cFVFirmaHSList, DataAccess DA) where T : new()
        {
            //Init
            dynamic ReturnValue = null;

            try
            {
                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Übergabe prüfen
                if (SelParameter == null)
                {
                    return Task.FromResult(CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200907, "Keine gültige Suche vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "6095152", "CPSAASRModelService", "GenericDBSearch"));
                }
                if ((cFVFirmaHSList == null) || (cFVFirmaHSList.Count == 0))
                {
                    return Task.FromResult(CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200908, "Kein gültiger Context vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "3655222", "CPSAASRModelService", "GenericDBSearch"));
                }

                //Fahrzeug holen
                ReturnValue = CServerRemoteFactory.remFact.GetList_MF(DA, typeof(T), SelParameter, cFVFirmaHSList);

                //Ergbnis vorhanden?
                if (ReturnValue is T)
                {
                    //zurück
                    return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = ReturnValue });
                }
                else
                {
                    //Fehler
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200635, "Suche hat kein Ergebnis zurückgegeben, bitte prüfen Sie den Trace"), WerbasResultLevel.SchwerwiegenderFehler, "9256908", "CPSAASRModelService", "GenericDBSearch"));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult($"{ex.Message}", WerbasResultLevel.SchwerwiegenderFehler, "2741266", "CPSAASRModelService", "GenericDBSearch"));
            }
        }

        /// <summary>
        /// Generische Suche
        /// </summary>
        /// <typeparam name="T">CBAsisListe</typeparam>
        /// <param name="SelParameter">Suchparameter</param>
        /// <param name="cFVFirmaHSList">Firmenliste</param>
        /// <param name="DA">IDataAccess</param>
        /// <param name="selSearchMode">FilialeGetMode</param>
        /// <returns></returns>
        public Task<WerbasResult> GenericDBSearch<T>(CSuche SelParameter, CFVFirmaHSList cFVFirmaHSList, IDataAccess DA, FilialeGetMode selSearchMode = FilialeGetMode.FilialeEgal) where T : new()
        {
            //Init
            dynamic ReturnValue = null;

            try
            {
                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Übergabe prüfen
                if (SelParameter == null)
                {
                    return Task.FromResult(CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200907, "Keine gültige Suche vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "6095152", "CPSAASRModelService", "GenericDBSearch"));
                }
                if ((cFVFirmaHSList == null) || (cFVFirmaHSList.Count == 0))
                {
                    return Task.FromResult(CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200908, "Kein gültiger Context vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "3655222", "CPSAASRModelService", "GenericDBSearch"));
                }

                //Fahrzeug holen
                ReturnValue = CServerRemoteFactory.remFact.GetList_MF(DA, typeof(T), SelParameter, selSearchMode, cFVFirmaHSList);

                //Ergbnis vorhanden?
                if (ReturnValue is T)
                {
                    //zurück
                    return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = ReturnValue });
                }
                else
                {
                    //Fehler
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200635, "Suche hat kein Ergebnis zurückgegeben, bitte prüfen Sie den Trace"), WerbasResultLevel.SchwerwiegenderFehler, "8115050", "CPSAASRModelService", "GenericDBSearch"));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult($"{ex.Message}", WerbasResultLevel.SchwerwiegenderFehler, "4888375", "CPSAASRModelService", "GenericDBSearch"));
            }
        }

        /// <summary>
        /// Generische Suche mit FilialeGetMode
        /// </summary>
        /// <typeparam name="T">CBAsisListe</typeparam>
        /// <param name="selParameter">Suchparameter</param>
        /// <param name="selSearchMode">FilialeGetMode</param>
        /// <param name="cFVFirmaHSList">Firmenliste</param>
        /// <param name="DA">DataAccess</param>
        /// <returns></returns>
        public Task<WerbasResult> GenericDBSearch<T>(CSuche SelParameter, FilialeGetMode selSearchMode, CFVFirmaHSList cFVFirmaHSList, DataAccess DA) where T : new()
        {
            //Init
            dynamic ReturnValue = null;

            try
            {
                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Übergabe prüfen
                if (SelParameter == null)
                {
                    return Task.FromResult(CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200907, "Keine gültige Suche vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "5729379", "CPSAASRModelService", "GenericDBSearch"));
                }
                if ((cFVFirmaHSList == null) || (cFVFirmaHSList.Count == 0))
                {
                    return Task.FromResult(CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200908, "Kein gültiger Context vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "6684084", "CPSAASRModelService", "GenericDBSearch"));
                }

                //Fahrzeug holen
                ReturnValue = CServerRemoteFactory.remFact.GetList_MF(DA, typeof(T), SelParameter, selSearchMode, cFVFirmaHSList);

                //Ergbnis vorhanden?
                if (ReturnValue is T)
                {
                    //zurück
                    return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, Level = WerbasResultLevel.Aktionsmeldung, ResultObject = ReturnValue });
                }
                else
                {
                    //Fehler
                    string returnErrorMsg = CMeldung.sRes(200635, "Suche hat kein Ergebnis zurückgegeben, bitte prüfen Sie den Trace");
                    if (!string.IsNullOrEmpty(CServerRemoteFactory.remFact.Errmessage))
                        returnErrorMsg = CServerRemoteFactory.remFact.Errmessage;
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(returnErrorMsg, WerbasResultLevel.SchwerwiegenderFehler, "1894329", "CPSAASRModelService", "GenericDBSearch"));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult($"{ex.Message}", WerbasResultLevel.SchwerwiegenderFehler, "2741266", "CPSAASRModelService", "SearchCAuftrZustandList"));
            }
        }

        /// <summary>
        /// Holt den Artikel-Vergleichswert aus den Schnittstelleneinstellungen
        /// </summary>
        /// <param name="SelInfoSystem">Infosystemeinstellungen</param>
        /// <param name="cFVFirmaHSList">Firmenliste</param>
        /// <param name="DA">DataAccess</param>
        /// <returns></returns>
        public Task<WerbasResult> GetPartComparer(enumInfosys SelInfoSystem, CFVFirmaHSList cFVFirmaHSList, DataAccess DA)
        {
            //Init
            CInfosystemEinstellung iseSelInfoSystem = null;

            //Übergabe ok?
            if (cFVFirmaHSList == null) return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "5293444", "CPSAASRModelService", "GetPartComparer"));

            try
            {
                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //ServerFactory vorhanden?
                if (CServerRemoteFactory.remFact != null)
                {
                    //InfosystemEinstellungen holen
                    iseSelInfoSystem = new CInfosystemEinstellung(CServerRemoteFactory.remFact, (int)SelInfoSystem, 0) as CInfosystemEinstellung;

                    //Infosystemeinstellungen vorhanden?
                    if (iseSelInfoSystem is CInfosystemEinstellung)
                    {
                        //Eingestellten Vergleichswert ermitteltn
                        if ((!string.IsNullOrEmpty(iseSelInfoSystem.VergleichsNrLager)) &&
                                (iseSelInfoSystem.VergleichsNrLager != ((int)enumInfosystemVergleichsNr.undefiniert).ToString()))
                        {
                            //BestellNr
                            if ((iseSelInfoSystem.VergleichsNrLager == ((int)enumInfosystemVergleichsNr.bestellnr).ToString()))
                            {
                                //Zurück
                                return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = "BestellNr" });

                            } //HerstellerNR
                            else if ((iseSelInfoSystem.VergleichsNrLager == ((int)enumInfosystemVergleichsNr.herstellernr).ToString()))
                            {
                                //Zurück
                                return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = "HerstellerNr" });
                            }
                            else
                            {
                                //Kein Vergleichswert gesetzt
                                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200723, "Artikel-Vergleichswert in den Infosystem-Einstellungen ist kein gültiger Wert"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "GetPartComparer"));
                            }
                        }
                        else
                        {
                            //Kein Vergleichswert gesetzt
                            return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200723, "Artikel-Vergleichswert in den Infosystem-Einstellungen ist kein gültiger Wert"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "GetPartComparer"));
                        }
                    }
                    else
                    {
                        //Warnung und zurück
                        //Kein Vergleichswert gesetzt
                        return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200724, "Infosystem-Einstellungen können nicht abgerufen werden"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "GetPartComparer"));
                    }
                }
                else
                {
                    //Warnung und zurück
                    //Kein Vergleichswert gesetzt
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200724, "Infosystem-Einstellungen können nicht abgerufen werden"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "GetPartComparer"));
                }

            }
            catch (Exception ex)
            {
                //Fehler
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "GetPartComparer"));
            }
        }

        /// <summary>
        /// Setzt die Bestellungen nebst den Positionen auf den Status "Versendet"
        /// </summary>
        /// <param name="OrderHeadList">Bestellungen</param>
        /// <param name="OrderPosList">Bestellpositionen</param>
        /// <param name="cFVFirmaHSList">Firmenliste</param>
        /// <param name="FirmenNr">FirmenNr</param>
        /// <param name="DA">DataAccess</param>
        /// <returns></returns>
        public async Task<WerbasResult> SetOrderToSended(CBestellungList OrderHeadList, CBestellPosList OrderPosList, CFVFirmaHSList cFVFirmaHSList, int FirmenNr, DataAccess DA)
        {
            try
            {
                //Übergabe ok?
                if (OrderHeadList == null) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "4475110", "CPSAASRModelService", "SetOrderToSended");
                if (OrderPosList == null) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "2166597", "CPSAASRModelService", "SetOrderToSended");
                if (cFVFirmaHSList == null) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "1982417", "CPSAASRModelService", "SetOrderToSended");
                if (FirmenNr == 0) return PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "9837299", "CPSAASRModelService", "SetOrderToSended");

                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Bestellkopf
                foreach (CBestellung OrderHeader in OrderHeadList)
                {
                    if (OrderHeader.Status == SatzStatus.Unchanged) OrderHeader.BeginEdit();
                    OrderHeader.OrderIsOpen = true;
                }
                WerbasResult HeaderResult = await CreateUpdateObjectList(OrderHeadList, cFVFirmaHSList, FirmenNr, DA).ConfigureAwait(false);
                if (!HeaderResult.Result) return HeaderResult;

                //Bestellpositionen
                foreach (CBestellPos bestpos in OrderPosList)
                {
                    if (bestpos.Status == SatzStatus.Unchanged) bestpos.BeginEdit();
                    bestpos.OrderPosIsOpen = true;
                }
                WerbasResult PosResult = await CreateUpdateObjectList(OrderPosList, cFVFirmaHSList, FirmenNr, DA).ConfigureAwait(false);
                if (!PosResult.Result) return PosResult;

                //Zurück
                return new WerbasResult() { ErrorCode = "0", Result = true };
            }
            catch (Exception ex)
            {
                //Fehler
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "2947934", "CPSAASRModelService", "SetOrderToSended");
            }
          
        }

        #region Basisoperationen
        /// <summary>
        /// Legt das übergebene Basisobjekt in der Datenbank an, und gibt es mit der ID zurück
        /// </summary>
        /// <param name="SelObject">CBasisSatz</param>
        /// <param name="cFVFirmaHSList">Context-Firmenliste</param>
        /// <param name="FirmenNr">Aktuelle Firma</param>
        /// <param name="DA">DataAccess</param>
        /// <returns>CBasisSatz</returns>
        public Task<WerbasResult> CreateObject(CBasisSatz SelObject, CFVFirmaHSList cFVFirmaHSList, int FirmenNr, IDataAccess DA)
        {
            try
            {
                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Übergabe prüfen
                if (SelObject == null)
                {
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200636, "Kein gültiges Basis-Objekt vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "CreateObject"));
                }

                if ((cFVFirmaHSList == null) || (cFVFirmaHSList.Count == 0))
                {
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200634, "Kein gültiger Context vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "CreateObject"));
                }

                //Speichern und zurück
                if (CServerRemoteFactory.remFact.InsertSatz((DataAccess)DA, ref SelObject, FirmenNr))
                {
                    //Erfolg
                    return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = SelObject });
                }
                else
                {
                    //Fehlermedung aus remFact?
                    string errorMSG = CMeldung.sRes(200637, "Das Object konnte nicht erfolgreich angelegt werden. Bitte prüfen Sie den Trace");
                    if (!string.IsNullOrEmpty(CServerRemoteFactory.remFact.Errmessage))
                        errorMSG = CServerRemoteFactory.remFact.Errmessage;

                    //Fehler
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(errorMSG, WerbasResultLevel.SchwerwiegenderFehler, CServerRemoteFactory.remFact.Errcode.ToString(), "CPSAASRModelService", "CreateObject"));
                }

            }
            catch (Exception ex)
            {
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult($"{ex.Message}", WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "CreateObject"));
            }
        }

        /// <summary>
        /// Aktualisiert das übergebene Basisobjekt in der Datenbank
        /// </summary>
        /// <param name="SelObject">CBasisSatz</param>
        /// <param name="cFVFirmaHSList">Context-Firmenliste</param>
        /// <param name="FirmenNr">Aktuelle Firma</param>
        /// <returns>CBasisSatz</returns>
        /// <param name="DA">DataAccess</param>
        public Task<WerbasResult> UpdateObject(CBasisSatz SelObject, CFVFirmaHSList cFVFirmaHSList, int FirmenNr, IDataAccess DA)
        {
            try
            {
                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Übergabe prüfen
                if (SelObject == null)
                {
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200636, "Kein gültiges Basis-Objekt vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "UpdateObject"));
                }

                if ((cFVFirmaHSList == null) || (cFVFirmaHSList.Count == 0))
                {
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200634, "Kein gültiger Context vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "UpdateObject"));
                }

                //Speichern und zurück
                CUpdateActionInfo UpdateResult = CServerRemoteFactory.remFact.UpdateSatz((DataAccess)DA, ref SelObject, FirmenNr);
                if (UpdateResult.ErrCode == 0)
                {
                    //Erfolg
                    return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = SelObject });
                }
                else
                {
                    //Fehlermedung aus remFact?
                    string errorMSG = CMeldung.sRes(-1, "Das Object konnte nicht erfolgreich aktualisiert werden. Bitte prüfen Sie den Trace");
                    if (!string.IsNullOrEmpty(CServerRemoteFactory.remFact.Errmessage))
                        errorMSG = CServerRemoteFactory.remFact.Errmessage;

                    //Fehler
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(errorMSG, WerbasResultLevel.SchwerwiegenderFehler, CServerRemoteFactory.remFact.Errcode.ToString(), "CPSAASRModelService", "UpdateObject"));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult($"{ex.Message}", WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "UpdateObject"));
            }
        }

        /// <summary>
        /// Loescht das übergebene Basisobjekt in der Datenbank
        /// </summary>
        /// <param name="SelObject">CBasisSatz</param>
        /// <param name="cFVFirmaHSList">Context-Firmenliste</param>
        /// <param name="FirmenNr">Aktuelle Firma</param>
        /// <param name="DA">DataAccess</param>
        /// <returns>CBasisSatz</returns>
        public Task<WerbasResult> DeleteObject(CBasisSatz SelObject, CFVFirmaHSList cFVFirmaHSList, int FirmenNr, DataAccess DA)
        {
            try
            {
                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Übergabe prüfen
                if (SelObject == null)
                {
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200636, "Kein gültiges Basis-Objekt vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "DeleteObject"));
                }

                if ((cFVFirmaHSList == null) || (cFVFirmaHSList.Count == 0))
                {
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200634, "Kein gültiger Context vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "DeleteObject"));
                }

                //Speichern und zurück
                if (CServerRemoteFactory.remFact.DeleteSatz(DA, SelObject, FirmenNr))
                {
                    //Erfolg
                    return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = SelObject });
                }
                else
                {
                    //Fehler
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200638, "Das Object konnte nicht erfolgreich gelöscht werden. Bitte prüfen Sie den Trace"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "DeleteObject"));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult($"{ex.Message}", WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "DeleteObject"));
            }
        }

        /// <summary>
        /// Loescht anhand der uebergebenen Suche die Objekte in der Datenbank
        /// </summary>
        /// <param name="SelParameter">Aufgebauter Suchparamete</param>
        /// <param name="cFVFirmaHSList">Context-Firmenliste</param>
        /// <param name="FirmenNr">Aktuelle Firma</param>
        /// <param name="DA">DataAccess</param>
        /// <param name="SelTimeOut">Timeout</param>
        /// <returns></returns>
        public Task<WerbasResult> DeleteObjectList(CSuche SelParameter, CFVFirmaHSList cFVFirmaHSList, int FirmenNr, IDataAccess DA, int SelTimeOut = 320)
        {
            try
            {
                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Übergabe prüfen
                if (SelParameter == null)
                {
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200633, "Keine gültige Suche vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "DeleteObjectList"));
                }

                if ((cFVFirmaHSList == null) || (cFVFirmaHSList.Count == 0))
                {
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200634, "Kein gültiger Context vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "DeleteObjectList"));
                }

                //Objekte löschen holen
                if (CServerRemoteFactory.remFact.DelList(DA, FirmenNr, SelParameter.ToString(), SelTimeOut))
                {
                    //zurück
                    return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = null });
                }
                else
                {
                    //Fehler
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200639, "Fehler beim lösche der Objekte in der DB, bitte prüfen Sie den Trace"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "DeleteObjectList"));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult($"{ex.Message}", WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "DeleteObjectList"));
            }
        }

        /// <summary>
        /// Erstellt/Editiert die Objektliste in der Datenbank.
        /// Rueckgabe als CBasisListe in ResultObject
        /// </summary>
        /// <param name="SelObject">Die zu speichernde Liste</param>
        /// <param name="cFVFirmaHSList">Context-Firmenliste</param>
        /// <param name="FirmenNr">Aktuelle Firma</param>
        /// <param name="DA">DataAccess</param>
        /// <returns></returns>
        public Task<WerbasResult> CreateUpdateObjectList(CBasisListe SelObject, CFVFirmaHSList cFVFirmaHSList, int FirmenNr, IDataAccess DA)
        {
            try
            {
                //Datenzugriff
                if (DA == null) DA = new DataAccess();

                //Übergabe prüfen
                if (SelObject == null)
                {
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200640, "Kein gültiges Listen-Objekt vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "CreateUpdateObjectList"));
                }

                if ((cFVFirmaHSList == null) || (cFVFirmaHSList.Count == 0))
                {
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200634, "Kein gültiger Context vorhanden"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "CreateUpdateObjectList"));
                }

                //Objekte update/create
                if (CServerRemoteFactory.remFact.UpdateList_MF(DA, SelObject.Changes, FirmenNr))
                {
                    //zurück
                    return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = SelObject });
                }
                else
                {
                    //Fehler
                    return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(CMeldung.sRes(200642, "Fehler beim anlegen/editieren des Listenobjekts in der DB, bitte prüfen Sie den Trace"), WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "CreateUpdateObjectList"));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult($"{ex.Message}", WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "CreateUpdateObjectList"));
            }
        }

        /// <summary>
        /// Fuehrt DA.ExecSQL aus, und gibt das Ergebnis als bool
        /// zurueck
        /// </summary>
        /// <param name="DA">DataAccess</param>
        /// <param name="selSQLCommand">SQL-Befehl</param>
        /// <param name="selFirmID">FirmenID</param>
        /// <param name="selTimeOut">Timeout</param>
        /// <returns></returns>
        public Task<WerbasResult> ExecSQL(DataAccess DA, string selSQLCommand, string selFirmID, int selTimeOut)
        {
            //Übergabe ok?
            if (string.IsNullOrEmpty(selFirmID)) return Task.FromResult(CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "6535334", "CPSAASRModelService", "ExecSQL"));
            if (string.IsNullOrEmpty(selSQLCommand)) return Task.FromResult(CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "6535362", "CPSAASRModelService", "ExecSQL"));
            if (DA == null) DA = new DataAccess();

            try
            {
                //SQl Command ausführen
                bool ReturnValue = DA.ExecSQL(selSQLCommand, selFirmID, selTimeOut);
    
                //Fehler aufgetreten?
                if ((DA.Errcode > 0) || (!ReturnValue))
                {
                    string ErrMessage = string.Empty;
                    if (!string.IsNullOrEmpty(DA.Errmessage))
                        ErrMessage = DA.Errmessage;
                    if (!string.IsNullOrEmpty(DA.MainErrorMessage))
                        ErrMessage = ErrMessage + DA.MainErrorMessage;
                    if (string.IsNullOrEmpty(ErrMessage))
                        ErrMessage = "Die Ausführung der DB-Funktion ist aus unbekannten Gründen fehlgeschlagen. Bitte prüfen Sie das Log vom SQL-Server";

                    return Task.FromResult(new WerbasResult() { ErrorCode = DA.Errcode.ToString(), Result = false, ErrorMessage = "CPSAASRModelService.ExecSQL(): " + ErrMessage, ResultObject = ReturnValue });
                }

                //zurück
                return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = ReturnValue });
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "6535324", "CPSAASRModelService", "ExecSQL"));
            }
        }

        /// <summary>
        /// Fuehrt DA.ExecSQLRowsAffected aus, und gibt das Ergebnis als int
        /// zurueck
        /// </summary>
        /// <param name="DA">DataAccess</param>
        /// <param name="selSQLCommand">SQL-Befehl</param>
        /// <param name="selFirmID">FirmenID</param>
        /// <param name="selTimeOut">Timeout</param>
        /// <returns></returns>
        public Task<WerbasResult> ExecSQLRowsAffected(IDataAccess DA, string selSQLCommand, string selFirmID, int selTimeOut)
        {
            //Übergabe ok?
            if (string.IsNullOrEmpty(selFirmID)) return Task.FromResult(CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "5471447", "CPSAASRModelService", "ExecSQLRowsAffected"));
            if (string.IsNullOrEmpty(selSQLCommand)) return Task.FromResult(CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "7029104", "CPSAASRModelService", "ExecSQLRowsAffected"));
            if (DA == null) DA = new DataAccess();

            try
            {
                //SQl Command ausführen
                int ReturnValue = DA.ExecSQLRowsAffected(selSQLCommand, selFirmID, selTimeOut);

                //Fehler aufgetreten?
                if (DA.Errcode > 0)
                {
                    string ErrMessage = string.Empty;
                    if (!string.IsNullOrEmpty(DA.Errmessage))
                        ErrMessage = DA.Errmessage;
                    if (!string.IsNullOrEmpty(DA.MainErrorMessage))
                        ErrMessage = ErrMessage + DA.MainErrorMessage;
                    if (string.IsNullOrEmpty(ErrMessage))
                        ErrMessage = "Die Ausführung der DB-Funktion ist aus unbekannten Gründen fehlgeschlagen. Bitte prüfen Sie das Log vom SQL-Server";

                    return Task.FromResult(new WerbasResult() { ErrorCode = DA.Errcode.ToString(), Result = false, ErrorMessage = "CPSAASRModelService.ExecSQLRowsAffected(): " + ErrMessage, ResultObject = ReturnValue });
                }

                //zurück
                return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = ReturnValue });
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "5470079", "CPSAASRModelService", "ExecSQLRowsAffected"));
            }
        }

        /// <summary>
        /// Fuehrt DA.ExecSQL_mitArrayList aus, und gibt das Ergebnis als bool
        /// zurueck
        /// </summary>
        /// <param name="DA">DataAccess</param>
        /// <param name="selSQLCommand">SQL-Befehl</param>
        /// <param name="selFirmID">FirmenID</param>
        /// <param name="selTimeOut">Timeout</param>
        /// <returns></returns>
        public Task<WerbasResult> ExecSQLWithArrayList(DataAccess DA, string selSQLCommand, string selFirmID, int selTimeOut)
        {
            //Übergabe ok?
            if (string.IsNullOrEmpty(selFirmID)) return Task.FromResult(CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "6535334", "CPSAASRModelService", "ExecSQL"));
            if (string.IsNullOrEmpty(selSQLCommand)) return Task.FromResult(CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "6535362", "CPSAASRModelService", "ExecSQL"));
            if (DA == null) DA = new DataAccess();
            ArrayList ResultList = new ArrayList();

            try
            {
                //SQl Command ausführen
                if (DA.ExecSQL_mitArrayList(selSQLCommand, selFirmID, selTimeOut, ref ResultList))
                    return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = ResultList });
                else
                    return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = false, ErrorMessage = "Fehler beim Ausfuehren des SQL-Command", ResultObject = new ArrayList() });
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "6135424", "CPSAASRModelService", "ExecSQLWithArrayList"));
            }
        }

        /// <summary>
        /// Kopie von CArtikelRemoteHelper.UpdateArtAndArtLagerdaten(),
        /// allerdings auf einen Artikel bezogen
        /// </summary>
        /// <param name="DA">Data Access</param>
        /// <param name="SelUpdatePart">Artikel</param>
        /// <param name="StorageList">Lagerliste</param>
        /// <param name="FVFirmaHSList">CFVFirmaHSList</param>
        /// <param name="FirmNr">FirmenNr</param>
        /// <param name="WorkShopNr">FilialNr</param>
        /// <returns></returns>
        public async Task<WerbasResult> UpdateArtAndArtLagerdaten(DataAccess DA, CArtikel SelUpdatePart, CLagerList StorageList, CFVFirmaHSList FVFirmaHSList, int FirmNr, int WorkShopNr)
        {
            //Übergabe ok?
            if (SelUpdatePart == null) return CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "6097372", "CPSAASRModelService", "UpdateArtAndArtLagerdaten");
            if (StorageList == null) return CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "2401430", "CPSAASRModelService", "UpdateArtAndArtLagerdaten");
            if (FVFirmaHSList == null) return CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "8420604", "CPSAASRModelService", "UpdateArtAndArtLagerdaten");
            if (FirmNr == 0) return CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "3812717", "CPSAASRModelService", "UpdateArtAndArtLagerdaten");
            if (WorkShopNr == 0) return CPSAASRFileModulService.TraceErrorAndCreateResult(CMeldung.sRes(200653, "Übergabewert ist NULL"), WerbasResultLevel.SchwerwiegenderFehler, "5800415", "CPSAASRModelService", "UpdateArtAndArtLagerdaten");
            if (DA == null) DA = new DataAccess();

            try
            {
                //Defaultlager
                CLager SelDefaultStorage = StorageList.GetDefaultLagerOderErstesLager(FirmNr, WorkShopNr);
                if (SelDefaultStorage == null) SelDefaultStorage = (CLager)StorageList.GetDefaultSatz(FirmNr);

                //Artikel aktualisieren
                WerbasResult SaveResult = await UpdateObject(SelUpdatePart, FVFirmaHSList, FirmNr, DA).ConfigureAwait(false);
                if (!SaveResult.Result) return SaveResult;

                //Artikellagerdaten setzen
                CArtikelLagerdatenList algList = new CArtikelLagerdatenList();
                CArtikelLieferantList artliefList = new CArtikelLieferantList();
                string artlgdnr_attr = CAttributeHelper.GetName(typeof(CArtikelLagerdaten), "ArtikelS");
                if (SelUpdatePart.HauptartikelID == 0)
                {
                    string sel = artlgdnr_attr + "='" + SelUpdatePart.ID + "'";
                    int laenge = CServerRemoteFactory.remFact.GetListLaenge(typeof(CArtikelLagerdatenList), sel, SelUpdatePart.FirmenNr, FirmNr);

                    if ((SelDefaultStorage is CLager) && (laenge <= 0)) algList.AddSatz(CArtikelLagerdaten.CreateNewArtikelLagerdaten(SelUpdatePart, FirmNr, WorkShopNr, SelDefaultStorage));
                }

                //Artikellagerdaten speichern
                try
                {
                    CServerRemoteFactory.remFact.UpdateList_MF(algList.Changes, FirmNr);
                }
                catch (Exception ex)
                {
                    return PSAGeneralParameters.TraceErrorAndCreateResult($"{ex.Message}", WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "UpdateArtAndArtLagerdaten");
                }

                //Artikel-Lieferanten Daten speichern
                try
                {
                    CServerRemoteFactory.remFact.UpdateList_MF(artliefList.Changes, WorkShopNr);
                }
                catch (Exception ex)
                {
                    return PSAGeneralParameters.TraceErrorAndCreateResult($"{ex.Message}", WerbasResultLevel.SchwerwiegenderFehler, "-2", "CPSAASRModelService", "UpdateArtAndArtLagerdaten");
                }

                //zurück
                return new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = SelUpdatePart };
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "7377898", "CPSAASRModelService", "UpdateArtAndArtLagerdaten");
            }
        }
        #endregion Basisoperationen

        #region RTF-Helper
        /// <summary>
        /// Erzeugt einen RTF-Text über einen internen RTF-Helper
        /// </summary>
        /// <param name="currentCompanyNumber">FirmenNr</param>
        /// <param name="plainText">Der zu wandelnde Text</param>
        /// <returns></returns>
        public Task<WerbasResult> GenerateRTFText(int currentCompanyNumber, string plainText)
        {
            try
            {
                //RTF-Helper vorhanden?
                if (m_rtfHelper == null)
                    m_rtfHelper = new CAuftrRTFHelper();
                
                //zurück
                return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = m_rtfHelper.ErzeugeNormalPosRTF(plainText, 'L') });
            }
            catch (Exception ex)
            {
                //Fehler loggen und zurück
                return Task.FromResult(PSAGeneralParameters.TraceErrorAndCreateResult(ex.Message, WerbasResultLevel.SchwerwiegenderFehler, "1293004", "CPSAASRModelService", "GenerateRTFText"));
            }
        }

        /// <inheritdoc/>
        public int NextClientNr()
        {
            return CServerRemoteFactory.remFact.NextClientNr(out DateTime serverTime);
        }

        /// <inheritdoc/>
        public Task<WerbasResult> RecalculateWorkOrderPosition(CAuftrKopf foundWorkOrder, CAuftrPos newWorkOrderPos, CArtikel foundNewPart, CFVFirmaHSList cFVFirmaHSList)
        {
            BO_Serial.Auftrag.CAuftrPosPreiskalkulation preisKalk = new BO_Serial.Auftrag.CAuftrPosPreiskalkulation(CServerRemoteFactory.remFact, cFVFirmaHSList);
            preisKalk.TeilPosNeuKalkulieren(foundWorkOrder, newWorkOrderPos, foundNewPart);
            return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = newWorkOrderPos });
        }

        /// <inheritdoc/>
        public Task<WerbasResult> SaveWorkOrderWithPositions(CAuftrKopf foundWorkOrder, CAuftrPosList foundWorkOrderPositions)
        {
            //Änderungen speichern
            ArrayList saveReturn = CAuftragRemoteHelper.AuftragSpeichern(foundWorkOrder, null, foundWorkOrderPositions, null, -1, "");
            return Task.FromResult(new WerbasResult() { ErrorCode = "0", Result = true, ErrorMessage = string.Empty, ResultObject = saveReturn });
        }
        #endregion
    }


}
