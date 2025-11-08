using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace Z_Shaw
{
    public class Z_Shaw : IModApi
    {
        public void InitMod(Mod _modInstance)
        {
            Log.Out("LOADING MOD Z_SHAW TEST *****************");

            var harmony = new HarmonyLib.Harmony(GetType().ToString());
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}