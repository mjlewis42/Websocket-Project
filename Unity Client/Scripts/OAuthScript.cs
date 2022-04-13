using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OAuthScript : MonoBehaviour
{
    string getState;
	
    void Start()
    {
        getState = PlayerPrefs.GetString("socketId");
    }

    public void GetOAuthData()
    {
        Application.OpenURL("https://discord.com/api/oauth2/authorize?client_id=[REDACTED]&redirect_uri=http%3A%2F%2F[REDACTED]%3A3000&response_type=code&state=" + getState + "&scope=identify");
    }
    public void GetAuthInfo()
    {
        Application.OpenURL("https://auth0.com/intro-to-iam/what-is-oauth-2/");
    }
}
