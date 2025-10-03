using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier5
{
    public class BansheeHowlRing : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 222;
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
            Item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Banshee Howl Ring");
      // Tooltip.SetDefault("The weak perish. One charge.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            if (play.charges[11] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            SoundEngine.PlaySound(SoundLoader.customSoundType, player.position, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Scream"));
            for (int i = 0; i < 100; i++)
            {
                NPC npc = Main.npc[i];
                float distance = (float)Math.Sqrt((npc.Center.X - player.Center.X) * (npc.Center.X - player.Center.X) + (npc.Center.Y - player.Center.Y) * (npc.Center.Y - player.Center.Y));
                if (distance < 800 && !npc.townNPC)
                {
                    if (npc.life <= Item.damage*player.GetDamage(DamageClass.Magic) && !npc.dontTakeDamage && npc.type != NPCID.DD2LanePortal)
                    {
                        npc.StrikeNPC(9999, 0f, 0, false, false);
                        NetMessage.SendData(28, -1, -1, null, npc.whoAmI, 9999, 0, 0, 0);
                    }
                }
            }
            MPlayer play = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            play.charges[11]--;
            if (play.cooldowns[11] == -1)
            {
                play.cooldowns[11] = play.maxCooldowns[11];
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("DeathKnellRing").Type, 1);
            recipe.AddIngredient(Mod.Find<ModItem>("RubyTicket").Type, 3);
            recipe.Register();
        }
    }
}
