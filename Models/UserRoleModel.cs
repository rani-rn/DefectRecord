using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserRole {
    [Key]
    public int RoleId { get; set; }
    public string RoleName {get; set;}
}