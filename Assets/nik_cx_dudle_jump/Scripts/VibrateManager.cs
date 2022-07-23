using UnityEngine;

public class VibrateManager : MonoBehaviour
{
    private static VibrateManager instance;
    public static VibrateManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<VibrateManager>();
            }

            return instance;
        }
    }

    bool canVibrate;

    public void SetDisable(out bool _canVibrate)
    {
        canVibrate = !canVibrate;
        _canVibrate = canVibrate;
    }

    public void TryVibrate()
    {
        if(!canVibrate)
        {
            return;
        }

        Handheld.Vibrate();
    }
}
