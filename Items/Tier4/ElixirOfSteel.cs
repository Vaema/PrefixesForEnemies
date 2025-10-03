using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier4
{
    public class ElixirOfSteel : ModItem
    {
        public override void SetDefaults()
        {


            Item.maxStack = 30;
            Item.value = 6000;
            Item.rare = 6;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = 2;
            Item.consumable = true;
            Item.useTurn = true;
            Item.width = 14;
            Item.height = 24;
            Item.UseSound = SoundID.Item3;
            Item.buffType = Mod.Find<ModBuff>("ElixirOfSteel").Type;
            Item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Elixir of Steel");
      // Tooltip.SetDefault("Gain 4 defense for 15 seconds when hit, stacking up to 20 defense");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("EmeraldTicket").Type);
            recipe.Register();
        }
    }
}
