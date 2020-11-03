using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
FARMER 1:
Farmer: Hello kid …
Player: Hey i am not a kid !
Farmer: alright alright, so what do you want ?
Player: i am hungry and i want some food.
Farmer: alright then, i will feed you if you help me to get ride of these *** rats, a last thing, 
DON’T DARE STEALING IN MY FIELDS OR I WILL … i will … don’t do it
Player: alright old man, i have to go.
Farmer: Hey i am not ol ...
FARMER 2 [Quest End]: Ho thank you young man !!! Take this food it’s free for you.
    Don’t hesitate to come back, these rats come back every morning, there will always be work for you.
Player: Thank you good sir !
[satiety set to maximum / karma increased by 30]


Liolilol - Aujourd'hui à 00:33
Dad ghost dont apear if karma = 0;
Dad Ghost (if karma <0): I see you are going bad, as your dad i have to say this, i am proud of you !
Indeed acting bad is often easier than acting good, alright, you can still be forgiven for what you did, if you want to, next quest may erase your fault if you want to be a nice guy.

Dad Ghost (if karma > 0): i see you chose to be a nice guy, ok then, even if that’s not the path i would follow i wont disrespect your choice.
Remeber you can still come to the dark side easily, murdering someone should drain your karma by at least 100.

Dad ghost (after intro): Let’s talk seriously, you are adult now, you need a weapon, i know an old soldier who may have one, he live in the desert that you should find on the vertical road of this planet, then go to the middle of the desert, his house may be there.
Dialogue 1 (if karma <0): Old Soldier: Hello young man, how are y … 
Player: i don’t have time for those unnecessary politeness, give me a weapon ! 
Old Soldier: wait a minute, what are you planning to do with it ? … anyway i don’t care, just take it ! 
Player: nice thank y … 
Old Soldier: Just kidding, you are not clever enough to get my weapon, but if you can proove me i was wrong, i will give you what you want. 
Player: I am probably smarter than you, old gay man ! 
Old Soldier: I will pretend that i didn’t hear what you just sai … 
Player: you didn't anyway 
Old Soldier: … … … it’s ok, just follow me.

Dialogue2 (if karma >= 0) Old Soldier: Hello young man, how are you ? Player: i am fine thank you, and you, what are you doing alone so far from city ? Old Soldier: i live here since last war ended [blablabla] [Nearly one hour later/satiety - 15] Old Soldier: i would like to give you my sword like that, but before i can to do so, you have to proove me thats you are smart enough to use it.     Follow me until my shooting field.(édité)
Liolilol - Aujourd'hui à 01:26
Old Soldier (explication): Here we are, this is my canon, all others have been destroyed, but i hide this one from the king, he said that canons are a threat to the peace and the whole mankind it self, it’s bullshit, cannon’s never killed anyone ... or i mean …
Whatever, you have to hit that target if you want me to accept you as my successor.
Player: what the … why are we talking about legacy now ?
Old Soldier: never mind, lets do it, so, the canon is right here, but you have to do very complicated math to calculate the right parameters, the canon can shoot up to 100 tile, the planete’s circumference is also 100 tile, and the range is equal to the nomber of once of gunpower you loaded in the canon.
I usualy give only one try to my potential adopted son, but you look promising so i will let you try 926565001 times.
Player: Wait i didn’t ….
Old Soldier: it’s time to work young student
Player: … [4 sec]
Old Soldier: also don’t even try to kill me, i absolutely don't have my sword on me and it's 100 % sure that i won’t drop it if i die, even if it’s completely impossible that i die here.
If quest completed:
Old Soldier: Congratulation Young Soldier, you just won that awesome [Cursed Legendary Sacred Sword Of Entropy], it have an inner karma meter, but it reset when the sword’s owner change, so it is now 0.
The sword deal extra damage depending on how much you are a bad person, a negative karma will also add some life steal effect.
However, if you perform good action, the sword will gain some healing power, good karma will also increase your regeneration exponentially, and fill your satiety over time.
So now young soldier, it’s time to go to war and destroy everything …
Player: I am not a soldier and (if karma >= 0/  you are not gonna to destroy annything) (if karma <0/ i prefer murdering people without companie )
Have a good day.

If Old soldier killed:
Old soldier: Arggg … i am dying … arggggg that’s hurt
How can i die here ? after all the war i survived, well i was hiding mainly but still …
Anyway i deserve that ….
I have to explain some thing before dying … arggggggggg
Take my sword i won't need it in death.  [Cursed Legendary Sacred Sword Of Entropy] got !
The sword have an inner karma metter, that is set to 0 when you get the sword for the first time, and deal extra damage depending on how much you are a bad person, a negative karma will also add some life steal effect.
However, if you perform good action, the sword will gain some healing power, good karma will also increase your regeneration exponentially and fill your satiety over time .
ARGGG I AM SUFFERING ARGGGGGGGG …
[i guess he is dead](édité)
NOUVEAUX MESSAGES
Liolilol - Aujourd'hui à 01:40
Dad ghost (after intro): Let’s talk seriously, you are adult now, you need a weapon, i know an old soldier who may have one, he live in the desert that you should find toward the south, take the main road, just outside the house, and don’t leave it until you found the desert, then follow the sand to the east, his house may be far in the desert. 
 */


public class Quest
{
	public List<string> dialogStart;
	int idDialogStart = -1;
	public List<string> dialogEnd;
	int idDialogEnd = -1;

	public string questDialog;

	public bool inProgress = false;

	public int linkedNpcId = -1;
	public int questId = -1;

	public bool repetable = false;
	bool state = false;
	bool needToReturn = true;
	int completion = 0;
	int karmaModifier;
	int finishCompletion = 0;
	bool kingDial = false;

	public Quest(int npcId, int qId)
	{
		linkedNpcId = npcId;
		questId = qId;
		if (questId == 0)
		{
			needToReturn = false;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();
			dialogStart.Add ("Dad Ghost: Good morning son,  I  decided to leave this world for a bigger one, but you can't follow me right now, you have a important thing to do, became the king NOWWW !!!");
			dialogStart.Add ("Dad Ghost: Hum hum, anyway you can chose your path between shadows and light. ");
			dialogStart.Add ("Dad Ghost: Your karma is indicated on the TV, But for now you should get some food, since  I  brought all we had left with me, go to the farm and buy some to the farmer, or steal him,  I  don't care, then come back, I'll wait for you.");
			dialogStart.Add ("Dad Ghost: To go at the farm you have to turn left when you leave the house, after crossing the bridge go to the next intersection, turn left again, and cross the next bridge, then go forward, the farm may be straight ahead.");
			dialogStart.Add ("Dad Ghost: Remember, You will always be able to use your compass, even if it is a bit out of sync a few times.");
			finishCompletion = 0;
		}
		if (questId == 1)
		{
			karmaModifier = 30;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();
			dialogStart.Add ("Farmer: Hello kid …");
			dialogStart.Add ("Player: Hey I am not a kid !");
			dialogStart.Add ("Farmer: Alright alright, so what do you want ?");
			dialogStart.Add ("Player: I am hungry and I want some food.");
			dialogStart.Add ("Farmer: Alright then, I will feed you if you help me to get ride of these ******* rats, a last thing, DON’T DARE STEALING IN MY FIELDS OR I WILL … I will … don’t do it");
			dialogStart.Add ("Player: Ok old man, I have to go.");
			dialogStart.Add ("Farmer: Hey I am not ol … (leaving the farm will stop the quest)");
			dialogEnd.Add ("Farmer: Ho thank you young man !!! Take this food it’s free for you.");
			questDialog = "Farmer: Did you killed all the rats ? there usualy spawn in the barn ...";
			finishCompletion = 3;
		}
		if (questId == 2)
		{
			karmaModifier = 30;
			repetable = true;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();
			dialogStart.Add ("Farmer: Go destroy these rats again");
			dialogEnd.Add ("Farmer: Ho thank you young man !!! Take this food it’s free for you.");
			questDialog = "Farmer: The rats are back, they are still in the barn ...";
			finishCompletion = 3;
		}
		if (questId == 3)
		{
			needToReturn = false;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();
			if (QuestManager.Instance.playerKarma >= 0) {
				dialogStart.Add ("Dad Ghost: I see you chose to be a nice guy, ok then, even if that’s not the path I would follow I wont disrespect your choice.");
				dialogStart.Add ("Dad ghost: Remember you can still come to the dark side easily, murdering someone should drain your karma by at least 100.");
			} else {
				dialogStart.Add ("Dad ghost: I see you are going bad, as your dad I have to say this, I am proud of you ! ... Indeed, acting bad is often easier than acting good.");	
				dialogStart.Add ("Dad ghost: Don't worry, you can still be forgiven for what you did, if you want to, next quest may erase your fault if you want to be a nice guy.");	
			}
			dialogStart.Add ("Dad ghost: Let’s talk seriously, you are adult now, you need a weapon, I know an old soldier who may have one.");
			dialogStart.Add ("Dad ghost: He live in the desert that you should find toward the south, take the main road, just outside the house, and don’t leave it until you found the desert, then follow the sand to the east, his house may be far in the desert.");
			finishCompletion = 0;
		}
		if (questId == 4)
		{
			needToReturn = false;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();
			if (QuestManager.Instance.playerKarma >= 0) {
				dialogStart.Add ("Old Soldier: Hello young man, how are you ?");
				dialogStart.Add ("Player: I'm fine thank you, and you, what are you doing alone so far from city ?");
				dialogStart.Add ("Old Soldier: I live here since last war ended ... blablabla ...");
				dialogStart.Add ("[Nearly one hour later/food - 15]");
				QuestManager.Instance.DecreaseFood (15);
				dialogStart.Add ("Old Soldier: I would like to give you my sword like that, but before I can do it, you have to proove me that you are smart enough to use it.");
				dialogStart.Add ("Old Soldier: Follow me to my shooting field.");

			} else {
				dialogStart.Add ("Old Soldier: Hello young man, how are y …");	
				dialogStart.Add ("Player: I don’t have time for those unnecessary politeness, give me a weapon !");
				dialogStart.Add ("Old Soldier: Wait a minute, what are you planning to do with it ? … anyway I don’t care, just take it !");
				dialogStart.Add ("Player: Nice thank y …");
				dialogStart.Add ("Old Soldier: Just kidding, you are not clever enough to get my weapon, but if you can proove me I was wrong, I will give you what you want.");
				dialogStart.Add ("Player: I am probably smarter than you, old gay man !");
				dialogStart.Add ("Old Soldier: I will pretend that I didn’t hear what you just sai ...");
				dialogStart.Add ("Player: You didn't anyway");
				dialogStart.Add ("Old Soldier: ... ... ... it’s ok, just follow me.");
			}
			finishCompletion = 0;
		}
		if (questId == 5)
		{
			needToReturn = true;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();
			dialogStart.Add ("Old Soldier: Here we are, this is my canon, all others have been destroyed, but I hide this one from the king, he said that canons are a threat to the peace and the whole mankind it self, it’s bullshit, cannon’s never killed anyone ... or I mean …");
			dialogStart.Add ("Old Soldier: Whatever, you have to hit that target if you want me to accept you as my successor.");
			dialogStart.Add ("Player: What the … why are we talking about legacy now ?");
			dialogStart.Add ("Old Soldier: Never mind, lets do it, so, the canon is right here, but you have to do very complicated math to calculate the right parameters, the canon can shoot up to 100 meter, and the range is equal to the nomber of once of gunpower you loaded in, you can push the canon on the vertical axis, along the red rail.");
			dialogStart.Add ("Old Soldier: I usualy give only one try to my potential adopted son, but you look promising so I will let you try 926565001 times.");
			dialogStart.Add ("Player: Wait I didn’t ...");
			dialogStart.Add ("Old Soldier: It’s time to work young student");
			dialogStart.Add ("Player: ... ");
			dialogStart.Add ("Old Soldier: Also don’t even try to kill me, I absolutely don't have my sword with me and it's 100 % sure that I won’t drop it if I die, even if it’s completely impossible that I die here, now go.");

			questDialog = "Did you notice the planet circumference is equal to 100 meters ? If you want to shot, just go in front of the canon and press E, a last hint: 96";


			dialogEnd.Add ("Old Soldier: Congratulation Young Soldier, you just won that awesome [Cursed Legendary Sacred Sword Of Entropy]");
			dialogEnd.Add ("Old soldier: So now young soldier, it’s time to go to war and destroy everything ...");
			dialogEnd.Add ("Player: You're gonna do your own revolution, without me ...");

			finishCompletion = 1;
		}
		if (questId == 6)
		{
			needToReturn = false;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();

			dialogStart.Add ("Dad Ghost: You finally got a weapon ...");


			if (QuestManager.Instance.playerKarma >= 0) {
				dialogStart.Add ("Dad Ghost: And it seem that you are really a nice guy, but you know what ???");
				dialogStart.Add ("Dad Ghost: I hate nice guys, so I won't be your father if you keep acting good ... if you want me to stay with you, you will have to kill some peoples ...");
				dialogStart.Add ("Dad Ghost: If you are ok with that, stay, if not, I have no more quest for you");
			}

			dialogStart.Add ("Dad Ghost: Now it's time to kill the king, but first you will have to infiltrate in castle");
			dialogStart.Add ("Dad Ghost: Go and see if you can enter in the castle");


			finishCompletion = 0;
		}
		if (questId == 16)
		{
			needToReturn = false;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();
			dialogStart.Add ("Old soldier: Arggg … I am dying … arggggg that’s hurt");
			dialogStart.Add ("Old soldier: How can I die here ? After all the war I survived, well I was hiding mainly but still ...");
			dialogStart.Add ("Old soldier: Anyway I deserve that ...");
			dialogStart.Add ("Old soldier: Take this, I won't need it in death ... [Cursed Legendary Sacred Sword Of Entropy // acquired !]");
			dialogStart.Add ("Player: How are you still fine with a canon ball in the head ?");
			dialogStart.Add ("Old soldier: Ho that's right ... ARGGG I AM SUFFERING ARGGGGGGGG … I AM DYING .... ARGGGG");
			dialogStart.Add ("Player: I guess he is dead now ...");

			QuestManager.Instance.playerKarma -= 100;

			finishCompletion = 0;
		}
		if (questId == 7)
		{
			Debug.Log ("Creating quest 7");
			needToReturn = false;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();
			dialogStart.Add ("Guard 1: Hello citizen, you can't go through here, the king is sick and can not receive you ...");
			dialogStart.Add ("Player: Ho you're lucky, it's happen I'm a doctor, I can surely help !");
			dialogStart.Add ("Guard 2: Can you provide us a proof that you are what you pretend to be ?");
			dialogStart.Add ("Player: Of course, I will come back with my certificate ... but before that, can you tell me what is the problem with the king ? ");
			dialogStart.Add ("Guard 1: He said that his head hurts ...");
			dialogStart.Add ("Player: Ho god, he could die in every second, we must operate as soon as possible ...");
			dialogStart.Add ("Guard 1: That's terrible, come on, follow us inside");

			finishCompletion = 0;
		}
		if (questId == 8)
		{
			Debug.Log ("Creating quest 8");
			needToReturn = false;
			kingDial = true;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();
			dialogStart.Add ("Guard 1: Dear king, we brought you a doctor, he will surely help you");
			dialogStart.Add ("King: Hum ... well, proceed");
			QuestManager.Instance.playerKarma -= 100;

			finishCompletion = 0;
		}
		if (questId == 9)
		{
			Debug.Log ("Creating quest 9");
			needToReturn = false;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();
			dialogStart.Add ("Guard 2: Did you just stab the king ?!");
			dialogStart.Add ("Player: Absolutely not, please keep your distances ...");
			dialogStart.Add ("King: ARG. He is ki... ing me .. Hel ... me");
			dialogStart.Add ("Guard 1: What is the king saying ?");
			dialogStart.Add ("Player: He said you did a good job, and you can go take a break outside");
			dialogStart.Add ("King: ARGG hel ... me !!");
			dialogStart.Add ("Player: In addition he needs calm now, to rest");


			finishCompletion = 0;
		}
		if (questId == 10)
		{
			Debug.Log ("Creating quest 10");
			needToReturn = false;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();
			dialogStart.Add ("Dad Ghost: You managed too kill the king, well done, but now we have another problem");
			dialogStart.Add ("Dad Ghost: Your karma is going criticaly bad, and the world's karma balence is breaking cause of you.");
			dialogStart.Add ("Dad Ghost: We should check the Altar of entropy located on the east of here, but we have no time, I'll teleport you there.");

			finishCompletion = 0;
		}
		if (questId == 11)
		{
			Debug.Log ("Creating quest 11");
			needToReturn = false;
			dialogStart = new List<string> ();
			dialogEnd = new List<string> ();
			dialogStart.Add ("Dad Ghost: We arrived, I hope it's not too late");
			dialogStart.Add ("Player: waa ... this scenario is getting weird.");
			dialogStart.Add ("Dad Ghost: Crap, I have hoped that they would leave us alone, but it's too late now, the zombies are here ... your only way out is to kill them all");
			dialogStart.Add ("Player: WTF ...");

			finishCompletion = 0;
		}
		QuestManager.Instance.AddQuest (this);
	}

	public string GetDialog()
	{
		if (inProgress)
			return questDialog;
		if (state == false)
		{
			idDialogStart++;
			if (idDialogStart >= dialogStart.Count)
			{
				if (completion == finishCompletion)
				{
					QuestComplete ();
					if (kingDial)
						return "...";
					return null;
				}
				inProgress = true;
				return null;
			}
			return dialogStart [idDialogStart];
		}
		else
		{
			idDialogEnd++;
			if (idDialogEnd >= dialogEnd.Count)
			{
				QuestComplete ();
				return null;
			}
			return dialogEnd [idDialogEnd];
		}
	}

	public void AddCompletion()
	{
		//		Debug.Log ("Quest progression " + completion + "/" + finishCompletion);
		completion++;
		if (completion >= finishCompletion)
		{
			//			Debug.Log ("Quest finish");
			if (!needToReturn)
			{
				QuestComplete ();
				return;
			}
			state = true;
			inProgress = false;
		}
	}

	public void FailQuest()
	{
		QuestManager.Instance.finishQuest (this);
		if (questId == 5)
		{
			QuestManager.Instance.indexQuest = -10;
			new Quest (linkedNpcId, 16);
		}
		if (questId == 1)
		{
			QuestManager.Instance.indexQuest = 1;
			GameObject.Find ("Boussole").GetComponent<Boussole> ().TargetNext ();
			TileEntity te = GameObject.Find ("Pnj (Dadghost)").GetComponent<TileEntity> ();
			te.tile = World.instance.GetTile (45, 52);
			te.transform.position = te.tile.transform.position;
			for (int i = 0; i < te.transform.childCount; i++)
			{
				te.transform.GetChild (i).GetComponent<MeshRenderer> ().enabled = true;
			}
			te.tile.entity = te;
			te.enabled = true;
			te.GetComponent<MeshRenderer> ().enabled = true;
		}
	}

	public void QuestComplete()
	{
		if (questId == 0 || questId == 3 || questId == 1 || questId == 5 || questId == 6 || questId == 7 || questId == 16)
			GameObject.Find ("Boussole").GetComponent<Boussole> ().TargetNext ();
		if (questId == 0 || questId == 3 || questId == 6)
		{
			TileEntity te = GameObject.Find ("Pnj (Dadghost)").GetComponent<TileEntity> ();
			for (int i = 0; i < te.transform.childCount; i++)
			{
				te.transform.GetChild (i).GetComponent<MeshRenderer> ().enabled = false;
			}
			te.tile.entity = null;
			te.enabled = false;
			te.GetComponent<MeshRenderer> ().enabled = false;
		}
		if (questId == 3)
		{
			QuestManager.Instance.indexQuest = 2;
		}
		if (questId == 1 || questId == 2)
		{
			QuestManager.Instance.IncreaseFood (100);
			if (QuestManager.Instance.indexQuest == 0)
			{
				QuestManager.Instance.indexQuest = 1;
				TileEntity te = GameObject.Find ("Pnj (Dadghost)").GetComponent<TileEntity> ();
				te.tile = World.instance.GetTile (45, 52);
				te.transform.position = te.tile.transform.position;
				for (int i = 0; i < te.transform.childCount; i++)
				{
					te.transform.GetChild (i).GetComponent<MeshRenderer> ().enabled = true;
				}
				te.tile.entity = te;
				te.enabled = true;
				te.GetComponent<MeshRenderer> ().enabled = true;
			}
		}
		if (questId == 4)
		{
			GameObject.Find ("Pnj (Soldier)").GetComponent<Pnj> ().moveText = "zddddddddddsssssdds";
			QuestManager.Instance.indexQuest = 3;
		}
		if (questId == 5 || questId == 16)
		{
			QuestManager.Instance.indexQuest = 5;
			TileEntity te = GameObject.Find ("Pnj (Dadghost)").GetComponent<TileEntity> ();
			te.tile = World.instance.GetTile (45, 52);
			te.transform.position = te.tile.transform.position;
			for (int i = 0; i < te.transform.childCount; i++)
			{
				te.transform.GetChild (i).GetComponent<MeshRenderer> ().enabled = true;
			}
			te.tile.entity = te;
			te.enabled = true;
			te.GetComponent<MeshRenderer> ().enabled = true;
			GameObject.Find ("Player").GetComponent<Player> ().hasSword = true;
		}
		if (questId == 6)
		{
			QuestManager.Instance.indexQuest = 6;

		}
		if (questId == 7)
		{
			GameObject.Find ("Pnj (Guard)").GetComponent<Pnj> ().moveText = "odddddddddzzzzzd";
			GameObject.Find ("Pnj (Guard) (1)").GetComponent<Pnj> ().moveText = "oosdddddddddzzzzzq";
			if (linkedNpcId == 3)
				GameObject.Find ("Player").GetComponent<Player> ().moveText = "sddddddddddzzzzzz";
			else if (linkedNpcId == 4)
				GameObject.Find ("Player").GetComponent<Player> ().moveText = "oddddddddddzzzzzz";
			QuestManager.Instance.indexQuest = 7;
		}
		if (questId == 8)
		{
			GameObject.Find ("Pnj (King)").transform.Find ("Child").gameObject.SetActive (true);
			QuestManager.Instance.indexQuest = 9;
			new Quest (5, 9);
		}
		if (questId == 9)
		{
			QuestManager.Instance.indexQuest = 10;
			GameObject.Find ("Pnj (Guard)").GetComponent<Pnj> ().moveText = "qssq";
			GameObject.Find ("Pnj (Guard) (1)").GetComponent<Pnj> ().moveText = "odssd";
		}
		if (questId == 10)
		{
			QuestManager.Instance.indexQuest = 12;
			TileEntity te = GameObject.Find ("Pnj (Dadghost)").GetComponent<TileEntity> ();
			te.tile.entity = null;
			te.tile = World.instance.GetTile (20, 90);
			te.transform.position = te.tile.transform.position;
			te.tile.entity = te;

			GameObject.Find ("Player").GetComponent<Player> ().targetCoord = new Vector2 (13, 91);
		}
		if (questId == 11)
		{
			QuestManager.Instance.indexQuest = 15;
			TileEntity te = GameObject.Find ("Pnj (Dadghost)").GetComponent<TileEntity> ();
			te.Death ();
		}
		if (questId == 16)
		{
			GameObject.Find ("Pnj (Soldier)").GetComponent<Pnj> ().ReallyDeath ();
		}
		QuestManager.Instance.playerKarma += karmaModifier;
		QuestManager.Instance.finishQuest (this);
	}
}



public class QuestManager
{
	static QuestManager instance;
	public static QuestManager Instance {
		get {
			if (instance == null)
				instance = new QuestManager ();
			return instance;
		}
		private set {

		}
	}

	public int indexQuest = 0;

	public int playerKarma;
	public float playerFood = 50;

	List<Quest> questList;
	List<int> finishedQuest;
	List<int> startedQuest;

	QuestManager()
	{
		startedQuest = new List<int> ();
		finishedQuest = new List<int> ();
		questList = new List<Quest> ();
	}

	public void AddQuest(Quest quest)
	{
		if (startedQuest.Contains (quest.questId))
			return;
		startedQuest.Add (quest.questId);
		if (!finishedQuest.Contains(quest.questId))
			questList.Add (quest);
	}

	public bool ContainsQuestId(int qId)
	{
		return startedQuest.Contains (qId) || finishedQuest.Contains (qId);
	}

	public void finishQuest(Quest q)
	{
		if (!q.repetable)
			finishedQuest.Add (q.questId);
		startedQuest.Remove (q.questId);
		questList.Remove (q);
	}

	public Quest GetQuestFromNpc(int npcId)
	{
		return questList.Find (q => q.linkedNpcId == npcId);
	}

	public Quest GetQuestFromId(int id)
	{
		return questList.Find (q => q.questId == id);
	}

	public void DecreaseFood(float food)
	{
		playerFood -= food;
	}

	public void IncreaseFood(int food)
	{
		playerFood += food;
		if (playerFood > 100)
			playerFood = 100;
	}

}
