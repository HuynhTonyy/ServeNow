using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class OperatableCounter : Counter
{
    [SerializeField] private float operateTime;
    [Header("Progress Bar")]
    [SerializeField] protected GameObject progressBarGroup;
    [SerializeField] private Image progressBar;
    [SerializeField] private float progressBarAnimDuration = 0.2f;
    [SerializeField] private Ease progressBarAnimEase =  Ease.Linear;
    private float operateTimeLeft = 0;
    protected bool Done = false;
    [Header("Auto")]
    [SerializeField] private bool isAutoOperate = false;
    [SerializeField] private float autoOperateMultiplier = 1f;
    [Header("Deplete")]
    [SerializeField] private bool isDepletOvertime =  false;
    [SerializeField] private float waitBeforeDepletTime = 0.5f;
    [SerializeField] private float depletMultiplier =  1f;
    private float currentWaitBeforeDepletTime;
    protected virtual void Start()
    {
        currentWaitBeforeDepletTime = waitBeforeDepletTime;
        progressBarGroup.SetActive(false);
    }

    protected virtual void Update()
    {
        if(Done) return;
        if(!carriedObject) return;
        if (isAutoOperate)
        {
            OperateTime(autoOperateMultiplier);
            return;
        }
        if(operateTimeLeft <= 0f) return;
        if(!isDepletOvertime) return;
        Deplete();
    }

    private void Deplete()
    {
        if (currentWaitBeforeDepletTime > 0f)
        {
            currentWaitBeforeDepletTime -=  Time.deltaTime;
            return; 
        }
        operateTimeLeft = Math.Max(operateTimeLeft - Time.deltaTime * depletMultiplier, 0f);
        OnProgressChange(operateTimeLeft / operateTime);
        
    }
    public override void Interact(Transform interacterTransform, GameObject currentObject)
    {
        base.Interact(interacterTransform, currentObject);
        Done = false;
        operateTimeLeft = 0;
        progressBar.fillAmount = 0;
    }
    public virtual void Operate()
    {
        if (!carriedObject || Done) return;
        currentWaitBeforeDepletTime = waitBeforeDepletTime;
        OperateTime();
    }

    private void OperateTime(float multiplier = 1f)
    {
        operateTimeLeft = Math.Min(operateTimeLeft + Time.deltaTime * multiplier, operateTime);
        OnProgressChange(operateTimeLeft / operateTime);
        if (operateTime > operateTimeLeft) return;
        Done = true;
        operateTimeLeft = operateTime;
    }

    private void OnProgressChange( float to)
    {
        DOTween.Kill(progressBar);
        DOTween.To(
            () => progressBar.fillAmount,
            x => progressBar.fillAmount = x,
            to,
            progressBarAnimDuration)
            .SetEase(progressBarAnimEase);
        DOTween.To(
            () => progressBar.color,
            x => progressBar.color = x,
            Color.Lerp(Color.white, Color.springGreen, to),
            progressBarAnimDuration)
            .SetEase(progressBarAnimEase);
    }
}
