using UnityEngine;
using System;
using UnityEngine.UI;

public class ShowRewardedVideo : MonoBehaviour
{
    public GameObject ReclamaBtn;

    public static String REWARDED_INSTANCE_ID = "0";

    // Use this for initialization
    void Start()
    {
        //Add Rewarded Video Events
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;

        IronSourceEvents.onRewardedVideoAdLoadedDemandOnlyEvent += this.RewardedVideoAdLoadedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdLoadFailedDemandOnlyEvent += this.RewardedVideoAdLoadFailedDemandOnlyEvent; 
    }

    /************* RewardedVideo API *************/
    public void ShowRewardedVideoButtonClicked()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
    }

    void LoadDemandOnlyRewardedVideo()
    {
        IronSource.Agent.loadISDemandOnlyRewardedVideo(REWARDED_INSTANCE_ID);
    }

    void ShowDemandOnlyRewardedVideo()
    {
        if (IronSource.Agent.isISDemandOnlyRewardedVideoAvailable(REWARDED_INSTANCE_ID))
        {
            IronSource.Agent.showISDemandOnlyRewardedVideo(REWARDED_INSTANCE_ID);
        }
    }

    /************* RewardedVideo Delegates *************/
    void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
    {
        ReclamaBtn.GetComponent<Button>().interactable = canShowAd;
    }

    void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    {       
      PlayerController.PaintForColoring += ssp.getRewardAmount() * PriceList.RewardShowAd;
        SaveManager.Instance.SaveGame();
    }

    void RewardedVideoAdLoadedDemandOnlyEvent(string instanceId)
    {
        ReclamaBtn.GetComponent<Button>().interactable = true;
    }

    void RewardedVideoAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        ReclamaBtn.GetComponent<Button>().interactable = false;
    }
}
