using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace EnemyMods.Projectiles.Duelist
{
    public class ShadowflameEstoc : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 54;
            Projectile.timeLeft = 60;
            Projectile.tileCollide = false;
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.scale = 1f;
            Projectile.aiStyle = 19;
            Projectile.friendly = true;
        }
        public override void AI()
        {
            Main.player[Projectile.owner].direction = Projectile.direction;
            Main.player[Projectile.owner].heldProj = Projectile.whoAmI;
            Main.player[Projectile.owner].itemTime = Main.player[Projectile.owner].itemAnimation;
            Projectile.position.X = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - (float)(Projectile.width / 2);
            Projectile.position.Y = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - (float)(Projectile.height / 2);
            Projectile.position += Projectile.velocity * Projectile.ai[0] + (Projectile.velocity / 5) * Projectile.width / 4.5f; if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 3f;
                Projectile.netUpdate = true;
            }
            if (Projectile.timeLeft % 2 == 0)
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame);
            }
            if (Main.player[Projectile.owner].itemAnimation < Main.player[Projectile.owner].itemAnimationMax / 3)
            {
                Projectile.ai[0] -= .68f;
            }
            else
            {
                Projectile.ai[0] += 0.68f;
            }

            if (Main.player[Projectile.owner].itemAnimation == 0)
            {
                Projectile.Kill();
            }

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= 1.57f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 180);
        }
    }
}
