namespace FinOps_Dashboard.Models
{
    public class VirtualMachine
    {

        public int Id { get; set; }
        public string? VMName { get; set; }
        public string? VMState { get; set; }
        public string? VMApplication { get; set; }
        public string? VMAllocatedCPU { get; set; }
        public string? VMMemory { get; set; }
        public string? VMNoOfProcessors { get; set; }
        public string? VMDisplayName { get; set; }
        public string? VMLastUpdated { get; set; }
        public string? VMDeploymentURL { get; set; }
        public string? VMPoweredOnOff { get; set; }
        public string? VMApplicationVersion { get; set; }
        public string? VMOperatingSystem { get; set; }
        public string? VMCPUUtilization { get; set; }
        public string? VMIpv4Address { get; set; }

    }
}
