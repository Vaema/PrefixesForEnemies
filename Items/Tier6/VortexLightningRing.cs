using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyMods.Items.Tier6
{
    public class VortexLightningRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 265;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 100000;
            Item.rare = 10;
            Item.autoReuse = false;
            Item.shoot = 580;
            Item.shootSpeed = 7f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Vortex Lightning Ring");
      // Tooltip.SetDefault("Summon a forking bolt of lightning. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[18] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundLoader.customSoundType, player.position, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Thunder"));
            Vector2 vector82 = -Main.player[Main.myPlayer].Center + Main.MouseWorld;
            float ai = Main.rand.Next(100);
            Vector2 vector83 = Vector2.Normalize(vector82) * Item.shootSpeed;
            Projectile.NewProjectile(player.Center.X, player.Center.Y, vector83.X, vector83.Y, type, damage, .49f, player.whoAmI, vector82.ToRotation(), ai);
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[18]--;
            if (play.cooldowns[18] == -1)
            {
                play.cooldowns[18] = play.maxCooldowns[18];
            }
            return false;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("LightningRing").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("AmberTicket").Type, 3);
            recipe.Register();
        }
    }
}
