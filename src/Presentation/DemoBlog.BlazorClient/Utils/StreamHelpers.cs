namespace DemoBlog.BlazorClient.Utils
{
    public static class StreamHelpers
    {
        public static string ToBase64Image(this MemoryStream stream)
        {
            string base64String = Convert.ToBase64String(stream.ToArray());
            return $"data:image/png;base64,{base64String}";
        }
    }
}
