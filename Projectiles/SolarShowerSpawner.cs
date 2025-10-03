using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Projectiles
{
    public class SolarShowerSpawner : ModProjectile
    {
        int timer = 0;

        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 100;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 0;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("SolarSpawner");
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
                //position randomizer weighted towards central values
                float posX = Projectile.Center.X;
                float posY = Projectile.Center.Y;
                posX += Main.rand.Next(-200, 200) + Main.rand.Next(-30, 30);
                posY += Main.rand.Next(-50, 50) + Main.rand.Next(-30, 30);

                int p = Projectile.NewProjectile(posX, posY, Main.rand.Next(-20, 21)/10, 4 + Main.rand.Next(0, 3), Mod.Find<ModProjectile>("SolarShower").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
        }
    }
}
