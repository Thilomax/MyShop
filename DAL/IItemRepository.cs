using MyShop.Models;

namespace MyShop.DAL;

public interface IItemRepository
{
	Task<IEnumerable<Item>> GetAll();
    Task<Item?> GetItemById(int id);
	Task Create(Item item);
    Task Update(Item item);
    Task<bool> Delete(int id);
}
/*- Interfacet. -> En "meny": sier *hva* du kan be om, ikke *hvordan* det gjøres.
- Controlleren kjenner bare menyen. Hvilken klasse som faktisk gjør jobben bestemmes én gang i Program.cs.*/