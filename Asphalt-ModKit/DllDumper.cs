using Eco.Core.Plugins;
using Eco.ModKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asphalt
{
    public static class DllDumper
    {
        private static string[] mAssemblies = new[]
        {
            "Eco.Core.dll",
            "Eco.Gameplay.dll",
            "Eco.ModKit.dll",
            "Eco.Shared.dll",
            "Eco.Simulation.dll",
            "Eco.World.dll",
            "Eco.Stats.dll",
            "Eco.Plugins.dll",
            "Eco.Stats.dll",
            "Eco.Webserver.dll",
            "Eco.Worldgenerator.dll",
            "LiteDB.dll"
        };

        public static void DumpDlls()
        {
            var configs = new PluginConfig<ModKitConfig>("ModKit");

            if (!configs.Config.PreserveGeneratedModsAssembly)
            {
                configs.Config.PreserveGeneratedModsAssembly = true;
                configs.Save();

                throw new Exception("PreserveGeneratedModsAssembly was false, please restart!");
            }

            Assembly entryAssembly = Assembly.GetEntryAssembly();

            string destDir = Path.Combine(Path.GetDirectoryName(entryAssembly.Location), "extracted");
            Directory.CreateDirectory(destDir);

            foreach (string assembly in mAssemblies)
            {
                string asmname = $"costura.{assembly}.compressed".ToLower();
                string destFileName = Path.Combine(destDir, assembly);

                File.Delete(destFileName);

                using (Stream stream = entryAssembly.GetManifestResourceStream(asmname))
                using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
                using (FileStream destination = File.OpenWrite(destFileName))
                    deflateStream.CopyTo(destination);
            }

            File.Delete(Path.Combine(destDir, "Eco.Mods.dll"));

            //SLG is using Directory.GetCurrentDirectory() here, so we use it, too
            File.Copy(Path.Combine(Directory.GetCurrentDirectory(), "Eco.Mods.dll"), Path.Combine(destDir, "Eco.Mods.dll"));

            File.Delete(Path.Combine(destDir, "EcoServer.exe"));
            File.Copy(entryAssembly.Location, Path.Combine(destDir, "EcoServer.exe"));


            Console.WriteLine("Dlls extracted!");
            Thread.Sleep(500);
        }
    }
}
