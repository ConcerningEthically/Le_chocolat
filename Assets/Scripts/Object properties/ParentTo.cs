using UnityEngine;

public class ParentTo : MonoBehaviour
{
    [SerializeField] private Transform childObj;
    [SerializeField] private Transform parentObjSheathed;
    [SerializeField] private Transform parentObjUnsheathed;
    [SerializeField] private AttackScript attackScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (attackScript.sheathed == false)
        {
        childObj.transform.parent = parentObjUnsheathed.transform;
        //transform.localScale = new Vector3(0.2f,0.2f,0.2f);            
        }
        if (attackScript.sheathed == true)
        {
        childObj.transform.parent = parentObjSheathed.transform;
        //transform.localScale = new Vector3(0.2f,0.2f,0.2f);            
        }
    }
}
