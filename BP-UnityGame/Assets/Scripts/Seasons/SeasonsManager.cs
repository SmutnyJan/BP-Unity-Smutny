using System;
using System.Collections;
using UnityEngine;

public class SeasonsManager : MonoBehaviour
{
    public Transform ColliderEndPoint;
    public static SeasonsManager Instance;
    public event Action<Season> OnSeasonChangeStarted;
    public GameObject WindArea;
    public GameObject SeasonIconCanvas;



    public Season CurrentSeason;
    private float _colliderMoveSpeed;
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
        MicrophoneManager.Instance.OnLoudNoiseDetected += NextSeason;
    }

    private void StartSeasonChange()
    {
        if (_usedAxis == Axis.X)
        {
            _usedAxis = Axis.Y;
            _colliderMoveSpeed = 20;
        }
        else
        {
            _usedAxis = Axis.X;
            _colliderMoveSpeed = 40;
        }

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
        SeasonIconCanvas.SetActive(false);

    }

    public void NextSeason()
    {
        if (_seasonChanging)
        {
            return;
        }

        _seasonChanging = true;
        SeasonIconCanvas.SetActive(true);

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

        foreach (MonoBehaviour comp in collision.gameObject.GetComponents<MonoBehaviour>())
        {
            if (comp is ISeasonChange)
            {
                (comp as ISeasonChange).AnimateToSeason(CurrentSeason);
            }

        }
    }

    private void OnDestroy()
    {
        MicrophoneManager.Instance.OnLoudNoiseDetected -= NextSeason;

    }

}
