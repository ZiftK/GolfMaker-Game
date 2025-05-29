using System;
using System.Threading.Tasks;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class EditorEventHandler : MonoBehaviour
{

    EditorLevelEvents levelEventsHandler;
    ILevelRepository levelRepository;

    void Awake()
    {
        levelRepository = ServerLevelRepository.GetInstance(); 
        levelEventsHandler = EditorLevelEvents.GetInstance();

        levelEventsHandler.SaveLevel += OnSaveLevel;
        levelEventsHandler.LoadLevel += OnLoadLevel;
    }

    private void OnSaveLevel(object sender, SaveLevelArgs e)
    {
        Debug.Log("Saving level...");
        LevelEntity levelToSave = e.levelData;

        EnvDataHandler.Instance.SetLevelInEditionUserData(ref levelToSave);

        _ = AsyncOnSaveLevel(levelToSave);
        
    }

    private async Task AsyncOnSaveLevel(LevelEntity levelToSave)
    {

        if (levelToSave.id_nivel == -1)
        {
            levelToSave = await levelRepository.Create(levelToSave);
            EnvDataHandler.Instance.SetLevelInEditionData(levelToSave);
            return;
        }

        LevelEntity tryLevel = await levelRepository.GetById((int)levelToSave.id_nivel);

        if (tryLevel.id_nivel == -1)
        {
            levelToSave = await levelRepository.Create(levelToSave);
            EnvDataHandler.Instance.SetLevelInEditionData(levelToSave);
            return;
        }

        await levelRepository.Update(tryLevel.id_nivel, levelToSave);
        EnvDataHandler.Instance.SetLevelInEditionData(levelToSave);

        Debug.Log("Nivel guardado correctamente"); 
    }

    private void OnLoadLevel(object sender, System.EventArgs e)
    {
        // LevelEntity level = levelRepository.LoadLevelRecord(1);

        // if (level == null)
        // {
        //     Debug.LogError("Level not found.");
        //     return;
        // }

        // if (level.AltoNivel != Grid2D.Instance.GetLevelHeight() || level.AnchoNivel != Grid2D.Instance.GetLevelWidth())
        // {
        //     Debug.LogError(
        //         $"Level dimensions must be {level.AnchoNivel}x{level.AltoNivel} not {Grid2D.Instance.GetLevelWidth()}x{Grid2D.Instance.GetLevelHeight()}");
        //     return;
        // }

        // // todo: here whe should assign the level width and height
        // int [,] levelIds = LevelParser.DeSerializeLevelIds(level.EstructuraNivel);

        // Grid2D.Instance.LoadLevelFromParseLevel(levelIds);
    }
}
