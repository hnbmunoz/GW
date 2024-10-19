﻿namespace MLAB.PlayerEngagement.Core.Models.AgentWorkspace;

public class CampaignPlayerModel
{
    public string PlayerId { get; set; }
    public string Username { get; set; }
    public string Status { get; set; }
    public string Brand { get; set; }
    public string Currency { get; set; }
    public DateTime? RegisteredDate { get; set; }
    public string MarketingSource { get; set; }
    public int CampaignId { get; set; }
    public int CampaignStatusId { get; set; }
    public string CampaignType { get; set; }
    public string CampaignName { get; set; }
    public decimal FTDAmount { get; set; }
    public DateTime? FTDDate { get; set; }
    public string AgentName { get; set; }
    public int? AgentId { get; set; }
    public string TaggedBy { get; set; }
    public DateTime? TaggedDate { get; set; }
    public string LastCallStatus { get; set; }
    public DateTime? CampaignLastCallDate { get; set; }
    public bool CampaignPrimaryGoalReached { get; set; }
    public int CampaignPrimaryGoalCount { get; set; }
    public decimal? CampaignPrimaryGoalAmount { get; set; }
    public string Country { get; set; }
    public string MobilePhone { get; set; }
    public int? ContactableCaseCount { get; set; }
    public DateTime? CampaignLastContactableCaseDate { get; set; }
    public int CallListId { get; set; }
    public int? CallListNoteId { get; set; }
    public string CallListNote { get; set; }
    public int? CaseInformationId { get; set; }
    public int? CaseStatusId { get; set; }
    public decimal? PrimaryGoalAmountInUSD { get; set; }
    public double? ValidIncentivePoints { get; set; }
    public string ValidIncentiveSourced { get; set; }
    public decimal? ValidIncentiveSourcedUSD { get; set; }
    public double InvalidIncentivePoints { get; set; }
    public string InvalidIncentiveSource { get; set; }
    public decimal? InvalidIncentiveSourceUSD { get; set; }
    public int? IncentiveValue { get; set; }
    public bool SystemValidation { get; set; }
    public DateTime? LastDepositDate { get; set; }
    public double? LastDepositAmount { get; set; }
    public bool AgentValidation { get; set; }
    public string AgentValidationNotes { get; set; }
    public bool? IsAgentValidated { get; set; }
    public bool LeaderValidation { get; set; }
    public string LeaderValidationNotes { get; set; }
    public int? HighDepositAmount { get; set; }
    public bool? IsLeaderValidated { get; set; }
    public int? LeaderJustificationId { get; set; }
    public string LeaderJustification { get; set; }
    public double? CallEvaluationPoint { get; set; }
    public string CallEvaluationNotes { get; set; }
    public string LastMessageStatus { get; set; }
    public int CallCount { get; set; }
    public int ContactableCallCount { get; set; }
    public string TotalCallDuration { get; set; }
    public string FirstCallDatetime { get; set; }
    public string FirstCallStatus { get; set; }
    public DateTime? SecondCallDatetime { get; set; }
    public string SecondCallStatus { get; set; }
    public string LastCallDateTimeAfter2nd { get; set; }
    public string LastCallStatusAfter2nd { get; set; }
    public string LastCallResponse { get; set; }
    public string FirstCallAttemptCount { get; set; }
    public int? SecondCallAttemptCount { get; set; }
    public int? AdditionalCallAttempt { get; set; }
    public bool? AgentValidationUpdated { get; set; }
    public bool? LeaderValidationUpdated { get; set; }
    public double? InitialDepositAmount { get; set; }
    public DateTime? InitialDepositDate { get; set; }
    public string InitialDepositMethod { get; set; }
    public bool? InitialDeposited { get; set; }
    public double? TotalDepositAmount { get; set; }
    public int? TotalDepositCount { get; set; }
    public int? DepositAttempts { get; set; }
    public int TotalRecordCount { get; set; }
    public int IsWithEmailAndWebPushCommumication { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public bool HasServiceCase { get; set; }
    public string MobileStatus { get; set; }
    public string MobileStatusCode { get; set; }
}