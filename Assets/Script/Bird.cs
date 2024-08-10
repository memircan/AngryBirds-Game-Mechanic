using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{

    public Rigidbody2D rb;
    public Rigidbody2D hook;

    public float releaseTime=0.15f;
    public float maxDragDistance = 2f;

    public GameObject nextBall;

    private bool isPressed=false;


    private void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            /*Eðer mesafe maxDragDistance'ý aþýyorsa, nesnenin pozisyonu sýnýrlandýrýlýr.
            (mousePos - hook.position).normalized, fare pozisyonu ile hook arasýndaki yönü verir.Bu yön vektörü, maxDragDistance ile çarpýlarak, nesnenin hook noktasýndan en fazla maxDragDistance kadar uzaklaþabileceði pozisyon hesaplanýr.*/
            else
                rb.position = mousePos; //mesafe maxDragDistance'dan küçükse, nesne özgürce fareyi takip edebilir.
        }
    }

    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPressed=false;
        rb.isKinematic = false;

        StartCoroutine(Release());
    }

    IEnumerator Release() 
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;//scripti inaktif ettik 

        yield return new WaitForSeconds(2f);

        if (nextBall != null)
        {
            nextBall.SetActive(true);
        }
        else
        {
            Enemy.EnemiesAlive = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
