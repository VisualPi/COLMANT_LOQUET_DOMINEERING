using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum EAiAlgo { EVALUATION = 0, MINAMAX, NEGAMAX, ALPHABETA, KILLER };

class essai
{
	public essai(int idx, Coordonnee c)
	{
		this.result = idx;
		this.c = c;
	}
	public int result;
	public Coordonnee c;
}

public class AI
{
	EPlayer _direction = EPlayer.HORIZONTAL;
	EAiAlgo _algo;
	public AI(EAiAlgo algo)
	{
		this._algo = algo;
	}

	public bool Move(Board b)
	{
		Coordonnee retCoord = new Coordonnee{ line = -1, column = -1};
		switch( _algo )
		{
		case EAiAlgo.EVALUATION:
			List< essai > essais = new List<essai>();
			List<Coordonnee> vecAI = SimulateMove(b, false);
			foreach( var move in vecAI )
			{
				Play(b, move);
				essais.Add(new essai(Evaluation(b, EPlayer.HORIZONTAL), move));
				Undo(b, move);
			}
			if( essais.Count != 0 )
			{
				essai e = (essai)essais.OrderBy(es => es.result).First();
				retCoord = e.c;
			}
			break;
		case EAiAlgo.MINAMAX:

			break;
		case EAiAlgo.NEGAMAX:
			break;
		case EAiAlgo.ALPHABETA:
			break;
		case EAiAlgo.KILLER:
			break;
		default:
			break;
		}
		if( retCoord.line != -1 )
		{
			Play(b, retCoord);
			return true;
		}
		return false;
	}

	public int Evaluation(Board b, EPlayer player)
	{
		return player == EPlayer.HORIZONTAL
			? ( SimulateMove(b, false).Count - SimulateMove(b, true).Count )
			: ( SimulateMove(b, true).Count - SimulateMove(b, false).Count );
	}

	public List<Coordonnee> SimulateMove(Board b, bool simulatePlayer)//if true, simulate player's turn
	{
		List<Coordonnee> retV = new List<Coordonnee>();
		if( simulatePlayer )
		{
			if( this._direction == EPlayer.HORIZONTAL )
			{//joueur est vertical
				for( int i = 0 ; i < b.GetLines() - 1 ; ++i )
				{
					for( int j = 0 ; j < b[i].Count ; ++j )
					{
						if( b[i][j] == false && b[i + 1][j] == false )
							retV.Add(new Coordonnee { line = i, column = j });
					}
				}
			}
			else
			{//joueur est horizontal
				for( int i = 0 ; i < b.GetLines() ; ++i )
				{
					for( int j = 0 ; j < b[i].Count - 1 ; ++j )
					{
						if( b[i][j] == false && b[i][j + 1] == false )
							retV.Add(new Coordonnee { line = i, column = j });
					}
				}
			}
		}
		else//pour ia
		{
			if( this._direction == EPlayer.HORIZONTAL )
			{
				for( int i = 0 ; i < b.GetLines() ; ++i )
				{
					for( int j = 0 ; j < b[i].Count - 1 ; ++j )
					{
						if( b[i][j] == false && b[i][j + 1] == false )
							retV.Add(new Coordonnee { line = i, column = j });
					}
				}
			}
			else
			{
				for( int i = 0 ; i < b.GetLines() - 1 ; ++i )
				{
					for( int j = 0 ; j < b[i].Count ; ++j )
					{
						if( b[i][j] == false && b[i + 1][j] == false )
							retV.Add(new Coordonnee { line = i, column = j });
					}
				}
			}
		}
		return retV;
	}

	private void Play(Board b, Coordonnee c)
	{
		b[c.line][c.column] = true;
		b[c.line][c.column + 1] = true;
	}
	private void Undo(Board b, Coordonnee c)
	{
		b[c.line][c.column] = false;
		b[c.line][c.column + 1] = false;
	}

	//public essai Max(int depth, Board b)
	//{
	//	if( depth == 0 )
	//	{
	//		return Evaluation(b, EPlayer.HORIZONTAL);
 //       }

	//}
}
