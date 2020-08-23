using System;
using System.Reflection;
using UnityModManagerNet;

namespace NoHistory
{
#if (DEBUG)
	[EnableReloading]
#endif
    internal static class Main
    {

        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            modEntry.OnToggle = new Func<UnityModManager.ModEntry, bool, bool>(Main.OnToggle);
            Main.Logger = modEntry.Logger;
            new HarmonyLib.Harmony(modEntry.Info.Id).PatchAll(Assembly.GetExecutingAssembly());
            return true;
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Main.enabled = value;
            return true;
        }

        public static bool enabled;

        public static UnityModManager.ModEntry.ModLogger Logger;
    }
}
