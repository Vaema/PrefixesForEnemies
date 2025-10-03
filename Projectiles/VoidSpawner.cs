using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class VoidSpawner : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.timeLeft = 90;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Breach");
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] >= 4f)
            {
                Projectile.ai[1] = 0f;
                for (int i = 0; i < 3; i++)
                {
                    int d = Dust.NewDust(new Vector2(Projectile.position.X + Main.rand.Next(-20, 21), Projectile.position.Y + Main.rand.Next(-20, 21)), Projectile.width, Projectile.height, Mod.Find<ModDust>("VoidDust").Type, (Main.rand.Next(-15, 16)) * .3f, (Main.rand.Next(-15, 16)) * .3f);
                    Main.dust[d].noGravity = false;
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            //find a sound to play here
            Vector2 velocity = new Vector2(Main.player[(int)Projectile.ai[0]].Center.X - Projectile.position.X, Main.player[(int)Projectile.ai[0]].Center.Y - Projectile.position.Y);
            velocity.Normalize();
            velocity *= 3.4f;
            Projectile.NewProjectile(Projectile.position, velocity, Mod.Find<ModProjectile>("VoidTendril").Type, Projectile.damage, 2);
        }
    }
}
