using RestPanda;
using Trivial.Security;
using PsixLibrary;
using PsixLibrary.Entity;

namespace WebServer;

internal class Program
{
    internal static HashSignatureProvider Sign { get; private set; }

    private static void Main(string[] args)
    {
        using var db = new ApplicationContext(ApplicationContext.GetDb()); 
        Sign = HashSignatureProvider.CreateHS256("oVKJYivecvudMHCELtNHDmER7Z3ASeXZ6D14vCnXk8zzcFlqemB5S8NMeNwqThmT");
        var config = new PandaConfig();
        config.AddHeader("access-control-allow-origin", "*");
        config.AddHeader("access-control-allow-headers", "*");
        using var server = new PandaServer(config, new Uri("http://localhost:8080/"));
        server.Start();
        Console.WriteLine("Server started");
        Console.Read();
    }
}