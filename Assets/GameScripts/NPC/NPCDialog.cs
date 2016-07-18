using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


    public class NPCDialog
    {
        /// <summary>Узел XML, содержащий записи диалога</summary>
        XmlNode root;

        /// <summary> </summary>
        /// <param name="Root">Узел XML, содержащий записи диалога</param>
        public NPCDialog(XmlNode root)
        {
            this.root = root;
        }

        /// <summary>Получение нового вопроса после выбора одного из ответов</summary>
        /// <param name="questionNumber">Номер вопроса, например 1.1</param>
        /// <param name="answerIndex">Номер ответа на вопрос</param>
        /// <returns></returns>
        public NPCEntry getEntry(string questionNumber, int answerIndex)
        {
            var node = root.SelectSingleNode("entry[@id='"
                + questionNumber
                + (answerIndex == 0 ? "" : "." + answerIndex)
                + "']");
            var entry = new NPCEntry(node);
            return entry;
        }

    }


