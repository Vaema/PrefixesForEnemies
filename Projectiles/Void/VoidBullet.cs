using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using EnemyMods.NPCs;

namespace EnemyMods.Projectiles.Void
{
    public class VoidBullet : ModProjectile
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
            // DisplayName.SetDefault("Void Bullet");
        }
        public override void AI()
        {
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("VoidDust").Type, 0, 0, 100, default(Color), 0.6f);
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("VoidDust").Type, 0, 0, 100, default(Color), 0.6f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(target.realLife != -1)
            {
                target = Main.npc[target.realLife];
            }
            gNPC info = target.GetGlobalNPC<gNPC>();
            info.voidBurn += damage;
        }
    }
}
