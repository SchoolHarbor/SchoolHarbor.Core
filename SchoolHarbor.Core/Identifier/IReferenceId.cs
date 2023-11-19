namespace SchoolHarbor.Core.Identifier;

public interface IReferenceId
{
    public ReferenceIdKind Kind { get; }

    public ReferenceIdSourceKind SourceKind { get; }

    public string Value { get; }
}