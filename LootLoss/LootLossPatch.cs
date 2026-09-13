using System.Collections.Generic;
using System.Reflection;
using EFT;
using HarmonyLib;
using JsonType;
using SPT.Reflection.Patching;

namespace CWX_MegaMod.LootLoss
{
    public class LootLossPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            // SPT 4.1: LocalGame.smethod_6 was refactored into the public LocalGame.Create,
            // with LocalRaidSettings raidSettings as its last parameter
            return AccessTools.Method(typeof(LocalGame), nameof(LocalGame.Create));
        }

        [PatchPrefix]
        public static void PatchPrefix(ref LocalRaidSettings raidSettings)
        {
            if (!MegaMod.LootLoss.Value)
            {
                return;
            }

            // SPT 4.1: Location.containers is Dictionary<string, LocationSettings.Location.LootContainer>
            // (was GClass1421) and Location.Loot is LootData (was GClass1404)
            raidSettings.selectedLocation.containers = new Dictionary<string, LocationSettings.Location.LootContainer>();
            raidSettings.selectedLocation.Loot = new LootData();
        }
    }
}
