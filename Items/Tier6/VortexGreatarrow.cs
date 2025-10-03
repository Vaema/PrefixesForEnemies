using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier6
{
    public class VortexGreatarrow : ModItem
    {
        public override void SetDefaults()
            {

                Item.damage = 28;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 14;
                Item.height = 48;
                Item.maxStack = 999;

                Item.consumable = true;
                Item.knockBack = 3f;
                Item.value = 2000;
                Item.rare = 10;
                Item.shoot = Mod.Find<ModProjectile>("VortexGreatarrow").Type;
                Item.shootSpeed = -2f;
                Item.ammo = AmmoID.Arrow;
            }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Vortex Greatarrow");
      // Tooltip.SetDefault("Too heavy for normal bows to use effectively");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(Mod.Find<ModItem>("AmberTicket").Type);
            recipe.Register();
        }
    }
}
