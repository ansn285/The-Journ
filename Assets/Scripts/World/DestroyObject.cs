using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject destroyableObject;
    private void OnEnable()
    {
        GameObject.Find("Amie").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GameObject.Find("Amie").GetComponent<CharacterMovement>().enabled = true;
        destroyableObject.SetActive(false);
        GameObject.Find("GameController").GetComponent<LoadDestroy>().enabled = true;
    }
}
