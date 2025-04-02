using UnityEngine;

public class C_Ladder : MonoBehaviour
{
    public GameObject gem;
    public GameObject gem2;
    public GameObject ladder;
    public GameObject ladder_M;

    void Start()
    {
        ladder.SetActive(false);
        ladder_M.SetActive(false);
    }
    void Update()
    {
        if (gem.activeSelf && gem2.activeSelf)
        {
            ladder.SetActive(true);
            ladder_M.SetActive(true);
        }
    }
}
