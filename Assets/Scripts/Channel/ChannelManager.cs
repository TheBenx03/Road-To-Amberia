using Mirror;
using UnityEngine;

public class ChannelManager : NetworkBehaviour
{
    public GameObject channelPrefab;

    public void CreateChannel(string name, string description)
    {
        GameObject instance = Instantiate(channelPrefab);
        Channel channel = instance.GetComponent<Channel>();
        channel.name = name;
        channel.description = description;

        NetworkServer.Spawn(newChannel);
    }
}