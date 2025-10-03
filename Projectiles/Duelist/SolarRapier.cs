using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace EnemyMods.Projectiles.Duelist
{
    public class SolarRapier : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 70;
            Projectile.height = 70;
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
            Projectile.position += Projectile.velocity * Projectile.ai[0] + (Projectile.velocity / 5) * Projectile.width / 3; if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 3f;
                Projectile.netUpdate = true;
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
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 259, 0, 0);
            Main.dust[d].noGravity = true;
            Main.dust[d].scale = .8f;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= 1.57f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.localAI[0] == 0f)
            {
                target.immune[Projectile.owner] = 0;
                int p = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0f, 0f, 612, Projectile.damage, 1f, Projectile.owner, 0f, 1.05f + Main.rand.NextFloat() * .35f);
                Projectile.localAI[0] = 1;
            }
        }
    }
}
