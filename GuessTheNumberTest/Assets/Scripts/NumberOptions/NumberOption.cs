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

    private Color _correctOptionColor;
    private Color _wrongOptionColor;
    private float _fadeDuration;

    private int _myNumber;
    private int _id;
    private Color _initialColor;
    private ExerciceAnswer _answer;

    public void Initialize(Color correctOptionColor, Color wrongOptionColor, float fadeDuration)
    {
        _correctOptionColor = correctOptionColor;
        _wrongOptionColor = wrongOptionColor;
        _fadeDuration = fadeDuration;
    }

    public void StartExercice(int number, int id, ExerciceAnswer answer)
    {
        _myNumber = number;
        _id = id;
        _answer = answer;
        _numberText.text = number.ToString();
        _initialColor = new Color(_numberText.color.r, _numberText.color.g, _numberText.color.b, 1);
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
        ResetValues();
    }

    private IEnumerator AlphaTransition(float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = _canvasGroup.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            _canvasGroup.transform.localScale= new Vector3(newAlpha, newAlpha, newAlpha);
            _canvasGroup.alpha = newAlpha;
            yield return null;
        }
    }

    public void ChooseNumber()
    {
        //Warn parent the number that has been chosen
        _answer.NumberChosen(_myNumber, _id);
    }

    public void ResetValues()
    {
        _numberText.color = _initialColor;
    }
}
