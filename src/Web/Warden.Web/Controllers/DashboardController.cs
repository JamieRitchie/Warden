﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Warden.Web.Core.Services;
using Warden.Web.ViewModels;

namespace Warden.Web.Controllers
{
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public DashboardController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        [Route("organizations/{id}/dashboard")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
                return HttpNotFound();
            var isUserInOrganization = await _organizationService.IsUserInOrganizationAsync(id, UserId);
            if(!isUserInOrganization)
                return HttpNotFound();
            var organization = await _organizationService.GetAsync(id);
            if(organization == null)
                return HttpNotFound();
            if(!organization.ApiKeys.Any())
                return HttpNotFound();
            var viewModel = new DashboardViewModel
            {
                OrganizationId = id,
                OrganizationName = organization.Name,
                ApiKey = organization.ApiKeys.First()
            };

            return View(viewModel);
        }
    }
}