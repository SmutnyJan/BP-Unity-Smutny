using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public PlayableDirector director;
    public KeyCode skipKey = KeyCode.Return;


    public void SkipTimelineAndLoadScene()
    {
        if (director != null)
        {
            director.Stop();
        }
        Debug.Log("Jump to another scene");
    }

    void Update()
    {
        if (Input.GetKeyDown(skipKey))
        {
            SkipTimelineAndLoadScene();
        }
    }
}
