using DDD.ApplicationLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PackagesManagement.Models.Packages;
using PackagesManagementApplicationServices.Commands;
using PackagesManagementApplicationServices.Queries;
using PackagesManagementDomainServices.DTOs;
using PackagesManagementDomainServices.IRepositories;
using System.Collections.ObjectModel;

namespace PackagesManagement.Controllers
{
    [Authorize(Roles= "Administrator")]
    public class ManagePackagesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index([FromServices] IPackagesListQuery query)
        {
            var results = await query.GetAllPackages();
            var vm = new PackagesListViewModel 
                { Items = new ReadOnlyCollection<PackageInfosDTO>(results) };
            return View(vm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(
            PackageCreationViewModel vm,
            [FromServices] ICommandHandler<CreatePackageCommand> command)
        {
            if (ModelState.IsValid) { 
                await command.HandleAsync(new CreatePackageCommand(){
                    Name = vm.Name,
                    Description = vm.Description,
                    DurationInDays = vm.DurationInDays,
                    StartValidityDate = vm.StartValidityDate,
                    EndValidityDate = vm.EndValidityDate,
                    Price = vm.Price,
                    DestinationId=vm.DestinationId
                
                });
                return RedirectToAction(
                    nameof(ManagePackagesController.Index));
            }
            else
                return View("Edit", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(
            int id,
            [FromServices] IPackageRepository repo)
        {
            if (id == 0) return RedirectToAction(
                nameof(ManagePackagesController.Index));
            var aggregate = await repo.GetAsync(id);
            if (aggregate is null) return RedirectToAction(
                nameof(ManagePackagesController.Index));
            var vm = new PackageFullEditViewModel
            {
                Id = aggregate.Id,
                Name = aggregate.Name,
                Description=aggregate.Description??string.Empty,
                Price=aggregate.Price,
                DurationInDays=aggregate.DurationInDays,
                StartValidityDate=aggregate.StartValidityDate, 
                EndValidityDate=aggregate.EndValidityDate

            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(
            PackageFullEditViewModel vm,
            [FromServices] ICommandHandler<UpdatePackageCommand> command)
        {
            if (ModelState.IsValid)
            {
                await command.HandleAsync(new UpdatePackageCommand { 
                    Id = vm.Id,
                    Name = vm.Name,
                    Description = vm.Description,
                    Price = vm.Price,
                    DurationInDays=vm.DurationInDays,
                    StartValidityDate=vm.StartValidityDate,
                    EndValidityDate=vm.EndValidityDate
                });
                return RedirectToAction(
                    nameof(ManagePackagesController.Index));
            }
            else
                return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] ICommandHandler<DeletePackageCommand> command)
        {
            if (id>0)
            {
                await command.HandleAsync(new DeletePackageCommand(id));
                
            }
            return RedirectToAction(
                    nameof(ManagePackagesController.Index));
        }
    }
}
