using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
	public Item input01;
	public Item input02;
	public Item input03;

	public Item result;

	public bool advanced;
	private int input = 1;

	public void SetInput(int x) { input = x; }
	public int GetInput() { return input; }
}
