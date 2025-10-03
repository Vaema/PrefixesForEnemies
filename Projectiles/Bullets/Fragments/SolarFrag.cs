using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace EnemyMods.Projectiles.Bullets.Fragments
{
    public class SolarFrag : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.extraUpdates = 1;
            Projectile.friendly = true;
            Projectile.scale = .6f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Solar Fragment");
        }
        public override void AI()
        {
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 1f, .6f, 0f);
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.timeLeft % 4 == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 259, 0, 0, 100, default(Color), 0.6f);
            }
            if (Projectile.alpha < 170)
            {
                for (int num136 = 0; num136 < 5; num136++)
                {
                    float x2 = Projectile.Center.X - Projectile.velocity.X / 10f * (float)num136*2;
                    float y2 = Projectile.Center.Y - Projectile.velocity.Y / 10f * (float)num136*2;
                    int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 259, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num137].alpha = Projectile.alpha;
                    Main.dust[num137].position.X = x2;
                    Main.dust[num137].position.Y = y2;
                    Main.dust[num137].velocity *= 0f;
                    Main.dust[num137].noGravity = true;
                }
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 15;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 259, 0, 0, 100, default(Color), 0.6f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int p = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0f, 0f, 612, Projectile.damage, 1f, Projectile.owner, 0f, 0.4f + Main.rand.NextFloat() * 1.15f);
        }
    }
}
