using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using POSInventory.Models;
using POSInventory.DTOs;

namespace POSInventory.Repositories
{
    public interface IBillingRepository
    {
        Task AddBillAsync(Bill bill);
        Task<List<Bill>> GetBillsAsync();
        Task<Bill> GetBillByIdAsync(int id);
        Task<(List<Bill> Items, int TotalCount)> GetPagedAsync(BillListRequestDto req);
    }

    public class BillingRepository : IBillingRepository
    {
        private readonly AppDbContext _context;
        public BillingRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddBillAsync(Bill bill)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Bill>> GetBillsAsync()
        {
            return await _context.Bills.Include(b => b.Items).ToListAsync();
        }
        public async Task<Bill> GetBillByIdAsync(int id)
        {
            return await _context.Bills.Include(b => b.Items).FirstOrDefaultAsync(b => b.BillId == id);
        }
        public async Task<(List<Bill> Items, int TotalCount)> GetPagedAsync(BillListRequestDto req)
        {
            var query = _context.Bills.Include(b => b.Items).AsQueryable();
            if (!string.IsNullOrWhiteSpace(req.Search))
            {
                var search = req.Search.ToLower();
                query = query.Where(b =>
                    b.Items.Any(i => i.Name != null && i.Name.ToLower().Contains(search))
                );
            }
            if (req.FromDate.HasValue)
                query = query.Where(b => b.CreatedDate >= req.FromDate.Value);
            if (req.ToDate.HasValue)
                query = query.Where(b => b.CreatedDate <= req.ToDate.Value);
            if (!string.IsNullOrWhiteSpace(req.SortBy))
            {
                if (req.SortBy.ToLower() == "date")
                    query = req.SortDesc == true ? query.OrderByDescending(b => b.CreatedDate) : query.OrderBy(b => b.CreatedDate);
                else if (req.SortBy.ToLower() == "total")
                    query = req.SortDesc == true ? query.OrderByDescending(b => b.Total) : query.OrderBy(b => b.Total);
                else
                    query = query.OrderByDescending(b => b.BillId);
            }
            else
            {
                query = query.OrderByDescending(b => b.BillId);
            }
            var total = await query.CountAsync();
            var page = req.Page.GetValueOrDefault(1);
            var pageSize = req.PageSize.GetValueOrDefault(10);
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
    }
}
