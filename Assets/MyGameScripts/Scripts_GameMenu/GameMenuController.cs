using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class GameMenuController : MonoBehaviour
{
    public GameObject endgamePanal;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI highestScore;
    public TextMeshProUGUI Result;
    public store_Highest_Score store_Highest_Score;
    public void display_score() {
        currentScore.text = Astroid_Script.Total_Astroid_Destroyed + AlienShip_Script.Total_AlienShip_Destroyed + " Score";
        Astroid_Script.Total_Astroid_Destroyed = 0;
        AlienShip_Script.Total_AlienShip_Destroyed = 0;
    }
    public void onSpaceShipDestroied()
    {
        store_Highest_Score.Store_Highest_Score(Astroid_Script.Total_Astroid_Destroyed + AlienShip_Script.Total_AlienShip_Destroyed);
        endgamePanal.SetActive(true);
        display_score();
        display_highestScore();
        display_result();
    }
    public void onMainAlienShipDestroyed() {
        onSpaceShipDestroied();
    }
    public void display_result() {
        if (Main_AlienShip_Script.Main_AlienShip_destroyed)
        {
            Result.text = "You Win!";
        }
        else {
            Result.text = "You Lost :)";
        }
    }
    public void display_highestScore() {

        highestScore.text = store_Highest_Score.LoadHighestScore() + " Highest Score";

    }
    public void StartGame() {
        Debug.Log("ctart button clicked");
        SceneManager.LoadSceneAsync("GameLevel1Scene");
    }

    public void Restart()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene(1);
    }
    public void returnMainMenu()
    {
        Debug.Log("return botton clicked");
        SceneManager.LoadSceneAsync("GameMenuScene");
    }
}
