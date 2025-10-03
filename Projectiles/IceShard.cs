using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class IceShard : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 10;
            Projectile.timeLeft = 480;
            Projectile.maxPenetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1f;
            Projectile.aiStyle = 0;
            Projectile.extraUpdates = 6;

        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("IceShard");
        }
        public override void AI()
        {
            if (Main.rand.Next(0, 150) < Math.Sqrt(Projectile.velocity.X*Projectile.velocity.X + Projectile.velocity.Y*Projectile.velocity.Y) + 2)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 113, Projectile.velocity.X * .3f, Projectile.velocity.Y * .3f, 100, Color.AliceBlue, 0.6f);
            }
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.timeLeft < 300)
            {
                Projectile.velocity.X *= 1.0255f;
                Projectile.velocity.Y *= 1.0255f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(0, 4) == 0)
            {
                target.AddBuff(BuffID.Frostburn, 180 + Main.rand.Next(0, 4) * 60);
            }
        }
        public override void OnKill(int timeLeft)
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 113, (Projectile.velocity.X + Main.rand.Next(-2, 3)) * .2f, (Projectile.velocity.Y + Main.rand.Next(-2, 3)) * .2f, 100, Color.AliceBlue, 1f);
            SoundEngine.PlaySound(SoundID.Item51, Projectile.position);
        }
    }
}
