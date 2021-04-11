using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    [Header("Object Data")]
    [SerializeField] private NumberOption _numberOptionObject;
    [SerializeField] private Transform _grid;

    [Header("Visual Info")]
    [SerializeField] private Color _correctOptionColor;
    [SerializeField] private Color _wrongOptionColor;
    [SerializeField] private float _transitionDuration;

    private List<NumberOption> _numbers = new List<NumberOption>();

    public void CreateOptions(int maxChoices)
    {
        for (int i = 0; i < maxChoices; ++i)
        {
            NumberOption nb = Instantiate(_numberOptionObject, _grid);
            nb.Initialize(_correctOptionColor, _wrongOptionColor, _transitionDuration);
            _numbers.Add(nb);
        }
    }

    public void StartExercice(Exercice exerciceData, ExerciceAnswer exerciceAnswer)
    {
        for (int i = 0; i < exerciceData.choices.Count; ++i)
        {
            NumberOption nb = _numbers.GetValueOrDefault(i);
            if (nb != null)
            {
                nb.StartExercice(exerciceData.choices[i], i, exerciceAnswer);
                StartCoroutine(nb.ShowOption());
            }
        }
    }

    public IEnumerator EndExercice()
    {
        foreach (NumberOption nb in _numbers)
            StartCoroutine(nb.HideOption());

        yield return new WaitForSeconds(_transitionDuration);
    }

    public List<NumberOption> GetOptionsObjects()
    {
        return _numbers;
    }

}
