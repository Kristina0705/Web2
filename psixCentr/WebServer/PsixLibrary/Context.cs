namespace PsixLibrary
{
    public class Context
    {
        public static ApplicationContext db { get; private set; }
        internal static void AddDb(ApplicationContext application)
        {
            db = application;
        }
        public Context(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }
    }
}
