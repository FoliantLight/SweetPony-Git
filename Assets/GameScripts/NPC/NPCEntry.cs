using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.NPC
{ 
    /// <summary>Диалоговая запись. Вопрос NPC и варианты ответа MainPerson.</summary>
    public class NPCEntry
    { 
        /// <summary>Вопрос NPC</summary>
        public string question;

        /// <summary>Номер вопроса: номер предыдущего + . + номер ответа с 1</summary>
        public string number;

        /// <summary>Список возможных ответов</summary>
        public List<string> answers;

        /// <summary>Диалоговая запись - вопрос и ответы из строк</summary>
        /// <param name="question">Вопрос NPC</param>
        /// <param name="number">Номер вопроса. Пример: 1.3.2</param>
        /// <param name="answers">Несколько вариантов ответа</param>
        public NPCEntry(string question, string number, params string[] answers)
        {
            this.question = question;
            this.number = number;
            this.answers = answers.ToList<string>();
        }

        /// <summary>Диалоговая запись - вопрос и ответы из узла XML</summary>
        /// <param name="xml"></param>
        public NPCEntry(XmlNode xml)
        {
            question = xml.Attributes.GetNamedItem("q").Value;
            number = xml.Attributes.GetNamedItem("id").Value;
            answers = new List<string>();
            foreach (XmlNode ch in xml.ChildNodes)
                answers.Add(ch.InnerText);
        }        
    }
}
