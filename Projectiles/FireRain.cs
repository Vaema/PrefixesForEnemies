using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class FireRain : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.timeLeft = 600;
            Projectile.maxPenetrate = -1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = .5f;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.extraUpdates = 2;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("FireRain");
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 4.71f;
            if (Main.rand.Next(0, 7) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0, 0, 100, Color.OrangeRed, 1.6f);
            }
        }
        public override void OnKill(int timeLeft)
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 127, Projectile.velocity.X + Main.rand.Next(-4, 4) * .2f, Projectile.velocity.Y + Main.rand.Next(-4, 4) * .2f, 100, Color.OrangeRed, 0.2f);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(24, 300);
        }
    }
}
