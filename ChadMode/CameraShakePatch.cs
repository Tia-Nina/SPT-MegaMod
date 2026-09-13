using System.Reflection;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace CWX_MegaMod.ChadMode
{
    public class CameraShakePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            // SPT 4.1: EffectsController.method_7 was renamed to OnPlayerDamaged
            return AccessTools.Method(typeof(EffectsController), nameof(EffectsController.OnPlayerDamaged));
        }

        [PatchPrefix]
        public static bool PatchPrefix()
        {
            if (MegaMod.CameraShake.Value)
            {
                // Skip method
                return false;
            }

            return true;
        }
    }
}