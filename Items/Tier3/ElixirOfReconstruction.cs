using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier3
{
    public class ElixirOfReconstruction : ModItem
    {
        public override void SetDefaults()
        {


            Item.maxStack = 30;
            Item.value = 4000;
            Item.rare = 4;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = 2;
            Item.consumable = true;
            Item.useTurn = true;
            Item.width = 14;
            Item.height = 24;
            Item.UseSound = SoundID.Item3;
            Item.buffType = Mod.Find<ModBuff>("ElixirOfReconstruction").Type;
            Item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Elixir of Reconstruction");
      // Tooltip.SetDefault("Heal 25% of the last damage you took over 10 seconds");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("SapphireTicket").Type);
            recipe.Register();
        }
    }
}
