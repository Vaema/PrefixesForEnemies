using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace EnemyMods.Projectiles.Bullets
{
    public class PillarFragment : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.extraUpdates = 1;
            Projectile.friendly = true;
            Projectile.light = 0.5f;
        }
        public override void AI()
        {
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.ai[1] == 0 && Projectile.timeLeft <= 580)
            {
                double n = Main.rand.Next(2, 4);
                float spread = 40f * 0.0174f;
                float baseSpeed = Projectile.velocity.Length();
                double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
                double deltaAngle = spread / n;
                double offsetAngle;
                int i;
                for (i = 0; i < n; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    int rand = Main.rand.Next(0, 4);
                    if (rand == 0)
                    {
                        int p = Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, baseSpeed * (float)Math.Sin(offsetAngle) + Main.rand.Next(-10, 11)/10, baseSpeed * (float)Math.Cos(offsetAngle) + Main.rand.Next(-10, 11)/10, Mod.Find<ModProjectile>("SolarFrag").Type, (int)(Projectile.damage * .6), Projectile.knockBack, Projectile.owner);
                        for(int j=0; j<4; j++)
                        {
                            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 259, 0, 0, 100, default(Color), 0.6f);
                        }
                    }
                    else if (rand == 1)
                    {
                        int p = Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, baseSpeed * (float)Math.Sin(offsetAngle) + Main.rand.Next(-10, 11)/10, baseSpeed * (float)Math.Cos(offsetAngle) + Main.rand.Next(-10, 11)/10, Mod.Find<ModProjectile>("VortexFrag").Type, (int)(Projectile.damage * .6), Projectile.knockBack, Projectile.owner);
                        for (int j = 0; j < 4; j++)
                        {
                            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229, 0, 0, 100, default(Color), 0.6f);
                        }
                    }
                    else if (rand == 2)
                    {
                        int p = Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, baseSpeed * .3f * (float)Math.Sin(offsetAngle) + Main.rand.Next(-10, 11)/10, baseSpeed * .3f * (float)Math.Cos(offsetAngle) + Main.rand.Next(-10, 11)/10, Mod.Find<ModProjectile>("NebulaFrag").Type, (int)(Projectile.damage * .6), Projectile.knockBack, Projectile.owner);
                        for (int j = 0; j < 4; j++)
                        {
                            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 69, 0, 0, 100, default(Color), 0.9f);
                        }
                    }
                    else if (rand == 3)
                    {
                        int p = Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, baseSpeed * .8f * (float)Math.Sin(offsetAngle) + Main.rand.Next(-10, 11)/10, baseSpeed * .8f * (float)Math.Cos(offsetAngle) + Main.rand.Next(-10, 11) / 10, Mod.Find<ModProjectile>("StardustFrag").Type, (int)(Projectile.damage * .6), Projectile.knockBack, Projectile.owner);
                        for (int j = 0; j < 4; j++)
                        {
                            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 68, 0, 0, 100, default(Color), 0.6f);
                        }
                    }
                }
                Projectile.Kill();
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);//look for better one
        }
    }
}
