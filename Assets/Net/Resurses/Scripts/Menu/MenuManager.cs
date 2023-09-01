using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Photon.Pun;
using Debug = UnityEngine.Debug;

namespace Net
{
    public class MenuManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private string nickName;
        private void Start()
        {

#if UNITY_EDITOR
            PhotonNetwork.NickName = nickName + "1";
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
        PhotonNetwork.NickName = nickName + "2";
#endif
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = "0.0.1";
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debugger.Log("Ready for connection!");
        }
        
        public void OnCreateRoom()
        {
            PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions{MaxPlayers = 2});
        }

        public void OnJoinRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }
        
        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
        
        public void OnQuitRoom()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying= false;
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
            Application.Quit();
#endif
        }
    }
}