using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BoardManager : MonoBehaviour
{
	public Board mainBoard;
	public List<Board> subBoards;
	void Start()
	{
		mainBoard = new Board(13);
	}
	void StartGame()
	{

	}
	public void AddSubBoard() { }
}
