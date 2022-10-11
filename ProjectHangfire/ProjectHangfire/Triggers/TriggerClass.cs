namespace ProjectHangfire.Triggers
{
    public static class TriggerClass
    {

        public static async Task SendData()
        {
            HttpClient client = new HttpClient();
            var data = await client.GetAsync("https://localhost:7297/api/BackgroundJob/GetDate");
            var data2 = await data.Content.ReadAsStringAsync();        
            var time = DateTimeOffset.Parse(data2);
        }
    }
}
