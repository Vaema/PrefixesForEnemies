using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier6
{
    public class GunbladeSpreadMkIV : ModItem
    {
        int shotBonus = 0;
        int reload = 0;
        public override void SetDefaults()
        {

            Item.damage = 178;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 54;
            Item.height = 56;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 1;
            Item.knockBack = 4;
            Item.value = 100000;
            Item.rare = 10;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = 10;
            Item.shootSpeed = 10f;
            Item.scale = 1.2f;
            Item.staff[Item.type] = true;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Gunblade: Spread MkIV");
      // Tooltip.SetDefault("Right-click to shoot.");
    }

        public override bool AltFunctionUse(Player player)
        {
            if (reload == 0)
                return true;
            else return false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < 5 + shotBonus / 3; i++)
                {
                    float vX = speedX + (float)Main.rand.Next(-10, 10 + 1) * 0.3f;
                    float vY = speedY + (float)Main.rand.Next(-10, 10 + 1) * 0.3f;

                    Projectile.NewProjectile(position.X, position.Y, vX, vY, type, (int)(damage * .8 + (25 * shotBonus * (player.GetDamage(DamageClass.Melee) - 1))), knockBack, Main.myPlayer);
                }
                shotBonus = 0;
            }
            return false;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (player.altFunctionUse == 2)
            {
                return true;
            }
            return false;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.altFunctionUse == 2)
            {
                float backX = 12f;
                float downY = 0f;
                float cosRot = (float)Math.Cos(player.itemRotation);
                float sinRot = (float)Math.Sin(player.itemRotation);
                player.itemLocation.X = player.itemLocation.X - (backX * cosRot * player.direction) - (downY * sinRot * player.gravDir);
                player.itemLocation.Y = player.itemLocation.Y - (backX * sinRot * player.direction) + (downY * cosRot * player.gravDir);
            }
        }
        public override bool CanUseItem(Player player)
        {
            if (Item.useAmmo == 0)
            {
                Item.useAmmo = AmmoID.Bullet;
                Item.shoot = 10;
            }
            if (!player.HasAmmo(Item, true))
            {
                Item.useAmmo = 0;
                Item.shoot = 0;
            }
            if (player.altFunctionUse == 2 && Item.useAmmo == AmmoID.Bullet)
            {
                Item.melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = 5;
                Item.UseSound = SoundID.Item36;
                reload = 100;
                Item.noMelee = true;
            }
            else
            {
                Item.useStyle = 1;
                Item.UseSound = SoundID.Item1;
                Item.ranged = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
                Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
                Item.noMelee = false;
            }
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            if (reload > 0)
                reload--;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            shotBonus++;
            if (crit)
            {
                shotBonus++;
            }
            if (shotBonus > 27)
            {
                shotBonus = 27;
                SoundEngine.PlaySound(SoundID.MenuTick, player.position);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("GunbladeSpreadMkIII").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("AmberTicket").Type, 3);
            recipe.Register();
        }
    }
}
