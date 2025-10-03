using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class CircleSlash : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 50;
            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = 3;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Blood: Circle Slash");
      // Tooltip.SetDefault("A blade of blood performs a slash around your Well");
    }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            Projectile p = null;
            int b = player.FindBuffIndex(Mod.Find<ModBuff>("BloodWell").Type);
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].owner == player.whoAmI && Main.projectile[i].type == Mod.Find<ModProjectile>("BloodWell").Type)
                {
                    p = Main.projectile[i];
                    break;
                }
            }
            if (p == null || b == -1)
            {
                return false;
            }
            if (player.buffTime[b] <= 121)
            {
                return false;
            }
            player.buffTime[b] -= 120;
            int q = Projectile.NewProjectile(p.position.X, p.position.Y, 0, 0, Mod.Find<ModProjectile>("BloodSword").Type, (int)(Item.damage * player.GetDamage(DamageClass.Magic)), Item.knockBack, player.whoAmI, p.whoAmI);
            return true;
        }
    }
}
