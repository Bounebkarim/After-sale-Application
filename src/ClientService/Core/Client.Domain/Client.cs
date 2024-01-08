using System.ComponentModel.DataAnnotations;
using Client.Domain.Common;

namespace Client.Domain;

public class Client : BaseEntity
{
    public Client(Guid id,string name,string lastName,string cin ,string address,string phoneNumber) : base(id)
    {
        this.PhoneNumber = phoneNumber;
        this.Name = name;
        this.LastName = lastName;
        this.Address = address;
        this.Cin = cin;
    }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Cin { get; set; }    
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
}
