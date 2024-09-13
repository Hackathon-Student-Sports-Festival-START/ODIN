// See https://aka.ms/new-console-template for more information

using StartOdin.Core.Database;
using StartOdin.Domain.Entities.Users;

try
{
    DatabaseController.GetInstance().Admins.Add(new Admin()
    {
        Password = "dsjalkda",
        Role = "dsjalkda",
        Username = "sdajslkda"
    });
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}   


