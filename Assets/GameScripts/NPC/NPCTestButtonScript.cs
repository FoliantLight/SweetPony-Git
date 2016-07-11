using UnityEngine;
using System.Collections;

namespace Assets.NPC
{
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
            var npc = new NPC("test");
        }
    }
}
