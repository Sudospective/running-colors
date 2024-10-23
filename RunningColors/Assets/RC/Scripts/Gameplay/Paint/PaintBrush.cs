using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrush : MonoBehaviour
{
    public PaintStats stats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        Controller pc = GameManager.GetInstance().player.GetComponent<Controller>();
        if (pc != null)
        {
            bool isNew = true;
            for (int i = 0; i < pc.availablePaints.Count; i++)
            {
                if (pc.availablePaints[i] == stats)
                {
                    isNew = false;
                    break;
                }
            }
            if (isNew)
            {
                pc.availablePaints.Add(stats);
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
