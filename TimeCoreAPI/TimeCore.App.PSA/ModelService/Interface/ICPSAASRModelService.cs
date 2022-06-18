using BO_Basis;
using BO_Serial;
using DA_Basis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wag.Dev.Common.Models;

namespace BO_Remote.PSA.ASR.ModelService.Interface
{
    // <summary>
    /// Schnittstellenklasse fuer PSA ASR
    /// Die Klasse ist einzig dazu da, Datenbankanfragen zur Verfügung zur stellen.
    /// </summary>
    public interface ICPSAASRModelService
    {
        /// <summary>
        /// Generische Suche
        /// </summary>
        /// <typeparam name="T">CBAsisListe</typeparam>
        /// <param name="SelParameter">Suchparameter</param>
        /// <param name="cFVFirmaHSList">Firmenliste</param>
        /// <param name="DA">DataAccess</param>
        /// <returns></returns>
        Task<WerbasResult> GenericDBSearch<T>(CSuche SelParameter, CFVFirmaHSList cFVFirmaHSList, DataAccess DA) where T : new();

        /// <summary>
        /// Generische Suche
        /// </summary>
        /// <typeparam name="T">CBAsisListe</typeparam>
        /// <param name="SelParameter">Suchparameter</param>
        /// <param name="cFVFirmaHSList">Firmenliste</param>
        /// <param name="DA">IDataAccess</param>
        /// <param name="selSearchMode">FilialeGetMode</param>
        /// <returns></returns>
        Task<WerbasResult> GenericDBSearch<T>(CSuche SelParameter, CFVFirmaHSList cFVFirmaHSList, IDataAccess DA, FilialeGetMode selSearchMode = FilialeGetMode.FilialeEgal) where T : new();

        // <summary>
        /// Generische Suche mit FilialeGetMode
        /// </summary>
        /// <typeparam name="T">CBAsisListe</typeparam>
        /// <param name="selParameter">Suchparameter</param>
        /// <param name="selSearchMode">FilialeGetMode</param>
        /// <param name="cFVFirmaHSList">Firmenliste</param>
        /// <param name="DA">DataAccess</param>
        /// <returns></returns>
        Task<WerbasResult> GenericDBSearch<T>(CSuche SelParameter, FilialeGetMode selSearchMode, CFVFirmaHSList cFVFirmaHSList, DataAccess DA) where T : new();

        /// <summary>
        /// Legt das übergebene Basisobjekt in der Datenbank an, und gibt es mit der ID zurück
        /// </summary>
        /// <param name="SelObject">CBasisSatz</param>
        /// <param name="cFVFirmaHSList">Context-Firmenliste</param>
        /// <param name="FirmenNr">Aktuelle Firma</param>
        /// <param name="DA">DataAccess</param>
        /// <returns>CBasisSatz</returns>
        Task<WerbasResult> CreateObject(CBasisSatz SelObject, CFVFirmaHSList cFVFirmaHSList, int FirmenNr, IDataAccess DA);

        /// <summary>
        /// Aktualisiert das übergebene Basisobjekt in der Datenbank
        /// </summary>
        /// <param name="SelObject">CBasisSatz</param>
        /// <param name="cFVFirmaHSList">Context-Firmenliste</param>
        /// <param name="FirmenNr">Aktuelle Firma</param>
        /// <param name="DA">DataAccess</param>
        /// <returns>CBasisSatz</returns>
        Task<WerbasResult> UpdateObject(CBasisSatz SelObject, CFVFirmaHSList cFVFirmaHSList, int FirmenNr, IDataAccess DA);

        /// <summary>
        /// Loescht das übergebene Basisobjekt in der Datenbank
        /// </summary>
        /// <param name="SelObject">CBasisSatz</param>
        /// <param name="cFVFirmaHSList">Context-Firmenliste</param>
        /// <param name="FirmenNr">Aktuelle Firma</param>
        /// <param name="DA">DataAccess</param>
        /// <returns>CBasisSatz</returns>
        Task<WerbasResult> DeleteObject(CBasisSatz SelObject, CFVFirmaHSList cFVFirmaHSList, int FirmenNr, DataAccess DA);

        /// <summary>
        /// Loescht anhand der uebergebenen Suche die Objekte in der Datenbank
        /// </summary>
        /// <param name="SelParameter">Aufgebauter Suchparamete</param>
        /// <param name="cFVFirmaHSList">Context-Firmenliste</param>
        /// <param name="FirmenNr">Aktuelle Firma</param>
        /// <param name="DA">DataAccess</param>
        /// <param name="SelTimeOut">Timeout</param>
        /// <returns></returns>
        Task<WerbasResult> DeleteObjectList(CSuche SelParameter, CFVFirmaHSList cFVFirmaHSList, int FirmenNr, IDataAccess DA, int SelTimeOut = 320);

        /// <summary>
        /// Erstellt/Editiert die Objektliste in der Datenbank
        /// </summary>
        /// <param name="SelObject">Die zu speichernde Liste</param>
        /// <param name="cFVFirmaHSList">Context-Firmenliste</param>
        /// <param name="FirmenNr">Aktuelle Firma</param>
        /// <param name="DA">DataAccess</param>
        /// <returns></returns>
        Task<WerbasResult> CreateUpdateObjectList(CBasisListe SelObject, CFVFirmaHSList cFVFirmaHSList, int FirmenNr, IDataAccess DA);

        // <summary>
        /// Holt den Artikel-Vergleichswert aus den Schnittstelleneinstellungen
        /// </summary>
        /// <param name="SelInfoSystem">Infosystemeinstellungen</param>
        /// <param name="cFVFirmaHSList">Firmenliste</param>
        /// <param name="DA">DataAccess</param>
        /// <returns></returns>
        Task<WerbasResult> GetPartComparer(enumInfosys SelInfoSystem, CFVFirmaHSList cFVFirmaHSList, DataAccess DA);

        /// <summary>
        /// Setzt die Bestellungen nebst den Positionen auf den Status "Versendet"
        /// </summary>
        /// <param name="OrderHeadList">Bestellungen</param>
        /// <param name="OrderPosList">Bestellpositionen</param>
        /// <param name="cFVFirmaHSList">Firmenliste</param>
        /// <param name="FirmenNr">FirmenNr</param>
        /// <param name="DA">DataAccess</param>
        /// <returns></returns>
        Task<WerbasResult> SetOrderToSended(CBestellungList OrderHeadList, CBestellPosList OrderPosList, CFVFirmaHSList cFVFirmaHSList, int FirmenNr, DataAccess DA);

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
        Task<WerbasResult> UpdateArtAndArtLagerdaten(DataAccess DA, CArtikel SelUpdatePart, CLagerList StorageList, CFVFirmaHSList FVFirmaHSList, int FirmNr, int WorkShopNr);

        /// <summary>
        /// Fuehrt DA.ExecSQL aus, und gibt das Ergebnis als bool
        /// zurueck
        /// </summary>
        /// <param name="DA">DataAccess</param>
        /// <param name="selSQLCommand">SQL-Befehl</param>
        /// <param name="selFirmID">FirmenID</param>
        /// <param name="selTimeOut">Timeout</param>
        /// <returns></returns>
        Task<WerbasResult> ExecSQL(DataAccess DA, string selSQLCommand, string selFirmID, int selTimeOut);

        /// <summary>
        /// Fuehrt DA.ExecSQL_mitArrayList aus, und gibt das Ergebnis als bool
        /// zurueck
        /// </summary>
        /// <param name="DA">DataAccess</param>
        /// <param name="selSQLCommand">SQL-Befehl</param>
        /// <param name="selFirmID">FirmenID</param>
        /// <param name="selTimeOut">Timeout</param>
        /// <returns></returns>
        Task<WerbasResult> ExecSQLWithArrayList(DataAccess DA, string selSQLCommand, string selFirmID, int selTimeOut);

        /// <summary>
        /// Fuehrt DA.ExecSQLRowsAffected aus, und gibt das Ergebnis als int
        /// zurueck
        /// </summary>
        /// <param name="DA">DataAccess</param>
        /// <param name="selSQLCommand">SQL-Befehl</param>
        /// <param name="selFirmID">FirmenID</param>
        /// <param name="selTimeOut">Timeout</param>
        /// <returns></returns>
        Task<WerbasResult> ExecSQLRowsAffected(IDataAccess DA, string selSQLCommand, string selFirmID, int selTimeOut);

        /// <summary>
        /// Erzeugt einen RTF-Text über einen internen RTF-Helper
        /// </summary>
        /// <param name="currentCompanyNumber">FirmenNr</param>
        /// <param name="plainText">Der zu wandelnde Text</param>
        /// <returns></returns>
        Task<WerbasResult> GenerateRTFText(int currentCompanyNumber, string plainText);

        /// <summary>
        /// Holt die aktuelle ClientNummer
        /// </summary>
        /// <returns></returns>
        int NextClientNr();

        /// <summary>
        /// Neuberechnung der Auftragsposition mit dem Artikel
        /// </summary>
        /// <param name="foundWorkOrder">Auftragskopf</param>
        /// <param name="newWorkOrderPos">Neue Auftragsposition</param>
        /// <param name="foundNewPart">Artikel</param>
        /// <param name="cFVFirmaHSList">Firmenliste</param>
        /// <returns></returns>
        Task<WerbasResult> RecalculateWorkOrderPosition(CAuftrKopf foundWorkOrder, CAuftrPos newWorkOrderPos, CArtikel foundNewPart, CFVFirmaHSList cFVFirmaHSList);

        /// <summary>
        /// Speichert den Auftrag mitsammt seinen Positionen ab, und fuehrt
        /// eine Neubewertung durch
        /// </summary>
        /// <param name="foundWorkOrder">Auftragskopf</param>
        /// <param name="foundWorkOrderPositions">Auftragspositionen</param>
        /// <returns></returns>
        Task<WerbasResult> SaveWorkOrderWithPositions(CAuftrKopf foundWorkOrder, CAuftrPosList foundWorkOrderPositions);
    }
}
