using UnityEngine;
using System.Collections;

public class LevelsManager : MonoBehaviour
{
    public LevelsSettings levelsSettings;
    public int CurrentLevel;

    public static LevelsManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void StartLevel()
    {
        UIManager.Instance.HideAll();
        UIManager.Instance.Show("Level");
        PedManager.Instance.SpawnPed(CurrentLevel);
        UIManager.Instance.SetText("Level", "LevelText", (CurrentLevel + 1).ToString() + " Level");
        Mixer.Instance.ClearMixer();
        Mixer.Instance.HideCylinder();
        Camera.main.GetComponent<CameraMovement>().MoveAway();
    }

    public void EnableControll(bool enable)
    {
        if (enable)
        {
            UIManager.Instance.Hide("Order");
            UIManager.Instance.Show("Mix Button");
            SpawnerManager.Instance.Enable();
            Camera.main.GetComponent<CameraMovement>().MoveToBlender();
        }
        else
        {
            SpawnerManager.Instance.Disable();
        }
    }

    public void LevelPassed()
    {
        int result = Mixer.Instance.Mix();
        UIManager.Instance.HideAll();
        if (result == 0)
        {
            return;
        }
        else if (result >= 90)
        {
            EndLevel();
            if (CurrentLevel == levelsSettings.Levels.Length - 1)
            {
                StartCoroutine("ShowResult", "Game Passed");
            }
            else
            {
                StartCoroutine("ShowResult", "Level Compleated");
                UIManager.Instance.SetText("Level Compleated", "Procent", result.ToString() + "%");
            }
        }
        else
        {
            UIManager.Instance.HideAll();
            UIManager.Instance.SetText("Level Failed", "Procent", result.ToString() + "%");
            StartCoroutine("ShowResult", "Level Failed");
        }
    }

    IEnumerator ShowResult(string Name)
    {
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.Show(Name);
    }

    public void EndLevel()
    {
        UIManager.Instance.Hide("Level");
        PedManager.Instance.HidePed();
        SpawnerManager.Instance.Disable();
        ObjectPooler.Instance.DisableAll();
        EnableControll(false);
    }

    public void RestartLevel()
    {
        EndLevel();
        StartLevel();
    }

    public void ContinueLevel()
    {
        UIManager.Instance.Hide("Level Failed");
        UIManager.Instance.Show("Mix Button");
        SpawnerManager.Instance.Enable();
    }

    public void NewGame()
    {
        EndLevel();
        CurrentLevel = 0;
        StartLevel();
    }

    public void NextLevel()
    {
        CurrentLevel++;
        EndLevel();
        StartLevel();
    }
}