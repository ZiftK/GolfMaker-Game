using UnityEngine;
using UnityEngine.VFX;
public class EffectsEvents
{

    public delegate void EffectByNameAndObject(string effectName, GameObject effect);

    public static event EffectByNameAndObject EffectDisabled;
    public static void OnEffectDisabled(string effectName, GameObject effect)
    {
        EffectDisabled?.Invoke(effectName, effect);
    }

    public static void ThrowEffect(string effectName, Vector3 position)
    {
        GameObject effect = EffectsFactory.Instance.GetEffect(effectName);
        effect.transform.position = position;
        effect.SetActive(true);
        effect.GetComponent<VisualEffect>().Play();
    }
}
