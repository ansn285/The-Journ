using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBox : MonoBehaviour
{
    public Vector2 minCamPos;
    public Vector2 maxCamPos;
    public bool isCurrScreenBox;
    public Camera mainCamera;

    Vector2[] screenBoxCorners = new Vector2[4];

    /*private void Update()
    {
        if (!isCurrScreenBox)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }*/

    private void OnDrawGizmos()
    {
        float halfCamHeight = mainCamera.orthographicSize;
        float halfCamWidth = halfCamHeight * mainCamera.aspect;

        /*Calculate the min and max dimensions of the screen
         * We do this by subtracting the half height and width from the min camera pos
         * then adding the half height and width to the max camera position
         * For both min and max we add in the position of the ScreenBox so we draw the rectangle
         * relative to the ScreenBox's current position. This makes it easy to move the screenbox if required
         */
        Vector2 minSBDimensions = new Vector2(minCamPos.x - halfCamWidth + transform.position.x,
                                               minCamPos.y - halfCamHeight + transform.position.y);

        Vector2 maxSBDimensions = new Vector2(maxCamPos.x + halfCamWidth + transform.position.x,
                                                maxCamPos.y + halfCamHeight + transform.position.y);

        //Set the coordinates of the ScreenBox corners
        screenBoxCorners[0] = new Vector2(minSBDimensions.x, minSBDimensions.y);
        screenBoxCorners[1] = new Vector2(minSBDimensions.x, maxSBDimensions.y);
        screenBoxCorners[2] = new Vector2(maxSBDimensions.x, maxSBDimensions.y);
        screenBoxCorners[3] = new Vector2(maxSBDimensions.x, minSBDimensions.y);

        //Set the colors of the ScreenBox. Current active green, rest red
        Gizmos.color = isCurrScreenBox ? Color.green : Color.red;

        //Draws a line from each corner of the ScreenBox to the next corner
        for(int i = 0; i < screenBoxCorners.Length; i++)
        {
            int nextPos = (i + 1) % 4;
            Gizmos.DrawLine(screenBoxCorners[i], screenBoxCorners[nextPos]);
        }
    }
}
