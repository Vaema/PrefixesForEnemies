using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class PrimordialGreatbow : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 12;
            Item.height = 42;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.useStyle = 5;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.damage = 38;
            Item.rare = 6;
            Item.value = 50000;
            Item.knockBack = 2;
            Item.shoot = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.noMelee = true;
            Item.shootSpeed = 6.8f;
            Item.DamageType = DamageClass.Ranged;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Primordial Greatbow");
      // Tooltip.SetDefault("");
    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int projIndex = Projectile.NewProjectile(position.X - speedX, position.Y - speedY, speedX, speedY, type, -player.whoAmI - 256, knockBack, player.whoAmI);

            float arrowSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            //damage is arrow index
            //knockback is arrow speed
            Projectile.NewProjectile(position.X, position.Y, 0, 0, Mod.Find<ModProjectile>("PrimordialGreatbow").Type, damage, arrowSpeed, player.whoAmI);
            return false;
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
}
