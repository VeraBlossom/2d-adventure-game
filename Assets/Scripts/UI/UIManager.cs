using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject text1;
    public GameObject text2;

    public static UIManager Instance;

    private void Start()
    {
        Instance = this;
    }
}
