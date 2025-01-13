using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public static SceneLoaderManager Instance;
    public Animator SceneTransitionAnimator;
    public GameObject CrossfadeCanvas;

    private bool _firstSceneLoaded = false;

    public enum Scene
    {
        MainMenu,
        Intro
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if(_firstSceneLoaded)
        {
            SceneTransitionAnimator.SetTrigger("End");
        }
        _firstSceneLoaded = true;

    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
    }



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
