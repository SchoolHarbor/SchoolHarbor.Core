namespace SchoolHarbor.Core.Identifier;

public class SchoolHarborSqlDbId : IReferenceId
{
    public SchoolHarborSqlDbId(int schoolHarborSqlDbId)
    {
        NumericSqlDbId = schoolHarborSqlDbId;
        Value = schoolHarborSqlDbId.ToString();
    }

    public ReferenceIdKind Kind => ReferenceIdKind.Database;
    
    public ReferenceIdSourceKind SourceKind => ReferenceIdSourceKind.SchoolHarborSql;
    
    public string Value { get; }

    public int NumericSqlDbId { get; }
}