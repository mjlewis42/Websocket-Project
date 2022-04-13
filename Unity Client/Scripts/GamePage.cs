using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GamePage : MonoBehaviour
{
    public TextMeshProUGUI discordName;
    public TextMeshProUGUI discordUserBalance;
    
    string ImageName;
    string getName;
    string getAvatar;
    int getBalance;

    public RawImage Image;

    void Start()
    {
        getName = PlayerPrefs.GetString("discordName");
        getAvatar = PlayerPrefs.GetString("discordAvatar");
        getBalance = PlayerPrefs.GetInt("discordUserBalance");
        discordName.text = getName.ToString();
        discordUserBalance.text = getBalance.ToString() + " Chips";
        StartCoroutine(LoadImage(getAvatar));

    }
    IEnumerator LoadImage(string ImageUrl)
    {
        WWW www = new WWW(ImageUrl);
        yield return www;

            if (!File.Exists(Application.persistentDataPath + ImageName))
            {
                if (www.error == null)
                {
                    Texture2D texture = www.texture;
                    Image.texture = texture;
                    byte[] dataByte = texture.EncodeToPNG();
                    File.WriteAllBytes(Application.persistentDataPath + ImageName + "png", dataByte);
                    //Debug.Log("Image Save");
                }
                else
                {
                    Debug.Log("We have an error! " + www.error);
                }
            }
            else 
            {
                byte[] UploadByte = File.ReadAllBytes(Application.persistentDataPath + ImageName);
                Texture2D texture = new Texture2D(10, 10);
                texture.LoadImage(UploadByte);
                Image.texture = texture;
                //Debug.Log("Image Loaded");
            }
    }

    public void BlackjackTableButton()
    {
        SceneHandler.SceneChanger("BlackjackTables");

        string message = "requestblackjacktables";
        SocketHandler.PackageSend(message);
    }

}
