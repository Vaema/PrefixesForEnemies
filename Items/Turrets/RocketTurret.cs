using Terraria.ModLoader;

namespace EnemyMods.Items.Turrets
{
    public class RocketTurret : ModItem
    {
        public override void SetDefaults()
            {

                Item.damage = 10;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 12;
                Item.height = 12;
                Item.maxStack = 999;
                Item.consumable = true;
                Item.useStyle = 1;
                Item.noUseGraphic = true;
                Item.knockBack = 3f;
                Item.value = 400;
                Item.rare = 3;
                Item.shoot = Mod.Find<ModProjectile>("RocketTurretCapsule").Type;
                Item.shootSpeed = 7f;
                Item.noMelee = true;
            }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Rocket Turret");
      // Tooltip.SetDefault("");
    }

        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }*/
    }
