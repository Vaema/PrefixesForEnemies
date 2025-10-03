using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier2
{
    [AutoloadEquip(EquipType.Body)]
    public class EmbellishedRobe : ModItem
    {

        public override void SetDefaults()
        {

            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = 3;
            Item.defense = 5;

        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Embellished Robe");
      // Tooltip.SetDefault("+4% magic damage and crit");
    }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += .04f;
            player.GetCritChance(DamageClass.Magic) += 4;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == Mod.Find<ModItem>("EmbellishedHood").Type && legs.type == Mod.Find<ModItem>("EmbellishedShoes").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "50% faster charge regeneration";
            ((MPlayer)player.GetModPlayer(Mod, "MPlayer")).embellishedRegen = true;
        }

        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }*/
    }
}
