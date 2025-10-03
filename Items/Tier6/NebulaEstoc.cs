using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier6
{
    public class NebulaEstoc : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 175;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 42;
            Item.height = 42;
            Item.noUseGraphic = true;
            Item.noMelee = true;


            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 5;
            Item.useTurn = true;
            Item.knockBack = 2;
            Item.value = 100000;
            Item.rare = 10;
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>("NebulaEstoc").Type;
            Item.scale = 1.1f;
            Item.shootSpeed = 5f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Nebula Estoc");
      // Tooltip.SetDefault("Right-click to counter.\nCounterattacks fire a Nebula Piercer");
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
                int p = Projectile.NewProjectile(position.X, position.Y, speedX * 2.5f, speedY * 2.5f, Mod.Find<ModProjectile>("NebulaPierce").Type, damage * 4, knockBack * 2, player.whoAmI);
                Main.projectile[p].friendly = true;
                Main.projectile[p].hostile = false;
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
            recipe.AddIngredient(Mod.Find<ModItem>("AmberTicket").Type, 3);
            recipe.Register();
        }
    }
}
