using System.Text.RegularExpressions;
using UnityEngine;

public class PaperPlaneController : MonoBehaviour, ISeasonChange
{
    public GameObject TargetDestination;
    public GameObject PickMoneyItem;

    private Vector2 _startPosition;
    private Vector2 _targetPosition;
    private Vector2 _currentTarget;

    private float _floatAmplitude = 0.5f;
    private float _floatFrequency = 2f;
    private float _defaultSpeed = 2f;
    private float _speed;
    private float _maxRotationAngle = 15f;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        if (!Regex.Match(this.transform.parent.name, @"^Moving Paper Plane \((\d+)\)$").Success)
        {
            Debug.LogWarning("Plane name " + this.transform.parent.name + " incorrect format!");
        }

        _startPosition = transform.position;
        _targetPosition = TargetDestination.transform.position;
        _currentTarget = _targetPosition;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _speed = _defaultSpeed;
    }

    void Update()
    {
        Vector2 basePosition = Vector2.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);

        float yOffset = Mathf.Sin(Time.time * _floatFrequency) * _floatAmplitude;


        // Pokud je sprite zrcadlený (flipX), otoè smìr rotace
        float directionMultiplier = _spriteRenderer.flipX ? -1f : 1f;

        //využíváme derivace sinus (cosinus) pro výpoèet sklonu
        float rotationZ = Mathf.Cos(Time.time * _floatFrequency) * _maxRotationAngle * directionMultiplier;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        // Nastavení výsledné pozice
        transform.position = new Vector2(basePosition.x, _startPosition.y + yOffset);

        // Pøepnutí smìru
        if (Mathf.Abs(transform.position.x - _currentTarget.x) < 0.1)
        {
            _currentTarget = (_currentTarget == _targetPosition) ? _startPosition : _targetPosition;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }

    public void SwitchToSeason(SeasonsManager.Season season)
    {
        switch (season)
        {
            case SeasonsManager.Season.Summer:
                _speed *= 2;
                break;
            default:
                _speed = _defaultSpeed;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SaveLoadManager.Instance.Progress.LevelConfig.PlanesDestroyedIndexes.Add(
            int.Parse(Regex.Match(this.transform.parent.name, @"\((\d+)\)").Groups[1].Value)
        );

        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.position = SaveLoadManager.Instance.Progress.LevelConfig.SpawnPoint + new Vector3(0, 5, 0);
            return;
        }
        else
        {
            PickMoneyController pickMoneyController = Instantiate(PickMoneyItem, transform.position, Quaternion.identity).GetComponent<PickMoneyController>();
            pickMoneyController.MoneyValue = Random.Range(5, 10);
        }

        Destroy(this.transform.parent.gameObject);
    }
}
