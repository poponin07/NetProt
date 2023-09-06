
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

        public void EndGame(string text)
        {
          StartCoroutine(EndGameCor(text));
        }

        private void ActivePopUpEndGame()
        {
            m_endGameUI.SetActive(true);
        }

        private IEnumerator EndGameCor(string text)
        {
            m_endgameTextUI.text = text;
            ActivePopUpEndGame();
            yield return new WaitForSeconds(3);
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(0);
        }

    }
}