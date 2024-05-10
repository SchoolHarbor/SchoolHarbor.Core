namespace SchoolHarbor.Core.Identifier;

public class SchoolHarborDbId : IReferenceId
{
    public SchoolHarborDbId(int schoolHarborDbId)
    {
        NumericDbId = schoolHarborDbId;
        Value = schoolHarborDbId.ToString();
    }

    public ReferenceIdKind Kind => ReferenceIdKind.Database;
    
    public ReferenceIdSourceKind SourceKind => ReferenceIdSourceKind.SchoolHarborSql;
    
    public string Value { get; }

    public int NumericDbId { get; }
}