using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class IceShardSpawner : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.timeLeft = 120;
            Projectile.penetrate = 100;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 0.7f;
            Projectile.alpha = 0;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("IceShardSpawner");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 10f && Projectile.owner==Main.myPlayer && Main.netMode != 1)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 113, (Projectile.velocity.X + Main.rand.Next(-2, 3)) * .01f, (Projectile.velocity.Y + Main.rand.Next(-2, 3)) * .01f, 100, Color.AliceBlue, 1.5f);
                Projectile.ai[0] = 0;

                //conversion to polar from cartesian for velocity
                double x = Projectile.Center.X - Main.MouseWorld.X;
                double y = -Projectile.Center.Y + Main.MouseWorld.Y;
                //double r = Math.Sqrt((x * x) + (y * y));
                double q = Math.Atan2(x, y);
                //x = r; r not needed since we always want r=1 so each has the same speed
                y = q;
                float XVel = (float)(0.1 * Math.Cos(q+ 1.57f));
                float YVel = (float)(0.1 * Math.Sin(q+ 1.57f));

                //position randomizer weighted towards central values
                float posX = Projectile.Center.X;
                float posY = Projectile.Center.Y;

                posX += (Main.rand.Next(-20, 20) + Main.rand.Next(-30, 30));
                posY += (Main.rand.Next(-20, 20) + Main.rand.Next(-30, 30));

                int p = Projectile.NewProjectile(posX, posY, XVel, YVel, Mod.Find<ModProjectile>("IceShard").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            }
        }
    }
}
