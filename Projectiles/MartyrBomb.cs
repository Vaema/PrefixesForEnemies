using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Projectiles
{
    public class MartyrBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.HappyBomb);
            Projectile.timeLeft = 181;
            Projectile.hostile = false;
        }
        public override void AI()
        {
            if(Projectile.timeLeft % 60 == 0 && Projectile.timeLeft > 59)
            {
                int time = Projectile.timeLeft / 60;
                CombatText.NewText(Projectile.getRect(), new Microsoft.Xna.Framework.Color(255, 20, 20), "" + time, true);
            }
        }
        public override void OnKill(int timeLeft)
        {
            int p = Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, 0, 0, ProjectileID.Landmine, Projectile.damage, 0);
        }
    }
}
