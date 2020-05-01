public class OathPrepare
{
	public OathType type { get; private set; }
	public IPiece TargetPiece { get; private set; }
	public PieceType MultipleTargetPieceType { get; private set; }
	public BoardAttribute boardAttribute { get; private set; }
	public BoardTime boardTime { get; private set; }
	public OathPrepare(IPiece p)
	{
		type = OathType.Enhance;
		TargetPiece = p;
	}
	public OathPrepare(BoardAttribute _attribute, BoardTime _time)
	{
		type = OathType.Field;
		boardAttribute = _attribute;
		boardTime = _time;
	}
	public OathPrepare(PieceType p)
	{
		type = OathType.TypeEnhance;
	}
	public OathPrepare()
	{
		type = OathType.FieldAbandonment;
	}
}
public enum OathType
{
	Enhance, Field, TypeEnhance, FieldAbandonment
}