using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier5
{
    public class SanguineGreatarrow : ModItem
    {
        public override void SetDefaults()
            {

                Item.damage = 13;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 14;
                Item.height = 48;
                Item.maxStack = 999;

                Item.consumable = true;
                Item.knockBack = 3f;
                Item.value = 1400;
                Item.rare = 8;
                Item.shoot = Mod.Find<ModProjectile>("SanguineGreatarrow").Type;
                Item.shootSpeed = -2f;
                Item.ammo = AmmoID.Arrow;
            }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Sanguine Greatarrow");
      // Tooltip.SetDefault("Too heavy for normal bows to use effectively");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(Mod.Find<ModItem>("RubyTicket").Type);
            recipe.Register();
        }
    }
}
