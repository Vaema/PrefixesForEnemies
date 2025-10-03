using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Items.Tier4
{
    public class ChargeBangle : ModItem
    {
        public override void SetDefaults()
        {

            Item.accessory = true;
            Item.rare = 6;
            Item.width = 10;
            Item.height = 10;

            Item.value = 50000;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Charge Bangle");
      // Tooltip.SetDefault("Adds an extra charge to rings with 2 or more base charges");
    }

        public override void UpdateEquip(Player player)
        {

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ChoiceToken").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("EmeraldTicket").Type, 3);
            recipe.Register();
        }
    }
}
