using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System;
using UnityEditor.PackageManager.UI;
using NHDigital.Scripts;


public class PlayfabManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] ColourController colourController;
    [SerializeField] GameObject nameWindow;
    [SerializeField] GameObject leaderboard;
    [SerializeField] InputField inputField;
    [SerializeField] GameObject unavailableText;
    [SerializeField] GameObject top10Button;
    [SerializeField] GameObject getPosButton;
    [SerializeField] GameObject motd;
    public GameObject rowPrefab;
    public Transform rowsParent;
    public string XboxGamerTag = "";
    public bool isXboxMode = false;

    NHDUSerLogin userlogin;
    NHDLogger log;
    public TextMeshProUGUI usernameText;
    public Button cloudloginbutton;
    public Button justplaybutton;

    string loggedInPlayfabID;

    //void DeactivateButtons()
    //{
    //    cloudloginbutton.gameObject.SetActive(false);
    //    justplaybutton.gameObject.SetActive(false);
    //}

    public void ButtonCloudLogin()
    {
        usernameText.text = "Signing In...";
        motd.SetActive(true);   

        // DeactivateButtons();

        UserLogin();

        return;
    }

    public void ButtonJustPlay()
    {
       
        usernameText.text = "Playing offline";
     
       // DeactivateButtons();

        return;
    }

    void UpdateStatus()
    {
        string strId = "PlayFab id: " + userlogin.strPFUserId;
        if (userlogin.strXboxGamertag != "")
            strId += " | Gamertag: " + userlogin.strXboxGamertag;
        log.Write(strId);


        usernameText.text = "Welcome " + (userlogin.strXboxGamertag != "" ? userlogin.strXboxGamertag : userlogin.strPFUserId) ;


        GetTitleData();
        GetMesh();
    }


    IEnumerator WaitforXboxLoginCompletion()
    {
        log.Write("WaitforXboxLoginCompletion pre-sleeper " + userlogin.bXboxLoginComplete);

        while (!userlogin.bXboxLoginComplete)
        {
            yield return new WaitForSeconds(1);
        }

        log.Write("WaitforXboxLoginCompletion post-sleeper " + userlogin.bXboxLoginComplete);

        userlogin.PlayFabLogin();
    }

    IEnumerator WaitforPlayFabLoginCompletion()
    {
        log.Write("WaitforPlayFabLoginCompletion pre-sleeper " + userlogin.bPlayFabLoginComplete);

        while (!userlogin.bPlayFabLoginComplete)
        {
            yield return new WaitForSeconds(1);
        }

        log.Write("WaitforPlayFabLoginCompletion post-sleeper " + userlogin.bPlayFabLoginComplete);

        UpdateStatus();
    }

    void UserLogin()
    {
        userlogin = new NHDUSerLogin(log);

        switch (Application.platform)
        {
            case RuntimePlatform.XboxOne:
            case RuntimePlatform.WSAPlayerX64:
                userlogin.XboxLogin();
                StartCoroutine(WaitforXboxLoginCompletion());
                break;

            case RuntimePlatform.WindowsEditor:
                userlogin.PlayFabLogin();
                break;

            default:
                userlogin.PlayFabLogin();
                break;

        }

        StartCoroutine(WaitforPlayFabLoginCompletion());

    }

    void Start()
    {
        log = new NHDLogger();

       usernameText.text = "Welcome";
    }
  
    
 


    
    //void LoginPlayFabDefault()
    //{
    //    var request = new LoginWithCustomIDRequest
    //    {
    //        CustomId = SystemInfo.deviceUniqueIdentifier,
    //        CreateAccount = true,
    //    };
    //    PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    //}


    //void OnLoginSuccess(LoginResult result)
    //{
    //    loggedInPlayfabID = result.PlayFabId;
    //    Debug.Log("Successful login/ account create!");
    //    GetMesh();
    //    GetTitleData();
    //}




    public void SendLeaderboard(int finalScore)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "FinalScore",
                    Value = finalScore
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);

    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful Leaderboard Sent.");
    }

    public void GetLeaderboard()
    {
        if (userlogin != null)
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = "FinalScore",
                StartPosition = 0,
                MaxResultsCount = 10,
            };
            PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
        }
        else
        {
            unavailableText.SetActive(true);
            top10Button.SetActive(false);
            getPosButton.SetActive(false);
        }
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {

        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }

        foreach (PlayerLeaderboardEntry item in result.Leaderboard)
        {

            GameObject newGO = Instantiate(rowPrefab, rowsParent);
            TextMeshProUGUI[] texts = newGO.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1).ToString();
            //texts[1].text =  item.DisplayName + ":" + item.PlayFabId;
            texts[1].text = item.DisplayName != null ? item.DisplayName : item.PlayFabId;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " " + texts[1].text + " " + item.StatValue);
        }
    }

    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "FinalScore",
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAroundPlayerGet, OnError);
    }

    void OnLeaderboardAroundPlayerGet(GetLeaderboardAroundPlayerResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }

        foreach (PlayerLeaderboardEntry item in result.Leaderboard)
        {

            GameObject newGO = Instantiate(rowPrefab, rowsParent);
            TextMeshProUGUI[] texts = newGO.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName != null ? item.DisplayName : item.PlayFabId;
            texts[2].text = item.StatValue.ToString();

            if (item.PlayFabId == userlogin.strPFUserId)
            {
                texts[0].color = Color.yellow;
                texts[1].color = Color.yellow;
                texts[2].color = Color.yellow;
            }

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

    public void GetMesh()
    {
        if (userlogin != null)
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
        }
        else
        {
            colourController.BlueSelected();
        }
    }


    public void OnDataRecieved(GetUserDataResult result)
    {
        Debug.Log("Recieved user data.");
        if (result.Data != null && result.Data.ContainsKey("Mesh"))
        { 
           if (result.Data["Mesh"].Value == "Red")
           {
                colourController.RedSelected();
           }
           else if (result.Data["Mesh"].Value == "Blue")
           {
                colourController.BlueSelected();
           }
           else if (result.Data["Mesh"].Value == "Yellow")
            {
                colourController.YellowSelected();
            }
        }
    }

    public void SaveMesh()
    {
        if (userlogin != null)
        {

            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
           {
               {"Mesh", colourController.chosenColour}
           }
            };
            PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        }
    }

    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Successful user data send.");
    }

    void GetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnTitleDataRecieved, OnError);
    }

    void OnTitleDataRecieved(GetTitleDataResult result)
    {
        if (result.Data == null || result.Data.ContainsKey("Message") == false)
        {
            Debug.Log("No Message.");
            return;
        }

        messageText.text = result.Data["Message"];
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while loggin in/ creating account!");
        Debug.Log(error.GenerateErrorReport());
    }


}
