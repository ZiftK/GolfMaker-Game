using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

public class ProvisionalGameLoader : MonoBehaviour
{
    void Awake()
    {
        GameLevelEvents.OnLoadLevelEvent += LoadLevel;
    }

    public void LoadLevel(int levelId)
    {
        _ = AsyncLoadLevel(levelId);
    }

    async Task AsyncLoadLevel(int levelId)
    {
        ILevelRepository levelRepository = ServerLevelRepository.GetInstance();

        var level = await levelRepository.GetById(levelId);

        //agregar la data al EnvDataHandler

        GameLevelEvents.TriggerOnSetLevelStruct(level.estructura_nivel);
    }
}
