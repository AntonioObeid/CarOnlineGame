using UnityEngine;
using TMPro;
using Unity.Netcode;

public class ChatManager : NetworkBehaviour
{
    public TMP_InputField chatInput;
    public TMP_Text chatOutput;

    void Start()
    {
        if (chatInput == null || chatOutput == null)
        {
            Debug.LogError("Chat input or output component not assigned!");
        }
    }

    private void Update()
    {
        if (IsClient && Input.GetKeyDown(KeyCode.Return))
        {
            if (!string.IsNullOrEmpty(chatInput.text))
            {
                SendMessageToServerRpc(chatInput.text);
                chatInput.text = "";
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SendMessageToServerRpc(string message, ServerRpcParams rpcParams = default)
    {
        UpdateChatClientRpc(message);
    }

    [ClientRpc]
    void UpdateChatClientRpc(string message)
    {
        chatOutput.text += "\n" + message;
    }
}
