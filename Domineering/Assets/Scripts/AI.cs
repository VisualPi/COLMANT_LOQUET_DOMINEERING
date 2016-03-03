using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum EAiAlgo {BASIC = 0, MINAMAX, NEGAMAX, ALPHABETA, KILLER};

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
	[SerializeField] EJoueur _direction = EJoueur.HORIZONTAL;
	[SerializeField] EAiAlgo _algo;
	public AI(EAiAlgo algo)
	{
		this._algo = algo;
	}

	public Coordonnee Move(Board b)
	{
		switch(_algo)
		{
		case EAiAlgo.BASIC:
			List< essai > essais = new List<essai>();
			List<Coordonnee> vecAI = SimulateMove(b, false);
			for( int i = 0 ; i < vecAI.Count ; i++ )
			{
				Board tmp = new Board(b.GetLines(), b.GetColumns());
				tmp.SetBoard(b.GetBoard());
				tmp[vecAI[i].line][vecAI[i].column] = true;
				tmp[vecAI[i].line][vecAI[i].column + 1] = true;
				essais.Add(new essai(SimulateMove(tmp, false).Count - SimulateMove(tmp, true).Count, vecAI[i]));
			}
			essai e = (essai)essais.OrderBy(es => es.result).First();
			b[e.c.line][e.c.column] = true;
			b[e.c.line][e.c.column + 1] = true;
			return new Coordonnee { line = e.c.line, column = e.c.column };
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
		return new Coordonnee { line = 0, column = 0 };
	}

	public List<Coordonnee> SimulateMove(Board b, bool simulatePlayer)//if true, simulate player's turn
	{
		List<Coordonnee> retV = new List<Coordonnee>();
		if( simulatePlayer )
		{
			if( this._direction == EJoueur.HORIZONTAL)
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
			if( this._direction == EJoueur.HORIZONTAL )
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

}
