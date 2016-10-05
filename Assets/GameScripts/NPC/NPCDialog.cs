using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using UnityEngine;


public class NPCDialog
{
    /// <summary>Узел XML, содержащий записи диалога</summary>
    XmlNode root;
    /// <summary>Текущая запись</summary>
    NPCEntry currentEntry = null;

    /// <summary> </summary>
    /// <param name="Root">Узел XML, содержащий записи диалога</param>
    public NPCDialog(XmlNode root)
    {
        this.root = root;
    }

    /// <summary>Получение нового вопроса после выбора одного из ответов</summary>
    /// <param name="answerIndex">Номер ответа на вопрос. Счет с 1</param>
    /// <returns></returns>
    public NPCEntry getEntry(int answerIndex)
    {
		// узел с диалоговой записью
        XmlNode node; 

		if (currentEntry == null) {
			// если это первый вопрос
			node = root.SelectSingleNode ("entry[@id='1']");
		} else { 
			// если это не первый вопрос
            node = root.SelectSingleNode("entry[@id='" + 
                currentEntry.number + "." + answerIndex + "']");
        }
			
		// если запись содержит ссылку на другую запись
		string gotoNumber = "";
		try {
			gotoNumber = node.Attributes.GetNamedItem("goto").Value;
		} catch (Exception ex) {}

		if (gotoNumber != "") {
			node = root.SelectSingleNode ("entry[@id='" + gotoNumber + "']");
			Debug.Log ("goto " + gotoNumber);
		}

		// если записи для выбранного вопроса не найдено, то завершить диалог записью по умолчанию
		if (node == null) {
			node = root.SelectSingleNode ("default");
			Debug.Log ("default");
		}

		currentEntry = new NPCEntry (node);
		return currentEntry;
    }

}


