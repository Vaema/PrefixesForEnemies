using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier5
{
    public class BattleDancerDraught : ModItem
    {
        public override void SetDefaults()
        {



            Item.maxStack = 30;
            Item.value = 8000;
            Item.rare = 8;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = 2;
            Item.consumable = true;
            Item.useTurn = true;
            Item.width = 14;
            Item.height = 24;
            Item.UseSound = SoundID.Item3;
            Item.buffType = Mod.Find<ModBuff>("BattleDancerDraught").Type;
            Item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Battle Dancer Draught");
      // Tooltip.SetDefault("Gain up to 20% damage by hitting enemies, but reset to 0% when hit\nGain Hunter effect when fully stacked");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("RubyTicket").Type);
            recipe.Register();
        }
    }
}
