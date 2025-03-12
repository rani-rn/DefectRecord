using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DefectReport {
    [Key]
    public int ReportId { get; set; }
    [StringLength(20)] 
    public string SerialNumber {get; set;}
    public string DefectId {get;  set;}
    [StringLength(250)]
    public string Description {get; set;}
    public string Status {get; set;}
    public int LineProductionId {get; set;}
    public string Reporter {get; set;}
    public  DateTime ReportDate {get; set;}
}