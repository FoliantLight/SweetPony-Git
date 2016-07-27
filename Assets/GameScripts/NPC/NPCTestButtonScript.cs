using UnityEngine;
using System.Collections;
using System;
using System.IO;

    public class NPCTestButtonScript : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void onClick()
        {


        while (true)
        {
            try
            {

                NPC.encode("Luna");
                var npc = new NPC("Luna");
                var e1 =  npc.getEntry();
                var e2 = npc.getEntry(2);
                var e3 = npc.getEntry(1);
            }
            catch (Exception ex)
            {
            }
        }
        
                
        }
    }

