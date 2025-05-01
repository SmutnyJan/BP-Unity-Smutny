using System.Collections;
using UnityEngine;

public class AutomatController : MonoBehaviour, ISeasonChange
{
    public GameObject Can;
    private float _shootForce = 10f;
    private int _directionMultiplier;
    private int _timeDelayOffset = 0;
    private Coroutine _spawnCoroutine;
    private AudioSource _audioSource;
    private void Start()
    {
        _directionMultiplier = GetComponent<SpriteRenderer>().flipX ? 1 : -1;
        _spawnCoroutine = StartCoroutine(SpawnCanRoutine());
        _audioSource = GetComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = AudioManager.Instance.SFXAudioSource.outputAudioMixerGroup;

    }

    private IEnumerator SpawnCanRoutine()
    {
        yield return new WaitForSeconds(Random.Range(2f, 6f));
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f) + _timeDelayOffset);
            int random = Random.Range(1, 5);
            AudioManager.Instance.PlayClipByName("Can_" + random, AudioManager.Instance.AudioLibrary.Player, _audioSource);

            GameObject newCan = Instantiate(Can, transform.TransformPoint(new Vector3(0, -2.91f, 0)), Quaternion.identity);

            Rigidbody2D rb = newCan.GetComponent<Rigidbody2D>();

            rb.AddForce(_directionMultiplier * transform.right * _shootForce, ForceMode2D.Impulse);
            rb.AddTorque(10f, ForceMode2D.Impulse);
        }
    }

    public void AnimateToSeason(SeasonsManager.Season season)
    {
        switch (season)
        {
            case SeasonsManager.Season.Spring:
                _timeDelayOffset = 0;
                break;
            case SeasonsManager.Season.Autumn:
                StopCoroutine(_spawnCoroutine);
                break;
            case SeasonsManager.Season.Winter:
                _spawnCoroutine = StartCoroutine(SpawnCanRoutine());
                _timeDelayOffset = 10;
                break;
            default:
                _timeDelayOffset = 7;
                break;
        }
    }
}
