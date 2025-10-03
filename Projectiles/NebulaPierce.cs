using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using System;
using EnemyMods.NPCs;

namespace EnemyMods.Projectiles
{
    public class NebulaPierce : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 26;
            Projectile.timeLeft = 300;
            Projectile.penetrate = -1;
            Projectile.maxPenetrate = -1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("NebulaPiercer");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.timeLeft -= 30;
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 1f, .1f, 0.6f);
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.alpha < 170)
            {
                for (int num136 = 0; num136 < 10; num136++)
                {
                    float x2 = Projectile.Center.X - Projectile.velocity.X / 10f * (float)num136;
                    float y2 = Projectile.Center.Y - Projectile.velocity.Y / 10f * (float)num136;
                    int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 242, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num137].alpha = Projectile.alpha;
                    Main.dust[num137].position.X = x2;
                    Main.dust[num137].position.Y = y2;
                    Main.dust[num137].velocity *= 0f;
                    Main.dust[num137].noGravity = true;
                }
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 25;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }
        public override void OnKill(int timeLeft)
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PinkCrystalShard, Projectile.velocity.X, Projectile.velocity.Y);
        }/*
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)(texture.Height / Main.projFrames[projectile.type]) * 0.5f);
            SpriteEffects effect = projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Vector2 adjustment = new Vector2(projectile.Center.X - 4, projectile.Center.Y - 4);
            Main.spriteBatch.Draw(texture, adjustment - Main.screenPosition, new Rectangle?(Utils.Frame(texture, 1, Main.projFrames[projectile.type], 0, projectile.frame)), lightColor, projectile.rotation, origin, projectile.scale, effect, 0);
            return false;
        }*/
    }
}
