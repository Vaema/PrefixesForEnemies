using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using EnemyMods.NPCs;

namespace EnemyMods.Projectiles
{
    public class BigHolyLance : ModProjectile
    {
        private NPC stuckTarget = null;
        private float stuckPosX, stuckPosY;

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.timeLeft = 510;
            Projectile.penetrate = -1;
            Projectile.maxPenetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 2f;
            Projectile.aiStyle = 0;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("BigHolyLance");
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 1f, 1f);
            if (Projectile.ai[0] == 0f)
            {
                Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + .785f;
                int num986 = (int)Projectile.ai[1];
                if (!Main.npc[num986].active)
                {
                    Projectile.Kill();
                    return;
                }
            }
            if (Projectile.ai[0] == 1f)
            {
                Projectile.ignoreWater = true;
                Projectile.tileCollide = false;
                bool flag53 = false;
                Projectile.localAI[0] += 1f;
                if (Projectile.localAI[0] % 30f == 0f)
                {
                    flag53 = true;
                }
                int num991 = (int)Projectile.ai[1];
                if (Main.npc[num991].active && !Main.npc[num991].dontTakeDamage)
                {
                    Projectile.Center = Main.npc[num991].Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = Main.npc[num991].gfxOffY;
                    if (flag53)
                    {
                        Main.npc[num991].HitEffect(0, 1.0);
                    }
                }
                int num986 = (int)Projectile.ai[1];
                if (!Main.npc[num986].active)
                {
                    Projectile.Kill();
                    return;
                }
            }
            if (Projectile.timeLeft < 450 && Projectile.ai[0] == 0f && Projectile.velocity.Length() < 1)
            {
                Projectile.velocity.X *= 8000f;
                Projectile.velocity.Y *= 8000f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0] = 1f;
            Projectile.ai[1] = target.whoAmI;
            gNPC info = target.GetGlobalNPC<gNPC>();
            info.lightSpearCount+=10;
            target.immune[Projectile.owner] = 0;
            Projectile.friendly = false;
            stuckTarget = target;
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 0;
        }
        public override void OnKill(int timeLeft)
        {
            if (stuckTarget != null)
            {
                gNPC info = stuckTarget.GetGlobalNPC<gNPC>();
                info.lightSpearCount-=10;
            }
            for (int i = 0; i < 50; i++)
            {
                int d = Dust.NewDust(new Vector2(Projectile.position.X-16, Projectile.position.Y), Projectile.width, Projectile.height, 57, (Projectile.velocity.X + Main.rand.Next(-2, 3)) * .2f, (Projectile.velocity.Y + Main.rand.Next(-2, 3)) * .2f, 100, Color.White, 3f);
            }
            SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
        }
    }
}
