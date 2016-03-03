public struct Coordonnee {
    public int line;
	public int column;
	public bool isValid()
	{
		return (line != -1 && column != -1);
	}

}
