using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
        private float m_randomInervalPos;
        private void Awake()
        {
            m_quit.Enable();
            m_quit.started += OnQuit;
            m_randomInervalPos = 7f;
        }

        private void Start()
        {
            Vector3 pos = new Vector3(Random.Range(-m_randomInervalPos,m_randomInervalPos), 0f, Random.Range(-m_randomInervalPos,m_randomInervalPos));
            var GO = PhotonNetwork.Instantiate(m_playerPrefabName + PhotonNetwork.NickName, pos, new Quaternion());
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
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