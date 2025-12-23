using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int index;
    public GameManager gameManager;
    Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        gameManager.OnCellClicked(index);
    }

}
