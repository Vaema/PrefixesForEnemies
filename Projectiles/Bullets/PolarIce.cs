using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace EnemyMods.Projectiles.Bullets
{
    public class PolarIce : ModProjectile
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
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Polar Ice");
        }
        public override void AI()
        {
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), .2f, 1f, 1f);
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.timeLeft%4 == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 91, 0, 0, 100, default(Color), 0.6f);
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 91, 0, 0, 100, default(Color), 0.6f);
            }
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
            if (Projectile.owner == Main.myPlayer)
            {
                for (int num385 = 0; num385 < 4; num385++)
                {
                    float num386 = -Projectile.velocity.X * (float)Main.rand.Next(40, 60) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
                    float num387 = -Projectile.velocity.Y * (float)Main.rand.Next(40, 60) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
                    num386 *= .5f;
                    num387 *= .5f;
                    int p = Projectile.NewProjectile(Projectile.position.X + num386, Projectile.position.Y + num387, num386, num387, 337, (int)((double)Projectile.damage * 0.65), 0f, Projectile.owner, 0f, 0f);
                    Main.projectile[p].timeLeft = 80;
                }
            }
        }
    }
}
