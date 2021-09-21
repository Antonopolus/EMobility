[![CI and CD E-Mobility](https://github.com/Antonopolus/EMobility/actions/workflows/ci-cd.yaml/badge.svg)](https://github.com/Antonopolus/EMobility/actions/workflows/ci-cd.yaml)
# EMobility

<pre><code>

Console.WriteLine("Hello, World!");


HttpClient client = new HttpClient();

var  resStream = await client.GetStreamAsync("https://localhost:5001/api/ChargingPoint/2");
var res =   await JsonSerializer.DeserializeAsync<ChargingPoint>(resStream);
Console.WriteLine(res);

resStream = await client.GetStreamAsync("https://localhost:5001/api/ChargingPoint");
var res2 = await JsonSerializer.DeserializeAsync<IEnumerable<ChargingPoint>>(resStream);

foreach (var item in res2)
{
    Console.WriteLine(item);
}

var resString = await client.GetStringAsync("https://localhost:5001/api/ChargingPoint");
var res3 = JsonSerializer.Deserialize<IEnumerable<ChargingPoint>>(resString);
foreach (var item in res3)
{
    Console.WriteLine(item);
}

var sepp = await client.GetAsync("https://localhost:5001/api/ChargingPoint");
if(sepp.IsSuccessStatusCode)
{
    var otto = await sepp.Content.ReadAsStringAsync();
    var res4 = JsonSerializer.Deserialize<IEnumerable<ChargingPoint>>(otto);
    foreach (var item in res4)
    {
        Console.WriteLine(item);
    }
}

var httpContent = new StringContent(
                            JsonSerializer.Serialize(res),
                            Encoding.UTF8,
                            "application/json");
   

await client.PostAsync("https://localhost:5001/api/ChargingPoint", httpContent);



public record ChargingPoint(int id, string name, string restUrl, string chargingPointId);
</code></pre>
