using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Turrets
{
    public class ShotgunTurretCapsule : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 2400;
        }
        public override void AI()
        {

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            float aiOut = 1.57f * 3;
            Vector2 tilePos = (Projectile.Center) / 16;
            //determines which way the turret should face depending on how it collided
            //aiOut is the angle from turret to stand
            if (Main.tile[(int)tilePos.X, (int)tilePos.Y + 1].HasTile && Projectile.oldVelocity.Y > 0)
            {
                aiOut = 1.57f * 3;
            }
            if (Main.tile[(int)tilePos.X + 1, (int)tilePos.Y].HasTile && Projectile.oldVelocity.X > 0)
            {
                aiOut = 3.14f;
            }
            if (Main.tile[(int)tilePos.X - 1, (int)tilePos.Y].HasTile && Projectile.oldVelocity.X <= 0)
            {
                aiOut = 0f;
            }
            if (Main.tile[(int)tilePos.X, (int)tilePos.Y - 1].HasTile && Projectile.oldVelocity.Y <= 0)
            {
                aiOut = 1.57f;
            }
            Player player = Main.player[Projectile.owner];
            player.AddBuff(Mod.Find<ModBuff>("ShotgunTurret").Type, 18002);
            MPlayer mplayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            mplayer.shotgunTurret = true;
            int p = Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, 0, 0, Mod.Find<ModProjectile>("ShotgunTurret").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, aiOut);
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            if (timeLeft == 0)
            {
                Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("ShotgunTurret").Type, 1, false, 0, false, false);
            }
        }
    }
}