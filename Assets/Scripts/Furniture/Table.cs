using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] Transform chairsContainer;
    [SerializeField] float chairDistance;
    [SerializeField] float checkingDistance;
    [SerializeField] LayerMask layerMask;
    private List<GameObject> chairs = new List<GameObject>();
    private List<Vector3> directions = new List<Vector3>(){Vector3.forward, Vector3.back, Vector3.right, Vector3.left};
    void Start()
    {
        GetChairs();
            CheckChairAvaiable();

    }
    private void GetChairs()
    {
        chairs = new List<GameObject>();
        for (int i = 0; i < chairsContainer.childCount; i++)
        {
            chairs.Add(chairsContainer.GetChild(i).gameObject);
        }
    }
    private void CheckChairAvaiable()
    {
        for (int i = 0; i < directions.Count; i++)
        {
            var isBlocked = CheckDirection(directions[i]);
            chairs[i].SetActive(!isBlocked);
            Debug.Log(directions[i].ToString() + isBlocked);
            if(isBlocked) continue;
            chairs[i].transform.SetLocalPositionAndRotation(directions[i] * chairDistance,Quaternion.identity);
        }
    }
    private bool CheckDirection(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, checkingDistance, layerMask);
    }
    private void OnDrawGizmos() {
        foreach (var item in directions)
        {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (item * checkingDistance) + transform.position);
        }
    }
}
