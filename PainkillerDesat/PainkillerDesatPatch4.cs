using System;
using System.Linq;
using System.Reflection;
using SPT.Reflection.Patching;
using HarmonyLib;

namespace CWX_MegaMod.PainkillerDesat
{
    // SPT 4.1: EffectsController.Class635 (CC_DoubleVision accumulator) is now the named
    // nested class CC_DoubleVisionAccumulator, field cc_DoubleVision_0
    public class PainkillerDesatScript4 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(EffectsController.CC_DoubleVisionAccumulator), nameof(EffectsController.CC_DoubleVisionAccumulator.Toggle));
        }

        [PatchPrefix] // removes the double vision effect from some painkillers
        public static bool PatchPrefix(ref CC_DoubleVision ___cc_DoubleVision_0)
        {
            if (!MegaMod.PainkillerDesat.Value)
            {
                return true;
            }

            if (___cc_DoubleVision_0 != null)
            {
                ___cc_DoubleVision_0.enabled = false;
            }

            return false; // dont do method
        }
    }
}
