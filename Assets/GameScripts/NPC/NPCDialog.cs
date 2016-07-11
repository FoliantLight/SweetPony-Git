using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.NPC
{
    public class NPCDialog
    {
        /// <summary>
        /// Узел XML, содержащий записи диалога
        /// </summary>
        XmlNode Root;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Root">Узел XML, содержащий записи диалога</param>
        public NPCDialog(XmlNode Root)
        {
            this.Root = Root;
        }

        /// <summary>
        /// Получение нового вопроса после выбора одного из ответов
        /// </summary>
        /// <param name="QuestionNumber">Номер вопроса, например 1.1</param>
        /// <param name="AnswerIndex">Номер ответа на вопрос</param>
        /// <returns></returns>
        public NPCEntry GetEntry(string QuestionNumber, int AnswerIndex)
        {
            var node = Root.SelectSingleNode("entry[@id='"
                + QuestionNumber
                + (AnswerIndex == 0 ? "" : "." + AnswerIndex)
                + "']");
            var entry = new NPCEntry(node);
            return entry;
        }

    }

}
