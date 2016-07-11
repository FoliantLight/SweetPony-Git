using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;

namespace Assets.NPC
{
    public class NPC
    {
        /// <summary>Имя нипа. Определяет xml с диалогом и другие файлы для нипа, если понадобится</summary>
        private string name;
        /// <summary>Активные - дают квесты и учавствуют в диалогах. Пассивные - не дают заданий, не общаются.</summary>
        public bool isActive = true;
        /// <summary>Начальная позиция нипа. Его дом. </summary>
        public Vector3 startPosition;
        /// <summary>Начальная позиция нипа случайна.Для нипов-путешественников, например.</summary>
        public bool isRandomStartPosition = false;
        /// <summary>Ночные нипы не возвращаются домой для сна.</summary>
        public bool isNight = true;
        /// <summary>Диалог с НИПом.</summary>
        NPCDialog dialog;
        /// <summary>Путь для папки с xml.</summary>
        static string Path = "Assets/GameScripts/NPC/";

        /// <summary></summary>
        /// <param name="name">Имя НИПа и xml файла с его параметрами</param>
        public NPC(string name)
        {
            this.name = name;
            // TODO: расшифровка

            var doc = new XmlDocument();
            doc.Load(Path + name + ".xml");
            var root = doc.DocumentElement;

            // TODO: загрузка остальных параметров
            dialog = new NPCDialog(
                root.GetElementsByTagName("dialog").Item(0));
        }


    }
}
