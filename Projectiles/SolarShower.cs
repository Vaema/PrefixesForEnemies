using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class SolarShower : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.timeLeft = 600;
            Projectile.maxPenetrate = -1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = .4f;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.light = 0.5f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("SolarShower");
        }
        public override void AI()
        {
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 4.71f;

            if (Main.rand.Next(0, 16) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 259, (Projectile.velocity.X) * .4f, (Projectile.velocity.Y + Main.rand.Next(-4, 4)) * .05f, 100, default(Color), 1.5f);
                //int dust2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 158, (projectile.velocity.X) * .3f, (projectile.velocity.Y + Main.rand.Next(-4, 4)) * .05f, 100, default(Color), 1.5f);
                int dust3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, (Projectile.velocity.X) * .2f, (Projectile.velocity.Y + Main.rand.Next(-4, 4)) * .2f, 100, default(Color), 1.5f);
            }
        }
        public override void OnKill(int timeLeft)
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 259, 0, 0, 100, default(Color), 0.5f);
            if (Main.rand.Next(0, 8) == 0)
            {
                int p = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0f, 0f, 612, Projectile.damage, 1f, Projectile.owner, 0f, 0.45f + Main.rand.NextFloat() * 1.15f);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //target.AddBuff(BuffID.Daybreak, 90);
        }
    }
}
