using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using SocketIOClient;

public class PackageHandler : MonoBehaviour
{
    public static List<SocketIOResponse> packageList = new List<SocketIOResponse>();
    bool hasMessage = false;


    void Update()
    {
        if (packageList.Count > 0 && hasMessage == false)
        {
            hasMessage = true;
            Debug.Log("A message is in the package array now.");
            UnpackDataPackage();
        }
    }

    public void UnpackDataPackage()
    {
        for (int i = 0; i < packageList.Count; i++)
        {
            var data = packageList[i].GetValue<PackageObj>();

            switch (data.packageType)
            {
                //get token message
                case "connection":
                    GetToken(data.socketData[0].socketId);
                    break;
                case "authorizedUser":
                    PlayerPrefs.SetString("socketId", data.socketData[0].socketId);
                    PlayerPrefs.SetString("discordId", data.dataPack[0].discordId);
                    PlayerPrefs.SetString("discordName", data.dataPack[0].discordName);
                    PlayerPrefs.SetString("discordAvatar", data.dataPack[0].discordAvatar);
                    PlayerPrefs.SetInt("discordUserBalance", data.dataPack[0].discordUserBalance);
                    SceneHandler.SceneChanger("GamePage");
                    break;
                case "charcreation":
                    SceneHandler.SceneChanger("Character_Creation");
                    break;
                case "blackjacktable":
                    int tableNumber = data.blackjackTable[0].tableNumber;
                    string seat1 = data.blackjackTable[0].seat1;
                    string seat2 = data.blackjackTable[0].seat2;
                    string seat3 = data.blackjackTable[0].seat3;
                    string seat4 = data.blackjackTable[0].seat4;
                    string seat5 = data.blackjackTable[0].seat5;
                    string seat6 = data.blackjackTable[0].seat6;
                    BlackjackTablePackage(tableNumber, seat1, seat2, seat3, seat4, seat5, seat6);
                    break;
                case "logout":
                    

                    break;
                default:
                    Debug.Log("Error unpacking data package. Package type: " + data.packageType);
                    break;
            }

            packageList.RemoveAt(i);
            hasMessage = false;
        }
    }

    public static void BlackjackTablePackage(int tableNumber, string seat1, string seat2, string seat3, string seat4, string seat5, string seat6)
    {
        switch (tableNumber)
        {
            case 1:
                Debug.Log("table 1 data");
                PlayerPrefs.SetString("seat1_table1", seat1);
                PlayerPrefs.SetString("seat2_table1", seat2);
                PlayerPrefs.SetString("seat3_table1", seat3);
                PlayerPrefs.SetString("seat4_table1", seat4);
                PlayerPrefs.SetString("seat5_table1", seat5);
                PlayerPrefs.SetString("seat6_table1", seat6);
                break;
            case 2:
                Debug.Log("table 2 data");
                PlayerPrefs.SetString("seat1_table2", seat1);
                PlayerPrefs.SetString("seat2_table2", seat2);
                PlayerPrefs.SetString("seat3_table2", seat3);
                PlayerPrefs.SetString("seat4_table2", seat4);
                PlayerPrefs.SetString("seat5_table2", seat5);
                PlayerPrefs.SetString("seat6_table2", seat6);
                break;
            default:
                Debug.Log("ERROR unpacking table message.");
                break;
        }
    }

    public void GetToken(string tokenId)
    {
        PlayerPrefs.SetString("socketId", tokenId);
        SceneHandler.SceneChanger("LoginPage");
    }

    public class PackageObj
    {
        public string packageType { get; set; }
        public BlackJackTable[] blackjackTable { get; set; }
        public SocketObj[] socketData { get; set; }
        public DataObj[] dataPack { get; set; }
    }

    public class SocketObj
    {
        public string socketId { get; set; }
    }

    public class DataObj
    {
        public int id { get; set; }
        public string discordName { get; set; }
        public string discordId { get; set; }
        public string discordAvatar { get; set; }
        public int discordUserBalance { get; set; }
        public string patreon { get; set; }
        public string char1 { get; set; }
        public string char2 { get; set; }
        public string char3 { get; set; }
        public string char4 { get; set; }
    }
    public class BlackJackTable
    {
        public int tableNumber { get; set; }
        public string seat1 { get; set; }
        public string seat2 { get; set; }
        public string seat3 { get; set; }
        public string seat4 { get; set; }
        public string seat5 { get; set; }
        public string seat6 { get; set; }

    }
}
