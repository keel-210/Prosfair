public class OathPrepare
{
	public OathType type { get; set; }
	public IPiece TargetPiece { get; set; }

}
public enum OathType
{
	Enhance, Field, TypeEnhance, FieldAbandonment
}