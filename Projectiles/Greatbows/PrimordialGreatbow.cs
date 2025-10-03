using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace EnemyMods.Projectiles.Greatbows
{
    public class PrimordialGreatbow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 40;
            Projectile.height = 66;
            Main.projFrames[Projectile.type] = 5;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 420;
            Projectile.scale = 1f;
        }
        //STATS (these values can be multiplied up to 3 times based on charge, added onto a base of 1)
        float multSpeed = 0.5f;
        float multDamage = 0.9f;
        float multKnockback = 0.4f; //x2.2
        float multSuperDamage = 1.15f; // multiplier for full charge
        //ai[0] = item rotation
        //ai[1] = item frame
        int arrowDamage = 0;
        bool arrowCollide = true;
        float arrowDrop = 0f;
        bool arrowHurtTile = true;
        float arrowSpeed = 0f;

        int chargeTick = 0;

        bool initial = true;
        bool released = false;
        bool fullPower = false;
        Projectile arrow = default(Projectile);
        public override void AI()
        {

            Player player = Main.player[Projectile.owner];//owner
            Item bow = player.inventory[player.selectedItem];//the bow item
            player.heldProj = Projectile.whoAmI;//held proj

            #region Val setups

            if (initial)//this stuff is the arrows stats
            {
                foreach (Projectile find in Main.projectile)
                {
                    if (find.damage == -Projectile.owner - 256 && find.owner == Projectile.owner)
                    {
                        arrow = find;
                        break;
                    }
                }

                Projectile.timeLeft += bow.useAnimation;
                arrowDamage = Projectile.damage;
                arrowCollide = arrow.tileCollide;
                arrowDrop = arrow.ai[0];
                //arrowHurtTile = arrow.;
                arrow.damage = 0;
                Projectile.damage = 0;
                arrow.tileCollide = false;
                //arrow.hurtsTiles = false;

                arrowSpeed = Projectile.knockBack;

                player.itemAnimation = bow.useAnimation - 1;
                player.itemTime = bow.useAnimation - 1;

                initial = false;
            }

            if (Projectile.owner == Main.myPlayer)
            {//if mouse down and its the owner
                if (!released)//only follow mouse when aiming
                {
                    float mX = (float)(Main.mouseX + Main.screenPosition.X);
                    float mY = (float)(Main.mouseY + Main.screenPosition.Y);
                    float pX = player.position.X + player.width * 0.5f;
                    float pY = player.position.Y + player.height * 0.5f;
                    Projectile.ai[0] = (float)Math.Atan2(mY - pY, mX - pX);
                    Projectile.netUpdate = true;
                }
            }

            float itemRotCos = (float)Math.Cos(Projectile.ai[0]);
            float itemRotSin = (float)Math.Sin(Projectile.ai[0]);
            float reverseItemRot = 0;//reversed item rotation for facing left
            if (Projectile.ai[0] < 0)
            {
                reverseItemRot = Projectile.ai[0] + (float)Math.PI;
            }
            else {
                reverseItemRot = Projectile.ai[0] - (float)Math.PI;
            }

            if (Math.Abs(Projectile.ai[0]) > Math.Abs(Math.PI / 2f))//rotate bow and player
            {
                Projectile.direction = -1;
                Projectile.spriteDirection = -1;
                Projectile.rotation = reverseItemRot;
                player.direction = -1;
                player.itemRotation = reverseItemRot;//change the arm direction
            }
            else {
                Projectile.direction = 1;
                Projectile.spriteDirection = 1;
                Projectile.rotation = Projectile.ai[0];
                player.direction = 1;
                player.itemRotation = Projectile.ai[0];//change the arm direction
            }

            //position bow
            //projectile.position = player.position - new Vector2(-(player.width - projectile.width) / 2 + 0.5f - 0.5f * player.direction, 12);
            Projectile.position = player.Center - new Vector2(Projectile.width / 2, Projectile.height / 2);
            Projectile.position += new Vector2((float)(Projectile.width * 0.1f * itemRotCos), (float)(Projectile.width * 0.1f * itemRotSin));

            #endregion

            if (player.channel && Projectile.timeLeft > bow.useAnimation && arrow.active)
            {//whilst channeling and 10 secs (600 ticks) hasn't expired
                Projectile.frame = (int)Projectile.ai[1];
                //hold player's arrow
                arrow.velocity = new Vector2((float)Math.Cos(Projectile.ai[0]), (float)Math.Sin(Projectile.ai[0]));
                arrow.position = player.Center - new Vector2(arrow.Name.Contains("Greatarrow") ? arrow.width : arrow.width / 1.5f, arrow.height / 2);
                arrow.position -= new Vector2((float)((2f * ((int)Projectile.ai[1]) - 22f) * itemRotCos), (float)((2f * ((int)Projectile.ai[1]) - 22) * itemRotSin));
                if (Projectile.owner == Main.myPlayer)
                {
                    arrow.netUpdate = true;
                }

                //suspend arrow detonate time and falling time
                arrow.timeLeft += 2;
                arrow.ai[0] = arrowDrop;

                //keep player animation full to continue the hold animation
                player.itemAnimation = bow.useAnimation; // eg if anim = 40, this will stay at 39 until this line
                player.itemTime = bow.useAnimation;

                //shaky bow and arrow if near release time
                if (Projectile.timeLeft < bow.useAnimation + bow.useTime * 5)
                {
                    Projectile.position += new Vector2(Main.rand.Next(-6, 7) / 6f, Main.rand.Next(-6, 7) / 6f);
                    arrow.position += new Vector2(Main.rand.Next(-6, 7) / 6f, Main.rand.Next(-6, 7) / 6f);
                }

                //charge up bow - faster charge based on item's useTime
                if (Projectile.ai[1] < 3)
                {
                    if (chargeTick >= bow.useTime)
                    {
                        chargeTick = 0;
                        Projectile.ai[1]++;
                    }
                    else {
                        chargeTick++;
                    }
                }
                else {
                    if (chargeTick == 0)
                    {
                        Projectile.ai[1] = 3;//failsafe
                        fullPower = true;
                        arrow.localAI[0] = 1f;
                        //beep mana sound and dust effect to indicate full charge
                        if (Projectile.owner == Main.myPlayer) SoundEngine.PlaySound(SoundID.MaxMana, arrow.position);
                        for (int i = 0; i < 4; i++)
                        {
                            int dustIndex = Dust.NewDust(arrow.position, arrow.width, arrow.height, 43, 0f, 0f, 100, Color.Transparent, 0.6f);
                            Main.dust[dustIndex].velocity *= 0.1f;
                            Main.dust[dustIndex].fadeIn = 0.8f;
                        }
                    }
                    if (chargeTick > 15)
                    {
                        multSuperDamage = 1f;
                    }
                    chargeTick++;
                }
            }
            else {//either player is firing or time has run out - either way
                if (!released)
                {//run on first tick of releasing
                    arrow.position = player.position + new Vector2((player.width - arrow.width) / 2, (player.height - arrow.height) / 2); // normal firing position
                                                                                                                                          //speed up arrow
                    arrowSpeed = (float)(arrowSpeed * (1f + multSpeed * ((int)Projectile.ai[1])));
                    arrow.velocity = new Vector2(arrowSpeed * itemRotCos, arrowSpeed * itemRotSin);
                    if (arrow.Name.Contains("Greatarrow"))
                    {
                        arrow.velocity *= 1.8f;
                    }
                    //increased damage and knockback
                    arrowDamage = (int)(arrowDamage * (1f + multDamage * ((int)Projectile.ai[1])) * multSuperDamage);
                    arrow.damage = arrowDamage;
                    arrow.knockBack *= 1f + multKnockback * ((int)Projectile.ai[1]);
                    //re-enable collision and such
                    arrow.tileCollide = arrowCollide;
                    //arrow.hurtsTiles = arrowHurtTile;

                    if (Projectile.owner == Main.myPlayer) arrow.netUpdate = true;

                    //pew sound and replace frame
                    SoundEngine.PlaySound(SoundID.Item5, Projectile.position);
                    Projectile.frame = 4;
                    //hold bow for additional bow use time
                    Projectile.timeLeft = bow.useAnimation;
                    /*
                    if(Main.netMode == 1){//arrow stuff
                        NetMessage.SendModData(ModWorld.ModIndex,1,-1,-1, projectile.owner, arrow.whoAmI,arrow.position.X,arrow.position.Y,arrow.velocity.X,arrow.velocity.Y);
                    }
                    */
                    if (multSuperDamage != 1 && fullPower)
                    {
                        float spread = 45f * 0.0174f;
                        float baseSpeed = 17;
                        double startAngle = Math.Atan2(arrow.velocity.X, arrow.velocity.Y) - spread / 2;
                        double deltaAngle = spread / 4f;
                        double offsetAngle;
                        int i;
                        for (i = 0; i < 4; i++)
                        {
                            offsetAngle = startAngle + deltaAngle * i;
                            int p = Projectile.NewProjectile(arrow.position.X, arrow.position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ProjectileID.SeedlerNut, (int)(arrow.damage * .4), arrow.knockBack / 4, arrow.owner);
                            Main.projectile[p].scale = .7f;
                            Main.projectile[p].timeLeft += Main.rand.Next(-30, 31);
                        }
                        for (i = 0; i < 20; i++)
                        {
                            int dustIndex = Dust.NewDust(arrow.position, arrow.width, arrow.height, 2, arrow.velocity.X * (1.5f - i / 25f), arrow.velocity.Y * (1.5f - i / 25f), 100, default(Color), 0.8f);
                            Main.dust[dustIndex].noLight = true;
                        }
                    }
                    released = true;
                }
                else {//run until bow is removed
                      //add trail effect to fully charged arrows
                    if (fullPower && arrow.active)
                    {
                        int dustIndex = Dust.NewDust(
                            arrow.position,
                            arrow.width, arrow.height,
                            3,
                            arrow.velocity.X, arrow.velocity.Y,
                            45, Color.Transparent, 0.9f
                        );
                        if (multSuperDamage != 1 && fullPower)
                        {
                            float rotToTarget = Main.rand.Next((int)(-Math.PI * 10000), (int)(Math.PI * 10000)) / 10000f;
                            Vector2 arrowCentre = arrow.position + new Vector2(arrow.width / 2, arrow.height / 2);
                            Vector2 spawnPosition = arrowCentre + new Vector2((float)(30 * Math.Cos(rotToTarget)), (float)(30 * Math.Sin(rotToTarget)));
                            dustIndex = Dust.NewDust(spawnPosition, 0, 0, 3, -10 * (float)Math.Cos(rotToTarget), -10 * (float)Math.Sin(rotToTarget), 0, Color.Transparent, 0.9f);
                            Main.dust[dustIndex].velocity /= 12f;
                            Main.dust[dustIndex].fadeIn = 0.3f;
                        }
                        Main.dust[dustIndex].velocity *= -0.1f;
                    }
                    if (player.itemAnimation == 0) Projectile.Kill();
                }
            }
        }
    }
}