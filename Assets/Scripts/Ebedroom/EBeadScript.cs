using UnityEngine;
using System.Collections;

public class BeadScript : MonoBehaviour
{
    public float moveDistanceY = -3f; 
    public float moveDuration = 2f;
    
    public void ActivateBead()
    {
        StartCoroutine(MoveBead());
    }

    private IEnumerator MoveBead()
    {
        Vector3 startPos = transform.position; 
        Vector3 endPos = startPos + new Vector3(0, moveDistanceY, 0); 

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos; 
    }
}
