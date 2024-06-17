using UnityEngine;
using Mirror;
using System;
using System.Security.Cryptography;
using System.Text;
using Mirror.Examples.Chat;

public class Dummy : NetworkBehaviour
{
    [Header("Dummy Sheet")]
    [SyncVar] public string matchId = "";
    private NetworkMatch networkMatch;

    void Awake(){
        networkMatch = GetComponent<NetworkMatch>();
    }

    void Start(){
       networkMatch.matchId = GetRandomMatchID();
       matchId = networkMatch.matchId.ToString();
    }

    public static Guid GetRandomMatchID() {
        string _id = string.Empty;
        for (int i = 0; i < 5; i++) {
            int random = UnityEngine.Random.Range (0, 36);
            if (random < 26) {
                _id += (char) (random + 65);
            } else {
                _id += (random - 26).ToString ();
            }
        }
        Debug.Log ($"Random Match ID: {_id}");
        MD5CryptoServiceProvider provider = new();
        byte[] inputBytes = Encoding.Default.GetBytes (_id);
        byte[] hashBytes = provider.ComputeHash (inputBytes);
        return new Guid (hashBytes);
    }
}