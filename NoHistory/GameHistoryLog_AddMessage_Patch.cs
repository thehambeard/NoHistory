using Kingmaker;
using System;
using System.IO;

namespace NoHistory
{
    internal static class DeleteHistory
    {
        private static Type _type = HarmonyLib.AccessTools.TypeByName("Kingmaker.EntitySystem.Persistence.AreaDataStash");
        private static string _path = (string)HarmonyLib.AccessTools.Property(_type, "Folder").GetValue(null);
        public static void Delete()
        {
            Main.Logger.Log("DELETE");
            string history = Path.Combine(_path, "history");
            if (new FileInfo(history).Length != 0)
                File.Create(history).Dispose();
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameStatistic), "PreSave")]
    internal static class Game_LoadNewGame_Patch
    {
        private static void Postfix()
        {
            DeleteHistory.Delete();
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameHistoryLog), "AddMessage")]
    internal static class GameHistoryLog_AddMessage_Patch
    {
        private static bool Prefix(GameHistoryLog __instance)
        {
            return !Main.enabled;
        }
    }
}
