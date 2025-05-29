using System;
using Extensions;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class ResultCanvas : MonoBehaviour, ICanvas
    {
        [SerializeField] private Canvas canvas;
        
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private Button restartButton;
        
        private const string WinText = "Win!";
        private const string LoseText = "Lose..";
        
        public Canvas Canvas => canvas;

        public void Construct()
        {
            restartButton.onClick.AddListener(RestartScene);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(RestartScene);
        }
        
        public void Initialize(object obj = null)
        {
            ArgumentTypeChecker.Check<GameResult>(obj, out var result);

            resultText.text = result switch
            {
                GameResult.Win => WinText,
                GameResult.Lose => LoseText,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
