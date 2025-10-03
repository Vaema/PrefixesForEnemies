using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace EnemyMods.Projectiles.Bullets
{
    public class HollowPoint : ModProjectile
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
            Projectile.light = .5f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hollow Point");
        }
        public override void AI()
        {
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 7, 0, 0, 100, default(Color), 0.6f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            damage = (int)(damage * 1.1) + 10;
            damage -= target.defense / 2;
        }
    }
}
