using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Nome da cena principal para carregar
    [SerializeField] private string gameSceneName = "GameScene";

    // Nome da cena de reset (pode ser a mesma do menu)
    [SerializeField] private string resetSceneName = "MenuScene";

    // Carregar o jogo
    public void LoadGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // Resetar o progresso e reiniciar a cena
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll(); // Limpa os dados salvos
        SceneManager.LoadScene(resetSceneName);
    }
}
