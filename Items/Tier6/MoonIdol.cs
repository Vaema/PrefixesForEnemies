using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier6
{
    public class MoonIdol : ModItem
    {
        int cooldown = 0;
        public override void SetDefaults()
        {

            Item.accessory = true;
            Item.width = 10;
            Item.height = 10;
            Item.rare = 10;

            Item.value = 100000;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Idol of the Moon God");
      // Tooltip.SetDefault("Critical hits have a 20% chance to deal an additional 2x damage");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ChoiceToken").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("AmberTicket").Type, 3);
            recipe.Register();
        }
    }
}
