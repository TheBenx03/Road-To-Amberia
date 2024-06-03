using UnityEngine;
using Mirror;

public class Channel : NetworkBehaviour
{
    [SyncVar] private readonly string cname;
    [SyncVar] public string description;
    [SyncVar] public string id;

    
}