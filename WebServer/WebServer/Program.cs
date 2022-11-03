using RestPanda;
using Trivial.Security;
using PsixLibrary;

namespace WebServer;
public class Program
{
    internal static HashSignatureProvider Sign { get; private set; } = null!;
    public static void Main()
    {
        using var ac = new ApplicationContext(ApplicationContext.GetDb());
        Sign = HashSignatureProvider.CreateHS256("pcix");
        var config = new PandaConfig();
        config.AddHeader("access-control-allow-origin", "*");
        config.AddHeader("access-control-allow-headers", "*");
        var server = new PandaServer(config, new Uri("http://localhost:8180"));
        server.Start();
        Console.WriteLine("Server started");
        Console.Read();
        server.Stop();
    }
}

