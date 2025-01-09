namespace SchoolHarbor.Core.Date;

public sealed class DateRange : IEquatable<DateRange>
{
    public DateRange(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
        {
            throw new InvalidDataException("Start date must be less than end date for date range.");
        }
        StartDate = startDate.Date;
        EndDate = endDate.Date;
    }

    public DateTime StartDate { get; }
    
    public DateTime EndDate { get; }

    public bool Equals(DateRange other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        
        return StartDate.Equals(other.StartDate) && EndDate.Equals(other.EndDate);
    }

    public override bool Equals(object obj)
    {
        return ReferenceEquals(this, obj) || obj is DateRange other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(StartDate, EndDate);
    }

    public bool OverlapsButNotEqual(DateRange otherRange)
    {
        return (this.RangeContains(otherRange.StartDate) || 
               this.RangeContains(otherRange.EndDate) || 
               otherRange.RangeContains(this.StartDate) || 
               otherRange.RangeContains(this.EndDate)) &&
               !this.Equals(otherRange);
    }
    
    public bool OverlapsIncludesEqual(DateRange otherRange)
    {
        return this.RangeContains(otherRange.StartDate) || 
                this.RangeContains(otherRange.EndDate) || 
                otherRange.RangeContains(this.StartDate) || 
                otherRange.RangeContains(this.EndDate);
    }

    public bool OverlapsBySingleDay(DateRange otherRange)
    {
        return this.OverlapsIncludesEqual(otherRange) &&
               this.StartDate <= this.EndDate &&
               otherRange.StartDate <= otherRange.EndDate &&
               (this.StartDate.Equals(otherRange.EndDate) ||
                this.EndDate.Equals(otherRange.StartDate));
    }

    public bool RangeContains(DateTime date)
    {
        bool result = date >= StartDate && EndDate >= date;

        return result;
    }
    
    public bool RangeContains(DateRange dateRange)
    {
        bool result = dateRange.StartDate >= this.StartDate && 
                      dateRange.EndDate <= this.EndDate;

        return result;
    }
}