using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Essences
{
    public class VengefulEssence : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 10;
            Item.height = 10;


            Item.value = 10000;
            Item.rare = 3;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Vengeful Essence");
      // Tooltip.SetDefault("Simply holding this grants you power\nDeal 20% more damage when below 20% life");
    }

        public override void UpdateInventory(Player player)
        {
            MPlayer mplayer = (MPlayer)(player.GetModPlayer(Mod, "MPlayer"));
            mplayer.vengeful = true;
        }
    }
}
