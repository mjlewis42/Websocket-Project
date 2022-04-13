using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public static void SceneChanger(string scene)
    {
        switch (scene)
        {
            case "Main_Menu":
                break;
            case "LoginPage":
                Debug.Log("Loading Scene: " + scene);
                SceneManager.LoadScene(scene);
                break;
            case "GamePage":
                Debug.Log("Loading Scene: " + scene);
                SceneManager.LoadScene(scene);
                break;
            case "Character_Creation":
                Debug.Log("Loading Scene: " + scene);
                SceneManager.LoadScene(scene);
                break;
            case "BlackjackTables":
                Debug.Log("Loading Scene: " + scene);
                SceneManager.LoadScene(scene);
                break;
            default:
                Debug.Log("Error changing scenes in scene handler with scene: " + scene);
                break;
        }
    }

}
