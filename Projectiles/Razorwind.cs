using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Projectiles
{
    public class Razorwind : ModProjectile
    {
        int timer = 0;

        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.timeLeft = 300;
            Projectile.penetrate = 100;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 0;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razorwind");
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            timer++;
            if (timer >= 3)
            {
                timer = 0;
                if (Main.rand.Next(0, 2) == 0) { timer++; }

                //position randomizer weighted towards central values
                float posX = Projectile.Center.X;
                float posY = Projectile.Center.Y;
                posX += Main.rand.Next(-50, 50) + Main.rand.Next(-30, 30);
                posY += Main.rand.Next(-150, 150) + Main.rand.Next(-50, 50);

                int p = Projectile.NewProjectile(posX, posY, 20 + Main.rand.Next(0, 2), 0, 227, Projectile.damage, Projectile.knockBack, Projectile.owner);
                Main.projectile[p].maxPenetrate = 5;
                Main.projectile[p].penetrate = 5;
            }
        }
    }
}
