namespace Instant
{
    internal class Program
    {
        [STAThread]
        static void Main() => new Program().Run();

        internal void Run()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new InstantApplicationContext());
        }
    }
}