using UnityEngine;

public class Switch : Interactable
{
    [SerializeField]
    private GameObject[] recieving;

    public override void Interact(GameObject source)
    {
        for(int i = 0; i < recieving.Length; i++)
        {
            Interactable target;
            GameObject receive = recieving[i];
            receive.TryGetComponent<Interactable>(out target);
            while(target == null)
            {
                receive = receive.transform.parent.gameObject;
                receive.TryGetComponent<Interactable>(out target);
            }
            target.Interact(source);
        }
    }
}
