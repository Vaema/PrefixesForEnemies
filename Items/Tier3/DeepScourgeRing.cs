using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Items.Tier3
{
    public class DeepScourgeRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 22;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 10;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 30000;
            Item.rare = 4;
            Item.UseSound = SoundID.Item43;//change
            Item.autoReuse = false;
            Item.shoot = ProjectileID.TinyEater;
            Item.shootSpeed = 6f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Deep Scourge Ring");
      // Tooltip.SetDefault("Summon a vast swarm of corrupt creatures. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[5] <= 0)
            {
                return false;
            }
            else
            {
                play.charges[5]--;
                if (play.cooldowns[5] == -1)
                {
                    play.cooldowns[5] = play.maxCooldowns[5];
                }
                return true;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < (Main.rand.Next(2, 4)); i++)
            {
                int p = Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.Next(-30, 30) / 15f, speedY + Main.rand.Next(-30, 30) / 15f, 307, damage, knockBack, Item.playerIndexTheItemIsReservedFor);
            }
            int q = Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.Next(-30, 30) / 15f, speedY + Main.rand.Next(-30, 30) / 15f, 306, (int)(damage * 1.3), knockBack, Item.playerIndexTheItemIsReservedFor);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ScourgeRing").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("SapphireTicket").Type, 3);
            recipe.Register();
        }
    }
}
