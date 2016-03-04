using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public enum EAiAlgo { EVALUATION = 0, MINAMAX, NEGAMAX, ALPHABETA, ITERATEALPHABETA, KILLER };

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
		Coordonnee outCoord;
		switch( _algo )
		{
		case EAiAlgo.EVALUATION:
			List< essai > essais = new List<essai>();
			List<Coordonnee> vecAI = SimulateMove(b, false);
			foreach( var move in vecAI )
			{
				Play(b, move, false);
				essais.Add(new essai(Evaluation(b, EPlayer.HORIZONTAL), move));
				Undo(b, move, false);
			}
			if( essais.Count != 0 )
			{
				essai e = (essai)essais.OrderBy(es => es.result).First();
				retCoord = e.c;
			}
			break;
		case EAiAlgo.MINAMAX:
			if( Max(3, b, out outCoord) >= 0 )
				retCoord = outCoord;
			break;
		case EAiAlgo.NEGAMAX:
			if( Negamax(3, b, out outCoord, EPlayer.HORIZONTAL) >= 0 )
				retCoord = outCoord;
			break;
		case EAiAlgo.ALPHABETA:
			int alpha = -8000, beta = 8000;
			int eval;
			if ((eval = NegamaxAlphaBeta (3, alpha, beta, b, out outCoord, EPlayer.HORIZONTAL)) >= 0)
				retCoord = outCoord;
			break;
		case EAiAlgo.ITERATEALPHABETA:
			iterateAlphaBeta (b, out retCoord, EPlayer.HORIZONTAL, 5000f);
			break;
		case EAiAlgo.KILLER:
			baseDepth = 3;
			if ((NegamaxAlphaBetaKiller (3, -8000, 8000, b, out outCoord, EPlayer.HORIZONTAL)) >= 0)
				retCoord = outCoord;
			break;
		default:
			break;
		}
		if( retCoord.line != -1 )
		{
			Play(b, retCoord, false);
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

	private void Play(Board b, Coordonnee c, bool simulatePlayer)
	{
		if( simulatePlayer )
		{
			b[c.line][c.column] = true;
			b[c.line + 1][c.column] = true;
		}
		else
		{
			b[c.line][c.column] = true;
			b[c.line][c.column + 1] = true;
		}
	}
	private void Undo(Board b, Coordonnee c, bool simulatePlayer)
	{
		if( simulatePlayer )
		{
			b[c.line][c.column] = false;
			b[c.line + 1][c.column] = false;
		}
		else
		{
			b[c.line][c.column] = false;
			b[c.line][c.column + 1] = false;
		}
	}

	public int Max(int depth, Board b, out Coordonnee c)
	{
		c = new Coordonnee();
		if( depth == 0 )
		{
			return Evaluation(b, EPlayer.HORIZONTAL);
		}
		int eval = -1;
		Coordonnee dumb;
		foreach( var move in SimulateMove(b, false) )
		{
			Play(b, move, false);
			int e = Min(depth-1, b, out dumb);
			Undo(b, move, false);
			if( e > eval )
			{
				c = move;
				eval = e;
			}
		}
		return eval;
	}
	public int Min(int depth, Board b, out Coordonnee c)
	{
		c = new Coordonnee();
		if( depth == 0 )
		{
			return Evaluation(b, EPlayer.VERTICAL);
		}
		int eval = +10000;
		Coordonnee dumb;
		foreach( var move in SimulateMove(b, true) )
		{
			Play(b, move, true);
			int e = Max(depth-1, b, out dumb);
			Undo(b, move, true);
			if( e < eval )
			{
				c = move;
				eval = e;
			}
		}
		return eval;
	}

	public int Negamax(int depth, Board b, out Coordonnee c, EPlayer player)
	{
		c = new Coordonnee();
		if( depth == 0 )
		{
			return Evaluation(b, player);
		}
		int eval = -2;
		Coordonnee dumb;
		foreach( var move in SimulateMove(b, player == EPlayer.HORIZONTAL ? false : true) )
		{
			Play(b, move, player == EPlayer.HORIZONTAL ? false : true );
			int e = -Negamax(depth-1, b, out dumb,player == EPlayer.HORIZONTAL ? EPlayer.VERTICAL : EPlayer.HORIZONTAL );
			Undo(b, move, player == EPlayer.HORIZONTAL ? false : true);
			if( e > eval )
			{
				c = move;
				eval = e;
			}
		}
		return eval;
	}

	public int NegamaxAlphaBeta(int depth, int alpha, int beta, Board b, out Coordonnee c, EPlayer player)
	{
		c = new Coordonnee();
		if( depth == 0 )
		{
			return Evaluation(b, player);
		}
		int e;
		Coordonnee dumb;
		foreach( var move in SimulateMove(b, player == EPlayer.HORIZONTAL ? false : true) )
		{
			Play(b, move, player == EPlayer.HORIZONTAL ? false : true );
			e = -NegamaxAlphaBeta(depth-1, -beta, -alpha, b, out dumb,player == EPlayer.HORIZONTAL ? EPlayer.VERTICAL : EPlayer.HORIZONTAL );
			Undo(b, move, player == EPlayer.HORIZONTAL ? false : true);
			if( e > alpha)
			{
				alpha = e;
				c = move;
				if(alpha >= beta)
					return beta;
			}
		}
		return alpha;
	}

	public int iterateAlphaBeta(Board b, out Coordonnee c, EPlayer player, float timer)
	{
		c = new Coordonnee ();
		int i = 1;
		int retValue = 0;
		Coordonnee c2 = new Coordonnee();
		System.Diagnostics.Stopwatch t = new System.Diagnostics.Stopwatch();
		t.Start ();
		Debug.Log ("je commence omg");
		while (t.ElapsedMilliseconds <= timer) {
			Thread thread = new Thread (()=>{retValue = NegamaxAlphaBeta(i++, -10000, 10000, b, out c2, player);});
			thread.Start ();
			if ((thread.Join((int)(timer-t.ElapsedMilliseconds)) && retValue > 0))
				c = c2;
		}
		return retValue;
	}

	private List<KeyValuePair<int, Coordonnee>> killerMoves = new List<KeyValuePair<int, Coordonnee>>();
	private int baseDepth;

	public int NegamaxAlphaBetaKiller(int depth, int alpha, int beta, Board b, out Coordonnee c, EPlayer player)
	{
		c = new Coordonnee();
		if( depth == 0 )
		{
			return Evaluation(b, player);
		}
		int e;
		Coordonnee dumb;
		List<Coordonnee> sims = SimulateMove (b, player == EPlayer.HORIZONTAL ? false : true);
		KeyValuePair<int, Coordonnee> kvp = killerMoves.Find (f => f.Key == baseDepth - depth);
		if ((killerMoves.Count > (baseDepth - depth)) && sims.Contains (kvp.Value)) {
			sims.OrderBy(l => l.Equals(kvp.Value));
			var tmp = sims [0];
			for (int i = 0; i < sims.Count; i++) {
				if (sims [i].line == kvp.Value.line && sims[i].column == kvp.Value.column) {
					sims [0] = kvp.Value;
					sims [i] = tmp;
				}
			}
		}
		foreach( var move in sims )
		{
			Play(b, move, player == EPlayer.HORIZONTAL ? false : true );
			e = -NegamaxAlphaBetaKiller(depth-1, -beta, -alpha, b, out dumb,player == EPlayer.HORIZONTAL ? EPlayer.VERTICAL : EPlayer.HORIZONTAL );
			Undo(b, move, player == EPlayer.HORIZONTAL ? false : true);
			if( e > alpha)
			{
				alpha = e;
				c = move;
				if (alpha >= beta) {
					killerMoves.Add(new KeyValuePair<int, Coordonnee>(baseDepth - depth, move));
					return beta;
				}
			}
		}
		return alpha;
	}

}
