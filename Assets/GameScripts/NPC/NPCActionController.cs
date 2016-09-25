using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPCActionController : ActionItem {

    Transform canvas;
    Text question;
    Text nameText;
    List<Transform> buttons = new List<Transform>();
	public Sprite face = null;
	public string name = "";

	private GameObject invCanvas;
	[SerializeField]
	private List<ItemSet> items;
	private Inventory inv;

    NPC npc = null;
    /// <summary>Получение ссылок на объекты диалогового окна внутри объекта НИПа</summary>
    protected override void Start () {
        base.Start();
		name = this.gameObject.name;
        canvas = transform.FindChild("NPCDialog");
        question = canvas.FindChild("Question").FindChild("Text").GetComponent<Text>();
        #region Всем созданным кнопкам добавляется обработчик нажатия
        int i = 0;
        var a = canvas.FindChild("NPCAnswer");
        while (a != null)
        {
            int buttonNumber = i;
            a.GetComponent<Button>().onClick.AddListener(() => checkAnswer(buttonNumber));
           // Debug.Log("Добавлен обработчик для кнопки " + buttonNumber);
            buttons.Add(a);
            i++;
            a = canvas.FindChild("NPCAnswer (" + i + ")");
        }
        #endregion
        nameText = canvas.FindChild("Name").FindChild("Text").GetComponent<Text>();
        canvas.GetComponent<Canvas>().enabled = false;

		#region инвентарь
		inventoryStart();
		#endregion
    }

	private void inventoryStart()
	{
		invCanvas = GameObject.Find(ObjectNames.InventoryCanvas);
		if (invCanvas == null) {
			Debug.Log (name + " не может найти объект " + ObjectNames.InventoryCanvas);
			return;
		}

		inv = new Inventory(GameConsts.inventorySize, false);
		inv.addItems(items);

		InventoryPanel panel = invCanvas.transform.GetChild(Inventories.OthersInventory).GetComponent<InventoryPanel>();
		if (panel == null) {
			Debug.Log (name + " не может найти объект " + Inventories.OthersInventory);
			return;
		}
		inv.inventoryPanel = panel;
	}

    /// <summary>Пояление диалогового окна</summary>
    public override void triggerAction() {
        name = this.gameObject.name;

        NPC.encode(name); // потом можно отключить
        npc = new NPC(name);

		if (MainPerson.getMainPersonScript ().isKnownNPCname (name))
			nameText.text = name;
		else
			nameText.text = "";
		canvas.FindChild ("Circle").GetComponent<Image> ().sprite = face;

		#region quests reward
		foreach (int id in npc.recived_quest)
			MainPerson.getMainPersonScript().checkQuest(id);
		#endregion

        canvas.GetComponent<Canvas>().enabled = true;
        showEntry(npc.getEntry());
    }

	/// <summary>При удалении игрока от НИПа все незабранные предметы исчезают и окна закрываются</summary>
    public override void exitAction() {
        canvas.GetComponent<Canvas>().enabled = false;
		invCanvas.transform.GetChild(Inventories.OthersInventory).gameObject.SetActive(false);
		inv.inventoryPanel = null;
		if (inv != null)
			inv.items.Clear (); 
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

    /// <summary>Вывод диалоговой записи в интерфейс</summary>
    /// <param name="entry">Диалоговая запись</param>
    void showEntry(NPCEntry entry)
    {
        //Debug.Log("Номер записи " + entry.number);
        question.text = entry.question;
        #region Появляются/скрываются лишние кнопки
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
        #endregion

		#region drag&drop
		//Debug.Log("drag " + entry.drag.Count.ToString() + ". drop " + entry.drop.Count.ToString());
		if (entry.drag.Count > 0)
		{
			if (invCanvas == null) 
				Debug.Log(name + " npc entry - inv canvas is null");
			
			invCanvas.SetActive(true);
			invCanvas.transform.GetChild(Inventories.OthersInventory).gameObject.SetActive(true);

			InventoryPanel panel = invCanvas.transform.GetChild(Inventories.OthersInventory).GetComponent<InventoryPanel>();
			if (panel == null) 
				Debug.Log(name + " npc entry - inv panel is null");
			inv.inventoryPanel = panel;
			inv.addItems(entry.drag);	
		}
		//MainPerson.getMainPersonScript().drop(entry.drop);
		#endregion

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
        if (entry.name != "")
            nameText.text = entry.name;
    }

    /// <summary>Обработка события нажатия на кнопку ответа</summary>
    public void checkAnswer(int answerNumber)
    {
        if (npc == null) return;
        //Debug.Log("Функция checkAnswer получила номер кнопки " + answerNumber);
        showEntry(npc.getEntry(answerNumber + 1));
    }
}
