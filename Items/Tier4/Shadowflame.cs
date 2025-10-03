using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier4
{
    public class Shadowflame : ModItem
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
                Item.value = 120;
                Item.rare = 6;
                Item.shoot = Mod.Find<ModProjectile>("Shadowflame").Type;
                Item.shootSpeed = 4;
                Item.ammo = AmmoID.Bullet;
            }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Shadowflame Round");
      // Tooltip.SetDefault("");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(200);
            recipe.AddIngredient(Mod.Find<ModItem>("EmeraldTicket").Type);
            recipe.Register();
        }
    }
}
