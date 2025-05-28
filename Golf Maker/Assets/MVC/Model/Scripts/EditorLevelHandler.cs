using UnityEngine;

[DefaultExecutionOrder(-100)]
public class EditorEventHandler : MonoBehaviour
{

    EditorLevelEvents levelEventsHandler;
    ILevelRepository levelRepository;

    void Awake()
    {
        levelRepository = new PrimalLevelRepository(); 
        levelEventsHandler = EditorLevelEvents.GetInstance();

        levelEventsHandler.SaveLevel += OnSaveLevel;
        levelEventsHandler.LoadLevel += OnLoadLevel;
    }

    private void OnSaveLevel(object sender, System.EventArgs e)
    {
        Debug.Log("Saving level...");
        LevelEntity level = new LevelEntity
        {
            IdNivel = 1,
            IdUsuario= 123, // Example user ID
            Nombre = "Level 1",
            FechaCreacion = System.DateTime.Now,
            Dificultad = Dificultad.Medio,
            Descripcion = "A challenging level with obstacles.",
            RatingPromedio = 0f,
            JugadoVeces = 0,
            CompletadoVeces = 0,
            CantidadMoneas = 0,
            AltoNivel = Grid2D.Instance.GetLevelHeight(),
            AnchoNivel = Grid2D.Instance.GetLevelWidth(),
            EstructuraNivel = LevelParser.SerializeLevelIds(Grid2D.Instance.GetLevelIds()),
        };

        // Add logic to save the level using levelRepository
        levelRepository.CreateLevelRecord(level);
    }

    private void OnLoadLevel(object sender, System.EventArgs e)
    {
        LevelEntity level = levelRepository.LoadLevelRecord(1);

        if (level == null)
        {
            Debug.LogError("Level not found.");
            return;
        }
        
        if (level.AltoNivel != Grid2D.Instance.GetLevelHeight() || level.AnchoNivel != Grid2D.Instance.GetLevelWidth())
        {
            Debug.LogError(
                $"Level dimensions must be {level.AnchoNivel}x{level.AltoNivel} not {Grid2D.Instance.GetLevelWidth()}x{Grid2D.Instance.GetLevelHeight()}");
            return;
        }

        // todo: here whe should assign the level width and height
        int [,] levelIds = LevelParser.DeSerializeLevelIds(level.EstructuraNivel);

        Grid2D.Instance.LoadLevelFromParseLevel(levelIds);
    }
}
