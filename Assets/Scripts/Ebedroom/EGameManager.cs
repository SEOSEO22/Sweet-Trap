using UnityEngine;
using System.Collections;
public class EGameManager : MonoBehaviour
{
    public static EGameManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActivateAndMoveBead(GameObject bead)
    {
        StartCoroutine(ActivateAndMoveBeadCoroutine(bead));
    }

    public IEnumerator ActivateAndMoveBeadCoroutine(GameObject bead)
    {
        bead.SetActive(true); 
        yield return null; 

        var beadScript = bead.GetComponent<BeadScript>();
        if (beadScript == null)
        {
            Debug.LogError("BeadScript component is missing on the Bead object!");
            yield break;
        }

        beadScript.ActivateBead(); 
    }
}
