using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace EnemyMods.Items.Tier6
{
    public class NebulaParasiteRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 1;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 100000;
            Item.rare = 10;
            Item.UseSound = SoundID.Item43;//change
            Item.autoReuse = false;
            Item.shootSpeed = 8f;
            Item.shoot = Mod.Find<ModProjectile>("NebulaParasite").Type;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Nebula Parasite");
      // Tooltip.SetDefault("Fires a parasite that damages enemies from within. One charge.");
    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, Item.damage, Item.knockBack, Item.playerIndexTheItemIsReservedFor);
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[16]--;
            if (play.cooldowns[16] == -1)
            {
                play.cooldowns[16] = play.maxCooldowns[16];
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[16] <= 0)
            {
                return false;
            }
            else return true;
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
