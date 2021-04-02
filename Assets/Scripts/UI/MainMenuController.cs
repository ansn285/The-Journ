using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public ParticleSystem buttonPressParticles;
    
    public void InstantiateParticles(float y)
    {
        Instantiate(buttonPressParticles, new Vector3(0, y, -30f), Quaternion.identity);
    }

}
