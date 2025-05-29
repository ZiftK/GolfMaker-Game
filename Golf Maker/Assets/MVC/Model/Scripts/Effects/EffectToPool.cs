using UnityEngine;
using UnityEngine.VFX;

public class EffectToPool : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public VisualEffect effect;
    public string effectName;

    void Update()
    {
        if (effect == null)
        {
            effect = gameObject.GetComponent<VisualEffect>();
        }

        if (effect.aliveParticleCount == 0)
        {
            EffectsEvents.OnEffectDisabled(effectName, gameObject);
        }
    }
}
