using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier3
{
    public class HollowPoint : ModItem
    {
        public override void SetDefaults()
            {

                Item.damage = 13;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 8;
                Item.height = 8;
                Item.maxStack = 999;

                Item.consumable = true;
                Item.knockBack = 4f;
                Item.value = 90;
                Item.rare = 4;
                Item.shoot = Mod.Find<ModProjectile>("HollowPoint").Type;
                Item.shootSpeed = 4;
                Item.ammo = AmmoID.Bullet;
            }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Hollow Point Round");
      // Tooltip.SetDefault("More effective against targets with low defense");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(200);
            recipe.AddIngredient(Mod.Find<ModItem>("SapphireTicket").Type);
            recipe.Register();
        }
    }
}
