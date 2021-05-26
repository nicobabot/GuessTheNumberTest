using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberOption : MonoBehaviour, IOption
{
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _canvasGroup;
    
    [Header("Visual Info")]
    [SerializeField] private Color _correctOptionColor;
    [SerializeField] private Color _wrongOptionColor;
    [SerializeField] private float _transitionDuration;

    //Number Option Data
    private int _myNumber;
    private int _index;
    private Color _initialColor;

    //Exercice answer parent
    private IExerciceAnswer _answer;

    public void Initialize()
    {
        _initialColor = new Color(_numberText.color.r, _numberText.color.g, _numberText.color.b, 1);
    }

    public void StartExercice(int number, int index, IExerciceAnswer answer)
    {
        _myNumber = number;
        _index = index;
        _answer = answer;
        _numberText.text = number.ToString();
    }

    public void SetInteractable(bool state)
    {
        _button.interactable = state;
    }

    public void SetCorrect()
    {
        _numberText.color = _correctOptionColor;
    }

    public void SetWrong()
    {
        _numberText.color = _wrongOptionColor;
    }

    public IEnumerator Show()
    {
        yield return AlphaScaleTransition(1, _transitionDuration);
        SetInteractable(true);
    }

    public IEnumerator Hide()
    {
        SetInteractable(false);
        yield return AlphaScaleTransition(0, _transitionDuration);
        ResetValues();
    }

    private IEnumerator AlphaScaleTransition(float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = _canvasGroup.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            _canvasGroup.transform.localScale = new Vector3(newAlpha, newAlpha, newAlpha);
            _canvasGroup.alpha = newAlpha;
            yield return null;
        }
    }

    //Function that will be called when player choses the number
    public void ChooseNumber()
    {
        _answer.ChoseNumber(_myNumber, _index);
    }

    //Reset color and state to the original state
    public void ResetValues()
    {
        _numberText.color = _initialColor;
    }
}
