using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsManager : MonoBehaviour
{
    public static PlatformsManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private IEnumerator FlipAndWait(GameObject Platform)
    {
        Platform.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
        yield return new WaitForSeconds(0.4f);
        Platform.GetComponent<PlatformEffector2D>().rotationalOffset = 0;

    }

    public void FlipPlatform(GameObject Platform)
    {
        if(Platform.GetComponent<PlatformEffector2D>() == null)
        {
            return;
        }
        StartCoroutine(FlipAndWait(Platform));
    }


}
