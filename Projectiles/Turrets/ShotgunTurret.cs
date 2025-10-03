using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Turrets
{
    public class ShotgunTurret : BasicTurret
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 42;
            Projectile.height = 30;

            target = null;
            range = 800;
            ammoType = 14;
            fireRate = 65;//number of frames between shots
            shootSpeed = 11;
            secondaryType = ProjectileID.GreenLaser;
            secondaryRate = 120;
            secondarySpeed = 15;
            secondaryDam = 20;
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
                Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("ShotgunTurret").Type, 1, false, 0, false, false);
        }
        protected override void shoot(Vector2 toTarget)
        {
            int type;
            int damage = consumeAmmo(out type);
            if (damage == -1)
            {
                return;
            }
            SoundEngine.PlaySound(SoundID.Item36, Projectile.position);
            toTarget.Normalize();
            toTarget *= shootSpeed;
            for(int i=0; i<5; i++)
            {
                float spread = 30f * 0.0174f;//40 degrees converted to radians
                float baseSpeed = toTarget.Length();
                double baseAngle = Math.Atan2(toTarget.X, toTarget.Y);
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                float speedX = baseSpeed * (float)Math.Sin(randomAngle);
                float speedY = baseSpeed * (float)Math.Cos(randomAngle);
                Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, speedX, speedY, type, damage, Projectile.knockBack, Projectile.owner);
            }
        }

        protected override void shootSecondary(Vector2 toTarget)
        {
            SoundEngine.PlaySound(SoundID.Item12, Projectile.position);
            int damageMod = (int)(secondaryDam*Main.player[Projectile.owner].GetDamage(DamageClass.Ranged));
            toTarget.Normalize();
            toTarget *= secondarySpeed;
            Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, toTarget.X, toTarget.Y, secondaryType, Projectile.damage + damageMod, Projectile.knockBack, Projectile.owner);
        }
        protected override void update()
        {
            Player player = Main.player[Projectile.owner];
            MPlayer mplayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (player.dead)
            {
                mplayer.shotgunTurret = false;
            }
            if (!mplayer.shotgunTurret)
            {
                Projectile.timeLeft = 1;
            }
        }
    }
}
