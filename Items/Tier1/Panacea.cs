using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier1
{
    public class Panacea : ModItem
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
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Panacea");
      // Tooltip.SetDefault("Cures most all debuffs");
    }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            for (int i = 0; i < player.buffType.Length; i++)
            {
                if (Main.debuff[player.buffType[i]])
                {
                    player.DelBuff(i);
                }
            }
            return true;
        }
    }
}
