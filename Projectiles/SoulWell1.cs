using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyMods.Projectiles
{
    public class SoulWell1 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1f;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.alpha = 150;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Well");
        }
        public override void AI()
        {
            if (Projectile.localAI[0]==0f && Projectile.timeLeft < 595)
            {
                Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
                for (int num136 = 0; num136 < 10; num136++)
                {
                    float x2 = Projectile.position.X - Projectile.velocity.X / 10f * num136;
                    float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f * num136;
                    int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 175, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num137].alpha = Projectile.alpha;
                    Main.dust[num137].position.X = x2;
                    Main.dust[num137].position.Y = y2;
                    Main.dust[num137].velocity *= 0f;
                    Main.dust[num137].noGravity = true;
                }
            }
            if (Projectile.localAI[0] == 1f && Projectile.timeLeft%8==0)
            {
                for (int num136 = 0; num136 < 10; num136++)
                {
                    int num137 = Dust.NewDust(Projectile.position, 10, 10, 263);
                    Main.dust[num137].velocity *= 4f;
                    Main.dust[num137].noGravity = true;
                    Main.dust[num137].color = new Color (255,255,255);
                }
                int p = Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, Projectile.ai[0] + Main.rand.Next(-30, 31) / 10f, Projectile.ai[1] + Main.rand.Next(-30, 31) / 10f, 297, Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.localAI[0] = 1f;
                Projectile.timeLeft = 240;
                Projectile.penetrate = 1;
                Projectile.ai[0] = -oldVelocity.X;
                Projectile.ai[1] = -oldVelocity.Y;
                SoundEngine.PlaySound(SoundID.Item46, Projectile.position);
                return false;
            }
            if (Projectile.localAI[0] == 1f)
            {
                Projectile.velocity = Vector2.Zero;
                return false;
            }
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int num136 = 0; num136 < 10; num136++)
            {
                int num137 = Dust.NewDust(Projectile.position, 10, 10, 175);
            }
            SoundEngine.PlaySound(SoundID.NPCDeath6, Projectile.position);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)(texture.Height / Main.projFrames[Projectile.type]) * 0.5f);
            SpriteEffects effect = Projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Vector2 adjustment = new Vector2(Projectile.Center.X, Projectile.Center.Y - 2);
            Main.spriteBatch.Draw(texture, adjustment - Main.screenPosition, new Rectangle?(Utils.Frame(texture, 1, Main.projFrames[Projectile.type], 0, Projectile.frame)), lightColor, Projectile.rotation, origin, Projectile.scale, effect, 0);
            return false;
        }
    }
}
