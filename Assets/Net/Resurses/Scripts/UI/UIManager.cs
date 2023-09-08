
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

namespace Net
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_endGameUI;
        [SerializeField] private Text m_endgameTextUI;

        public void EndGame(Text text)
        {
            m_endgameTextUI = text;
            StartCoroutine(EndGameCor());
        }

        public Text SetFinalText()
        {
            return m_endgameTextUI;
        }
        private void ActivePopUpEndGame(bool value)
        {
            m_endGameUI.SetActive(value);
        }

        private IEnumerator EndGameCor()
        {
           // m_endgameTextUI.text = text;
            ActivePopUpEndGame(true);
            yield return new WaitForSeconds(3);
            ActivePopUpEndGame(false);
            PhotonNetwork.LoadLevel("MenuScene");
            PhotonNetwork.LeaveRoom();
        }

    }
}