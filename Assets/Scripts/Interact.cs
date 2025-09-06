using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 offset;
    [SerializeField] private int rayNum = 1;
    [SerializeField, Range(1,90)] private float angle;
    private Coroutine coroutine;
    private IInteractable interactable = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coroutine = StartCoroutine(RaycastLoop());
    }
    private void OnEnable() {
        EventManager.Instance.onInteract += ExecuteInteract;
    }
    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }
    // Update is called once per frame
    private void ExecuteInteract()
    {
        if (interactable == null)
        {
            Debug.Log("Interactable is null!");
        }
        else
        {
            interactable.Interact(transform);
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
    private void SearchForInteract()
    {
        float sectionPropotion = 1f / (rayNum + 1);
        for (int i = 1; i <= rayNum; i++)
        {
            float sectionAngle = Mathf.Lerp(-angle, angle, sectionPropotion * i);
            Vector3 direction = Quaternion.Euler(0, sectionAngle, 0) * transform.forward;
            if (Physics.Raycast(transform.position + offset, direction, out RaycastHit hit, maxDistance, layerMask))
            {
                interactable = hit.transform.GetComponent<IInteractable>();
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
}
