using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;


namespace EnemyMods.Items.Tier1
{
    public class ScourgeRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 9;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 6;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item43;//change
            Item.autoReuse = false;
            Item.shoot = ProjectileID.TinyEater;
            Item.shootSpeed = 6f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Scourge Ring");
      // Tooltip.SetDefault("Summon a swarm of corrupt creatures. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[1] <= 0)
            {
                return false;
            }
            else
            {
                play.charges[1]--;
                if (play.cooldowns[1] == -1)
                {
                    play.cooldowns[1] = play.maxCooldowns[0];
                }
                return true;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int p = Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.Next(-30, 31)/15f, speedY + Main.rand.Next(-30, 31) / 15f, type, damage, knockBack, Item.playerIndexTheItemIsReservedFor);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ChoiceToken").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("AmethystTicket").Type, 3);
            recipe.Register();
        }
    }
}
