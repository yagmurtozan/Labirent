using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public Button btn;
    public float hiz = 10f; // Topun hareket h�z�
    public Text zaman, can, durum;
    float zamanSayaci = 900;
    float canSayaci = 100;
    bool oyunDevam = true;
    bool oyunTamam = false;
    private Rigidbody rb; // Topun Rigidbody bile�eni

    // Start is called before the first frame update
    void Start()
    {
        // Topun Rigidbody bile�enine eri�im sa�l�yoruz
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (oyunDevam && !oyunTamam)
        {
            zamanSayaci -= Time.deltaTime;
            zaman.text = (int)zamanSayaci + "";
        }
        else if (!oyunDevam)
        {
            durum.text = "Oyun Tamamlanamad�.";
            btn.gameObject.SetActive(true);
        }
            if (zamanSayaci < 0)
            {
                oyunDevam = false;
            }
        }
    

    // FixedUpdate fizik hesaplamalar� i�in kullan�l�r
    void FixedUpdate()
    {
        // Input.GetAxisRaw metodu ile klavyeden girdi al�yoruz
        if (oyunDevam && !oyunTamam)
        {
            float yatay = Input.GetAxis("Horizontal"); // A ve D tu�lar�yla sa�a ve sola hareket
            float dikey = Input.GetAxis("Vertical"); // W ve S tu�lar�yla ileri ve geri hareket

            // Hareket vekt�r�n� olu�turuyoruz
            Vector3 kuvvet = new Vector3(yatay, 0, dikey);

            // Rigidbody'e kuvvet uygulayarak topu hareket ettiriyoruz
            rb.AddForce(kuvvet * hiz);
        }
        else
        {
            rb.velocity= Vector3.zero;
            rb.angularVelocity= Vector3.zero;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        string objIsmi = other.gameObject.name;
        if (objIsmi.Equals("bitis"))
        {
            //print("Oyunu Kazand�n�z");
            oyunTamam = true;
            durum.text = "Oyun Tamamland�, Tebrikler!!";
            btn.gameObject.SetActive(true);
        } else if (!objIsmi.Equals("zemin") && !objIsmi.Equals("labirentzemin") && (!objIsmi.Equals("baslangic")))
        {
            canSayaci -= 1;
            can.text = (int)canSayaci + "";
            if (canSayaci == 0 )
            {
                oyunDevam = false;
            }
        }
    } 
}
