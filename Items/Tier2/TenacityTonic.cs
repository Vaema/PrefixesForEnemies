using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier2
{
    public class TenacityTonic : ModItem
    {
        public override void SetDefaults()
        {


            Item.maxStack = 30;
            Item.value = 2000;
            Item.rare = 3;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = 2;
            Item.consumable = true;
            Item.useTurn = true;
            Item.width = 14;
            Item.height = 24;
            Item.UseSound = SoundID.Item3;
            Item.buffType = Mod.Find<ModBuff>("Tenacity").Type;
            Item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Tenacity Tonic");
      // Tooltip.SetDefault("Taking damage has a lesser effect on natural life regen");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("TopazTicket").Type);
            recipe.Register();
        }
    }
}
