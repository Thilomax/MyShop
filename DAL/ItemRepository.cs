using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.DAL;
/*- En klasse med én jobb: hente og lagre én type ting (her: Items).
- Som et lager med en ekspeditør: controlleren sier "gi meg alle items", 
ekspeditøren vet hvor de ligger og henter dem.*/

public class ItemRepository : IItemRepository
{
    private readonly ItemDbContext _db;

    public ItemRepository(ItemDbContext db)
    {
        _db = db;
    }
    //metode for å hente alle items
    public async Task<IEnumerable<Item>> GetAll()
    {
        return await _db.Items.ToListAsync();
    }

    public async Task<Item?> GetItemById(int id)
    {
        return await _db.Items.FindAsync(id);
    }

    public async Task Create(Item item)
    {
        _db.Items.Add(item);
        await _db.SaveChangesAsync();
    }

    public async Task Update(Item item)
    {
        _db.Items.Update(item);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id)
    {
        var item = await _db.Items.FindAsync(id);
        if (item == null)
        {
            return false;
        }

        _db.Items.Remove(item);
        await _db.SaveChangesAsync();
        return true;
    }
}
