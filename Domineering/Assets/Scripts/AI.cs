using UnityEngine;
using System.Collections;

public enum EAiAlgo {BASIC = 0, MINAMAX, ALPHABETA, KILLER};

public class AI
{
	[SerializeField] EJoueur _direction = EJoueur.HORIZONTAL;
	[SerializeField] EAiAlgo _algo;
	public AI(EAiAlgo algo)
	{
		this._algo = algo;
	}

	public Coordonnee Move()
	{
		switch(_algo)
		{
		case EAiAlgo.BASIC:
			//std::vector<essai> essais;
			//std::vector<coord> vecAI = m(b, false);
			//for( int i = 0 ; i < vecAI.size() ; ++i )
			//{
			//	Board tmp(8);
			//	tmp.SetBoard(b.GetBoard());
			//	tmp[vecAI[i].x_][vecAI[i].y_] = 1;
			//	tmp[vecAI[i].x_][vecAI[i].y_ + 1] = 1;
			//	essais.push_back(essai(m(tmp, false).size() - m(tmp, true).size(), vecAI[i]));
			//}
			//std::sort(essais.begin(), essais.end(), compareEssai);
			//b[essais[0].c_.x_][essais[0].c_.y_] = 1;
			//b[essais[0].c_.x_][essais[0].c_.y_ + 1] = 1;
			break;
		case EAiAlgo.MINAMAX:
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

}
