using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackJackTables : MonoBehaviour
{
    public TextMeshProUGUI seat1_Table1;
    public TextMeshProUGUI seat2_Table1;
    public TextMeshProUGUI seat3_Table1;
    public TextMeshProUGUI seat4_Table1;
    public TextMeshProUGUI seat5_Table1;
    public TextMeshProUGUI seat6_Table1;

    public TextMeshProUGUI seat1_Table2;
    public TextMeshProUGUI seat2_Table2;
    public TextMeshProUGUI seat3_Table2;
    public TextMeshProUGUI seat4_Table2;
    public TextMeshProUGUI seat5_Table2;
    public TextMeshProUGUI seat6_Table2;

    string seat1_Table1S;
    string seat2_Table1S;
    string seat3_Table1S;
    string seat4_Table1S;
    string seat5_Table1S;
    string seat6_Table1S;

    string seat1_Table2S;
    string seat2_Table2S;
    string seat3_Table2S;
    string seat4_Table2S;
    string seat5_Table2S;
    string seat6_Table2S;

    private float waitTime = 10.0f;
    private bool optionSwitch = false;
    private float timer = 0.0f;

    string message = "requestblackjacktables";
    bool firstRun = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(PackageHandler.packageList.Count == 0 && firstRun)
        {
            firstRun = false;
            SetSeats();
        }
        if (PackageHandler.packageList.Count == 0 && optionSwitch == true)
        {
            optionSwitch = false;
            SetSeats();
        }
        if (timer > waitTime && optionSwitch == false)
        {
            Debug.Log("Requesting Blackjack tables update..");
            SocketHandler.PackageSend(message);
            timer -= waitTime;
            optionSwitch = true;
        }
        if(timer > 100.0f)
        {
            timer = 0;
        }
    }

    public void SetSeats()
    {
        seat1_Table1S = PlayerPrefs.GetString("seat1_table1");
        seat2_Table1S = PlayerPrefs.GetString("seat2_table1");
        seat3_Table1S = PlayerPrefs.GetString("seat3_table1");
        seat4_Table1S = PlayerPrefs.GetString("seat4_table1");
        seat5_Table1S = PlayerPrefs.GetString("seat5_table1");
        seat6_Table1S = PlayerPrefs.GetString("seat6_table1");

        seat1_Table2S = PlayerPrefs.GetString("seat1_table2");
        seat2_Table2S = PlayerPrefs.GetString("seat2_table2");
        seat3_Table2S = PlayerPrefs.GetString("seat3_table2");
        seat4_Table2S = PlayerPrefs.GetString("seat4_table2");
        seat5_Table2S = PlayerPrefs.GetString("seat5_table2");
        seat6_Table2S = PlayerPrefs.GetString("seat6_table2");


        seat1_Table1.text = seat1_Table1S.ToString();
        seat2_Table1.text = seat2_Table1S.ToString();
        seat3_Table1.text = seat3_Table1S.ToString();
        seat4_Table1.text = seat4_Table1S.ToString();
        seat5_Table1.text = seat5_Table1S.ToString();
        seat6_Table1.text = seat6_Table1S.ToString();


        seat1_Table2.text = seat1_Table2S.ToString();
        seat2_Table2.text = seat2_Table2S.ToString();
        seat3_Table2.text = seat3_Table2S.ToString();
        seat4_Table2.text = seat4_Table2S.ToString();
        seat5_Table2.text = seat5_Table2S.ToString();
        seat6_Table2.text = seat6_Table2S.ToString();
    }

    public void BlackjackTableRequest()
    {
        SocketHandler.PackageSend(message);
    }
}
