using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EnemyMods.NPCs;

namespace EnemyMods.Projectiles
{
    public class HolyLance : ModProjectile
    {
        private NPC stuckTarget = null;
        private float stuckPosX, stuckPosY;

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.timeLeft = 480;
            Projectile.penetrate = -1;
            Projectile.maxPenetrate = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1f;
            Projectile.aiStyle = 0;
            //projectile.extraUpdates = 6;
            Projectile.alpha = 50;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("HolyLance");
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 1f, 1f);
            if (Projectile.ai[0] == 0f)
            {
                Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
                int num986 = (int)Projectile.ai[1];
                if (!Main.npc[num986].active)
                {
                    Projectile.Kill();
                    return;
                }
                /*
                Vector2 vector122 = Main.npc[num986].Center - projectile.Center;
                if (vector122 != Vector2.Zero)
                {
                    vector122.Normalize();
                    vector122 *= 14f;
                }
                float num987 = 5f;
                projectile.velocity = (projectile.velocity * (num987 - 1f) + vector122) / num987;
                projectile.velocity.ToRotation();
                */
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
                Projectile.velocity.X *= 12000f;
                Projectile.velocity.Y *= 12000f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0] = 1f;
            Projectile.ai[1] = target.whoAmI;
            gNPC info = target.GetGlobalNPC<gNPC>();
            info.lightSpearCount++;
            target.immune[Projectile.owner] = 0;
            Projectile.friendly = false;
            stuckTarget = target;
            /*
            stuckPosX = projectile.position.X - target.position.X;
            stuckPosY = -projectile.position.Y + target.position.Y;
            */
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 0;
        }
        public override void OnKill(int timeLeft)
        {
            if(stuckTarget != null)
            {
                gNPC info = stuckTarget.GetGlobalNPC<gNPC>();
                info.lightSpearCount--;
            }
            for(int i=0; i<10; i++)
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, (Projectile.velocity.X + Main.rand.Next(-2, 3)) * .2f, (Projectile.velocity.Y + Main.rand.Next(-2, 3)) * .2f, 100, Color.White, 3f);
            }
            SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)(texture.Height / Main.projFrames[Projectile.type]) * 0.5f);
            SpriteEffects effect = Projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Vector2 adjustment = new Vector2(Projectile.Center.X - 4, Projectile.Center.Y - 8);
            Main.spriteBatch.Draw(texture, adjustment - Main.screenPosition, new Rectangle?(Utils.Frame(texture, 1, Main.projFrames[Projectile.type], 0, Projectile.frame)), lightColor, Projectile.rotation, origin, Projectile.scale, effect, 0);
            return false;
        }
    }
}
