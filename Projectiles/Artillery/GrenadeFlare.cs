using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace EnemyMods.Projectiles.Artillery
{
    public class GrenadeFlare : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(163);
            Projectile.timeLeft = 1200;
        }
        public override void AI()
        {
            if (Projectile.velocity.Length() == 0 && Main.player[Projectile.owner].FindBuffIndex(Mod.Find<ModBuff>("ArtilleryCooldown").Type) == -1 && Projectile.position.Y < Main.worldSurface)
            {
                for (int i = 0; i < 8; i++)
                {
                    float X = Main.rand.Next(-100, 101) + Projectile.position.X;
                    float Y = Main.rand.Next(-800, 301) + Projectile.position.Y - 2000;
                    int p = Projectile.NewProjectile(X, Y, -X / 200, 5 + Main.rand.Next(-100, 101) / 100, 30, (int)(20 * Main.player[Projectile.owner].GetDamage(DamageClass.Ranged)), 2, Projectile.owner);
                }
                Main.player[Projectile.owner].AddBuff(Mod.Find<ModBuff>("ArtilleryCooldown").Type, 1200);
            }
        }
    }
}
