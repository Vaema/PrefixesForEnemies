using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Items.Tier4
{
    public class LightningRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 90;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = 3;
            Item.autoReuse = false;
            Item.shoot = 580;
            Item.shootSpeed = 7f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Lightning Ring");
      // Tooltip.SetDefault("Summon a bolt of lightning. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[7] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundLoader.customSoundType, player.position, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Thunder"));
            Vector2 vector82 =  -Main.player[Main.myPlayer].Center + Main.MouseWorld;
            float ai = Main.rand.Next(100);
            Vector2 vector83 = Vector2.Normalize(vector82) * Item.shootSpeed;
            Projectile.NewProjectile(player.Center.X, player.Center.Y, vector83.X, vector83.Y, type, damage, .5f, player.whoAmI, vector82.ToRotation(), ai);
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[7]--;
            if (play.cooldowns[7] == -1)
            {
                play.cooldowns[7] = play.maxCooldowns[7];
            }
            return false;
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
