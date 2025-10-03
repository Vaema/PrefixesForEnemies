using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier5
{
    public class GunbladeMkIV : ModItem
    {
        int shotBonus = 0;
        public override void SetDefaults()
        {

            Item.damage = 85;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 50;
            Item.height = 50;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 1;
            Item.knockBack = 4;
            Item.value = 80000;
            Item.rare = 8;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.scale = 1.2f;
            Item.shoot = 10;
            Item.shootSpeed = 10f;
            Item.staff[Item.type] = true;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Gunblade MkIV");
      // Tooltip.SetDefault("Right-click to shoot.");
    }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                damage += -8 + shotBonus * Item.damage / 4;
                player.AddBuff(Mod.Find<ModBuff>("GunbladeDefense").Type, shotBonus * 40);
                shotBonus = 0;
                return true;
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
                float backX = 18f;
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
                Item.useStyle = 5;
                Item.UseSound = SoundID.Item11;
                //add defense buff based on shotBonus consumed
                Item.melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
                Item.DamageType = DamageClass.Ranged;
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
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            shotBonus++;
            if (crit)
            {
                shotBonus++;
            }
            if (shotBonus > 20)
            {
                shotBonus = 20;
                SoundEngine.PlaySound(SoundID.MenuTick, player.position);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("GunbladeMkIII").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("RubyTicket").Type, 3);
            recipe.Register();
        }
    }
}
