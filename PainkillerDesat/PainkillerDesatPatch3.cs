using System;
using System.Linq;
using System.Reflection;
using SPT.Reflection.Patching;
using HarmonyLib;

namespace CWX_MegaMod.PainkillerDesat
{
    // SPT 4.1: EffectsController.Class645 (CC_Wiggle accumulator) is now the named
    // nested class CC_WiggleAccumulator, field cc_Wiggle_0
    public class PainkillerDesatScript3 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(EffectsController.CC_WiggleAccumulator), nameof(EffectsController.CC_WiggleAccumulator.Toggle));
        }

        [PatchPrefix] // removes the wiggle effect from some painkillers
        public static bool PatchPrefix(ref CC_Wiggle ___cc_Wiggle_0)
        {
            if (!MegaMod.PainkillerDesat.Value)
            {
                return true;
            }

            if (___cc_Wiggle_0 != null)
            {
                ___cc_Wiggle_0.enabled = false;
            }

            return false; // dont do method
        }
    }
}
