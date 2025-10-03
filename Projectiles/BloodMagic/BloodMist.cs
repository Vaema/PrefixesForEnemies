using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles.BloodMagic
{
    public class BloodMist : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.timeLeft = 120;
            Projectile.maxPenetrate = 100;
            Projectile.penetrate = 100;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1f;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 220;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Mist");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }
        public override void AI()
        {
            if (Main.rand.Next(0, 2) == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 5, 0, 0, 100, default(Color), 1f);
                    Main.dust[dust].fadeIn = 0.7f + Projectile.timeLeft / 800f;
                    Main.dust[dust].noGravity = true;
                }
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 60, 0, 0, 100, default(Color), 0.5f);
                Main.dust[d].fadeIn = 0.7f + Projectile.timeLeft / 800f;
                Main.dust[d].noGravity = true;
            }
            Projectile.velocity *= .98f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("Bloodied").Type, 180);
        }
    }
}
