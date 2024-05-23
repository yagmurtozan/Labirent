using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public Button btn;
    public float hiz = 10f; // Topun hareket hýzý
    public Text zaman, can, durum;
    float zamanSayaci = 900;
    float canSayaci = 100;
    bool oyunDevam = true;
    bool oyunTamam = false;
    private Rigidbody rb; // Topun Rigidbody bileþeni

    // Start is called before the first frame update
    void Start()
    {
        // Topun Rigidbody bileþenine eriþim saðlýyoruz
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
            durum.text = "Oyun Tamamlanamadý.";
            btn.gameObject.SetActive(true);
        }
            if (zamanSayaci < 0)
            {
                oyunDevam = false;
            }
        }
    

    // FixedUpdate fizik hesaplamalarý için kullanýlýr
    void FixedUpdate()
    {
        // Input.GetAxisRaw metodu ile klavyeden girdi alýyoruz
        if (oyunDevam && !oyunTamam)
        {
            float yatay = Input.GetAxis("Horizontal"); // A ve D tuþlarýyla saða ve sola hareket
            float dikey = Input.GetAxis("Vertical"); // W ve S tuþlarýyla ileri ve geri hareket

            // Hareket vektörünü oluþturuyoruz
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
            //print("Oyunu Kazandýnýz");
            oyunTamam = true;
            durum.text = "Oyun Tamamlandý, Tebrikler!!";
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
