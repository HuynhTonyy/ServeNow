using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 offset;
    [SerializeField] private int rayNum = 1;
    [SerializeField, Range(1, 90)] private float angle;
    private Coroutine coroutine;
    private GameObject carriedObject = null;
    [SerializeField] private Vector3 holdingPos;
    [SerializeField] private string selectTag;
    GameObject selectObj = null;
    GameObject highligh = null;
    ItemData itemData = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coroutine = StartCoroutine(RaycastLoop());
    }
    private void OnEnable()
    {
        EventManager.Instance.onInteract += ExecuteInteract;
        EventManager.Instance.onOperate += ExecuteOperate;
        EventManager.Instance.onPickUpCarriedObject += PickupCarriedObject;
        EventManager.Instance.onClearCrarriedObject += ClearCarriedObject;
    }
    private void OnDisable()
    {
        EventManager.Instance.onInteract -= ExecuteInteract;
        EventManager.Instance.onOperate -= ExecuteOperate;
        EventManager.Instance.onPickUpCarriedObject -= PickupCarriedObject;
        EventManager.Instance.onClearCrarriedObject -= ClearCarriedObject;
        StopCoroutine(coroutine);
    }
    // Update is called once per frame
    private void ExecuteInteract()
    {
        if (!selectObj) return;
        if (!selectObj.TryGetComponent<IInteractable>(out var interactable))
        {
            Debug.Log("Interactable is null!");
        }
        else
        {
            interactable.Interact(transform,itemData, carriedObject);
        }
    }
    IEnumerator RaycastLoop()
    {
        while (true)
        {
            SearchForInteract();
            yield return new WaitForSeconds(0.2f);
        }
    }
    private void ExecuteOperate()
    {
        if (!selectObj) return;
        if (!selectObj.TryGetComponent<OperatableCounter>(out var operatable))
        {
            Debug.Log("Operatable not found!");
        }
        else
        {
            operatable.Operate();
        }
        
    }
    private void SearchForInteract()
    {
        bool found = false;
        if (selectObj && highligh && highligh.CompareTag(selectTag))
        {
            highligh.SetActive(false);
            selectObj = null;
            highligh = null;
        }
        float sectionPropotion = 1f / (rayNum + 1);
        for (int i = 1; i <= rayNum; i++)
        {
            float sectionAngle = Mathf.Lerp(-angle, angle, sectionPropotion * i);
            Vector3 direction = Quaternion.Euler(0, sectionAngle, 0) * transform.forward;
            if (Physics.Raycast(transform.position + offset, direction, out RaycastHit hit, maxDistance, layerMask))
            {
                selectObj = hit.transform.gameObject;
                highligh = selectObj.transform.childCount > 0 ? selectObj.transform.GetChild(0).gameObject : null;
                found = true;
            }
        }
        if (!found)
        {
            selectObj = null;
            highligh = null;
        }
        else
        {
            if (selectObj && highligh && highligh.CompareTag(selectTag))
            {
                highligh.SetActive(true);
            }
        }
    }

    private void OnDrawGizmos()
    {
        float sectionPropotion = 1f / (rayNum + 1);

        for (int i = 1; i <= rayNum; i++)
        {
            float sectionAngle = Mathf.Lerp(-angle, angle, sectionPropotion * i);
            Vector3 direction = Quaternion.Euler(0, sectionAngle, 0) * transform.forward;
            Debug.DrawRay(transform.position + offset, direction * maxDistance, Color.white, 0.1f);
        }
    }
    private void PickupCarriedObject(GameObject pickupObj, ItemData newItemData)
    {
        itemData = newItemData;
        carriedObject = pickupObj;
        carriedObject.transform.parent = transform;
        carriedObject.transform.SetLocalPositionAndRotation(holdingPos, Quaternion.identity);
    }
    private void ClearCarriedObject()
    {
        carriedObject = null;
        itemData = null;
    }
}
