namespace SchoolHarbor.Core.Identifier;

public static class Extensions
{
    public static int ExtractNumericValue(this IReferenceId referenceId)
    {
        if(int.TryParse(referenceId.Value, out var result))
        {
               return result;
        }

        throw new InvalidDataException($"ReferenceId is not valid int. Value: {referenceId.Value}");
    }

    public static bool IsSchoolHarborDbId(this IReferenceId referenceId)
    {
        return referenceId.Kind == ReferenceIdKind.Database &&
               referenceId.SourceKind == ReferenceIdSourceKind.SchoolHarbor;
    }

    public static SchoolHarborDbId ToSchoolHarborDbId(this IReferenceId referenceId)
    {
        if (referenceId.IsSchoolHarborDbId() &&
            int.TryParse(referenceId.Value, out var dbId))
        {
            return new SchoolHarborDbId(dbId);
        }
        
        throw new InvalidDataException(
            $"ReferenceId is not valid School Harbor DB Id. Value: {referenceId.Value} Kind: {referenceId.Kind} Source {referenceId.SourceKind}");
    }
}