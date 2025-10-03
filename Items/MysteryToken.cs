using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items
{
    class MysteryToken : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 20;
            Item.height = 20;

            Item.value = 10000;
            Item.rare = 3;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.UseSound = SoundID.Item4;
            Item.useStyle = 3;
            Item.useAnimation = 30;
            Item.useTime = 30;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Mystery Token");
      // Tooltip.SetDefault("What will you get?");
    }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            int lootLevel = 0;
            if (NPC.downedBoss1) lootLevel++;
            if (NPC.downedBoss2) lootLevel++;
            if (NPC.downedBoss3) lootLevel++;
            if (NPC.downedQueenBee) lootLevel++;
            if (NPC.downedSlimeKing) lootLevel++;
            if (NPC.downedMechBoss1) lootLevel++;
            if (NPC.downedMechBoss2) lootLevel++;
            if (NPC.downedMechBoss3) lootLevel++;
            if (NPC.downedPlantBoss) lootLevel++;
            if (NPC.downedGolemBoss) lootLevel++;
            if (NPC.downedFishron) lootLevel++;
            if (NPC.downedAncientCultist) lootLevel++;
            if (Main.hardMode) lootLevel++;
            if(lootLevel == 0)
            {

            }
            if(lootLevel == 1)
            {

            }
            if (lootLevel == 2)
            {

            }
            if (lootLevel == 3)
            {

            }
            if (lootLevel == 4)
            {

            }
            if (lootLevel == 5)
            {

            }
            if (lootLevel == 6)
            {

            }
            if (lootLevel == 7)
            {

            }
            if (lootLevel == 8)
            {

            }
            if (lootLevel == 9)
            {

            }
            if (lootLevel == 10)
            {

            }
            if (lootLevel == 11)
            {

            }
            if (lootLevel == 12)
            {

            }
            if (lootLevel == 13)
            {

            }
            return true;
        }
    }
}
