using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ChannelManager : MonoBehaviour
{
    public Dictionary<Guid, string> channels;
    public Dictionary<NetworkConnectionToClient, Guid> players;
    
    [ServerCallback] 
    public void CreateMatch(string name){
        Guid newMatchId = Guid.NewGuid();
        channels.Add(newMatchId, name);
    }
    [ServerCallback]
    public void JoinMatch(NetworkConnectionToClient player, Guid matchId){
        player.identity.GetComponent<NetworkMatch>().matchId = matchId;
        players.Add(player, matchId);
    }
}