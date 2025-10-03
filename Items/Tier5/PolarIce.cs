using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier5
{
    public class PolarIce : ModItem
    {
        public override void SetDefaults()
            {

                Item.damage = 16;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 8;
                Item.height = 8;
                Item.maxStack = 999;

                Item.consumable = true;
                Item.knockBack = 3f;
                Item.value = 150;
                Item.rare = 8;
                Item.shoot = Mod.Find<ModProjectile>("PolarIce").Type;
                Item.shootSpeed = 4;
                Item.ammo = AmmoID.Bullet;
            }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Polar Ice Cap");
      // Tooltip.SetDefault("Breaks into ice shards on impact");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(200);
            recipe.AddIngredient(Mod.Find<ModItem>("RubyTicket").Type);
            recipe.Register();
        }
    }
}
