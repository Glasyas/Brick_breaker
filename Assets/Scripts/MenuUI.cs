using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]

public class MenuUI : MonoBehaviour
{
    public Text BestScoreText;
    public TMP_InputField NameText;
    public TextMeshProUGUI ErrorText;


    // Start is called before the first frame update
    void Start()
    {
        MainManager.Instance.LoadData();
        ChangeBestScoreText();
    }

    void ChangeBestScoreText()
    {
        if (MainManager.Instance.BestScore != 0) { BestScoreText.text = $"Best Score : {MainManager.Instance.BestName} : {MainManager.Instance.BestScore}"; }
        else { BestScoreText.text = "Best Score : null"; }
    }

    public void StartGame()
    {
        if(string.IsNullOrEmpty(NameText.text))
        {
            Debug.Log("error : no name");
            ErrorText.gameObject.SetActive(true);
        }
        else
        {
            MainManager.Instance.Name = NameText.text;
            SceneManager.LoadScene(1);
        }
    }

    public void ResetScore()
    {
        MainManager.Instance.ResetData();
        MainManager.Instance.LoadData();
        ChangeBestScoreText();
    }

    public void Exit()
    {
        //MainManager.Instance.SaveData();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player (dont work in editor)
#endif
    }
}
