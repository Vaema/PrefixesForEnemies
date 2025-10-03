using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class WrathfulGreatarrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 7;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 2400;
            Projectile.maxPenetrate = 10;
            Projectile.penetrate = 10;
            Projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wrathful Greatarrow");
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(Projectile.localAI[0]==1f)
            Projectile.ghostHurt(Projectile.damage/ (crit ? 1:2), target.Center);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            crit = false;
            if (Main.rand.Next(0, 100) < Main.player[Projectile.owner].GetCritChance(DamageClass.Ranged))
            {
                crit = true;
            }
        }
        public override void AI()
        {
        }
        public override void OnKill(int timeLeft)
        {
            if (Projectile.localAI[0] == 1f)
            {
                for (int num385 = 0; num385 < 2; num385++)
                {
                    Projectile.ghostHurt(Projectile.damage / 2, Projectile.position);
                }
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.AncientLight, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !Projectile.noDropItem)
            {
                Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("WrathfulGreatarrow").Type, 1, false, 0, false, false);
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