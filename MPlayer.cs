using EnemyMods.NPCs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace EnemyMods
{
    public class MPlayer : ModPlayer
    {
        public bool embellishedRegen = false;
        public bool fireSpirit = false;
        public bool waterSpirit = false;
        public bool soulMinion = false;
        public bool gunTurret = false;
        public bool bowTurret = false;
        public bool rocketTurret = false;
        public bool shotgunTurret = false;
        public bool chargeBangle = false;
        public bool moonIdol = false;
        public bool dyingStar = false;
        public bool undying = false;
        public bool bloodWell = false;
        public bool killWell = true;
        public int bloodArmor = 0;
        public bool reducedCounterCD = false;
        public bool increasedCounterLength = false;
        public bool counterPlus = false;
        public int gunbladeMeleeDebuff = 0;
        public int gunbladeRangedDebuff = 0;
        public int voidTargetDamage = 0;
        public int voidBurn = 0;
        public float voidAffinity = 1f;
        public int typeNeedles = 0;
        public int needleTime = 0;
        public int needleDamage = 0;
        //potions
        public bool duelistDraught = false;
        public int duelistTarget = 0;
        public bool tenacity = false;
        public int tenacityLifeCount = 0;
        public bool shockTonic = false;
        public float shockCharge = 0;
        public bool flightElixir = false;
        public bool earthenDraught = false;
        public bool steelElixir = false;
        public int steelDefense = 0;
        public int steelTime = 0;
        public bool reconstruction = false;
        public float reconstructionHeal = 0;
        public int reconstructionTime = 0;
        public bool battleDance = false;
        public int battleDanceDamage = 0;
        //essences
        public bool vengeful = false;
        //cooldowns and charges by index of spell, by tier then alphabetically
        public int[] cooldowns = new int[] { -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2 };
        public int[] charges = new int[19];
        private int[] maxCharges = new int[] {2, 2, 1, 1, 1, 2, 1, 2, 2, 2, 2, 1, 1, 1, 1, 2, 1, 1, 2};
        public int[] maxCooldowns = new int[] {600, 900, 1500, 1500, 2100, 900, 1500, 480, 720, 1500, 600, 2100, 1800, 1500, 1800, 720, 1200, 1800, 480};
        private string[] chargeText = new string[] {"Ice Shard", "Scourge", "Acid Rain", "Razorwind", "Death Knell", "Deep Scourge", "Fire Rain", "Lightning", "Light Spear", "Shadowflame", "Shattershard", "Banshee Howl", "Petalstorm", "Soul Well", "Undying", "Holy Lance", "Nebula Parasite", "Solar Shower", "Vortex Lightning" };
        private bool[] noText = new bool[19];

        public override void Initialize()
        {
            cooldowns = new int[] { -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2 };
            charges = new int[19];
            noText = new bool[19];
        }

        public override void ResetEffects()
        {
            gunTurret = false;
            bowTurret = false;
            rocketTurret = false;
            shotgunTurret = false;
            embellishedRegen = false;
            fireSpirit = false;
            waterSpirit = false;
            soulMinion = false;
            chargeBangle = false;
            moonIdol = false;
            dyingStar = false;
            undying = false;
            killWell = true;
            reducedCounterCD = false;
            increasedCounterLength = false;
            counterPlus = false;
            gunbladeMeleeDebuff = 0;
            gunbladeRangedDebuff = 0;
            //potions
            duelistDraught = false;
            tenacity = false;
            tenacityLifeCount = Player.lifeRegenTime;
            shockTonic = false;
            flightElixir = false;
            earthenDraught = false;
            steelElixir = false;
            reconstruction = false;
            battleDance = false;
            //essences
            vengeful = false;
        }
        public override void PreUpdate()
        {
        }
        public override void PostHurt(Player.HurtInfo info)
        {
            int b = Player.FindBuffIndex(Mod.Find<ModBuff>("BloodWell").Type);
            if(b >= 0)
            {
                Player.buffTime[b] += (int)(damage * 30);
            }
            if (tenacity)
            {
                Player.lifeRegenTime = tenacityLifeCount - 300;
                if (Player.lifeRegenTime < 0)
                {
                    Player.lifeRegenTime = 0;
                }
            }
            if (steelElixir)
            {
                steelDefense = Math.Min(20, steelDefense + 4);
                steelTime = 900;
            }
            if (reconstruction)
            {
                reconstructionHeal = (float)damage;
                reconstructionTime = 600;
            }
            battleDanceDamage = 0;
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (undying)
            {
                Player.statLife = 1;
                CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 50, Player.width, Player.height), new Color(255, 100, 100, 255), "UNDYING");
                return false;
            }
            return true;
        }
        public override void PreUpdateBuffs()
        {
            steelTime--;
            if (steelTime <= 0)
            {
                steelTime = 0;
                steelDefense = 0;
            }
            reconstructionTime--;
            if (reconstructionTime <= 0)
            {
                reconstructionTime = 0;
            }
            for (int k = 3; k < 8 + Player.extraAccessorySlots; k++)
            {
                if (Player.armor[k].type == Mod.Find<ModItem>("ChargeBangle").Type)
                {
                    chargeBangle = true;
                }
                if (Player.armor[k].type == Mod.Find<ModItem>("MoonIdol").Type)
                {
                    moonIdol = true;
                }
                if (Player.armor[k].type == Mod.Find<ModItem>("BloodMagePact").Type)
                {
                    killWell = false;
                }
            }
            if (killWell)
            {
                int p = Player.FindBuffIndex(Mod.Find<ModBuff>("BloodWell").Type);
                if(p >= 0)
                {
                    Player.DelBuff(p);
                    p--;
                }
            }
        }
        public override void PostUpdateBuffs()
        {
            if(battleDance && battleDanceDamage >= 20)
            {
                Player.AddBuff(BuffID.Hunter, 2);
            }
        }
        public override void PostUpdateEquips()
        {
            Player.statDefense += steelDefense;
            Player.GetDamage(DamageClass.Melee) += (battleDanceDamage / 100f);
            Player.GetDamage(DamageClass.Ranged) += (battleDanceDamage / 100f);
            Player.GetDamage(DamageClass.Magic) += (battleDanceDamage / 100f);
            Player.GetDamage(DamageClass.Throwing) += (battleDanceDamage / 100f);
            Player.GetDamage(DamageClass.Summon) += (battleDanceDamage / 100f);
            if (Player.FindBuffIndex(Mod.Find<ModBuff>("BloodArmor").Type) == -1)
            {
                bloodArmor = 0;
            }
            if (bloodArmor > 0)
            {
                Player.endurance += .1f;
                Player.statDefense += bloodArmor;
            }
            //throw charged needles
            if(needleTime > 0)
            {
                if (needleTime % 3 == 0)
                {
                    Vector2 vel = Main.MouseWorld - Player.Center;
                    vel.Normalize();
                    vel *= 12 * Player.ThrownVelocity;
                    int p = Projectile.NewProjectile(Player.Center.X, Player.Center.Y, vel.X, vel.Y, typeNeedles, needleDamage, .5f, Player.whoAmI);
                    SoundEngine.PlaySound(SoundID.Item1, Player.position);
                }
                needleTime--;
            }
            //decrement cooldowns and add charges
            for (int i=0; i<cooldowns.Length; i++)
            {
                if (cooldowns[i] == -2)
                {
                    charges[i] = maxCharges[i];
                    cooldowns[i] = -1;
                }
                if (maxCharges[i] == 1)
                {
                    if (cooldowns[i] == -1 && charges[i] < maxCharges[i])
                    {
                        cooldowns[i] = maxCooldowns[i];
                        noText[i] = true;
                    }
                }
                else if (cooldowns[i] == -1 && charges[i] < maxCharges[i] + (chargeBangle ? 1 : 0))
                {
                    cooldowns[i] = maxCooldowns[i];
                    noText[i] = true;
                }
                if (cooldowns[i] == 0)
                {
                    charges[i]++;
                    if (!noText[i])
                    {
                        if(maxCharges[i]!=1)
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 50, Player.width, Player.height), new Color(255, 255, 255, 255), chargeText[i] + " " + charges[i] + "/" + (maxCharges[i] + (chargeBangle ? 1 : 0)));
                        else
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 50, Player.width, Player.height), new Color(255, 255, 255, 255), chargeText[i] + " " + charges[i] + "/" + maxCharges[i]);
                    }
                    noText[i] = false;
                    if (charges[i] >= maxCharges[i] && (maxCharges[i] == 1 || charges[i] == maxCharges[i] + (chargeBangle ? 1 : 0)))
                    {
                        cooldowns[i] = -1;
                    }
                    else
                    {
                        cooldowns[i] = maxCooldowns[i];
                    }
                }
                if (cooldowns[i] > 0)
                {
                    cooldowns[i]--;
                    if(embellishedRegen && cooldowns[i] % 2 == 1)
                    {
                        cooldowns[i]--;
                    }
                }
                if(!chargeBangle && charges[i] > maxCharges[i])
                {
                    charges[i]--;
                }
            }
        }
        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Item, consider using ModifyHitNPC instead */
        {
            if (moonIdol && crit && Main.rand.Next(0, 5)==0)
            {
                damage *= 2;
            }
            if(gunbladeMeleeDebuff>0 && item.CountsAsClass(DamageClass.Melee) && crit)
            {
                gNPC info = target.GetGlobalNPC<gNPC>();
                info.gunbladeMeleeDebuff = gunbladeMeleeDebuff;
                info.gunbladeMeleeDebuffTime = 180;
            }
            if (duelistDraught)
            {
                if(duelistTarget == target.whoAmI)
                {
                    damage = (int)(damage * 1.1);
                }
                duelistTarget = target.whoAmI;
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Projectile, consider using ModifyHitNPC instead */
        {
            if (moonIdol && crit && Main.rand.Next(0, 5) == 0)
            {
                damage *= 2;
            }
            if (gunbladeRangedDebuff > 0 && proj.CountsAsClass(DamageClass.Ranged) && crit)
            {
                gNPC info = target.GetGlobalNPC<gNPC>();
                info.gunbladeRangedDebuff = gunbladeRangedDebuff;
                info.gunbladeRangedDebuffTime = 180;
            }
            if (duelistDraught)
            {
                if (duelistTarget == target.whoAmI)
                {
                    damage = (int)(damage * 1.1);
                }
                duelistTarget = target.whoAmI;
            }
        }
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (duelistDraught)
            {
                if (duelistTarget == npc.whoAmI)
                {
                    damage = (int)(damage * .9);
                }
            }
            if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceRapier").Type) >= 0)
            {
                Player.AddBuff(Mod.Find<ModBuff>("Counter").Type, 300);
                damage = 0;
                Player.immune = true;
                Player.immuneTime = 60;
            }
            else if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceEstoc").Type) >= 0)
            {
                Player.AddBuff(Mod.Find<ModBuff>("Counter").Type, 300);
                damage = 0;
                Player.immune = true;
                Player.immuneTime = 60;
            }
            else if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceEpee").Type) >= 0 || Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceEpee2").Type) >= 0 || Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceEpee3").Type) >= 0)
            {
                if(npc.aiStyle == 9)// this style behaves like a projectile, but can't be reflected, so we kill it instead
                {
                    npc.StrikeNPC(999, 0, 0);
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, 999, 0, 0, 0);
                }
                damage = 0;
                Player.immune = true;
                Player.immuneTime = 60;
            }
            else if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceFoil").Type) >= 0)
            {
                int direction = (Player.position.X >= npc.position.X) ? -1 : 1;
                int dam = (int)(10 * Player.GetDamage(DamageClass.Melee) * (Main.expertMode ? .7 : 1)) + damage;
                dam = (int)(dam * Main.rand.Next(90, 111)/100.0);
                if (counterPlus)
                {
                    dam = (int)(dam * 1.2);
                }
                npc.StrikeNPC(dam, 10, direction, true);
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, dam, 10, direction, 1);
                Player.addDPS(2 * dam);
                Player.immune = true;
                Player.immuneTime = 60;
                damage = 0;
            }
            else if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceFoil2").Type) >= 0)
            {
                int direction = (Player.position.X >= npc.position.X) ? -1 : 1;
                int dam = (int)(52 * Player.GetDamage(DamageClass.Melee) * (Main.expertMode ? .7 : 1)) + (damage*3)/2;
                dam = (int)(dam * Main.rand.Next(90, 111) / 100.0);
                if (counterPlus)
                {
                    dam = (int)(dam * 1.2);
                }
                npc.StrikeNPC(dam, 10, direction, true);
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, dam, 10, direction, 1);
                Player.addDPS(2 * dam);
                Player.immune = true;
                Player.immuneTime = 60;
                damage = 0;

                npc.AddBuff(BuffID.CursedInferno, 600);
                for(int i=0; i<12; i++)
                {
                    int d = Dust.NewDust(Player.position, 38, 38, 75);
                    Main.dust[d].velocity *= 3;
                    Main.dust[d].scale = 1.3f;
                }
            }
            else if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceFoil3").Type) >= 0)
            {
                int dam = (int)(80 * Player.GetDamage(DamageClass.Melee)) + (int)(damage* 2.25 * (Main.expertMode ? .7 : 1));
                if (npc.boss || (npc.aiStyle == 6 && !npc.FullName.Contains("Head")))
                {
                    dam = (int)(dam*1.6);//bonus damage to compensate for no stun
                }
                else
                {
                    npc.AddBuff(Mod.Find<ModBuff>("Stunned").Type, 180);
                }
                dam = (int)(dam * Main.rand.Next(90, 111) / 100.0);
                if (counterPlus)
                {
                    dam = (int)(dam * 1.2);
                }
                npc.StrikeNPC(dam, 10, 0, true);
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, dam, 10, 0, 1);
                Player.addDPS(2 * dam);
                Player.immune = true;
                Player.immuneTime = 60;
                damage = 0;
            }
            else if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceFoil4").Type) >= 0)
            {
                int direction = (Player.position.X >= npc.position.X) ? -1 : 1;
                int dam = (int)(140 * Player.GetDamage(DamageClass.Melee) * (Main.expertMode ? .7f : 1)) + damage * 3;
                if (npc.boss || (npc.aiStyle == 6 && !npc.FullName.Contains("Head")))
                {
                    dam = (int)(dam * 2);
                }
                else
                {
                    npc.AddBuff(Mod.Find<ModBuff>("Suspended").Type, 180);
                }
                dam = (int)(dam * Main.rand.Next(90, 111) / 100.0);
                if (counterPlus)
                {
                    dam = (int)(dam * 1.2);
                }
                npc.StrikeNPC(dam, 10, direction, true);
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, dam, 10, direction, 1);
                Player.addDPS(2 * dam);
                Player.immune = true;
                Player.immuneTime = 60;
                damage = 0;
            }
        }
        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceRapier").Type) >= 0)
            {
                Player.AddBuff(Mod.Find<ModBuff>("Counter").Type, 600);
                damage = 0;
                Player.immune = true;
                Player.immuneTime = 60;
            }
            else if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceEstoc").Type) >= 0)
            {
                Player.AddBuff(Mod.Find<ModBuff>("Counter").Type, 600);
                damage = 0;
                Player.immune = true;
                Player.immuneTime = 60;
            }
            else if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceEpee").Type) >= 0)
            {
                //reflects projectile, generally makes it damage eneimes and ignore players
                proj.velocity = -proj.velocity;
                proj.friendly = true;
                proj.hostile = false;
                proj.damage = (int)((proj.damage+10) * 4 * Player.GetDamage(DamageClass.Melee));
                if (counterPlus)
                {
                    proj.damage = (int)(proj.damage * 1.2);
                }
                damage = 0;
                //leaves the player with very brief immunity so they can reflect further projectiles
                Player.immune = true;
                Player.immuneTime = 6;
            }
            else if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceEpee2").Type) >= 0)
            {
                proj.velocity = -proj.velocity * 1.5f;
                proj.friendly = true;
                proj.hostile = false;
                proj.damage = (int)((proj.damage+20) * 6 * Player.GetDamage(DamageClass.Melee));
                if (counterPlus)
                {
                    proj.damage = (int)(proj.damage * 1.2);
                }
                Player.buffTime[Player.FindBuffIndex(Mod.Find<ModBuff>("CounterCooldown").Type)] -= 30;//reduces counter cooldown by .5 sec for each projectile reflected
                damage = 0;
                Player.immune = true;
                Player.immuneTime = 6;
            }
            else if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceEpee3").Type) >= 0)
            {
                proj.velocity = -proj.velocity * 2f;
                proj.friendly = true;
                proj.hostile = false;
                proj.damage = (int)((proj.damage+30) * 8 * Player.GetDamage(DamageClass.Melee));
                if (counterPlus)
                {
                    proj.damage = (int)(proj.damage * 1.2);
                }
                Player.buffTime[Player.FindBuffIndex(Mod.Find<ModBuff>("CounterCooldown").Type)] -= 60;
                for (int i = 0; i < 12; i++)
                {
                    int d = Dust.NewDust(Player.position, 38, 38, 135, proj.velocity.X, proj.velocity.Y);
                    Main.dust[d].velocity /= 2;
                    Main.dust[d].scale = 1.3f;
                }
                damage = 0;
                Player.immune = true;
                Player.immuneTime = 6;
            }
            else if (Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceFoil").Type) >= 0 || Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceFoil2").Type) >= 0 || Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceFoil3").Type) >= 0 || Player.FindBuffIndex(Mod.Find<ModBuff>("CounterStanceFoil4").Type) >= 0)
            {
                damage = 0;
                Player.immune = true;
                Player.immuneTime = 60;
            }
        }
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (shockTonic && shockCharge >= 100)
            {
                int dam = 30 + Player.statDefense;
                int direction = (npc.Center.X - Player.Center.X > 0) ? 1 : -1;
                npc.StrikeNPC(dam, 10, direction);
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, dam, 10, direction, 1);
                shockCharge = 0;
                SoundEngine.PlaySound(SoundID.Item66, Player.position);
            }
        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
        {
            if (shockTonic && shockCharge >= 100)
            {
                int dam = 30 + Player.statDefense;
                int direction = (target.Center.X - Player.Center.X > 0) ? 1 : -1;
                target.StrikeNPC(dam, 10, direction);
                NetMessage.SendData(28, -1, -1, null, target.whoAmI, dam, 10, direction, 1);
                shockCharge = 0;
                SoundEngine.PlaySound(SoundID.Item66, Player.position);
            }
            int index = Player.FindBuffIndex(Mod.Find<ModBuff>("VoidBurn").Type);
            if (index != -1)
            {
                Player.buffTime[index] -= (5 + damage);
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
        {
            int index = Player.FindBuffIndex(Mod.Find<ModBuff>("VoidBurn").Type);
            if(index != -1)
            {
                Player.buffTime[index] -= (5 + damage);
            }
        }
        public override void PostUpdateRunSpeeds()
        {
            if (flightElixir)
            {
                Player.accRunSpeed *= 1.3f;
                Player.wingTimeMax = (int)(Player.wingTimeMax * 1.3);
            }
            if (earthenDraught && Player.velocity.Y == 0)
            {
                Player.endurance += .15f;
            }
        }
        public override void PostUpdate()
        {
            if (shockTonic && Player.velocity.Y == 0)
            {
                shockCharge += Math.Abs(Player.velocity.X) / 9f;
            }
            if (shockCharge >= 100)
            {
                shockCharge = 100;
                if (Main.rand.Next(0, 12) == 0)
                {
                    int d = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Electric);
                }
            }
        }
        public override void UpdateLifeRegen()
        {
            if (reconstructionTime>0 && reconstructionTime%20==0)
            {
                int regen = (int)Math.Max(reconstructionHeal, 1);
                Player.lifeRegen += regen;
                if(regen > Main.rand.Next(5, 20))
                {
                    for(int i=0; i < regen/2+1; i++)
                    {
                        int d = Dust.NewDust(Player.position - new Vector2(Player.width, Player.height), Player.width * 2, Player.height * 2, DustID.SomethingRed);
                        Main.dust[d].velocity = (Player.Center - Main.dust[d].position) * .01f;
                    }
                }
            }
        }
        public override void UpdateBadLifeRegen()
        {
            if (Player.FindBuffIndex(Mod.Find<ModBuff>("VoidBurn").Type) == -1)
            {
                voidBurn = 0;
            }
            else
            {
                Player.lifeRegenTime = 0;
                if(Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegen -= voidBurn;
            }
        }
        public override void OnHitAnything(float x, float y, Entity victim)
        {
            if (battleDance)
            {
                battleDanceDamage = Math.Min(20, battleDanceDamage + 1);
            }
        }
    }
}
