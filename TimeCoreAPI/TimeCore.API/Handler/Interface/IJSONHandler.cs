namespace TimeCore.API.Handler
{
    public interface IJSONHandler
    {
        RequestModel ConvertJSONStringtoRequestModel(string jsonData);
    }
}