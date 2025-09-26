namespace DisasterRelief.Models
{
    public enum SeverityLevel { Low, Medium, High, Critical }
    public enum IncidentStatus { Open, InProgress, Resolved, Closed }


    public enum DonationType { Food, Clothing, MedicalSupplies, Money, Other }
    public enum DonationStatus { Pending, Collected, Distributed }


    public enum TaskStatus { Open, Assigned, InProgress, Completed, Cancelled }
}