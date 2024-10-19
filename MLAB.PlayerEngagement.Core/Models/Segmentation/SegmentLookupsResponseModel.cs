namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentLookupsResponseModel
{
    public List<SegmentationFilterFieldModel> FieldList { get; set; }
    public List<SegmentationFilterOperatorModel> OperatorList { get; set; }
    public List<LookupModel> SegmentList { get; set; }
    public List<LookupModel> CampaignList { get; set; }
    public List<LookupModel> LifecycleStageList { get; set; }
    public List<LookupModel> ProductList { get; set; }
    public List<LookupModel> VendorList { get; set; }
    public List<LookupModel> PaymentMethodList { get; set; }
    public List<LookupModel> BonusContextStatusList { get; set; }
    public List<LookupModel> BonusContextDateMappingList { get; set; }
    public List<LookupModel> BonusCategoryList { get; set; }
    public List<LookupModel> ProductTypeList { get; set; }
    public List<LookupModel> RemProfileList { get; set; }
}
