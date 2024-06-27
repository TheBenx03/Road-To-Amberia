using UnityEngine;
using Mirror;
using System;

namespace Assets.Scripts
{
    public class Ticket : MonoBehaviour
    {
        [Header("Ticket Attributes")]
        [SerializeField] private string ticketUsername;
        [SerializeField] private string ticketPassword;
        [SerializeField] private string boardAdress;

        #region Messages
        public struct AuthTicketMessage : NetworkMessage
        {
            public string authUsername;
            public string authPassword;
        }
        public struct AuthResponseMessage : NetworkMessage
        {
            public byte code;
            public string message;
        }
        
        public struct RegisterTicketMessage : NetworkMessage
        {
            public string registerUsername;
            public string registerPassword;
        }
        public struct RegisterResponseMessage : NetworkMessage
        {
            public byte code;
            public string message;
        }
        #endregion

        
    }
}