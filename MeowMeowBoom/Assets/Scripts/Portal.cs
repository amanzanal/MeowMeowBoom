using System.Collections;
using UnityEngine;
using TMPro;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform linkedPortal;
    public TextMeshProUGUI portalColdwon;
    private bool isTeleporting = false;
    private Collider2D portalCollider;
    private Collider2D linkedPortalCollider;

    private void Start()
    {
        portalCollider = GetComponent<Collider2D>();
        linkedPortalCollider = linkedPortal.GetComponent<Collider2D>();
        portalColdwon.text = "Portal is now active!";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTeleporting) return;

        if (collision.CompareTag("Cat"))
        {
            if (linkedPortal != null)
            {
                isTeleporting = true;
                StartCoroutine(WaitToTeleport());
                collision.transform.position = linkedPortal.transform.position;
                StartCoroutine(DeactivatePortals());
            }
        }
    }

    private IEnumerator WaitToTeleport()
    {
        yield return new WaitForSeconds(2f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            isTeleporting = false;
        }
    }

    private IEnumerator DeactivatePortals()
    {
        portalCollider.enabled = false;
        linkedPortalCollider.enabled = false;

        yield return StartCoroutine(PortalCooldownText());

        portalCollider.enabled = true;
        linkedPortalCollider.enabled = true;
        portalColdwon.text = "Portal is now active!";
    }

    private IEnumerator PortalCooldownText()
    {
        int countdown = 10;

        while (countdown > 0)
        {
            portalColdwon.text = "Portal reactivating in: " + countdown + "s";
            yield return new WaitForSeconds(1);
            countdown--;
        }
    }
}
