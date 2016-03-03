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
				Board tmp = new Board(b.GetHeight(), b.GetWidth());
				tmp.SetBoard(b.GetBoard());
				tmp[vecAI[i].x][vecAI[i].y] = true;
				tmp[vecAI[i].x][vecAI[i].y + 1] = true;
				essais.Add(new essai(SimulateMove(tmp, false).Count - SimulateMove(tmp, true).Count, vecAI[i]));
			}
			essais.Min(es => es.result);
			b[essais[0].c.x][essais[0].c.y] = true;
			b[essais[0].c.x][essais[0].c.y + 1] = true;
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
		return new Coordonnee { x = 0, y = 0 };
	}

	public List<Coordonnee> SimulateMove(Board b, bool simulatePlayer)//if true, simulate player's turn
	{
		List<Coordonnee> retV = new List<Coordonnee>();
		if( simulatePlayer )
		{
			if( this._direction == EJoueur.HORIZONTAL)
			{//joueur est vertical
				for( int i = 0 ; i < b.GetHeight() - 1 ; ++i )
				{
					for( int j = 0 ; j < b[i].Count ; ++j )
					{
						if( b[i][j] == false && b[i + 1][j] == false )
							retV.Add(new Coordonnee { x = i, y = j });
					}
				}
			}
			else
			{//joueur est horizontal
				for( int i = 0 ; i < b.GetHeight() ; ++i )
				{
					for( int j = 0 ; j < b[i].Count - 1 ; ++j )
					{
						if( b[i][j] == false && b[i][j + 1] == false )
							retV.Add(new Coordonnee { x = i, y = j });
					}
				}
			}
		}
		else//pour ia
		{
			if( this._direction == EJoueur.HORIZONTAL )
			{
				for( int i = 0 ; i < b.GetHeight() ; ++i )
				{
					for( int j = 0 ; j < b[i].Count - 1 ; ++j )
					{
						if( b[i][j] == false && b[i][j + 1] == false )
							retV.Add(new Coordonnee { x = i, y = j });
					}
				}
			}
			else
			{
				for( int i = 0 ; i < b.GetHeight() - 1 ; ++i )
				{
					for( int j = 0 ; j < b[i].Count ; ++j )
					{
						if( b[i][j] == false && b[i + 1][j] == false )
							retV.Add(new Coordonnee { x = i, y = j });
					}
				}
			}
		}
		return retV;
	}

}
