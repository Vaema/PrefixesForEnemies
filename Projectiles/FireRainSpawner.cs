using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Projectiles
{
    public class FireRainSpawner : ModProjectile
    {
        int timer = 0;

        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.timeLeft = 480;
            Projectile.penetrate = 100;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 0;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("FireRainSpawner");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            timer++;
            if (timer >= 2)
            {
                timer = 0;
                if (Main.rand.Next(0, 2) == 0) { timer++; }
                
                    //position randomizer weighted towards central values
                float posX = Projectile.Center.X;
                float posY = Projectile.Center.Y;
                posX += Main.rand.Next(-200, 200) + Main.rand.Next(-30, 30);
                posY += Main.rand.Next(-50, 50) + Main.rand.Next(-30, 30);

                int p = Projectile.NewProjectile(posX, posY, 2 + Main.rand.Next(0,2), 7 + Main.rand.Next(0,3), Mod.Find<ModProjectile>("FireRain").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                if(Main.rand.Next(0, 10) == 0)
                {
                    int q = Projectile.NewProjectile(posX + Main.rand.Next(-20, 20), posY + Main.rand.Next(-20, 20), 2 + Main.rand.Next(0, 2), 7 + Main.rand.Next(0, 3), 400 + Main.rand.Next(0, 3), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }
        }
    }
}
