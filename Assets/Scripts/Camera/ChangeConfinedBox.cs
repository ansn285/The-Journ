using UnityEngine;

public class ChangeConfinedBox : MonoBehaviour
{
    public PolygonCollider2D[] colliders;

    private Cinemachine.CinemachineConfiner cinemachineConfiner;

    private void Start()
    {
        cinemachineConfiner = gameObject.GetComponent<Cinemachine.CinemachineConfiner>();
    }

    public void ChangeConfiner(string box)
    {
        cinemachineConfiner.m_BoundingShape2D = colliders[int.Parse(box.Split('x')[1]) - 1];
    }

}
