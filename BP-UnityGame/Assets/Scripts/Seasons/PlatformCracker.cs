using System.Collections;
using UnityEngine;

public class PlatformCracker : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private MaterialPropertyBlock _propertyBlock;
    private BoxCollider2D _boxCollider;
    private CrackState _crackState;

    public enum CrackState
    {
        None,
        One,
        Two,
        Three
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _propertyBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        SetCrackLevel(1f, 1f);
        _crackState = CrackState.None;
    }

    private void SetCrackLevel(float edgeMin, float edgeMax)
    {
        _spriteRenderer.GetPropertyBlock(_propertyBlock);

        _propertyBlock.SetFloat("_CrackEdgeMin", edgeMin);
        _propertyBlock.SetFloat("_CrackEdgeMax", edgeMax);

        _spriteRenderer.SetPropertyBlock(_propertyBlock);
    }

    public void Crack()
    {
        switch (_crackState)
        {
            case CrackState.None:
                _crackState = CrackState.One;
                SetCrackLevel(0.5f, 0);
                break;
            case CrackState.One:
                _crackState = CrackState.Two;
                SetCrackLevel(0.7f, 0.1f);
                break;
            case CrackState.Two:
                _crackState = CrackState.Three;
                SetCrackLevel(1f, 0.1f);
                break;
            case CrackState.Three:
                _crackState = CrackState.Three;
                StartCoroutine(HandleBreak());
                break;
        }
    }

    private IEnumerator HandleBreak()
    {
        _spriteRenderer.enabled = false;
        _boxCollider.enabled = false;
        yield return new WaitForSeconds(5f);
        _spriteRenderer.enabled = true;
        _boxCollider.enabled = true;
        _crackState = CrackState.None;
        SetCrackLevel(1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "GroundCheck") //odstranìní duplicitního volání
        {
            return;
        }

        Crack();
    }
}
