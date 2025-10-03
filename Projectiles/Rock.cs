using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Projectiles
{
    public class Rock : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 25;
            Projectile.height = 25;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1.3f;
            Projectile.aiStyle = 1;
        }
        public override void AI()
        {
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.velocity.Y += 0.14f;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int p = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (-Projectile.velocity.X + Main.rand.Next(-4, 4)) * .25f, (-Projectile.velocity.Y + Main.rand.Next(1, 4)) * 0.6f, Mod.Find<ModProjectile>("Rock2").Type, (int)(Projectile.damage * .6f), Projectile.knockBack - 2, Projectile.owner);
            }
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 0, -Projectile.velocity.X + Main.rand.Next(-4, 4) * .2f, -Projectile.velocity.Y + Main.rand.Next(-4, -4) * 2f, 0);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
