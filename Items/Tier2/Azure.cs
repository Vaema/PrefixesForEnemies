using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier2
{
    public class Azure : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;

            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 70;
            Item.rare = 3;
            Item.shoot = Mod.Find<ModProjectile>("Azure").Type;
            Item.shootSpeed = 5;
            Item.ammo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Azure Round");
      // Tooltip.SetDefault("Explodes if it hits a tile");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(200);
            recipe.AddIngredient(Mod.Find<ModItem>("TopazTicket").Type);
            recipe.Register();
        }
    }
}
