using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

#if ENABLE_WINMD_SUPPORT
using Microsoft.Xbox.Services;
using Microsoft.Xbox.Services.System;
#endif

namespace NHDigital.Scripts
{
    class NHDUSerLogin
    {

#if ENABLE_WINMD_SUPPORT

    Windows.UI.Core.CoreDispatcher m_uiDispatcher = null;
    XboxLiveUser xlu;

#endif 

        public string strXboxGamertag { get; set; }
        public string strPFUserId { get; set; }
        public string strXboxToken { get; set; }
        public bool bXboxLoginComplete { get; set; }

        public bool bPlayFabLoginComplete { get; set; }


#if DEBUG
        GUIStyle m_guiStyle = new GUIStyle();
        string m_logText = string.Empty;
        List<string> m_logLines = new List<string>();

#endif
        NHDLogger log = null;

        public NHDUSerLogin(NHDLogger _log)
        {
            strXboxGamertag = "";
            strPFUserId = "";
            strXboxToken = "";
            bXboxLoginComplete = false;
            bPlayFabLoginComplete = false;
            log = _log;

            LogLine("NHDUSerLogin Constructed");

        }

        public void XboxLogin()
        {

            LogLine("XboxLogin Start");

            XboxAuthentication();

            LogLine("XboxLogin Exit ");

        }

        private async void XboxAuthentication()
        {

            LogLine("XboxAuthentication Start");

#if ENABLE_WINMD_SUPPORT
            
            IReadOnlyList<Windows.System.User> users = await Windows.System.User.FindAllAsync();

            LogLine("XboxAuthentication users: " + users.GetType() );

            try 
            {     
                Windows.ApplicationModel.Core.CoreApplicationView mainView = Windows.ApplicationModel.Core.CoreApplication.MainView;
                Windows.UI.Core.CoreWindow cw = mainView.CoreWindow;
                m_uiDispatcher = cw.Dispatcher;  
                xlu = new XboxLiveUser(users[0]);

                SignInResult signInSilentResult = await xlu.SignInSilentlyAsync(m_uiDispatcher);

                LogLine("XboxAuthentication signInSilentResult: " + signInSilentResult.Status.ToString() );

                switch (signInSilentResult.Status)
                {
                    case SignInStatus.Success:
                        strXboxGamertag = xlu.Gamertag;
                        LogLine("XboxAuthentication silent sucesss : " + strXboxGamertag);
                        await Task.Run(() => 
                        {
                           XboxLoginGetToken(xlu);
                        });                       
                        break;
                    case SignInStatus.UserInteractionRequired:
                        LogLine("XboxAuthentication user interaction");

                        SignInResult signInLoudResult = await xlu.SignInAsync(m_uiDispatcher);
                        LogLine("XboxAuthentication user interaction: " + signInLoudResult.Status.ToString() );
                        switch (signInLoudResult.Status)
                        {
                            case SignInStatus.Success:
                                strXboxGamertag = xlu.Gamertag;
                                LogLine("XboxAuthentication loud sucesss: " + strXboxGamertag);
                                await Task.Run(() => 
                                {
                                   XboxLoginGetToken(xlu); 
                                }); 
                                break;
                            case SignInStatus.UserCancel:
                                LogLine("XboxAuthentication cancel"); 
                                xlu = null;
                                bXboxLoginComplete = true;
                                break;
                            default:
                                xlu = null;
                                bXboxLoginComplete = true;
                                LogLine("XboxAuthentication loud failure: " + signInLoudResult.Status.ToString() );
                                break;
                        }
                        break;
                    default:
                        xlu = null;
                        bXboxLoginComplete = true;
                        LogLine("XboxAuthentication silent failure: " + signInSilentResult.Status.ToString() );
                        break;
                }
            }
            catch (Exception e)
            {
                xlu = null;
                bXboxLoginComplete = true;
                LogLine("XboxAuthentication Exception:" + e.Message);
            }

#endif

            LogLine("XboxAuthentication Exit");

            await Task.Run(() => { });

        }

#if ENABLE_WINMD_SUPPORT

        private async void XboxLoginGetToken(XboxLiveUser xlu)
        {
            LogLine("XboxLoginGetToken start");
            try
            {
                var result = await xlu.GetTokenAndSignatureAsync("POST", "https://playfabapi.com/ ", "");
                LogLine("XboxLoginGetToken Token:" + result.Token);

                strXboxToken = result.Token;

            }
            catch (Exception e)
            {
                LogLine("XboxLoginGetToken Exception:" + e.Message);
            }

            xlu = null;
            bXboxLoginComplete = true;

            LogLine("XboxLoginGetToken exit");

        }
#endif

        public void PlayFabLogin()
        {
            LogLine("PlayFabLogin Title:" + PlayFabSettings.staticSettings.TitleId.ToString());

            if (strXboxToken != "")
                PlayFabLoginWithXbox();
            else
                PlayFabLoginWithCustomID();

        }

        private void PlayFabLoginWithXbox()
        {
            LogLine("PlayFabLoginWithXbox start");

            var request = new LoginWithXboxRequest
            {
                XboxToken = strXboxToken,
                CreateAccount = true,
            };

            PlayFabClientAPI.LoginWithXbox(request, OnPlayFabLoginWithXboxSuccess, OnPlayFabError);  
        }

        void OnPlayFabLoginWithXboxSuccess(LoginResult result)
        {
            LogLine("OnPlayFabLoginWithXboxSuccess");
            strPFUserId = result.PlayFabId;

            PlayFabUpdateDisplayField(strXboxGamertag);

        }

        private void PlayFabLoginWithCustomID()
        {
            LogLine("PlayFabLoginWithCustomID start");

            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true,
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnPlayFabLoginWithCustomIDSuccess, OnPlayFabError);

        }

        void OnPlayFabLoginWithCustomIDSuccess(LoginResult result)
        {
            LogLine("OnPlayFabLoginWithCustomIDSuccess");
            strPFUserId = result.PlayFabId;
            bPlayFabLoginComplete = true;

        }

        private void PlayFabUpdateDisplayField(string title)
        {
            LogLine("PlayFabUpdateDisplayField start");
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName= title
            };

            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnPlayFabUpdateDisplayFieldSuccess, OnPlayFabError);

        }

        private void OnPlayFabUpdateDisplayFieldSuccess(UpdateUserTitleDisplayNameResult obj)
        {
            LogLine("OnPlayFabUpdateDisplayFieldSuccess"); 
            bPlayFabLoginComplete = true;
        }

        void OnPlayFabError(PlayFabError error)
        {
            LogLine("PlayFabLogin error");
            LogLine(error.GenerateErrorReport());
            bPlayFabLoginComplete = true;
        }


        public void LogLine(string line)
        {
#if DEBUG
            log.Write(line);
#endif
        }
 
    }
}
