using Newtonsoft.Json;
using System.Net;
using System.Text;

for (int i = 0; i < 6; i++)
{
    Map map = new()
    {

        sizeX = 1, sizeY = 1, Triangles = new int[] { 1,2}, Vertices = new()
    };
    var request = (HttpWebRequest)WebRequest.Create($"http://devel1:5113/GameObject/MapChange");
    var json = JsonConvert.SerializeObject(map);

    // create a request
    request.KeepAlive = false;
    request.ProtocolVersion = HttpVersion.Version10;
    request.Method = "POST";
    request.Proxy = null;

    // turn our request string into a byte stream
    byte[] postBytes = Encoding.UTF8.GetBytes(json);

    // this is important - make sure you specify type this way
    request.ContentType = "application/json; charset=UTF-8";
    request.Accept = "application/json";
    request.ContentLength = postBytes.Length;
    Stream requestStream = request.GetRequestStream();

    // now send it
    requestStream.Write(postBytes, 0, postBytes.Length);
    requestStream.Close();

    // grab te response and print it out to the console along with the status code
    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    string result;
    using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
    {
        result = rdr.ReadToEnd();
    }

    Console.WriteLine(result);

    Console.WriteLine(i);
  
}

class Map
{
    public List<float[]> Vertices;
    public int[] Triangles;
    public int sizeX;
    public int sizeY;
  
}


