using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using EnemyMods.NPCs;

namespace EnemyMods.Items.Tier6
{
    public class GunbladeBurstMkIII : ModItem
    {
        int shotBonus = 0;
        int DoT = 0;
        int shots = 0;
        public override void SetDefaults()
        {

            Item.damage = 158;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 52;
            Item.height = 61;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = 1;
            Item.knockBack = 4;
            Item.value = 100000;
            Item.rare = 10;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = 10;
            Item.scale = 1.2f;
            Item.shootSpeed = 10f;
            Item.staff[Item.type] = true;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Gunblade: Burst MkIII");
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
                SoundEngine.PlaySound(SoundID.Item11, player.position);
                damage += -60 + shotBonus * Item.damage / 8;
                shots++;
                if (DoT < 15)
                    DoT++;
                if (shots >= 3)
                {
                    shotBonus = 0;
                    shots = 0;
                }
                return true;
            }
            return false;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (player.altFunctionUse == 2 && shots == 0)
            {
                return true;
            }
            return false;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.altFunctionUse == 2 || Item.useStyle == 5)
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
                Item.useTime = 8;
                Item.reuseDelay = 30;
                Item.useStyle = 5;
                Item.UseSound = SoundID.Item31;
                Item.noMelee = true;
            }
            else
            {
                Item.useTime = 24;
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
            if (shotBonus > 30)
            {
                shotBonus = 30;
                SoundEngine.PlaySound(SoundID.MenuTick, player.position);
            }
            if (DoT > 0)
            {
                gNPC info = target.GetGlobalNPC<gNPC>();
                info.gunbladeBurn += DoT * 100;
                DoT = 0;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("GunbladeBurstMkII").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("AmberTicket").Type, 3);
            recipe.Register();
        }
    }
}
