using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class PlayerObjectController : NetworkBehaviour
{
    [SyncVar] public int ConnectionID;
    [SyncVar] public int PlayerIdNumber;
    [SyncVar] public ulong PlayerSteamID;
    [SyncVar(hook = nameof(PlayerNameUpdate))] public string PlayerName;
    [SyncVar(hook = nameof(PlayerReadyUpdate))] public bool Ready;

    private CustomNetworkManager manager;

    private CustomNetworkManager Manager{
        get{
            if(manager!= null){
                return manager;
            }
            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    private void Start(){
        DontDestroyOnLoad(this.gameObject);
    }

    private void PlayerReadyUpdate(bool oldValue, bool newValue){
        if(isServer){
            this.Ready = newValue;
        }
        if(isClient){
            LobbyController.Instance.UpdatePlayerList();
        }
    }

    [Command]
    private void CMDSetPlayerReady(){
        this.PlayerReadyUpdate(this.Ready,!this.Ready);
    }

    public void ChangeReady(){
        if(hasAuthority){
            CMDSetPlayerReady();
        }
    }

    public override void OnStartAuthority()
    {
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalGamePlayer";
        LobbyController.Instance.FindLocalPlayer();
        LobbyController.Instance.UpdateLobbyName();
    }

    public override void OnStartClient()
    {
        Manager.GamePlayers.Add(this);
        LobbyController.Instance.UpdateLobbyName();
        LobbyController.Instance.UpdatePlayerList();
    }

    public override void OnStopClient()
    {
        Manager.GamePlayers.Remove(this);
        LobbyController.Instance.UpdatePlayerList();
    }

    [Command]
    private void CmdSetPlayerName(string PlayeName){
        this.PlayerNameUpdate(this.PlayerName,PlayeName);
    }

    public void PlayerNameUpdate(string OldValue, string NewValue){
        if(isServer){
            this.PlayerName = NewValue;
        }
        if(isClient){
            LobbyController.Instance.UpdatePlayerList();
        }
    }

    public void CanStartGame(string SceneName){
        if(hasAuthority){
            CMDCanStartGame(SceneName);
        }
    }

    [Command]
    public void CMDCanStartGame(string SceneName){
        manager.StartGame(SceneName);
    }

}
