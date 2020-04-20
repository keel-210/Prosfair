using System.Collections.Generic;
public interface IBeldidect
{
	PieceType BeldidectType { get; set; }
	List<IPiece> pieces { get; set; }
}