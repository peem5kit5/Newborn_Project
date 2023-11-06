using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Guild
{
    public GuildType Guilds;
    public enum GuildType
    {
        Mercenary,
        Worshipper,
        Assasins,
        Merchant,
        Philosopher,
        Ward,
        Biochemist
    }
    

    
    public void ChangingGuild(GuildType _guildType)
    {
        Guilds = _guildType;
    }


}
