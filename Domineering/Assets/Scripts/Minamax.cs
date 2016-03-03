//using UnityEditor;

//public class Minamax {
//    private int _valMin;
//    private int _valMax;

//    public Minamax(int valMin, int valMax) {
//        _valMin = valMin;
//        _valMax = valMax;
//    }

//    public int Min(int depth) {
//        if (depth == 0)
//            return evalution();

//        int eval = _valMax;

//        foreach (var move in possibleMoves) {
//            play(m);
//            int e = Max(depth);
//            undo(m);
//            if (e < eval)
//                eval = e;
//        }
//        return eval;
//    }

//    public int Max(int depth) {
//        if (depth == 0)
//            return evalution();

//        int eval = _valMin;

//        foreach (var move in possibleMoves) {
//            play(m);
//            int e = Min(depth);
//            undo(m);
//            if (e > eval)
//                eval = e;
//        }
//        return eval;
//    }
//}
