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
        /// <summary>
        /// Имя нипа. Определяет xml с диалогом 
        /// и другие файлы для нипа, если понадобится
        /// </summary>
        private string Name;
        /// <summary>
        /// Активные - дают квесты и учавствуют в диалогах
        /// Пассивные - не дают заданий, не общаются
        /// </summary>
        public bool isActive = true;
        /// <summary>
        /// Начальная позиция нипа. Его дом. 
        /// </summary>
        public Vector3 startPosition;
        /// <summary>
        /// Начальная позиция нипа случайна
        /// Для нипов-путешественников, например
        /// </summary>
        public bool isRandomStartPosition = false;
        /// <summary>
        /// Ночные нипы не возвращаются домой для сна
        /// </summary>
        public bool isNight = true;
        /// <summary>
        /// Диалог с НИПом
        /// </summary>
        NPCDialog Dialog;
        /// <summary>
        /// Путь для папки с xml
        /// </summary>
        string Path = "Assets/GameScripts/NPC/";



        // как-то надо указать внешний вид:
        // либо файл с изображением name.png (или несколько для анимации)
        // либо указание цвета и других ништяков
        // тогда нужно наследование от MainPerson

        /// <summary>
        /// </summary>
        /// <param name="Name">Имя НИПа и xml файла с его параметрами</param>
        public NPC(string Name)
        {
            this.Name = Name;
            // TODO: расшифровка

            var doc = new XmlDocument();
            doc.Load(Path + Name + ".xml");
            var root = doc.DocumentElement;

            // TODO: загрузка остальных параметров
            Dialog = new NPCDialog(
                root.GetElementsByTagName("dialog").Item(0));


          //  Dialog.GetEntry("1", 0);
          //  Dialog.GetEntry("1", 2);
        }


        /// <summary>
        /// Функция, связанная с юнити
        /// </summary>
        public void ShowCurrentEntry()
        {

        }

    }
}
