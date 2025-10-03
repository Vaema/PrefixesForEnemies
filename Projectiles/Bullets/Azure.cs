using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace EnemyMods.Projectiles.Bullets
{
    public class Azure : ModProjectile
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
            // DisplayName.SetDefault("Azure Bullet");
        }
        public override void AI()
        {
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), .4f, .4f, 1f);
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int num259 = 0; num259 < 30; num259++)
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 172, 0f, 0f, 0, default(Color), 2f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity.Normalize();
                Main.dust[d].velocity *= 6;
            }
            Projectile.position = Projectile.Center;
            Projectile.width = (Projectile.height = 115);
            Projectile.Center = Projectile.position;
            Projectile.penetrate = -1;
            Projectile.damage = (int)(Projectile.damage * 1.3);
            Projectile.knockBack *= 1.2f;
            Projectile.Damage();
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 172, 0, 0, 100, default(Color), 0.6f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
