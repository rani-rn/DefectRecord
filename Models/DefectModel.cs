using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Defect {
    [Key]
    public int DefectId { get; set; }
    public string DefectName {get; set;}
}