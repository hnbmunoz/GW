using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{

    public class FieldMappingConfigurationModel
    {
        public List<FormConfigurationModel> FormConfigurations { get; set; }
        public List<GroupingConfigurationModel> GroupConfiguration { get; set; }
    }

    public class FormConfigurationModel
    {
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string FieldConstraint { get; set; }
        public int TicketSectionId { get; set; }
        public int TicketGroupId { get; set; }
        public string TicketGroupName { get; set; }
        public int GroupOrder { get; set; }
        public bool hasAdd { get; set; }
        public bool hasEdit { get; set; }
        public bool hasView { get; set; }
        public bool IsRequired { get; set; }
        public int FieldOrder { get; set; }
        public string FieldSizeName { get; set; }
        public int FieldMappingId { get; set; }
    }


    public class GroupingConfigurationModel
    {
        public int GroupId { get; set; }
        public int ColumnCount { get; set; }
    }


}
