using UnityEngine;

public class Script : MonoBehaviour
{
    public static string appKey = "c888140d";

    void Start()
    {
        //Dynamic config example
        IronSourceConfig.Instance.setClientSideCallbacks(true);

        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(appKey);
       // IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
        //IronSource.Agent.initISDemandOnly(appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL);
    }
    
    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
