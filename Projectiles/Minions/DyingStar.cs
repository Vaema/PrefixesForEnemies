using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Minions
{
    public abstract class DyingStar : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 24;
            Projectile.height = 24;
            Main.projFrames[Projectile.type] = 3;//frames on the sprite sheet
            Projectile.friendly = true;
            Main.projPet[Projectile.type] = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            //projectile.aiStyle = 54;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dying Star");
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void AI()
        {
            MPlayer playerinfo = (MPlayer)Main.player[Projectile.owner].GetModPlayer(Mod, "MPlayer");
            if (Main.player[Main.myPlayer].dead)
            {
                playerinfo.dyingStar = false;
            }
            if (playerinfo.dyingStar)
            {
                Projectile.timeLeft = 2;
            }
            // most of vanilla raven code
            for (int num522 = 0; num522 < 1000; num522++)
            {
                if (num522 != Projectile.whoAmI && Main.projectile[num522].active && Main.projectile[num522].owner == Projectile.owner && Main.projectile[num522].type == Projectile.type && Math.Abs(Projectile.position.X - Main.projectile[num522].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num522].position.Y) < (float)Projectile.width)
                {
                    if (Projectile.position.X < Main.projectile[num522].position.X)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - 0.05f;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X + 0.05f;
                    }
                    if (Projectile.position.Y < Main.projectile[num522].position.Y)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - 0.05f;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + 0.05f;
                    }
                }
            }
            float num523 = Projectile.position.X;
            float num524 = Projectile.position.Y;
            float num525 = 900f;
            bool flag19 = false;
            int num526 = 500;
            if (Projectile.ai[1] != 0f || Projectile.friendly)
            {
                num526 = 1400;
            }
            if (Math.Abs(Projectile.Center.X - Main.player[Projectile.owner].Center.X) + Math.Abs(Projectile.Center.Y - Main.player[Projectile.owner].Center.Y) > (float)num526)
            {
                Projectile.ai[0] = 1f;
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.tileCollide = true;
                for (int num527 = 0; num527 < 200; num527++)
                {
                    if (Main.npc[num527].CanBeChasedBy(this, false))
                    {
                        float num528 = Main.npc[num527].position.X + (float)(Main.npc[num527].width / 2);
                        float num529 = Main.npc[num527].position.Y + (float)(Main.npc[num527].height / 2);
                        float num530 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num528) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num529);
                        if (num530 < num525 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num527].position, Main.npc[num527].width, Main.npc[num527].height))
                        {
                            num525 = num530;
                            num523 = num528;
                            num524 = num529;
                            flag19 = true;
                        }
                    }
                }
            }
            else
            {
                Projectile.tileCollide = false;
            }
            if (!flag19)
            {
                Projectile.friendly = true;
                float num531 = 8f;
                if (Projectile.ai[0] == 1f)
                {
                    num531 = 12f;
                }
                Vector2 vector38 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num532 = Main.player[Projectile.owner].Center.X - vector38.X;
                float num533 = Main.player[Projectile.owner].Center.Y - vector38.Y - 60f;
                float num534 = (float)Math.Sqrt((double)(num532 * num532 + num533 * num533));
                if (num534 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                }
                if (num534 > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.width / 2);
                }
                if (num534 > 70f)
                {
                    num534 = num531 / num534;
                    num532 *= num534;
                    num533 *= num534;
                    Projectile.velocity.X = (Projectile.velocity.X * 20f + num532) / 21f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num533) / 21f;
                }
                else
                {
                    if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                    {
                        Projectile.velocity.X = -0.15f;
                        Projectile.velocity.Y = -0.05f;
                    }
                    Projectile.velocity *= 1.01f;
                }
                Projectile.friendly = false;
                Projectile.rotation = Projectile.velocity.X * 0.05f;
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 4)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                }
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
                if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
                {
                    Projectile.spriteDirection = -Projectile.direction;
                    return;
                }
            }
            else
            {
                if (Projectile.ai[1] == -1f)
                {
                    Projectile.ai[1] = 17f;
                }
                if (Projectile.ai[1] > 0f)
                {
                    Projectile.ai[1] -= 1f;
                }
                if (Projectile.ai[1] == 0f)
                {
                    Projectile.friendly = true;
                    float num535 = 8f;
                    Vector2 vector39 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    float num536 = num523 - vector39.X;
                    float num537 = num524 - vector39.Y;
                    float num538 = (float)Math.Sqrt((double)(num536 * num536 + num537 * num537));
                    if (num538 < 100f)
                    {
                        num535 = 10f;
                    }
                    num538 = num535 / num538;
                    num536 *= num538;
                    num537 *= num538;
                    Projectile.velocity.X = (Projectile.velocity.X * 14f + num536) / 15f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 14f + num537) / 15f;
                }
                else
                {
                    Projectile.friendly = false;
                    if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 10f)
                    {
                        Projectile.velocity *= 1.05f;
                    }
                }
                Projectile.rotation = Projectile.velocity.X * 0.05f;
            }
        }
        public void DamageArea(Projectile p, int x)//hostile npcs, no crit, no immunity
        {
            Rectangle hurtbox = new Rectangle((int)p.position.X - x, (int)p.position.Y - x, x * 2, x * 2);
            for (int i = 0; i < 200; i++)
            {
                bool flag = !p.usesLocalNPCImmunity || p.localNPCImmunity[i] == 0;
                bool flag2 = p.Colliding(hurtbox, Main.npc[i].getRect());
                if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && flag && flag2 && ((p.friendly && (!Main.npc[i].friendly || p.type == 318 || (Main.npc[i].type == 22 && p.owner < 255 && Main.player[p.owner].killGuide) || (Main.npc[i].type == 54 && p.owner < 255 && Main.player[p.owner].killClothier))) || (p.hostile && Main.npc[i].friendly)) && (p.owner < 0 || Main.npc[i].immune[p.owner] == 0 || p.maxPenetrate == 1))
                {
                    int damage = (int)Main.npc[i].StrikeNPC(p.damage, p.knockBack, p.direction, false, false, false);
                }
            }
        }
    }
}
