using UnityEngine;

public class Platform : MonoBehaviour
{
    Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void SetActiveForGetScore(bool IsActive)
    {
        name = IsActive ? 1.ToString() : 0.ToString();
    }

    public void Interact()
    {
        SetActiveForGetScore(false);
        anim.Play();
    }
}
