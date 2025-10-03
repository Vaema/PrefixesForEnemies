using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier1
{
    public class Splitter : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;

            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 50;
            Item.rare = 2;
            Item.shoot = Mod.Find<ModProjectile>("Splitter").Type;
            Item.shootSpeed = 4;
            Item.ammo = AmmoID.Bullet;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Splitter Round");
      // Tooltip.SetDefault("Splits into 2 bullets");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(200);
            recipe.AddIngredient(Mod.Find<ModItem>("AmethystTicket").Type);
            recipe.Register();
        }
    }
}
