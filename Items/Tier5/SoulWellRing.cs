using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace EnemyMods.Items.Tier5
{
    public class SoulWellRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 50;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 80000;
            Item.rare = 8;
            Item.UseSound = SoundID.Item43;//change
            Item.autoReuse = false;
            Item.shootSpeed = 10f;
            Item.shoot = Mod.Find<ModProjectile>("SoulWell1").Type;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Soul Geyser");
      // Tooltip.SetDefault("Pierce the veil. One charge.");
    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[13]--;
            if (play.cooldowns[13] == -1)
            {
                play.cooldowns[13] = play.maxCooldowns[13];
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[13] <= 0)
            {
                return false;
            }
            else return true;
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
