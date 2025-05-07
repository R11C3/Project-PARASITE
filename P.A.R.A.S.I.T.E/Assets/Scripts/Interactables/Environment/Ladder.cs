using UnityEngine;

public class Ladder : Interactable
{
    public GameObject ladderTop;
    public GameObject ladderBottom;

    public override void Interact(GameObject source)
    {
        if(source.CompareTag("Player"))
        {
            PlayerStats player = source.GetComponent<PlayerStats>();
            PlayerController playerController = source.GetComponent<PlayerController>();

            source.transform.position = new Vector3(ladderTop.transform.position.x, source.transform.position.y, ladderTop.transform.position.z);

            player.action = Action.Ladder;
            playerController.ladderTop = ladderTop.GetComponent<BoxCollider>().bounds;
            playerController.ladderBottom = ladderBottom.GetComponent<BoxCollider>().bounds;

            Vector3 direction = new Vector3(transform.position.x - source.transform.position.x, 0, transform.position.z - source.transform.position.z);
            playerController.ladderDirection = direction.normalized;
            source.transform.forward = direction.normalized;
        }
    }
}
