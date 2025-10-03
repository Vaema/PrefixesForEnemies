using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class Firecracker : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.timeLeft = 1200;
            Projectile.maxPenetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1f;
            Projectile.aiStyle = 0;
            Projectile.light = 1f;
            Projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Firecracker");
        }
        public override void AI()
        {
            for(int i=0; i<4; i++)
            {
                int num356 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 8, 8, 6, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1.2f);
                Main.dust[num356].noGravity = true;
                Main.dust[num356].velocity *= 0.2f;
            }
            Projectile.velocity.X += Projectile.ai[1];
            Projectile.velocity.Y += Projectile.ai[2];
            Projectile.ai[1] += (float)(Main.rand.Next(-4, 5) / 100);
            Projectile.ai[2] += (float)(Main.rand.Next(-4, 5) / 100);

        }
        public override void OnKill(int timeLeft)
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, (Projectile.velocity.X + Main.rand.Next(-2, 2)) * .2f, (Projectile.velocity.Y + Main.rand.Next(-2, -2)) * .2f, 100, Color.White, 3f);
            SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
            int p = Projectile.NewProjectile(Projectile.Center.X + Main.rand.Next(-30, 30), Projectile.Center.Y + Main.rand.Next(-30, 30), 0, 0, 415, Projectile.damage, Projectile.knockBack, Projectile.owner);
            int q = Projectile.NewProjectile(Projectile.Center.X + Main.rand.Next(-30, 30), Projectile.Center.Y + Main.rand.Next(-30, 30), 0, 0, 416, Projectile.damage, Projectile.knockBack, Projectile.owner);
            int w = Projectile.NewProjectile(Projectile.Center.X + Main.rand.Next(-30, 30), Projectile.Center.Y + Main.rand.Next(-30, 30), 0, 0, 417, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Main.projectile[p].timeLeft = 2;
            Main.projectile[q].timeLeft = 2;
            Main.projectile[w].timeLeft = 2;
            Main.projectile[p].alpha = 255;
            Main.projectile[q].alpha = 255;
            Main.projectile[w].alpha = 255;
        }
    }
}
