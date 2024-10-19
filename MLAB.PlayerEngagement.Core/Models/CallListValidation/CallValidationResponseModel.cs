namespace MLAB.PlayerEngagement.Core.Models.CallListValidation;

public class CallValidationResponseModel
{
    public int CampaignPlayerId { get; set; }
    public string CallListNotes { get; set; }
    public string PlayerId { get; set; }
    public string Username { get; set; }
    public string PlayerStatusName { get; set; }
    public string Brand { get; set; }
    public string Currency { get; set; }
    public string Country { get; set; }
    public string  MobileNumber { get; set; }
    public string MarketingSource { get; set; }
    public string CampaignName { get; set; }
    public DateTime RegisteredDate { get; set; }
    public bool Deposited { get; set; }
    public double FTDAmount { get; set; }
    public DateTime FTDDate { get; set; }
    public DateTime LastDepositDate { get; set; }
    public int LastDepositAmount { get; set; }
    public string AgentName { get; set; }
    public int AgentId { get; set; }
    public string TaggedBy { get; set; }
    public DateTime TaggedDate { get; set; }
    public bool PrimaryGoalReached { get; set; }
    public int PrimaryGoalCount { get; set; }
    public double PrimaryGoalAmount { get; set; }
    public double PrimaryGoalAmountInUSD { get; set; }
    public double ValidIncentivePoints { get; set; }
    public string ValidIncentiveSourced { get; set; }
    public double ValidIncentiveSourcedUSD { get; set; }
    public double InvalidIncentivePoints { get; set; }
    public string InvalidIncentiveSource { get; set; }
    public double InvalidIncentiveSourceUSD { get; set; }
    public double  IncentiveValue { get; set; }
    public bool  SystemValidation { get; set; }
    public bool AgentValidation { get; set; }
    public string AgentValidationNotes { get; set; }
    public bool LeaderValidation { get; set; }
    public string LeaderJustification { get; set; }
    public string  LeaderValidationNotes { get; set; }
    public double? CallEvaluationPoint { get; set; }
    public string CallEvaluationNotes { get; set; }
    public int HighDepositAmount { get; set; }
    public string LastMessageStatus { get; set; }
    public DateTime LastCallDate { get; set; }
    public int CallCount { get; set; }
    public int ContactableCallCount { get; set; }
    public DateTime LastContactableCaseDate { get; set; }
    public string TotalCallDuration { get; set; }
    public string FirstCallDatetime { get; set; }
    public string FirstCallStatus { get; set; }
    public DateTime SecondCallDatetime { get; set; }
    public string SecondCallStatus { get; set; }
    public string LastCallDateTimeAfter2nd { get; set; }
    public string LastCallStatusAfter2nd { get; set; }
    public string FirstCallAttemptCount { get; set; }
    public int SecondCallAttemptCount { get; set; }
    public int AdditionalCallAttempt { get; set; }
    public int CampaignId { get; set; }
    public int DepositAttempts { get; set; }
    public bool IsAgentValidated { get; set; }
    public bool IsLeaderValidated { get; set; }
    public int CampaignStatus { get; set; }
    public string MessageStatusAndResponseResult { get; set; }
    public string CampaignType { get; set; }
    public double? InitialDepositAmount { get; set; }
    public DateTime? InitialDepositDate { get; set; }
    public double? TotalDepositAmount { get; set; }
    public int? TotalDepositCount { get; set; }

    // Method to map dynamic object to CallValidationResponseModel
    public static CallValidationResponseModel FromDynamic(dynamic item)
    {
        var model = new CallValidationResponseModel();
        var properties = typeof(CallValidationResponseModel).GetProperties();

        foreach (var property in properties)
        {
            var dynamicProperty = ((IDictionary<string, object>)item).FirstOrDefault(x => x.Key.Equals(property.Name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(dynamicProperty.Key))
            {
                var value = Convert.ChangeType(dynamicProperty.Value, property.PropertyType);
                property.SetValue(model, value);
            }
        }

        return model;
    }
}
