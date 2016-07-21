using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


public class NPCDialog
{
    /// <summary>Узел XML, содержащий записи диалога</summary>
    XmlNode root;

    /// <summary>Текущая запись</summary>
    NPCEntry currentEntry = null;

    /// <summary>Уровень дружелюбности</summary>
    int friendly = 0;

    /// <summary> </summary>
    /// <param name="Root">Узел XML, содержащий записи диалога</param>
    public NPCDialog(XmlNode root)
    {
        this.root = root;
    }

    /// <summary>Получение нового вопроса после выбора одного из ответов</summary>
    /// <param name="answerIndex">Номер ответа на вопрос. Счет с 1. Получение первого вопроса - без параметра </param>
    /// <returns></returns>
    public NPCEntry getEntry(int answerIndex = 0)
    {
        XmlNode node;
        if (currentEntry == null) // первый вопрос
            node = root.SelectSingleNode("entry[@id='1']");
        else { 
            friendly += currentEntry.friendly[answerIndex-1];
            node = root.SelectSingleNode("entry[@id='" + 
                currentEntry.number + "." + answerIndex + "']");
        }

        // если записи для выбранного вопроса не найдено 
        // то завершить диалог записью по умолчанию
        if (node == null)
            node = root.SelectSingleNode("default");

        currentEntry = new NPCEntry(node);
        return currentEntry;
    }


}


