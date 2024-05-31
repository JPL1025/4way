using Godot;
using System;
using System.Collections.Generic;

public class World : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private PackedScene ballScene = GD.Load<PackedScene>("res://Ball.tscn");
	int[,] board;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		board = new int[7, 7];
		
		for (int i = 0; i < 7; i++) {
			for (int j = 0; j < 7; j++) {
				board[i, j] = 0;
			}
		}
		
		AddBall(1);
		AddBall(1);
		
		printBoard();
	}
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta) {
		if (Input.IsActionJustPressed("ui_up")) {
			up();
		}
		if (Input.IsActionJustPressed("ui_down")) {
			down();
		}
		if (Input.IsActionJustPressed("ui_left")) {
			left();
		}
		if (Input.IsActionJustPressed("ui_right")) {
			right();
		}
	}

	private void AddBall(int size) {
		List<int> zeroIndices = new List<int>();
		
		for (int i = 0; i < 7; i++) {
			for (int j = 0; j < 7; j++) {
				if (board[i, j] == 0) {
					zeroIndices.Add((i * 7) + j);
				}
			}
		}
		
		Random rand = new Random();
		int y = zeroIndices[rand.Next(zeroIndices.Count)];
		
		int x = y % 7;
		y /= 7;
		
		board[y, x] = size;
		/*
		foreach (int num in zeroIndices) {
			GD.Print(num);
		}
		*/
		//GD.Print(x + " " + y);
	}
	
	
	private void up() {
		// Store the next available space for the ball to move
		int[] nextFree = {0, 0, 0, 0, 0, 0, 0};
		
		// Check first row
		for (int j = 0; j < 7; j++) {
			if (board[0, j] != 0) {
				nextFree[j] = 1;
			}
		}
		
		// Check other rows, move and merge if possible
		for (int i = 1; i < 7; i++) {
			for (int j = 0; j < 7; j++) {
				if (board[i, j] > 0) {
					if (nextFree[j] > 0 && board[i, j] == board[nextFree[j] - 1, j]) {
						board[nextFree[j] - 1, j]++;
						board[i, j] = 0;
					} else if (nextFree[j] < i) {
						board[nextFree[j], j] = board[i, j];
						board[i, j] = 0;
						nextFree[j]++;
					} else {
						nextFree[j]++;
					}
				} else if (board[i, j] < 0) { // -1 == non-usable space
					nextFree[j] = i;
				}
			}
		}
		
		AddBall(1);
		printBoard();
	}
	
	private void down() {
		
	}
	
	private void left() {
		
	}
	
	private void right() {
		
	}
	
	private void printBoard() {
		for (int i = 0; i < 7; i++) {
			string line = "";
			for (int j = 0; j < 7; j++) {
				line += board[i, j].ToString();
				line += " ";
			}
			GD.Print(line);
		}
		GD.Print("");
	}
	
	private bool CheckGameOver() {
		return false;
	}
	
	private void GameOver() {
		
	}
}
