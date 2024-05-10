namespace SchoolHarbor.Core.Identifier;

public class SchoolHarborMongoDbId : IReferenceId
{
    public SchoolHarborMongoDbId(int schoolHarborMongoDbId)
    {
        NumericMongoDbId = schoolHarborMongoDbId;
        Value = schoolHarborMongoDbId.ToString();
    }

    public ReferenceIdKind Kind => ReferenceIdKind.Database;

    public ReferenceIdSourceKind SourceKind => ReferenceIdSourceKind.SchoolHarborSql;

    public string Value { get; }

    public int NumericMongoDbId { get; }
}