using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

public class ProvisionalGameLoader : MonoBehaviour
{
    void Awake()
    {
        _ = AsyncLoadLevel(40);
    }

    async Task AsyncLoadLevel(int levelId)
    {
        ILevelRepository levelRepository = ServerLevelRepository.GetInstance();

        var level = await levelRepository.GetById(levelId);

        //agregar la data al EnvDataHandler

        GameLevelEvents.TriggerOnSetLevelStruct(level.estructura_nivel);
    }
}
