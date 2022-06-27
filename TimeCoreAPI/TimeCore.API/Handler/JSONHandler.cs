using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using TimeCore.ErrorHandler;

namespace TimeCore.API.Handler
{
    public class JSONHandler : IJSONHandler
    {
        public RequestModel ConvertJSONStringtoRequestModel(string jsonData)
        {
            //Init
            RequestModel returnModel = null;

            try
            {
                //RequestModel aus parameter holen
                Regex requestRegex = new Regex(@"\{([^}]*)\}", RegexOptions.IgnoreCase);
                Match requestMatch = requestRegex.Match(jsonData);
                if ((requestMatch.Success) && (requestMatch.Groups.Count == 2) && (requestMatch.Groups[0].Value.StartsWith("{")))
                    returnModel = JsonConvert.DeserializeObject<RequestModel>(requestMatch.Groups[0].Value);

                //Ungültig? Dann zumindest leer setzen
                if (returnModel is null)
                    returnModel = new RequestModel();

                //zurück
                return returnModel;
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"JSONHandler.ConvertJSONStringtoRequestModel(): {ex.Message}");
                return returnModel;
            }
        }
    }
}
