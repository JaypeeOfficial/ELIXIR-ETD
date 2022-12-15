﻿using ELIXIRETD.DATA.CORE.INTERFACES.SETUP_INTERFACE;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.DTOs.SETUP_DTO;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.HELPERS;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS.SETUP_MODEL;
using ELIXIRETD.DATA.DATA_ACCESS_LAYER.STORE_CONTEXT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIXIRETD.DATA.DATA_ACCESS_LAYER.REPOSITORIES.SETUP_REPOSITORY
{
    public class SupplierRepository : ISupplierRepository
    {
        private new readonly StoreContext _context;

        public SupplierRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<SupplierDto>> GetAllActiveSupplier()
        {
            var supplier = _context.Suppliers.Where(x => x.IsActive == true)
                                             .Select(x => new SupplierDto
                                             {
                                                 Id = x.Id,
                                                 SupplierCode = x.SupplierCode,
                                                 SupplierName = x.SupplierName,
                                                 SupplierAddress = x.SupplierAddress,
                                                 DateAdded = x.DateAdded.ToString("MM/dd/yyyy"),
                                                 AddedBy = x.AddedBy,
                                                 IsActive = x.IsActive
                                             });

            return await supplier.ToListAsync();

        }

        public async Task<IReadOnlyList<SupplierDto>> GetAllInActiveSupplier()
        {
            var supplier = _context.Suppliers.Where(x => x.IsActive == false)
                                           .Select(x => new SupplierDto
                                           {
                                               Id = x.Id,
                                               SupplierCode = x.SupplierCode,
                                               SupplierName = x.SupplierName,
                                               SupplierAddress = x.SupplierAddress,
                                               DateAdded = x.DateAdded.ToString("MM/dd/yyyy"),
                                               AddedBy = x.AddedBy,
                                               IsActive = x.IsActive
                                           });

            return await supplier.ToListAsync();
        }

        public async Task<bool> AddSupplier(Supplier supp)
        {
            await _context.AddAsync(supp);

            return true;
        }

        public async Task<bool> UpdateSupplier(Supplier supp)
        {
            var supplier = await _context.Suppliers.Where(x => x.Id == supp.Id)
                                                   .FirstOrDefaultAsync();

            supplier.SupplierName = supp.SupplierName;
            supplier.SupplierAddress = supp.SupplierAddress;

            return true;

        }



        public async Task<bool> ActivateSupplier(Supplier supp)
        {
            var supplier = await _context.Suppliers.Where(x => x.Id == supp.Id)
                                                   .FirstOrDefaultAsync();
            supplier.IsActive = true;

            return true;
        }


        public async Task<bool> InActiveSupplier(Supplier supp)
        {
            var supplier = await _context.Suppliers.Where(x => x.Id == supp.Id)
                                                   .FirstOrDefaultAsync();
            supplier.IsActive = false;

            return true;
        }


        public async Task<PagedList<SupplierDto>> GetAllSupplierWithPagination(bool status, UserParams userParams)
        {
            var supplier = _context.Suppliers.Where(x => x.IsActive == status)
                                         .Select(x => new SupplierDto
                                         {
                                             Id = x.Id,
                                             SupplierCode = x.SupplierCode,
                                             SupplierName = x.SupplierName,
                                             SupplierAddress = x.SupplierAddress,
                                             DateAdded = x.DateAdded.ToString("MM/dd/yyyy"),
                                             AddedBy = x.AddedBy,
                                             IsActive = x.IsActive
                                         });

            return await PagedList<SupplierDto>.CreateAsync(supplier, userParams.PageNumber, userParams.PageSize);


        }

        public async Task<PagedList<SupplierDto>> GetSupplierWithPaginationOrig(UserParams userParams, bool status, string search)
        {
            var supplier = _context.Suppliers.Where(x => x.IsActive == status)
                                      .Select(x => new SupplierDto
                                      {
                                          Id = x.Id,
                                          SupplierCode = x.SupplierCode,
                                          SupplierName = x.SupplierName,
                                          SupplierAddress = x.SupplierAddress,
                                          DateAdded = x.DateAdded.ToString("MM/dd/yyyy"),
                                          AddedBy = x.AddedBy,
                                          IsActive = x.IsActive
                                      }).Where(x => x.SupplierName.ToLower()
                                        .Contains(search.Trim().ToLower()));

            return await PagedList<SupplierDto>.CreateAsync(supplier, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> SupplierCodeExist(string supplier)
        {
            return await _context.Suppliers.AnyAsync(x => x.SupplierCode == supplier);
        }
    }
}
