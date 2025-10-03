using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace EnemyMods.Projectiles.Bullets
{
    public class Splitter : ModProjectile
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
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Splitter");
        }
        public override void AI()
        {
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.ai[1] == 0 && Projectile.timeLeft <= 580 && Main.myPlayer == Projectile.owner)
            {
                float spread = 25f * 0.0174f;
                float baseSpeed = Projectile.velocity.Length();
                double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
                double deltaAngle = spread;
                double offsetAngle;
                int i;
                for (i = 0; i < 2; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    int p = Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Projectile.type, (int)(Projectile.damage * .7), Projectile.knockBack, Projectile.owner, 0, 1);
                }
                Projectile.Kill();
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 7, 0, 0, 100, default(Color), 0.6f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
