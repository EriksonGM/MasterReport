using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using MasterReport.Models;
using MasterReport.Models.Paging;
using MasterReport.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace MasterReport.UI.Controllers
{
    public class DataSourcesController : BaseController
    {
        private readonly ILogger<DataSourcesController> _logger;
        private readonly IDataSourceService _dataSource;
        public DataSourcesController(ILogger<DataSourcesController> logger, IDataSourceService dataSource)
        {
            _logger = logger;
            _dataSource = dataSource;
        }

        public async Task<IActionResult> Index([FromQuery]PaginatedFilter<DataSourceDTO> dto)
        {
            var res = await _dataSource.Get(dto.Filter, dto.Page, dto.PageSize);

            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewBag.DataSourceTypeId = new SelectList(await _dataSource.GetDataSourceTypes(), "DataSourceTypeId", "DataSourceName");

            if (id.HasValue)
            {
                
            }
            

            return View(new DataSourceDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DataSourceDTO dto)
        {
            ViewBag.DataSourceTypeId = new SelectList(await _dataSource.GetDataSourceTypes(), "DataSourceTypeId", "DataSourceName");
            
            try
            {
                if (dto.DataSourceTypeId != 1)
                    throw new Exception("Only Microsoft SQL DataSource supported at the moment");

                await _dataSource.AddOrUpdate(dto);

                await _dataSource.SaveAsync();
                
                return RedirectToAction("Index", "DataSources");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("",e.Message);
                _logger.LogError(e.Message, e);
            }

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Test(DataSourceDTO dto)
        {
            try
            {
                if (dto.DataSourceTypeId != 1)
                    throw new Exception("Only Microsoft SQL DataSource supported at the moment");

                await _dataSource.Test(dto, CancellationToken.None);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                _logger.LogError(e.Message, e);
            }

            return View("Edit", dto);
        }

    }
}
