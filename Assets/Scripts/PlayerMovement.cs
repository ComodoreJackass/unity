using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;
    public float setSpeed = 2.5f;
    public int tileSize = 1;
    

    Vector3 start;
    Vector3 destination;
   
    private bool canMove = true;
    private bool didNotCollide = true;
    private float speed;

    private void Start()
    {
        start = transform.position;
        destination = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ak smo kresnuli o zid
        if (collision.collider.tag == "Wall")
        {
            // uzima Vector3 colidera najbliži točki s koje smo krenuli, sprječava clipping sa zidovima
            destination = transform.position;

            // reset brzine zbog matematike
            speed = 0f;
            // flag na false da se ubije kretanje u tijeku i pokrene grana za povratak na startnu poziciju
            didNotCollide = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Forward") && canMove)
        {
            //Ponovno se možemo kretati tek kad se završi prvo kretanje
            canMove = false;
            //Onemogućava rotiranje tokom kretanja
            GetComponent<CameraControl>().enabled = false;
            // Resetiranje 
            speed = 0.0f;
            //početna pozicija
            start = transform.position;

            //ako smo rotirani 90 stupnjeva
            if (transform.eulerAngles.y == 90)
            {
                //konačna pozicija je početna pozicija uvećana za
                destination = start + new Vector3(tileSize, 0, 0);
            }
            else if (transform.eulerAngles.y == 180)
            {
                destination = start + new Vector3(0, 0, -tileSize);
            }
            else if (transform.eulerAngles.y == 270)
            {
                destination = start + new Vector3(-tileSize, 0, 0);
            }
            // nula stupnjeva je zadnji zato jer postoji neki edge case koji sjebe
            // cijelu stvar ako eksplicitno stavim transform.eulerAngles.y == 0 a else sve pokriva
            else
            {
                destination = start + new Vector3(0, 0, tileSize);
            }
        }
        
        // copy paste od iznad s obrnutim vektorima
        if (Input.GetButtonDown("Back") && canMove)
        {
            canMove = false;
            GetComponent<CameraControl>().enabled = false;
            speed = 0.0f;
            start = transform.position;

            if (transform.eulerAngles.y == 90)
            {
                destination = transform.position + new Vector3(-tileSize, 0, 0);
            }
            else if (transform.eulerAngles.y == 180)
            {
                destination = transform.position + new Vector3(0, 0, tileSize);
            }
            else if (transform.eulerAngles.y == 270)
            {
                destination = transform.position + new Vector3(tileSize, 0, 0);
            }
            else {
                destination = transform.position + new Vector3(0, 0, -tileSize);
            }
            
        }

        // u slucaju sudara sa zidom
        // više manje ista stvar kao i za normalno kretanje samo su destination i start
        // zamjenili mjesta jer recikliram varijable
        if (!didNotCollide)
        {
            // dupla brzina da sakrije glitchanje
            speed += 2 * setSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(destination, start, speed);

            if (transform.position == start)
            {
                canMove = true;
                didNotCollide = true;
                GetComponent<CameraControl>().enabled = true;
            }
        }

        // za normalno kretanje
        // ako smo pokrenuli kretanje i nismo se sudarili
        // U update funkciji je pa se izvodi dok god su uvjeti zadovoljeni
        // što znači da se svaki frame malo mičemo
        if (!canMove && didNotCollide) {
            // sumiramo u brzinu, kad dođe do jedan prešli smo cijeli put
            // Time.deltaTime služi da brzina ne zavisi o broju renderiranih frejmova
            speed += setSpeed * Time.deltaTime;
            
            // Lerp je smoothing funkcija kojoj damo start end i inkrement
            // transform.position je pozicija lika
            transform.position = Vector3.Lerp(start, destination, speed);

            // Ako smo se pomakli na destinaciju, omogući unos novog kretanja
            // i omogući rotaciju kamere
            if (transform.position == destination) {
                canMove = true;
                GetComponent<CameraControl>().enabled = true;
            }
        }
    }
}
