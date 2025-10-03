using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using System;

namespace EnemyMods.Projectiles
{
    public class SoulProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.timeLeft = 600;
            Projectile.maxPenetrate = -1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("SoulProj");
        }
        public override void AI()
        {
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.5f, 0.5f, 0.9f);
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 8f)
            {
                Projectile.frame = (Projectile.frame + 1) % 4;
                Projectile.ai[0] = 0f;
            }
            //homing
            if (Projectile.alpha < 170)
            {
                for (int num136 = 0; num136 < 10; num136++)
                {
                    float x2 = Projectile.position.X - Projectile.velocity.X / 10f * (float)num136;
                    float y2 = Projectile.position.Y - Projectile.velocity.Y / 10f * (float)num136;
                    int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, DustID.BlueCrystalShard, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num137].alpha = Projectile.alpha;
                    Main.dust[num137].position.X = x2;
                    Main.dust[num137].position.Y = y2;
                    Main.dust[num137].velocity *= 0f;
                    Main.dust[num137].noGravity = true;
                }
            }
            float num138 = (float)Math.Sqrt((double)(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y));
            float num139 = Projectile.localAI[0];
            if (num139 == 0f)
            {
                Projectile.localAI[0] = num138;
                num139 = num138;
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 25;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            float num140 = Projectile.position.X;
            float num141 = Projectile.position.Y;
            float num142 = 300f;
            bool flag4 = false;
            int num143 = 0;
            if (Projectile.ai[1] == 0f)
            {
                for (int num144 = 0; num144 < 200; num144++)
                {
                    if (Main.npc[num144].CanBeChasedBy(this, false) && (Projectile.ai[1] == 0f || Projectile.ai[1] == (float)(num144 + 1)))
                    {
                        float num145 = Main.npc[num144].position.X + (float)(Main.npc[num144].width / 2);
                        float num146 = Main.npc[num144].position.Y + (float)(Main.npc[num144].height / 2);
                        float num147 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num145) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num146);
                        if (num147 < num142 && Collision.CanHit(new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2)), 1, 1, Main.npc[num144].position, Main.npc[num144].width, Main.npc[num144].height))
                        {
                            num142 = num147;
                            num140 = num145;
                            num141 = num146;
                            flag4 = true;
                            num143 = num144;
                        }
                    }
                }
                if (flag4)
                {
                    Projectile.ai[1] = (float)(num143 + 1);
                }
                flag4 = false;
            }
            if (Projectile.ai[1] > 0f)
            {
                int num148 = (int)(Projectile.ai[1] - 1f);
                if (Main.npc[num148].active && Main.npc[num148].CanBeChasedBy(this, true) && !Main.npc[num148].dontTakeDamage)
                {
                    float num149 = Main.npc[num148].position.X + (float)(Main.npc[num148].width / 2);
                    float num150 = Main.npc[num148].position.Y + (float)(Main.npc[num148].height / 2);
                    float num151 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num149) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num150);
                    if (num151 < 1000f)
                    {
                        flag4 = true;
                        num140 = Main.npc[num148].position.X + (float)(Main.npc[num148].width / 2);
                        num141 = Main.npc[num148].position.Y + (float)(Main.npc[num148].height / 2);
                    }
                }
                else
                {
                    Projectile.ai[1] = 0f;
                }
            }
            if (!Projectile.friendly)
            {
                flag4 = false;
            }
            if (flag4)
            {
                float num152 = num139;
                Vector2 vector13 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num153 = num140 - vector13.X;
                float num154 = num141 - vector13.Y;
                float num155 = (float)Math.Sqrt((double)(num153 * num153 + num154 * num154));
                num155 = num152 / num155;
                num153 *= num155;
                num154 *= num155;
                int num156 = 8;
                Projectile.velocity.X = (Projectile.velocity.X * (float)(num156 - 1) + num153) / (float)num156;
                Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num156 - 1) + num154) / (float)num156;
            }
            Projectile.rotation = 0f;
        }
        public override void OnKill(int timeLeft)
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueCrystalShard, Projectile.velocity.X + Main.rand.Next(-4, 4) * .2f, Projectile.velocity.Y + Main.rand.Next(-4, 4) * .2f, 100, default(Color), 0.2f);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            DamageArea(Projectile, 70);
            for (int num958 = 0; num958 < 40; num958++)
            {
                int dust11 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 68, 0f, -2f, 0, default(Color), 2f);
                Main.dust[dust11].noGravity = true;
                Main.dust[dust11].position.X += Main.rand.Next(-50,51)/20 - 1.5f;
                Main.dust[dust11].position.Y += Main.rand.Next(-50, 51) / 20 - 1.5f;
                if (Main.dust[dust11].position != Projectile.Center)
                {
                    Main.dust[dust11].velocity = Projectile.DirectionTo(Main.dust[dust11].position) * 6f;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

        }
        public void DamageArea(Projectile p, int x)//hostile npcs, no crit, no immunity
        {
            Rectangle hurtbox = new Rectangle((int)p.position.X-x, (int)p.position.Y-x, x*2, x*2);
            for(int i=0; i<200; i++)
            {
                bool flag = !p.usesLocalNPCImmunity || p.localNPCImmunity[i] == 0;
                bool flag2 = p.Colliding(hurtbox, Main.npc[i].getRect());
                if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && flag && flag2 && ((p.friendly && (!Main.npc[i].friendly || p.type == 318 || (Main.npc[i].type == 22 && p.owner < 255 && Main.player[p.owner].killGuide) || (Main.npc[i].type == 54 && p.owner < 255 && Main.player[p.owner].killClothier))) || (p.hostile && Main.npc[i].friendly)) && (p.owner < 0 || Main.npc[i].immune[p.owner] == 0 || p.maxPenetrate == 1))
                {
                    int damage = (int)Main.npc[i].StrikeNPC(p.damage, p.knockBack, p.direction, false, false, false);
                }
            }
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
