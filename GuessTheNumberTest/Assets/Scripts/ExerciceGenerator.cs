using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciceGenerator : MonoBehaviour
{
    List<int> _testList = new List<int>();

    void Start()
    {
        _testList.Add(5);
        _testList.Add(7);
        _testList.Add(8); 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ShuffleList();
        }
    }

    private void ShuffleList()
    {
        _testList.Shuffle();

        foreach (var item in _testList)
        {
            Debug.Log(item);
        }
    }
}
