using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier6
{
    public class PillarFragment : ModItem
    {
        public override void SetDefaults()
        {

                Item.damage = 17;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 8;
                Item.height = 8;
                Item.maxStack = 999;

                Item.consumable = true;
                Item.knockBack = 3f;
                Item.value = 200;
                Item.rare = 10;
                Item.shoot = Mod.Find<ModProjectile>("PillarFragment").Type;
                Item.shootSpeed = 4;
                Item.ammo = AmmoID.Bullet;
            }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Pillar Fragment Round");
      // Tooltip.SetDefault("Splits into 2-3 pillar fragments midair");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(200);
            recipe.AddIngredient(Mod.Find<ModItem>("AmberTicket").Type);
            recipe.Register();
        }
    }
}
