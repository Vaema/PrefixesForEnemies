using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class CrystalGreatarrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 7;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 2400;
            Projectile.maxPenetrate = 3;
            Projectile.penetrate = 3;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crystal Greatarrow");
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.localAI[0] == 1f && Projectile.owner == Main.myPlayer)
            {
                for (int num385 = 0; num385 < 3; num385++)
                {
                    float num386 = -Projectile.velocity.X * Main.rand.Next(30, 60) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    float num387 = -Projectile.velocity.Y * Main.rand.Next(30, 60) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    Projectile.NewProjectile(Projectile.position.X + num386, Projectile.position.Y + num387, num386, num387, 90, (int)(Projectile.damage * 0.5), 0f, Projectile.owner, 0f, 0f);
                }
                Projectile.velocity = Projectile.velocity * .95f;
                Projectile.damage = (int)(Projectile.damage * .95);

            }
        }
        public override void OnKill(int timeLeft)
        {
            if(Projectile.owner == Main.myPlayer)
            {
                for (int num385 = 0; num385 < 5; num385++)
                {
                    float num386 = -Projectile.velocity.X * Main.rand.Next(30, 60) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    float num387 = -Projectile.velocity.Y * Main.rand.Next(30, 60) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    Projectile.NewProjectile(Projectile.position.X + num386, Projectile.position.Y + num387, num386, num387, 90, (int)(Projectile.damage * 0.5), 0f, Projectile.owner, 0f, 0f);
                }
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 68, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !Projectile.noDropItem)
            {
                Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("CrystalGreatarrow").Type, 1, false, 0, false, false);
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