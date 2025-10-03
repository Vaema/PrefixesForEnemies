using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class Shattershard : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 10;
            Projectile.timeLeft = 480;
            Projectile.maxPenetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1f;
            Projectile.aiStyle = 0;
            Projectile.extraUpdates = 6;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shattershard");
        }
        public override void AI()
        {
            if (Main.rand.Next(0, 320) < Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y) + 2)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 113, Projectile.velocity.X *.3f, Projectile.velocity.Y * .3f, 100, Color.AliceBlue, 0.8f);
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
            if (Main.rand.Next(0, 3) == 0)
            {
                target.AddBuff(BuffID.Frostburn, 600 + Main.rand.Next(0, 6) * 60);
            }
        }
        public override void OnKill(int timeLeft)
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 113, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 100, Color.AliceBlue, 1f);
            SoundEngine.PlaySound(SoundID.Item51, Projectile.position);
            for(int i=0; i<Main.rand.Next(2,4); i++)
            {
                float velX = 0;
                float velY = 0;
                while (velX == 0 || velY == 0)
                {
                    velX = Main.rand.Next(-8, 9) / 2f;
                    velY = Main.rand.Next(-8, 9) / 2f;
                }
                int p = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, velX, velY, 344, (int)(Projectile.damage * .75), 0, Projectile.owner);
                Main.projectile[p].penetrate = 4;
                Main.projectile[p].maxPenetrate = 4;
                Main.projectile[p].timeLeft = 90;
                Main.projectile[p].tileCollide = true;
            }
        }
    }
}
