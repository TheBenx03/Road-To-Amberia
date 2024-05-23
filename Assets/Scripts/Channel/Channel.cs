using UnityEngine;
using Mirror;

public class Channel : NetworkBehaviour
{
    [SyncVar] public string name;
    [SyncVar] public string description;
    [SyncVar] public string id;

    
}