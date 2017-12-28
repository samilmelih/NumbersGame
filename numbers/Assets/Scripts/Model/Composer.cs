using System;
using System.Collections.Generic;

public class Composer
{
	int noteCount;
	int currNote;

	List<int>[] noteGraph;

	public Composer(int noteCount)
	{
		Random rnd = new Random();

		this.noteCount = noteCount;
		this.currNote = rnd.Next(0, noteCount);

		noteGraph = new List<int>[noteCount];
	}

	public void AddNote(string noteStr)
	{
		int[] note = ParseNote(noteStr);

		noteGraph[note[0]] = new List<int>(note.Length);
		for(int i = 0; i < note.Length; i++)
		{
			if(i != 0)
			{
				noteGraph[note[0]].Add(note[i]);
			}
		}
	}

	public int NextNote()
	{
		Random rnd = new Random();

		int possibleNoteCount = noteGraph[currNote].Count;
		int nextNodeIndex = rnd.Next(0, possibleNoteCount);
		currNote = noteGraph[currNote][nextNodeIndex];

		return currNote;
	}

	static int[] ParseNote(string note)
	{
		return Array.ConvertAll(note.Split('-'), int.Parse);
	}
}
