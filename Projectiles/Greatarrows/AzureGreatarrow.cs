using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class AzureGreatarrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 7;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 2400;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Azure Greatarrow");
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 2f)
            {
                Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.4f, 0.4f, 0.9f);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.localAI[0] == 1f)
            {
                Projectile.aiStyle = 0;
                Projectile.velocity = Vector2.Zero;
                Projectile.localAI[0] = 2f;
                Projectile.timeLeft = 60;
                Projectile.penetrate = 1;
                return false;
            }
            if (Projectile.localAI[0] == 2f)
            {
                Projectile.velocity = Vector2.Zero;
                return false;
            }
                return true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            if(Projectile.localAI[0] != 2f)
                for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, 172, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && Projectile.localAI[0] != 2f && !Projectile.noDropItem)
            {
                Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("AzureGreatarrow").Type, 1, false, 0, false, false);
            }
            if (Projectile.localAI[0] == 2f && timeLeft < 2)
            {
                for (int num259 = 0; num259 < 40; num259++)
                {
                    int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 172, 0f, 0f, 0, default(Color), 2f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity.Normalize();
                    Main.dust[d].velocity *= 8;
                }
                Projectile.position = Projectile.Center;
                Projectile.width = (Projectile.height = 160);
                Projectile.Center = Projectile.position;
                Projectile.penetrate = -1;
                Projectile.damage = (int)(Projectile.damage * 1.4);
                Projectile.Damage();
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