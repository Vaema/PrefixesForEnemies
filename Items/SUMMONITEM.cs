using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items//be sure to change this to your modname
{
    public class SUMMONITEM:ModItem
    {
        public override void SetDefaults()
        {
            //placeholder stats, adjust as needed

            Item.damage = 100;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 26;
            Item.height = 28;

            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 9;
            Item.UseSound = SoundID.Item44;
            Item.shoot = Mod.Find<ModProjectile>("UnboundSoul").Type;
            Item.shootSpeed = 10f;
            Item.buffType = Mod.Find<ModBuff>("UnboundSoul").Type;
            Item.buffTime = 3600;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Summon Item");
      // Tooltip.SetDefault("");
    }

    }
}
