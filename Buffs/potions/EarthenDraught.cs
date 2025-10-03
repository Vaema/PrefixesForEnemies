using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Buffs.potions
{
    public class EarthenDraught : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Earthen Draught");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(Mod, "MPlayer");
            modPlayer.earthenDraught = true;
        }
    }
}
