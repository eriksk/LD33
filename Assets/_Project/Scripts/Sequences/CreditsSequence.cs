using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Sequences
{
    public class CreditsSequence : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                Application.LoadLevel("Level1");
            }
        }
    }
}
