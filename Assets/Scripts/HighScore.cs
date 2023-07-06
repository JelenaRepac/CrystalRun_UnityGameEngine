using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        highScoreText.text = gameManager.GetHighScore().ToString();
    }
    public void BackOnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
