using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Turrets
{
    public class TurretStand : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.scale = 1f;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            if (Main.projectile[(int)Projectile.ai[0]].timeLeft == 0)
            {
                Projectile.Kill();
            }
        }
    }
}
