using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSpell : MonoBehaviour
{
    #region constants
    public const float EnabledAlpha = 1;
    public const float DisabledAlpha = 0.3f;
    public const float FadeSpeed = 0.07f;
    #endregion

    #region privates
    private bool _enabled;
    private SpriteRenderer _spriteRender;
    private Coroutine _currentcoroutine;
    #endregion

    #region publics
    [SerializeField]
    public CreateSpellType Type;
    #endregion

    void Start ()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        _enabled = true;
        Disable();
    }

    public void Enable()
    {
        if (_enabled)
            return;

        _enabled = true;

        if (_currentcoroutine != null)
            StopCoroutine(_currentcoroutine);

        StartCoroutine(FadeIn());
    }

    public void Disable()
    {
        if (!_enabled)
            return;

        _enabled = false;

        if (_currentcoroutine != null)
            StopCoroutine(_currentcoroutine);

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        while (_spriteRender.color.a <= EnabledAlpha)
        {
            _spriteRender.color = new Color(_spriteRender.color.r, _spriteRender.color.g, _spriteRender.color.b, _spriteRender.color.a + FadeSpeed);
            yield return null;
        }

        _spriteRender.color = new Color(_spriteRender.color.r, _spriteRender.color.g, _spriteRender.color.b, EnabledAlpha);
    }

    private IEnumerator FadeOut()
    {
        while (_spriteRender.color.a >= DisabledAlpha)
        {
            _spriteRender.color = new Color(_spriteRender.color.r, _spriteRender.color.g, _spriteRender.color.b, _spriteRender.color.a - FadeSpeed);
            yield return null;
        }
    }

}
