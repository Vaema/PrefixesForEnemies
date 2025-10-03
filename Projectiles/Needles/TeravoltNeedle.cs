using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using EnemyMods.NPCs;

namespace EnemyMods.Projectiles.Needles
{
    public class TeravoltNeedle : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.aiStyle = 0;
            Projectile.extraUpdates = 1;
            Projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Teravolt Needle");
        }
        public override void AI()
        {
            Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(8) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0, 0, 100, default(Color), 0.6f);
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            gNPC info = target.GetGlobalNPC<gNPC>();
            info.numNeedlesTeravolt++;
            if (info.numNeedlesTeravolt >= 6)
            {
                info.numNeedlesTeravolt = 0;
                if (!target.boss)
                {
                    if(target.realLife != -1)
                    {
                        target = Main.npc[target.realLife];
                    }
                    target.AddBuff(Mod.Find<ModBuff>("Stunned").Type, 40);
                }
                damage += (int)(180 * Main.player[Projectile.owner].GetDamage(DamageClass.Throwing) + Math.Min(250, target.lifeMax * .08));
                SoundEngine.PlaySound(SoundID.Item93, Projectile.position);
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0, 0, 100, default(Color), 0.6f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
