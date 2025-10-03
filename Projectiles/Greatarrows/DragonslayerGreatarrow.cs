using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class DragonslayerGreatarrow : ModProjectile
    {
        private bool noApply = false;
        public override void SetDefaults()
        {
            Projectile.width = 11;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 2400;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragonslayer Greatarrow");
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.localAI[0] == 1f && !noApply)
            {
                target.AddBuff(Mod.Find<ModBuff>("Dragonslay").Type, 300);
            }
            noApply = false;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            //apply buff OR clear buff and add damage
            if (Projectile.localAI[0] == 1f)
            {
                if (target.FindBuffIndex(Mod.Find<ModBuff>("Dragonslay").Type) >= 0)
                {
                    damage += (int)(Math.Min(target.lifeMax / 20, 500) * Main.player[Projectile.owner].GetDamage(DamageClass.Ranged));
                    for(int i=0; i<30; i++)
                    {
                        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 183, 0f, 0f, 0, default(Color), 1.5f);
                    }
                    target.DelBuff(target.FindBuffIndex(Mod.Find<ModBuff>("Dragonslay").Type));
                    noApply = true;
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 7, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !Projectile.noDropItem)
            {
                Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("DragonslayerGreatarrow").Type, 1, false, 0, false, false);
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