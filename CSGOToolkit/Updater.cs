using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Web.Script.Serialization;

namespace CSGOToolkit
{
    public static class Updater
    {
        private const string Address = "";

        public static void SendUpdate(IList<Player> players)
        {
            using (var client = new WebClient())
            {
                try
                {
                    var serializer = new JavaScriptSerializer();
                    var json = serializer.Serialize(players);
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.UploadString(Address, "POST", json);
                    PrintConsole(players);
                }
                catch
                {
                    Trace.WriteLine("Sending update has failed.");
                }
            }
        }

        private static void PrintConsole(IEnumerable<Player> players)
        {
            Console.Clear();

            foreach (var p in players)
            {
                Console.WriteLine("PlayerId: 0x" + p.PlayerId.ToString("X2")
                    + ", TeamId: 0x" + p.TeamId.ToString("X2") 
                    + ", Health: " + p.Health.ToString("000")
                    + ", X: " + p.X.ToString(" 0000.00;-0000.00") 
                    + ", Y: " + p.Y.ToString(" 0000.00;-0000.00") 
                    + ", Z: " + p.Z.ToString(" 0000.00;-0000.00"));
            }
        }
    }
}
