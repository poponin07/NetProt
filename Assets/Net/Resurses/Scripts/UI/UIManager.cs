
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
namespace Net
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_endGameUI;
        [SerializeField] private Text m_endgameTextUI;

        public void EndGame(string player)
        {
          StartCoroutine(EndGameCor(player));
        }

        private void ActivePopUpEndGame()
        {
            m_endGameUI.SetActive(true);
        }

        private IEnumerator EndGameCor(string player)
        {
            m_endgameTextUI.text = player;
            ActivePopUpEndGame();
            yield return null;
            PhotonNetwork.LoadLevel("MenuScene");
        }

    }
}