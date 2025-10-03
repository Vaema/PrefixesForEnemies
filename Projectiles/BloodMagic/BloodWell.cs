using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections;

namespace EnemyMods.Projectiles.BloodMagic
{
    public class BloodWell : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1f;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Well");
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            int b = player.FindBuffIndex(Mod.Find<ModBuff>("BloodWell").Type);
            if (player.dead)
            {
                modPlayer.bloodWell = false;
            }
            if (modPlayer.bloodWell)
            {
                Projectile.timeLeft = 2;
            }
            Projectile.localAI[0]++;//drain counter
            if(Projectile.localAI[0] >= 60)
            {
                int dam = (int)(10 * player.GetDamage(DamageClass.Summon));
                ArrayList drainNPCs = getNPCsInRange(Projectile, 300);
                for(int i=0; i<drainNPCs.Count; i++) //(NPC npcD in drainNPCs)
                {
                    NPC npcD = (NPC)drainNPCs[i];
                    npcD.life -= (int)(dam * ((npcD.FindBuffIndex(Mod.Find<ModBuff>("Bloodied").Type)>=0) ? 1.5 : 1));//drain 50% more if bloodied
                    CombatText.NewText(new Rectangle((int)npcD.position.X, (int)npcD.position.Y - 30, npcD.width, npcD.height), CombatText.DamagedHostile, "" + (int)(dam * ((npcD.FindBuffIndex(Mod.Find<ModBuff>("Bloodied").Type) >= 0) ? 1.5 : 1)));
                    if (npcD.realLife != -1) { npcD = Main.npc[npcD.realLife]; }
                    if (npcD.life <= 0)
                    {
                        npcD.life = 1;
                        if (Main.netMode != 1)
                        {
                            npcD.StrikeNPC(9999, 0f, 0, false, false);
                            if (Main.netMode == 2) { NetMessage.SendData(28, -1, -1, null, npcD.whoAmI, 9999f, 0f, 0f); }
                        }
                    }
                    int d = Dust.NewDust(npcD.position, (npcD.width), (npcD.height), 5, 0, 0, 0, default(Color), 3.5f);
                    Main.dust[d].velocity = (Projectile.Center - npcD.Center)/9.8f;
                    Main.dust[d].noGravity = true;
                    
                    if (b>=0)
                    player.buffTime[b] += 30* (int)(dam * ((npcD.FindBuffIndex(Mod.Find<ModBuff>("Bloodied").Type) >= 0) ? 1.5 : 1));//buff timer keeps track of size
                }
                Projectile.localAI[0] = 0;
            }

            if(Projectile.ai[0] == 0f)//hovering over player
            {
                Projectile.position = new Vector2(player.position.X, player.position.Y - 80 - player.buffTime[b] / 180);
                Projectile.tileCollide = false;
            }
            if(Projectile.ai[1] >= 0f)//move command issued
            {
                Projectile.ai[1]--;
                Projectile.tileCollide = true;
            }
            if(Projectile.ai[1] <= 0f)//move command complete
            {
                Projectile.velocity = Vector2.Zero;
            }
            if (Projectile.localAI[1] > 0)//persistent command
            {

            }
            Projectile.scale = 1 + player.buffTime[b] / 2400f;
            //projectile.localAI[1] = Math.Max(2, projectile.localAI[1] - 1);
            //projectile.localAI[1] = Math.Min(10800, projectile.localAI[1]);

            if (Main.rand.Next(0, 8) == 0 || Main.rand.Next (0, 100) < player.buffTime[b] / 100)
            {
                Dust.NewDust(Projectile.position, (Projectile.width), (Projectile.height), 5);
                if(Main.rand.Next(0, 3) == 0)
                {
                    Dust.NewDust(Projectile.position, (Projectile.width), (Projectile.height), 60);
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }
        private ArrayList getNPCsInRange(Projectile focus, int distance)
        {
            ArrayList NPCsInRange = new ArrayList();
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.realLife != -1)
                {
                    npc = Main.npc[npc.realLife];
                }
                if (npc.Distance(focus.Center) < distance && npc.aiStyle != 7 && !(npc.catchItem > 0) && npc.type != 401 && npc.type != 488 && npc.life > 0 && npc.active)
                {
                    NPCsInRange.Add(npc);
                }
            }
            return NPCsInRange;
        }
    }
}
