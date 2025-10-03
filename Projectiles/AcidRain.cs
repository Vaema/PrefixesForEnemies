using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class AcidRain : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.timeLeft = 600;
            Projectile.maxPenetrate = 1;
            Projectile.penetrate = 1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1f;
            Projectile.aiStyle = 1;
            Projectile.extraUpdates = 2;
            Projectile.friendly = true;
            //projectile.light = 0.5f;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("AcidRain");
        }

        public override void AI()
        {
            if (Main.rand.Next(0, 65) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 61, 0, 0, 100, Color.Green, 0.6f);
            }
        }
        public override void OnKill(int timeLeft)
        {
            if (Main.rand.Next(0, 5) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 61, 0, 0, 100, Color.Green, 0.6f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);//look for better one
            if (Main.rand.Next(1, 20) == 1)
            {
                int p = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0, -1f, 512, Projectile.damage, Projectile.knockBack + 2, Projectile.owner);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(20, 300);
        }
    }
}
