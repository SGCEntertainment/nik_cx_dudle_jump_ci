using UnityEngine;

public class Manager : MonoBehaviour
{
    public static bool IsPaused;
    public static bool IsStarted;

    private static Manager instance;
    public static Manager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<Manager>();
            }

            return instance;
        }
    }

    int index;

    const float yOffset = 3.0f;
    const float minY = 6;

    [HideInInspector]
    public int scoreCount;

    [SerializeField] Transform platforms;
    public Player player;

    private void Awake()
    {
        IsPaused = false;
        IsStarted = false;
    }

    private void Update()
    {
        UpdatePlatformPos();
    }

    public void StartGame()
    {
        scoreCount = 0;
        UIManager.Instance.UpdateScore(scoreCount);

        SetPltformInitPos();
        ResetPlayer();
        ResetCamera();
    }

    public void AddScore()
    {
        scoreCount++;
        UIManager.Instance.UpdateScore(scoreCount);
    }

    void SetPltformInitPos()
    {
        index = 0;
        for(index = 0; index < platforms.childCount; index++)
        {
            platforms.GetChild(index).position = GetPosForPlatform();
            platforms.GetChild(index).GetComponent<Platform>().SetActiveForGetScore(true);
        }
    }

    Vector2 GetFirstPlatformPos()
    {
       return platforms.GetChild(0).position;
    }

    void UpdatePlatformPos()
    {
        for (int i = 0; i < platforms.childCount; i++)
        {
            Transform _platform = platforms.GetChild(i);

            Vector2 relative = player.transform.position - _platform.position + Vector3.down * minY;
            float product = Vector2.Dot(relative, Vector2.up);

            bool isDown = product > 0;
            if(isDown)
            {
                _platform.position = GetPosForPlatform();
                _platform.GetComponent<Platform>().SetActiveForGetScore(true);

                index++;
            }

            //Debug.LogFormat("platform {0}:{1}", _platform.name, isDown);
        }
    }

    Vector2 GetPosForPlatform()
    {
        return new Vector2(index > 0 ? GetRandX() : 0, index * yOffset);
    }

    float GetRandX()
    {
        return Random.Range(-1.5f, 1.5f);
    }

    void ResetPlayer()
    {
        player.transform.position = GetFirstPlatformPos() + Vector2.up;
    }

    void ResetCamera()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }

    public void GameOver()
    {
        UIManager.Instance.SaveResult();
        UIManager.Instance.Open(Windows.Game_over);
    }
}
