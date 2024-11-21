using System.ComponentModel.DataAnnotations;

namespace FinOps_Dashboard.Models
{
    public class Resource_VM
    {       
        public int Row_id { get; set; }
        public string? id { get; set; }
        public string? name { get; set; }
        public string? resourceType { get; set; }
        public string? resourceGroupName { get; set; }
        public string? skuName { get; set; }
        public string? locationName { get; set; }
        public string? locationDisplayName { get; set; }
    }
}
