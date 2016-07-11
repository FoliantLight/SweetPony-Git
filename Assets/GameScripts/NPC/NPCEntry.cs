using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.NPC
{ 
    /// <summary>
    /// Диалоговая запись
    /// Вопрос NPC и варианты ответа MainPerson
    /// </summary>
    public class NPCEntry
    {
        // new NPCEntry(){Question = "123", Answer = new List<string>(){"a","b"}};
       
        /// <summary>
        /// Вопрос NPC
        /// </summary>
        public string Question;

        /// <summary>
        /// Номер вопроса. 
        /// Номер ответа генерируется так:
        /// Number + "." + выбранный номер в списке ответов.
        /// Нумерация в списке ответов с нуля.
        /// </summary>
        public string Number;

        /// <summary>
        /// Список возможных ответов. 
        /// Может быть пустым.
        /// </summary>
        public List<string> Answers;

        /// <summary>
        /// Создание диалоговой записи - вопроса и ответов из строк
        /// </summary>
        /// <param name="Question">Вопрос NPC</param>
        /// <param name="Number">Номер вопроса. Пример: 1.3.2</param>
        /// <param name="Answers">Несколько вариантов ответа</param>
        public NPCEntry(string Question, string Number, params string[] Answers)
        {
            this.Question = Question;
            this.Number = Number;
            this.Answers = Answers.ToList<string>();
        }


        public NPCEntry(XmlNode xml)
        {
            Question = xml.Attributes.GetNamedItem("q").Value;
            Number = xml.Attributes.GetNamedItem("id").Value;
            Answers = new List<string>();
            foreach (XmlNode ch in xml.ChildNodes)
                Answers.Add(ch.InnerText);
        }        
    }
}
