public class OathUIData
{
	public OathType type { get; private set; }
	public IPiece TargetPiece { get; private set; }
	public PieceType MultipleTargetPieceType { get; private set; }
	public BoardAttribute boardAttribute { get; private set; }
	public BoardTime boardTime { get; private set; }
	public OathUIData(IPiece p)
	{
		type = OathType.Enhance;
		TargetPiece = p;
	}
	public OathUIData(BoardAttribute _attribute, BoardTime _time)
	{
		type = OathType.Field;
		boardAttribute = _attribute;
		boardTime = _time;
	}
	public OathUIData(PieceType p)
	{
		type = OathType.TypeEnhance;
		MultipleTargetPieceType = p;
	}
	public OathUIData()
	{
		type = OathType.FieldAbandonment;
	}
}