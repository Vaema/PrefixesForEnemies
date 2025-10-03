using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace EnemyMods.Items.Tier5
{
    public class UndyingRing : ModItem
    {
        int maxCharges = 1;
        int rechargeTime = 1800;
        int charges = 1;
        int rechargeCount = 0;

        //conditional charge modifiers go here

        public override void SetDefaults()
        {

            Item.DamageType = DamageClass.Magic;
            Item.width = 10;
            Item.height = 10;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 80000;
            Item.rare = 8;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Ring of the Undying");
      // Tooltip.SetDefault("Refuse death for 5 seconds. One charge.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[14] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            player.AddBuff(Mod.Find<ModBuff>("Undying").Type, (int)(5 * player.GetDamage(DamageClass.Magic)) * 60);
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[14]--;
            if (play.cooldowns[14] == -1)
            {
                play.cooldowns[14] = play.maxCooldowns[14];
            }
            return true;
        }
        /*
        public override void UpdateInventory(Player player)
        {
            item.ToolTip = "Refuse death for " + (int)(5*player.magicDamage) + " seconds. One charge.";
        }
        */
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ChoiceToken").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("RubyTicket").Type, 3);
            recipe.Register();
        }
    }
}
