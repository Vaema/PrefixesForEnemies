using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier3
{
    public class CrystalGreatarrow : ModItem
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
                Item.value = 600;
                Item.rare = 4;
                Item.shoot = Mod.Find<ModProjectile>("CrystalGreatarrow").Type;
                Item.shootSpeed = -2f;
                Item.ammo = AmmoID.Arrow;
            }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Crystal Greatarrow");
      // Tooltip.SetDefault("Too heavy for normal bows to use effectively");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(Mod.Find<ModItem>("SapphireTicket").Type);
            recipe.Register();
        }
    }
}
