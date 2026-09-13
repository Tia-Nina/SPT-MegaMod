using System.Reflection;
using SPT.Reflection.Patching;
using Comfort.Common;
using EFT;
using EFT.UI;
using EFT.UI.DragAndDrop;
using HarmonyLib;

namespace CWX_MegaMod.TradingPlayerView
{
    public class TradingPlayerItemViewPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(TradingPlayerItemView), nameof(TradingPlayerItemView.ShowTooltip));
        }

        [PatchPrefix]
        public static bool PatchPrefix(ref TradingPlayerItemView __instance)
        {
            if (MegaMod.TradingPlayerView.Value == true)
            {
                // SPT 4.1: ItemFactoryClass was renamed to ItemFactory, and Tooltip.Show
                // lost its optional parameters (now a single text argument)
                string text = Singleton<ItemFactory>.Instance.BriefItemName(__instance.Item, __instance.Item.Name.Localized(null));

                var itemView = (ItemView)__instance;

                var context = (ItemUiContext)AccessTools.Field(itemView.GetType(), "ItemUiContext").GetValue(itemView);
                context.Tooltip.Show(text);

                return false;
            }

            return true;
        }
    }
}