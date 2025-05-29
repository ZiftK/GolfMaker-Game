using System;
using System.Collections.Generic;
using UnityEngine;


public struct EffectPool
{
    public GameObject prefab;
    public Stack<GameObject> pool;

    public EffectPool(GameObject prefab)
    {
        this.prefab = prefab;
        pool = new Stack<GameObject>();
    }
}

public class EffectsFactory : MonoBehaviour
{
    public static EffectsFactory Instance;

    public EffectStorage effectStorage;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        RecopileEffects();

        EffectsEvents.EffectDisabled += DisableEffect;
    }

    void RecopileEffects()
    {
        foreach (var effect in effectStorage.effects)
        {
            if (effectsMap.ContainsKey(effect.name))
            {
                Debug.LogError($"Effect {effect.name} already exists in effects map.");
                continue;
            }

            effectsMap.Add(effect.name, new EffectPool(effect.prefab));
        }
        
        
    }

    void DisableEffect(string effectName, GameObject effect)
    {
        if (!effectsMap.ContainsKey(effectName))
        {
            Debug.LogError($"Effect {effectName} not found in effects map.");
            return;
        }

        effect.GetComponent<EffectToPool>().effect.Stop();
        effect.SetActive(false);
        effectsMap.TryGetValue(effectName, out EffectPool effectStack);
        effectStack.pool.Push(effect);
    }

    public Dictionary<string, EffectPool> effectsMap = new Dictionary<string, EffectPool>();

    public GameObject GetEffect(string effectName)
    {
        if (!effectsMap.ContainsKey(effectName))
        {
            Debug.LogError($"Effect {effectName} not found in effects map.");
            return null;
        }

        effectsMap.TryGetValue(effectName, out EffectPool effectStack);

        if (effectStack.pool.Count == 0)
        {
            GameObject effect = Instantiate(effectStack.prefab);
            EffectToPool effectToPool = effect.AddComponent<EffectToPool>();
            effectToPool.effectName = effectName;
            return effect;
        }

        return effectStack.pool.Pop();
    }
}
