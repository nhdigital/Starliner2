using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Animator cameraAnim;

    public void TransitionToSettings()
    {
        cameraAnim.SetTrigger("SettingsButton");
    }

    public void SettingsReturn()
    {
        cameraAnim.SetTrigger("SettingsReturnButton");
    }


    public void TransitionToLeaderBoard()
    {
        cameraAnim.SetTrigger("LeaderboardButton");
    }

    public void LeaderboardReturn()
    {
        cameraAnim.SetTrigger("LeaderboardReturnButton");

    }


    public void TransitionToHTP()
    {
        cameraAnim.SetTrigger("HTPButton");
    }

    public void HTPReturn()
    {
        cameraAnim.SetTrigger("HTPReturnButton");
    }



    public void TransitionToCredits()
    {
        cameraAnim.SetTrigger("CreditsButton");
    }

    public void CreditsReturn()
    {
        cameraAnim.SetTrigger("CreditsReturnButton");
    }

}
