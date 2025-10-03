using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier4
{
    public class LightcrystalGreatbow : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 28;
            Item.height = 48;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.useStyle = 5;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.damage = 46;
            Item.rare = 6;
            Item.value = 50000;
            Item.knockBack = 2;
            Item.shoot = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.noMelee = true;
            Item.shootSpeed = 7.4f;
            Item.DamageType = DamageClass.Ranged;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Lightcrystal Greatbow");
      // Tooltip.SetDefault("");
    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int projIndex = Projectile.NewProjectile(position.X - speedX, position.Y - speedY, speedX, speedY, type, -player.whoAmI - 256, knockBack, player.whoAmI);

            float arrowSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            //damage is arrow index
            //knockback is arrow speed
            Projectile.NewProjectile(position.X, position.Y, 0, 0, Mod.Find<ModProjectile>("LightcrystalGreatbow").Type, damage, arrowSpeed, player.whoAmI);
            return false;
        }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(Mod.Find<ModItem>("ChoiceToken").Type, 1);
        recipe.AddIngredient(Mod.Find<ModItem>("EmeraldTicket").Type, 3);
        recipe.Register();
    }
}
}
