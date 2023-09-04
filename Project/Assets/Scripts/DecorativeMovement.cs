/*This file is part of Enzigame.
Enzigame is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as 
published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Enzigame is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Foobar.  If not, see <https://www.gnu.org/licenses/>6.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeMovement : MonoBehaviour
{
    public float speed;

    void Start()
    {
        StartCoroutine(Fade());
    }
    void Update()
    {
        transform.position += transform.up * (Time.deltaTime * speed);
    }
    IEnumerator Fade(){
        Color c = GetComponent<Renderer>().material.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.05f)
        {
            c.a = alpha;
            GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(this);
    }
}
