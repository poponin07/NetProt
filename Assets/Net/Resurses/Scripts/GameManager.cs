using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;


namespace Net
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private InputAction m_quit;
        [SerializeField] private string m_playerPrefabName;
        private PlayerController m_playerController1;
        private PlayerController m_playerController2;
        private float m_randomInervalPos;

        private void Start()
        {
            m_quit.Enable();
            m_quit.started += OnQuit;
            m_randomInervalPos = 7f;
            
            Vector3 pos = new Vector3(Random.Range(-m_randomInervalPos,m_randomInervalPos), 0f, Random.Range(-m_randomInervalPos,m_randomInervalPos));
            var GO = PhotonNetwork.Instantiate(PhotonNetwork.NickName, pos, new Quaternion());
            
            //PhotonPeer.RegisterType(typeof(PlayerData), 100, Debugger.SerializePlayerData, Debugger.DeserializePlayerData);
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }
        

        public void AddPlayerContr(PlayerController player)
        {
            if (player.name.Contains("1")) m_playerController1 = player;
            else m_playerController2 = player;
            
            if (m_playerController1 != null && m_playerController2 != null)
            {
                m_playerController1.SetTarget(m_playerController2.transform);
                m_playerController2.SetTarget(m_playerController1.transform);
            }
        }

        private void OnQuit(InputAction.CallbackContext context)
        {
            PhotonNetwork.LeaveRoom();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying= false; 
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
            Application.Quit();
#endif
        }
    }
}