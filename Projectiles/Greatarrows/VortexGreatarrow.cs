using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class VortexGreatarrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 7;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 2400;
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vortex Greatarrow");
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.localAI[0] == 1f)
            {
                Projectile.damage = (int)(Projectile.damage * 1.1);
                Projectile.localAI[1]++;
                if (!target.boss)
                {
                    if(target.realLife != -1)
                    {
                        NPC realTarget = Main.npc[target.realLife];
                        if (!realTarget.boss)
                        {
                            realTarget.AddBuff(Mod.Find<ModBuff>("Suspended").Type, 90 + (int)Projectile.localAI[1] * 30);
                        }
                    }
                    else
                        target.AddBuff(Mod.Find<ModBuff>("Suspended").Type, 90 + (int)Projectile.localAI[1]*30);
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.boss)
            {
                damage = (int)(damage * 1.3);
            }
        }
        public override void AI()
        {
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.7f, 0.9f, 0.9f);
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !Projectile.noDropItem)
            {
                Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("VortexGreatarrow").Type, 1, false, 0, false, false);
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.localAI[0]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[0] = (float)reader.ReadDouble();
        }
    }
}