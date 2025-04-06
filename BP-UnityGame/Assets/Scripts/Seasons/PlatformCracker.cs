using UnityEngine;

public class PlatformCracker : MonoBehaviour
{
    private SpriteRenderer sr;
    private MaterialPropertyBlock propertyBlock;
    public CrackState _crackState;

    public enum CrackState
    {
        None,
        One,
        Two,
        Three
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        propertyBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        SetCrackLevel(1f, 1f);
        _crackState = CrackState.None;
    }

    private void SetCrackLevel(float edgeMin, float edgeMax)
    {
        sr.GetPropertyBlock(propertyBlock);

        propertyBlock.SetFloat("_CrackEdgeMin", edgeMin);
        propertyBlock.SetFloat("_CrackEdgeMax", edgeMax);

        sr.SetPropertyBlock(propertyBlock);
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
                _crackState = CrackState.None;
                SetCrackLevel(1f, 1f);
                break;
        }
    }
}
