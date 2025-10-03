using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Projectiles.Minions
{
    public class WaterElementalSpirit : HoverLOS
    {
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 24;
            Projectile.height = 32;
            Main.projFrames[Projectile.type] = 5;
            Projectile.friendly = true;
            Main.projPet[Projectile.type] = true;
            Projectile.minion = true;
            Projectile.minionSlots = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            inertia = 20f;
            shoot = ProjectileID.WaterBolt;
            shootSpeed = 8f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Water Elemental Spirit");
        }
        public override void CheckActive()
        {
            Player player = Main.player[Projectile.owner];
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (player.dead)
            {
                modPlayer.waterSpirit = false;
            }
            if (modPlayer.waterSpirit)
            {
                Projectile.timeLeft = 2;
            }
        }

        public override void CreateDust()
        {
            if (Projectile.ai[0] == 0f)
            {
                if (Main.rand.Next(5) == 0)
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height / 2, 45);
                    Main.dust[dust].velocity.Y -= 1.2f;
                }
            }
            else
            {
                if (Main.rand.Next(3) == 0)
                {
                    Vector2 dustVel = Projectile.velocity;
                    if (dustVel != Vector2.Zero)
                    {
                        dustVel.Normalize();
                    }
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 45);
                    Main.dust[dust].velocity -= 1.2f * dustVel;
                }
            }
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }

        public override void SelectFrame()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 5;
            }
        }
    }
}
