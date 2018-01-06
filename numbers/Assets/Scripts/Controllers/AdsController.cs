using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour {

    public GameObject adsLoadingGO;
    public void ShowAds()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            return;

        DataTransfer.remainingTime += 8f;
        adsLoadingGO.SetActive(true);

        Advertisement.Show(
            "rewardedVideo",
            new ShowOptions()
            {
                resultCallback = delegate (ShowResult res) {
                    adsLoadingGO.SetActive(false);
                }
            }
        );
        float oldTime = ProgressController.GetRemainingTime();
        DataTransfer.remainingTime += oldTime;
        ProgressController.SetRemainingTime(DataTransfer.remainingTime);
    }


}
