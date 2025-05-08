using UnityEngine;
using System.IO;
public class store_Highest_Score : MonoBehaviour
{
    private string filepath;
    private void Start()
    {
        filepath = Application.persistentDataPath + "/highscore.json";
    }

    public void Store_Highest_Score(float currentScore) {
       
        float highest_score = LoadHighestScore();

        if (currentScore > highest_score) {
            File.WriteAllText(filepath, currentScore.ToString());
            Debug.Log("highest score saved " + currentScore);
        }
    }

    public float LoadHighestScore() {
        if (File.Exists(filepath)) { 
            string highScoreString = File.ReadAllText(filepath);
            Debug.Log("loaded score " + highScoreString);
            return float.Parse(highScoreString);
        }

        return 0;
    }


}
