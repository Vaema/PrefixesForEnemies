using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier2
{
    public class VileGreatarrow : ModItem
    {
        public override void SetDefaults()
            {

                Item.damage = 14;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 14;
                Item.height = 48;
                Item.maxStack = 999;

                Item.consumable = true;
                Item.knockBack = 3f;
                Item.value = 400;
                Item.rare = 3;
                Item.shoot = Mod.Find<ModProjectile>("VileGreatarrow").Type;
                Item.shootSpeed = -2f;
                Item.ammo = AmmoID.Arrow;
            }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Vile Greatarrow");
      // Tooltip.SetDefault("Too heavy for normal bows to use effectively");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(Mod.Find<ModItem>("TopazTicket").Type);
            recipe.Register();
        }
    }
}
