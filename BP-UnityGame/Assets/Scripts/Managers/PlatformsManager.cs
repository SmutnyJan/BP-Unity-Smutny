using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsManager : MonoBehaviour
{
    private GameObject[] _platforms;
    private IEnumerator FlipAndWait()
    {
        foreach(GameObject platform in _platforms)
        {
            platform.GetComponent<PlatformEffector2D>().surfaceArc = -180;
        }
        yield return new WaitForSeconds(1);
        foreach (GameObject platform in _platforms)
        {
            platform.GetComponent<PlatformEffector2D>().surfaceArc = 180;
        }

    }

    public void FlipPlatform()
    {
        _platforms = GameObject.FindGameObjectsWithTag("Platform");
        StartCoroutine(FlipAndWait());
    }


}
