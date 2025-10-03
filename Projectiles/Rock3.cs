using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Projectiles
{
    public class Rock3 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 0.7f;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
        }
        public override void AI()
        {
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.velocity.Y += 0.14f;
        }
        public override void OnKill(int timeLeft)
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 0, -Projectile.velocity.X + Main.rand.Next(-4, 4) * .2f, -Projectile.velocity.Y + Main.rand.Next(-4, -4) * 1f, 0);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
