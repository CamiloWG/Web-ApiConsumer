namespace ApiConsumer.Services
{
    public class ConnectionAPI
    {
        public HttpClient Connection
        {
            get
            {
                var con = new HttpClient();
                con.BaseAddress = new Uri("http://localhost:5160/");
                return con;
            }
        }
    }
}
