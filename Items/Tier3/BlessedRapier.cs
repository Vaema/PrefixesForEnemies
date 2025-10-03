using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier3
{
    public class BlessedRapier : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 39;
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
            Item.value = 30000;
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>("BlessedRapier").Type;
            Item.scale = 1.1f;
            Item.shootSpeed = 5f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Blessed Rapier");
      // Tooltip.SetDefault("Right-click to counter.\nCounterattacks deal 2.5x damage");
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
                player.AddBuff(Mod.Find<ModBuff>("CounterStanceRapier").Type, Item.useAnimation + bonus);
                player.AddBuff(Mod.Find<ModBuff>("CounterCooldown").Type, 360);
                return false;
            }
            if (player.FindBuffIndex(Mod.Find<ModBuff>("Counter").Type) >= 0)
            {
                damage = (int)(damage * 2.5);
                MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
                if (modPlayer.counterPlus)
                {
                    damage = (int)(damage * 1.2);
                }
                player.DelBuff(player.FindBuffIndex(Mod.Find<ModBuff>("Counter").Type));
                for(int i=0; i<20; i++)
                {
                    int d = Dust.NewDust(position, Item.width, Item.height, 73, speedX, speedY);
                }
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
            recipe.AddIngredient(Mod.Find<ModItem>("SapphireTicket").Type, 3);
            recipe.Register();
        }
    }
}
