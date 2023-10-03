using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BonusParticlesManager : MonoBehaviour
{
    [Header("Elements ")]
    [SerializeField] private CarrotManager carrotManager;
    [SerializeField] private GameObject bonusParticlePrefab;

    [Header(" Pooling")]
    private ObjectPool<GameObject> bonusParticlesPool;

    private void Awake()
    {
        InputManager.onCarrotClickedPosition += CarrotClickedCallback;
    }
    private void OnDestroy()
    {
        InputManager.onCarrotClickedPosition -= CarrotClickedCallback;
    }

    private void Start()
    {
        bonusParticlesPool = new ObjectPool<GameObject>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy, true, 50);
    }

    private GameObject CreateFunction()
    {
        return Instantiate(bonusParticlePrefab, transform);
    }
    private void ActionOnGet(GameObject bonusParticle)
    {
        bonusParticle.SetActive(true);
    }
    private void ActionOnRelease(GameObject bonusParticle)
    {
        bonusParticle.SetActive(false);
    }

    private void ActionOnDestroy(GameObject bonusParticle)
    {
        Destroy(bonusParticle);
    }

    private void CarrotClickedCallback(Vector2 clickedPosition)
    {
        GameObject bonusParticleInstance = bonusParticlesPool.Get();
        
        bonusParticleInstance.transform.position = clickedPosition;
        bonusParticleInstance.GetComponent<BonusParticle>().Configure(carrotManager.GetCurrentMultiplier());
        
        LeanTween.delayedCall(1, () => bonusParticlesPool.Release(bonusParticleInstance));
    }

}
