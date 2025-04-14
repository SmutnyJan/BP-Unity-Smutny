using System;
using System.Collections;
using UnityEngine;

public class SeasonsManager : MonoBehaviour
{
    public Transform ColliderEndPoint;
    public static SeasonsManager Instance;
    public event Action<Season> OnSeasonChangeStarted;
    public GameObject WindArea;



    public Season CurrentSeason;
    private float _colliderMoveSpeed = 15f;
    private Axis _usedAxis = Axis.X;
    private bool _seasonChanging = false;



    public GameObject SnowParticles;
    public GameObject LeavesParticles;
    private enum Axis { X, Y }
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CurrentSeason = Season.Spring;
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();

        MicrophoneManager.Instance.OnLoudNoiseDetected += NextSeason;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            NextSeason();
        }
    }

    private void StartSeasonChange()
    {
        _usedAxis = _usedAxis == Axis.X ? Axis.Y : Axis.X;
        OnSeasonChangeStarted?.Invoke(CurrentSeason);

        StartCoroutine(MoveCollider(this.transform, _usedAxis));



    }

    private IEnumerator MoveCollider(Transform targetTransform, Axis axis)
    {
        Vector3 startPos = targetTransform.position;
        Vector3 endPos = startPos;

        switch (axis)
        {
            case Axis.X:
                endPos = new Vector3(ColliderEndPoint.position.x, startPos.y, startPos.z);
                break;
            case Axis.Y:
                endPos = new Vector3(startPos.x, ColliderEndPoint.position.y, startPos.z);
                break;
        }

        while (Vector3.Distance(targetTransform.position, endPos) > 0.01f)
        {
            targetTransform.position = Vector3.MoveTowards(targetTransform.position, endPos, _colliderMoveSpeed * Time.deltaTime);
            yield return null;
        }

        targetTransform.position = startPos;


        _seasonChanging = false;
        Debug.Log("zmena dokoncena");
    }

    public void NextSeason()
    {
        if (_seasonChanging)
        {
            return;
        }
        Debug.Log("Zmìna poèasí");

        _seasonChanging = true;

        switch (CurrentSeason)
        {
            case Season.Spring:
                CurrentSeason = Season.Summer;
                break;
            case Season.Summer:
                CurrentSeason = Season.Autumn;
                LeavesParticles.SetActive(true);
                WindArea.SetActive(true);
                break;
            case Season.Autumn:
                CurrentSeason = Season.Winter;
                LeavesParticles.SetActive(false);
                WindArea.SetActive(false);
                SnowParticles.SetActive(true);
                FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.Freezing);
                break;
            case Season.Winter:
                CurrentSeason = Season.Spring;
                SnowParticles.SetActive(false);
                FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.None);
                break;
            default:
                CurrentSeason = Season.Spring;
                break;
        }

        StartSeasonChange();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision.gameObject.name);

        foreach (MonoBehaviour comp in collision.gameObject.GetComponents<MonoBehaviour>())
        {
            if (comp is ISeasonChange)
            {
                (comp as ISeasonChange).SwitchToSeason(CurrentSeason);
            }

        }
    }

    private void OnDestroy()
    {
        MicrophoneManager.Instance.OnLoudNoiseDetected -= NextSeason;

    }

}
