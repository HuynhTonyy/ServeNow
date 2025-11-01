using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private List<GameObject> skin;
    private int currentSkinIndex = 0;
    private void OnEnable() {
        SetRandomSkin();
    }
    private void SetRandomSkin(){
        currentSkinIndex = Random.Range(0, skin.Count);
        for (int i = 0; i < skin.Count; i++)
        {
            skin[i].SetActive(false);
        }
        skin[currentSkinIndex].SetActive(true);
    }
}
