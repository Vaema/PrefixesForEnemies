using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace EnemyMods.Projectiles.Bullets.Fragments
{
    public class NebulaFrag : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 22;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.extraUpdates = 1;
            Projectile.friendly = true;
            Projectile.scale = .6f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nebula Fragment");
        }
        public override void AI()
        {
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 1f, .1f, 0.6f);
            //homing
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.alpha < 170)
            {
                for (int num136 = 0; num136 < 5; num136++)
                {
                    float x2 = Projectile.Center.X - Projectile.velocity.X / 10f * (float)num136*2;
                    float y2 = Projectile.Center.Y - Projectile.velocity.Y / 10f * (float)num136*2;
                    int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 69, 0f, 0f, 0, default(Color), 1f);
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
                Projectile.alpha -= 15;
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
            Projectile.rotation += 3.1416f;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 69, 0, 0, 100, default(Color), 0.6f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
