namespace SchoolHarbor.Core.Identifier;

public class ReferenceId : IReferenceId
{
    public ReferenceId(
        ReferenceIdKind referenceIdKind,
        ReferenceIdSourceKind referenceIdSourceKind,
        string referenceIdValue)
    {
        Kind = referenceIdKind;
        SourceKind = referenceIdSourceKind;
        Value = referenceIdValue;
    }
        
    public ReferenceIdKind Kind { get; }
        
    public ReferenceIdSourceKind SourceKind { get; }
        
    public string Value { get; }

    public bool Equals(ReferenceId other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
            
        return Kind == other.Kind && 
               SourceKind == other.SourceKind && 
               Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        return ReferenceEquals(this, obj) || 
               obj is ReferenceId other && 
               Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int) Kind, (int) SourceKind, Value);
    }
}