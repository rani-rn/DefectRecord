using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DefectReport {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int ReportId { get; set; }
    [StringLength(20)] 
    [Required]
    public string SerialNumber {get; set;}
    [Required]
    public int DefectId {get;  set;}
    [StringLength(250)]
    
    public string? Description {get; set;}
    [Required]
    public string Status {get; set;}
    [Required]
    public int LineProductionId {get; set;}
    [Required]
    public string Reporter {get; set;}
    [Required]
    public int RoleId { get; set; }

    [Required]
public DateTime ReportDate { get; set; }

     [ForeignKey("LineProductionId")]
    public LineProduction LineProduction { get; set; }

    [ForeignKey("DefectId")]
    public Defect Defect { get; set; }

    [ForeignKey("RoleId")]
    public UserRole Role { get; set; }
}