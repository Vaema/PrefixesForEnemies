using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier4
{
    public class ShadowflameEstoc : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 45;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 54;
            Item.height = 54;
            Item.noUseGraphic = true;
            Item.noMelee = true;


            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 5;
            Item.useTurn = true;
            Item.knockBack = 2;
            Item.value = 50000;
            Item.rare = 6;
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>("ShadowflameEstoc").Type;
            Item.scale = 1.1f;
            Item.shootSpeed = 5f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Shadowflame Estoc");
      // Tooltip.SetDefault("Right-click to counter.\nCounterattacks project shadowflames");
    }

        public override bool AltFunctionUse(Player player)
        {
            if (player.FindBuffIndex(Mod.Find<ModBuff>("CounterCooldown").Type) == -1)
            {
                return true;
            }
            return false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
                int bonus = modPlayer.increasedCounterLength ? 15 : 5;
                player.AddBuff(Mod.Find<ModBuff>("CounterStanceEstoc").Type, Item.useAnimation + bonus);
                player.AddBuff(Mod.Find<ModBuff>("CounterCooldown").Type, 360);
                return false;
            }
            if (player.FindBuffIndex(Mod.Find<ModBuff>("Counter").Type) >= 0)
            {
                int p = Projectile.NewProjectile(position.X, position.Y, speedX * 2.5f, speedY * 2.5f, 496, damage * 3, knockBack * 2, player.whoAmI);
                MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
                if (modPlayer.counterPlus)
                {
                    Main.projectile[p].damage = (int)(Main.projectile[p].damage * 1.2);
                }
                player.DelBuff(player.FindBuffIndex(Mod.Find<ModBuff>("Counter").Type));
            }
            return true;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.altFunctionUse == 2)
            {
                Item.noUseGraphic = false;
            }
            else
            {
                Item.noUseGraphic = true;
            }
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
