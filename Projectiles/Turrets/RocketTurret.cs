using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Turrets
{
    public class RocketTurret : BasicTurret
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 42;
            Projectile.height = 30;

            target = null;
            range = 1000;
            ammoType = 771;
            fireRate = 90;//number of frames between shots
            shootSpeed = 12;
        }
        public override void AI()
        {
            basicAI();
            if (!hasStand)
            {
                createStand();
                hasStand = true;
            }

        }
        public override void OnKill(int timeLeft)
        {
            if (Main.netMode != 1)
                Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("RocketTurret").Type, 1, false, 0, false, false);
        }
        protected override void shoot(Vector2 toTarget)
        {
            int type;
            int damage = consumeAmmo(out type);
            type += 134;
            if (damage == -1)
            {
                return;
            }
            SoundEngine.PlaySound(SoundID.Item11, Projectile.position);
            toTarget.Normalize();
            toTarget *= shootSpeed;
            Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, toTarget.X, toTarget.Y, type, damage, Projectile.knockBack, Projectile.owner);

        }

        protected override void shootSecondary(Vector2 toTarget)
        {
            return;
        }

        protected override void update()
        {
            Player player = Main.player[Projectile.owner];
            MPlayer mplayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (player.dead)
            {
                mplayer.rocketTurret = false;
            }
            if (!mplayer.rocketTurret)
            {
                Projectile.timeLeft = 1;
            }
        }
    }
}
