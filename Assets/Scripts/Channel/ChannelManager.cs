using System;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class ChannelManager : NetworkBehaviour
{
    public static ChannelManager instance;

    public static Dictionary<Guid, string> channels;
    public static Dictionary<NetworkConnectionToClient, Guid> players;
    public static Guid localMatch;
    public GameObject testObject;
    
    void Start(){
        instance = this;
    }

    public void CreateMatch(string name){
        Guid newMatchId = Guid.NewGuid();
        channels.Add(newMatchId, name);
        localMatch = newMatchId;
    }

    public void JoinMatch(NetworkConnectionToClient player, Guid matchId){
        player.identity.GetComponent<NetworkMatch>().matchId = matchId;
        players.Add(player, matchId);
    }

    public void TestObject(Guid matchId){
        GameObject o = Instantiate(testObject, PlayerList.instance.transform);
        o.GetComponent<NetworkMatch>().matchId = matchId;
        o.GetComponent<TMP_Text>().text = matchId.ToString();
    }
}