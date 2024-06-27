using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace Assets.Scripts
{
    public class TicketUI : MonoBehaviour
    {
        [Header("TicketUI Elements")]
        [SerializeField] internal InputField networkAddressInput;
        [SerializeField] internal InputField usernameInput;
        [SerializeField] internal Button clientButton;
        [SerializeField] internal Button registerButton;
        [SerializeField] internal Text errorText;

        //TODO: Make actual login
        //TODO: Remember credentials
        //TODO: Enter as anonymous
        //TODO: Player credentials database
        //TODO: Login.cs for backend and networking
        
        public static TicketUI instance;
        string originalNetworkAddress;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            // if we don't have a networkAddress, set a default one.
            if (string.IsNullOrWhiteSpace(NetworkManager.singleton.networkAddress))
                NetworkManager.singleton.networkAddress = "localhost";

            // cache the original networkAddress for resetting if blank.
            originalNetworkAddress = NetworkManager.singleton.networkAddress;
        }

        void Update()
        {
            // bidirectional sync of networkAddressInput and NetworkManager.networkAddress
            // Order of operations is important here...Don't switch the order of these steps.
            if (string.IsNullOrWhiteSpace(NetworkManager.singleton.networkAddress))
                NetworkManager.singleton.networkAddress = originalNetworkAddress;

            if (networkAddressInput.text != NetworkManager.singleton.networkAddress)
                networkAddressInput.text = NetworkManager.singleton.networkAddress;
        }

        // Called by UI element UsernameInput.OnValueChanged
        public void ToggleButtons(string username)
        {
            clientButton.interactable = !string.IsNullOrWhiteSpace(username);
            registerButton.interactable = !string.IsNullOrWhiteSpace(username);
        }
    }
}
