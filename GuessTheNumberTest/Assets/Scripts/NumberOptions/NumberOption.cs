using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Visual Info")]
    [SerializeField] private Color _correctOptionColor;
    [SerializeField] private Color _wrongOptionColor;
    [SerializeField] private float _fadeDuration;

    private int _myNumber;
    private Color _initialColor;

    //Have parent to send warning that means player has chosen
    public void Initialize(int number)
    {
        _myNumber = number;
        _initialColor = new Color(_numberText.color.r, _numberText.color.g, _numberText.color.b, 0);
    }

    public void SetState(bool state)
    {
        _button.interactable = state;
    }

    public void SetCorrectOption()
    {
        _numberText.color = _correctOptionColor;
    }

    public void SetWrongOption()
    {
        _numberText.color = _wrongOptionColor;
    }

    public IEnumerator ShowOption()
    {
        yield return AlphaTransition(1, _fadeDuration);
        SetState(true);
    }

    public IEnumerator HideOption()
    {
        SetState(false);
        yield return AlphaTransition(0, _fadeDuration);
    }

    private IEnumerator AlphaTransition(float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = _canvasGroup.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            _canvasGroup.alpha = newAlpha;
            yield return null;
        }
    }

    public void ChooseNumber()
    {
        //Warn parent the number that has been chosen
    }

    public void ResetValues()
    {
        _numberText.color = _initialColor;
    }

    //debug
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            _fadeDuration = 2;
            StartCoroutine(ShowOption());
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            _fadeDuration = 2;
            StartCoroutine(HideOption());
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SetCorrectOption();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            SetWrongOption();
        }
    }
}
