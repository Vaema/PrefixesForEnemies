using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier1
{
    public class MysteriousPhilter : ModItem
    {
        public override void SetDefaults()
        {


            Item.maxStack = 30;
            Item.value = 1000;
            Item.rare = 2;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = 2;
            Item.consumable = true;
            Item.useTurn = true;
            Item.width = 14;
            Item.height = 24;
            Item.UseSound = SoundID.Item3;
            Item.buffType = Mod.Find<ModBuff>("MysteriousPhilter").Type;
            Item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Mysterious Philter");
      // Tooltip.SetDefault("Grants a random buff each minute");
    }

    }
}
