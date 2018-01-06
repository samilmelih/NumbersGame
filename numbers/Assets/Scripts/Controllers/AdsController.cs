using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour {

    public GameObject adsLoadingGO;
    public TextMeshProUGUI RemaningTimeText;

    public void ShowAds()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            return;

        
        adsLoadingGO.SetActive(true);

        Advertisement.Show(
            "rewardedVideo",
            new ShowOptions()
            {
                resultCallback = delegate (ShowResult res) {
                    adsLoadingGO.SetActive(false);
                    switch (res)
                    {
                        case ShowResult.Failed:
                            break;
                        case ShowResult.Skipped:
                            DataTransfer.remainingTime += 8f;
                            break;
                        case ShowResult.Finished:
                            DataTransfer.remainingTime += 8f;
                            break;
                        default:
                            break;
                    }
                }
            }
        );
    }
    private void Update()
    {
        RemaningTimeText.text = string.Format("{0:F2}", DataTransfer.remainingTime);
    }
}
