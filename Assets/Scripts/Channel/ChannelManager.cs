using System;
using Mirror;
using UnityEngine;

public class ChannelManager : MonoBehaviour
{
    
    [ServerCallback] 
    void CreateMatch(GameObject manager){
        Guid newMatchId = Guid.NewGuid();
        
    }
}