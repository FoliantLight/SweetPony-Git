using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPCActionController : ActionItem {
	/// <summary>Ссылка на объект интерфейса диалога </summary>
    Transform canvas;
	/// <summary>Ссылка на объект ответа </summary>
    Text question;
	/// <summary>Ссылка на объект рус. имени нипа </summary>
    Text nameText;
	/// <summary>Ссылки на объекты кнопок </summary>
    List<Transform> buttons = new List<Transform>();
	/// <summary>Картинка с мордочкой нипа. Задаётся из юнити. Рекомендуется 400*400, кружок в нем 306*306 и кружок выровнен по горизнтали - по центру и по вертикали - по нижнему краю</summary>
	public Sprite face = null;
	/// <summary>Латинское имя нипа и xml-файла с его диалогом</summary>
    string name = "";

	/// <summary>Ссылка на интерфейс инвентаря</summary>
	private GameObject invCanvas;
	/// <summary>Ссылка на объекты, которые уже есть в инвентаре нипа. Задается из юнити</summary>
	[SerializeField]
	private List<ItemSet> items;
	/// <summary>Ссылка на инвентарь</summary>
	private Inventory inv;

    NPC npc = null;
    /// <summary>Получение ссылок на объекты диалогового окна внутри объекта НИПа</summary>
    protected override void Start () {
        base.Start();
		name = this.gameObject.name;
		canvas = this.gameObject.transform.FindChild("NPCDialog").transform; // GameObject.Find
        question = canvas.FindChild("Question").FindChild("Text").GetComponent<Text>();
        #region Всем созданным кнопкам добавляется обработчик нажатия
        int i = 0;
        var a = canvas.FindChild("NPCAnswer");
        while (a != null)
        {
            int buttonNumber = i;
            a.GetComponent<Button>().onClick.AddListener(() => checkAnswer(buttonNumber));
            buttons.Add(a);
			//Debug.Log("NPCANswer " + i);
            i++;
            a = canvas.FindChild("NPCAnswer (" + i + ")");
        }
        #endregion
        nameText = canvas.FindChild("Name").FindChild("Text").GetComponent<Text>();
        canvas.GetComponent<Canvas>().enabled = false;
    }

	/// <summary>Определение ссылок на инвентарь</summary>
	void Awake()
	{
		invCanvas = GameObject.Find(ObjectNames.InventoryCanvas);
		if (invCanvas == null) {
			Debug.Log (name + " не может найти объект " + ObjectNames.InventoryCanvas);
			return;
		}

		inv = new Inventory(GameConsts.inventorySize, false);
		inv.addItems(items);
	}

    /// <summary>Пояление диалогового окна </summary>
    public override void triggerAction() {
        name = this.gameObject.name;

        NPC.encode(name); // потом можно отключить. шифрует xml диалога т.к. в процессе создания игры xml диалога может меняться
        npc = new NPC(name);

		// Если игрок уже узнал имя нипа в предыдущих общениях, то можно сразу вывести имя нипа
		if (MainPerson.getMainPersonScript ().isKnownNPCname (name))
			nameText.text = name;
		else
			nameText.text = "";
		// устанавливается картинка с мордочкой нипа
		canvas.FindChild ("Name").FindChild ("Circle").GetComponent<Image> ().sprite = face;

		// проверяется, выполнил ли нип квесты, которые может принять этот нип. не используется в альфе
		#region quests reward
		foreach (int id in npc.recived_quest)
			MainPerson.getMainPersonScript().checkQuest(id);
		#endregion

		// появляется форма диалога
        canvas.GetComponent<Canvas>().enabled = true;
		// показывается первая запись
        showEntry(npc.getEntry());
    }

	/// <summary>При удалении игрока от НИПа все незабранные предметы исчезают и окна диалога и инвентаря закрываются</summary>
    public override void exitAction() {
		if (canvas != null)
        	canvas.GetComponent<Canvas>().enabled = false;
		if (invCanvas != null)
			invCanvas.transform.GetChild(Inventories.OthersInventory).gameObject.SetActive(false);
		if (inv != null) {
			inv.items.Clear (); 
			inv.inventoryPanel = null;
		}
	}

    /// <summary>Прячет кнопку с ответом</summary>
    /// <param name="buttonObject">Объект кнопки с ответом</param>
    void hideButton(Transform buttonObject)
    {
        buttonObject.GetComponent<Image>().enabled = false;
        buttonObject.FindChild("Text").GetComponent<Text>().text = "";
    }

    /// <summary>Показывает кнопку с ответом</summary>
    /// <param name="buttonObject">Объект кнопки с ответом</param>
    /// <param name="text">Текст на кнопке (ответ)</param>
    void showButton(Transform buttonObject, string text = "")
    {
        buttonObject.GetComponent<Image>().enabled = true;
        buttonObject.FindChild("Text").GetComponent<Text>().text = text;
    }



	/// <summary>Вывод диалоговой записи (вопрос нипа, варианты ответов игрока и т.д.) в интерфейс диалога</summary>
    /// <param name="entry">Диалоговая запись</param>
    void showEntry(NPCEntry entry)
    {
		// если запись почему-то пустая - диалог закончен
		if (entry == null) {
			exitAction ();
			return;
		}

       	//Debug.Log("Номер записи " + entry.number);
		// если эта запись - последняя в диалога и НИП ничего не говорит, то просто пустую форму выводить не надо. диалог закончен
		canvas.FindChild("Question").GetComponent<Image>().enabled = ! (entry.question.Equals("") && entry.isFinish);

		// выводится вопрос нипа
        question.text = entry.question;

		// если в этой записи становится известным имя нипа
		if (entry.name != "") {
			nameText.text = entry.name;
			m_mainPersonScript.addKnownNPCName (entry.name);
		}

        #region Появляются/скрываются лишние элементы интерфейса диалога
        int answersCount;
        if (buttons.Count < entry.answers.Count)
        {
            answersCount = buttons.Count;
            Debug.Log(name + " НИП Слишком много вариантов ответов " + entry.answers.Count);
        }
        else
        {
            answersCount = entry.answers.Count;
            for (int i = entry.answers.Count; i < buttons.Count; hideButton(buttons[i++])) ;
        }

		// если запись - монолог игрока сам с собой, то картинка нипа и его имя не отображаются
		canvas.FindChild("Question").GetComponent<Image>().enabled = !entry.isMonolog;
		canvas.FindChild("Name").GetComponent<Image>().enabled = !entry.isMonolog;
        #endregion

		#region drag&drop, money
		// если нип забирает предмет у игрока. не используется в альфе
		if (entry.drop.Count > 0) {
			if (MainPerson.getMainPersonScript().haveInInventory(entry.drop))
			{
				MainPerson.getMainPersonScript().drop(entry.drop);
			} else{
				question.text += "\n У тебя нет такого предмета";
			}
		}

		// если нип что-то дает, то игрок может это взять (перетащить в свой инвентарь из инвентаря нипа)
		if (entry.drag.Count > 0) 
		{
			// если этот предмет стоит денег (обычно в этом упоминается в предыдущей диалоговой записи)
			if (m_mainPersonScript.money >= entry.money)
			{
				// если денег у игрока достаточно, то предмет покупается и появляется возможность его забрать
				m_mainPersonScript.money -= entry.money;
				// изменяется значение баланса на форме инвентаря
				if (entry.money > 0)
					m_mainPersonScript.updateMoney(m_mainPersonScript.money);

				#region появляется инвентарь нипа и предмет в нем
				if (invCanvas == null) 
					Debug.Log(name + " npc entry - inv canvas is null");
				invCanvas.SetActive(true);
				invCanvas.transform.GetChild(Inventories.OthersInventory).gameObject.SetActive(true);
				InventoryPanel panel = invCanvas.transform.GetChild(Inventories.OthersInventory).GetComponent<InventoryPanel>();
				if (panel == null) 
					Debug.Log(name + " npc entry - inv panel is null");
				inv.inventoryPanel = panel;
				inv.addItems(entry.drag);	
				#endregion
			} else {
				question.text += "Не хватает денег";
			}
		}
		#endregion

		// варианты ответов игрока записываются в кнопки
        for (int i = 0; i < answersCount; i++)
        {
            showButton(buttons[i], entry.answers[i]);
            #region TODO: разные кнопки для ответов в 1 и 2 строки
            // рассчитать длину строки entry.answers[i] в пикселях
            // установить, одну или две строки займет ответ
            // buttons[i].GetComponent<Image>().sprite = one_answer или two_answer
            // buttons[i].GetComponent<Button>().spriteState.pressedSprite = one_answer_pressed или two_answer_pressed
            #endregion
        }
    }

    /// <summary>Обработка события нажатия на кнопку ответа</summary>
    public void checkAnswer(int answerNumber)
    {
		//Debug.Log ("checkAnswer " + answerNumber);
        if (npc == null) return;
        showEntry(npc.getEntry(answerNumber + 1));
    }
}
