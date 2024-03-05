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
}