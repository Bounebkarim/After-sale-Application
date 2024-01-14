using System.ComponentModel.DataAnnotations;
using Client.Domain.Common;

namespace Client.Domain;

public class Client : BaseEntity
{
    public Client(Guid id) : base(id) { }

    public Client(Guid newGuid, string name, string lastname, string cin, string adress, string phoneNumber) : base(newGuid)
    {
        Name=name;
        LastName=lastname;
        Cin=cin;
        Address = adress;
        PhoneNumber=phoneNumber;
    }

    public string Name { get; set; }
    public string LastName { get; set; }
    public string Cin { get; set; }    
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
}
