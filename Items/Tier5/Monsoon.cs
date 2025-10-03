using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier5
{
    public class Monsoon : ModItem
    {
        public override void SetDefaults()
        {

            Item.width = 32;
            Item.height = 52;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.useStyle = 5;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.damage = 60;
            Item.value = 80000;
            Item.rare = 8;
            Item.knockBack = 2;
            Item.shoot = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.noMelee = true;
            Item.shootSpeed = 7.4f;
            Item.DamageType = DamageClass.Ranged;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Monsoon");
      // Tooltip.SetDefault("");
    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int projIndex = Projectile.NewProjectile(position.X - speedX, position.Y - speedY, speedX, speedY, type, -player.whoAmI - 256, knockBack, player.whoAmI);

            float arrowSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            //damage is arrow index
            //knockback is arrow speed
            Projectile.NewProjectile(position.X, position.Y, 0, 0, Mod.Find<ModProjectile>("Monsoon").Type, damage, arrowSpeed, player.whoAmI);
            return false;
        }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(Mod.Find<ModItem>("ChoiceToken").Type, 1);
        recipe.AddIngredient(Mod.Find<ModItem>("RubyTicket").Type, 3);
        recipe.Register();
    }
}
}
