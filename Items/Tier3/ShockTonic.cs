using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier3
{
    public class ShockTonic : ModItem
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
            Item.buffType = Mod.Find<ModBuff>("ShockTonic").Type;
            Item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Shock Tonic");
      // Tooltip.SetDefault("Build up static charge to damage enemies on contact");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("SapphireTicket").Type);
            recipe.Register();
        }
    }
}
