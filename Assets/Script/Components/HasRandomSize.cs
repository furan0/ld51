using UnityEngine;

public class HasRandomSize : MonoBehaviour
{
    [Header("Size Parameter")]
    [SerializeField] float sizeMax=2.0f;
    [SerializeField] float sizeMin=1.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale *= Random.Range(sizeMax, sizeMin); 
    }
}
