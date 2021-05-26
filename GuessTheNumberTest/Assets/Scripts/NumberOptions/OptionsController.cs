using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    [Header("Object Data")]
    [SerializeField] private GameObject _optionPrefab;
    [SerializeField] private RectTransform _grid;

    [Header("Visual Info")]
    [SerializeField] private float _transitionDuration;
    
    private List<IOption> _options = new List<IOption>();
    private Exercice _exerciceData;

    public void CreateOptions(int maxChoices)
    {
        for (int i = 0; i < maxChoices; ++i)
        {
            IOption option = Instantiate(_optionPrefab, _grid).GetComponent<IOption>();
            option.Initialize();
            _options.Add(option);
        }
    }

    public void StartExercice(Exercice exerciceData, IExerciceAnswer exerciceAnswer)
    {
        _exerciceData = exerciceData;
        for (int i = 0; i < _exerciceData.choices.Count; ++i)
        {
            IOption option = _options.GetValueOrDefault(i);
            if (option != null)
            {
                option.StartExercice(_exerciceData.choices[i], i, exerciceAnswer);
                StartCoroutine(option.Show());
            }
        }
    }

    public IEnumerator EndExercice()
    {
        foreach (IOption option in _options)
        {
            StartCoroutine(option.Hide());
        }

        yield return new WaitForSeconds(_transitionDuration);
    }

    public void SetOptionsInteractable(bool state)
    {
        foreach (IOption option in _options)
        {
            option.SetInteractable(state);
        }
    }

    #region Animations
    public void ShowCorrect()
    {
        IOption option = _options.GetValueOrDefault(_exerciceData.correctIndexChoice);
        if (option != null)
        {
            option.SetCorrect();
        }
    }

    public void ShowFailed(int index)
    {
        IOption option = _options.GetValueOrDefault(index);
        if (option != null)
        {
            option.SetWrong();
        }
    }

    public void ShowFailedAndCorrect(int index)
    {
        ShowFailed(index);
        ShowCorrect();
    }
    #endregion

}
