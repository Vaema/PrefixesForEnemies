using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyMods.Projectiles
{
    public class LightSpear : ModProjectile
    {
        
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 24;
            Projectile.timeLeft = 480;
            Projectile.maxPenetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1f;
            Projectile.aiStyle = 0;
            Projectile.extraUpdates = 6;
            Projectile.light = 1f;
            Projectile.alpha = 100;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("LightSpear");
        }
        public override void AI()
        {
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if(Projectile.timeLeft < 360)
            {
                Projectile.velocity.X *= 1.0255f;
                Projectile.velocity.Y *= 1.0255f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, (Projectile.velocity.X + Main.rand.Next(-2, 2)) * .2f, (Projectile.velocity.Y + Main.rand.Next(-2, -2)) * .2f, 100, Color.White, 3f);
            SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
