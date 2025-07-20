﻿using System;
using System.Collections.Generic;

public class TicTacToe
{

    bool gameOver = false;
    char player = 'x';
    char ai = 'o';
    char[][] board = [
        [' ', ' ', ' '],
        [' ', ' ', ' '],
        [' ', ' ', ' ']
    ];

    // mostrar tabuleiro
    public void DisplayBoard()
    {
        for (int i = 0; i < board.Length; i++)
        {
            Console.Write("\t");
            for (int j = 0; j < board[i].Length; j++)
            {
                Console.Write($" {char.ToUpper(board[i][j])} ");
                if (j < 2) Console.Write("|");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("\t---+---+---");
        }
    }


    // lógica da ia
    public int[] AiTurn()
    {
        int worstScore = int.MaxValue;
        int[] move = null;

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board.Length; j++)
            {
                if (board[i][j] == ' ')
                {
                    board[i][j] = ai;
                    int score = MiniMax(board, 0, false);
                    board[i][j] = ' ';
                    if (score < worstScore)
                    {
                        worstScore = score;
                        move = [i, j];
                    }
                }
            }
        }
        return move;
    }

    public int MiniMax(char[][] b, int depth, bool isMaximizing)
    {
        char result = CheckWinner();
        if (result == player) return depth - 10;
        else if (result == ai) return 10 - depth;
        else if (result == 't') return 0;

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    if (b[i][j] == ' ')
                    {
                        b[i][j] = ai;
                        int score = MiniMax(b, depth + 1, false);
                        b[i][j] = ' ';
                        bestScore = bestScore > score ? bestScore : score;
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    if (b[i][j] == ' ')
                    {
                        b[i][j] = player;
                        int score = MiniMax(b, depth + 1, true);
                        b[i][j] = ' ';
                        bestScore = bestScore < score ? bestScore : score;
                    }
                }
            }
            return bestScore;
        }
    }

    // verificação de vencedor
    public char CheckWinner()
    {
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i][0] != ' ' && board[i][0] == board[i][1] && board[i][0] == board[i][2]) return board[i][0];
            if (board[0][i] != ' ' && board[0][i] == board[1][i] && board[0][i] == board[2][i]) return board[0][i];
        }

        if (board[0][0] != ' ' && board[0][0] == board[1][1] && board[0][0] == board[2][2]) return board[0][0];
        if (board[0][2] != ' ' && board[0][2] == board[1][1] && board[0][2] == board[2][0]) return board[0][2];

        if (board.All(row => row.All(cell => cell != ' '))) return 't';

        return ' ';
    }

    public void PlayRound()
    {
        char whoPlays = player;
        gameOver = false;
        board = [
            [' ', ' ', ' '],
            [' ', ' ', ' '],
            [' ', ' ', ' ']
        ];

        while (!gameOver)
        {
            if (whoPlays == player)
            {
                DisplayBoard();
                Console.WriteLine("Digite a posição na qual quer marcar (1-9)");
                int index = int.Parse(Console.ReadLine()) - 1;
                int i = index / 3;
                int j = index % 3;

                if (board[i][j] == ' ' && index >= 0 && index <= 8)
                {
                    board[i][j] = player;
                    whoPlays = ai;
                    Console.Clear();
                }
            }
            else
            {
                var position = AiTurn();
                board[position[0]][position[1]] = ai;
                whoPlays = player;
            }

            char winner = CheckWinner();
            if (winner != ' ')
            {
                gameOver = true;
                if (winner == 't') Console.WriteLine("Velha!");
                else Console.WriteLine($"Você {(winner == player ? "venceu" : "perdeu")}");
            }
        }

        Console.WriteLine("Mais uma rodada? (s/n)");
        string playAgain = Console.ReadLine();
        if (playAgain.ToLower() == "s")
        {
            PlayRound();
        }
    }

    public static void Main()
    {
        Console.WriteLine("Tente perder pra IA\n\n");
        var game = new TicTacToe();
        game.PlayRound();
    }   
}