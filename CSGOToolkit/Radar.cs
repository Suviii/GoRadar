using ProcessMemoryReaderLib;
using System.Diagnostics;
using System.Threading;

namespace CSGOToolkit
{
    public class Radar
    {
        public Radar()
        {
            Start();
        }

        public void Start()
        {
            var memoryReader = new ProcessMemoryReader();

            while (true)
            {
                foreach (var p in Process.GetProcesses())
                {
                    if (!p.ProcessName.Contains("csgo")) continue;
                    foreach (ProcessModule m in p.Modules)
                    {
                        if (m.ModuleName.Contains("client.dll"))
                        {
                            Offsets.ClientBaseAddress = (int) m.BaseAddress;
                        }
                    }

                    memoryReader.ReadProcess = p;
                    memoryReader.OpenProcess();
                    Updater.SendUpdate(Core.GetPlayers(memoryReader));
                }

                Thread.Sleep(10);
            }
        }
    }
}
