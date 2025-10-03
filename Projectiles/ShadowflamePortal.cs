using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class ShadowflamePortal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.timeLeft = 720;
            Projectile.penetrate = 100;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("ShadowflamePortal");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            Projectile.ai[0] += 1f;
            //projectile.ai[1] += 1f;
            if (Projectile.ai[0] >= 6f)
            {
                for(int i = 0; i < 8; i++)
                {
                    int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 21, (Projectile.velocity.X + Main.rand.Next(-2, 3)) * .05f, (Projectile.velocity.Y + Main.rand.Next(-2, 3)) * .05f, 100, default(Color), 1.5f);
                }
                Projectile.ai[0] = -54;
                NPC npc = getClosestNPC(Projectile);
                float distance = (float)Math.Sqrt((npc.Center.X - Projectile.Center.X) * (npc.Center.X - Projectile.Center.X) + (npc.Center.Y - Projectile.Center.Y) * (npc.Center.Y - Projectile.Center.Y));
                if(distance < 330) { int p = Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, -(Projectile.position.X - npc.position.X) / distance * 10, -(Projectile.position.Y - npc.position.Y) / distance * 10, 496, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f); }
            }
            /* Shadowbeam pentagram
            if(projectile.ai[1] == 6)
            {
                int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y + 24, 2*.025f, -5.5f * .025f, 294, (int)(projectile.damage*0.6), projectile.knockBack, projectile.owner, 0f, 0f);
            }
            if (projectile.ai[1] == 12)
            {
                int p = Projectile.NewProjectile(projectile.position.X + 16, projectile.position.Y - 20, -5 * .025f, 3.5f * .025f, 294, (int)(projectile.damage * 0.6), projectile.knockBack, projectile.owner, 0f, 0f);
            }
            if (projectile.ai[1] == 18)
            {
                int p = Projectile.NewProjectile(projectile.position.X - 24, projectile.position.Y + 8, 6 * .025f, 0 * .025f, 294, (int)(projectile.damage * 0.6), projectile.knockBack, projectile.owner, 0f, 0f);
            }
            if (projectile.ai[1] == 24)
            {
                int p = Projectile.NewProjectile(projectile.position.X + 24, projectile.position.Y + 8, -5 * .025f, -3.5f * .025f, 294, (int)(projectile.damage * 0.6), projectile.knockBack, projectile.owner, 0f, 0f);
            }
            if (projectile.ai[1] == 30)
            {
                int p = Projectile.NewProjectile(projectile.position.X - 16, projectile.position.Y - 20, 2 * .025f, 5.5f * .025f, 294, (int)(projectile.damage * 0.6), projectile.knockBack, projectile.owner, 0f, 0f);
                projectile.ai[1] = -120;
            }
            */
        }
        // returns the living npc closest to the projectile
        private NPC getClosestNPC(Projectile projectile)
        {
            float lowestD = 99999;
            NPC closest = Main.npc[0];
            for (int i = 0; i < 100; i++)
            {
                NPC npc = Main.npc[i];
                float distance = (float)Math.Sqrt((npc.Center.X - projectile.Center.X) * (npc.Center.X - projectile.Center.X) + (npc.Center.Y - projectile.Center.Y) * (npc.Center.Y - projectile.Center.Y));
                if (lowestD > distance && !npc.townNPC && npc.life > 0 && npc.active)
                {
                    closest = npc;
                    lowestD = distance;
                }
            }
            return closest;
        }
    }
}
