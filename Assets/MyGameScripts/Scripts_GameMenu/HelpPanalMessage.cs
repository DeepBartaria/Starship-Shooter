using UnityEngine;

public class HelpPanalMessage : MonoBehaviour
{
    public GameObject helpPanal;
    public void ToggleHelpPanel() {
        if (helpPanal != null) {
            helpPanal.SetActive(!helpPanal.activeSelf);
        }
    }

    public void exitGame() {
        Application.Quit();
    }
}
