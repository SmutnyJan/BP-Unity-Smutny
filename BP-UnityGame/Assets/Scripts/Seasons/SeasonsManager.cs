using System;
using System.Collections;
using UnityEngine;

public class SeasonsManager : MonoBehaviour
{
    public Transform ColliderEndPoint;
    public static SeasonsManager Instance;
    public static event Action<Season> OnSeasonChangeStarted;



    public Season CurrentSeason;
    private float _colliderMoveSpeed = 10f;
    private Axis _usedAxis = Axis.X;
    private bool _seasonChanging = false;
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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CurrentSeason = Season.Spring;
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
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
    }

    public void NextSeason()
    {
        if (_seasonChanging)
        {
            return;
        }

        _seasonChanging = true;

        switch (CurrentSeason)
        {
            case Season.Spring:
                CurrentSeason = Season.Summer;
                break;
            case Season.Summer:
                CurrentSeason = Season.Autumn;
                break;
            case Season.Autumn:
                CurrentSeason = Season.Winter;
                break;
            case Season.Winter:
                CurrentSeason = Season.Spring;
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
                (comp as ISeasonChange).SwitchToSeason(CurrentSeason);
            }
        }
    }

}
