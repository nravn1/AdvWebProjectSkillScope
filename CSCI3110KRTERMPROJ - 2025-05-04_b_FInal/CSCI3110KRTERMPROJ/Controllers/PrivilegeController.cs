using CSCI3110KRTERMPROJ.Models;
using CSCI3110KRTERMPROJ.Models.ViewModels;
using CSCI3110KRTERMPROJ.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSCI3110KRTERMPROJ.Controllers;

[Authorize]
public class PrivilegeController : Controller
{
    private readonly IPrivilegeRepository _privilegeRepo;

    public PrivilegeController(IPrivilegeRepository privilegeRepo)
    {
        _privilegeRepo = privilegeRepo;
    }

    //Read all privileges for the privilege index view
    public async Task<IActionResult> Index()
    {
        //return View(await _privilegeRepo.ReadAllAsync());
        var privileges = await _privilegeRepo.ReadAllAsync();

        var vm = privileges.Select(p => new PrivilegeDetailsVM
        {
            PrivilegeId = p.PrivilegeId,
            PrivilegeTitle = p.PrivilegeTitle,
            Description = p.Description,
            PersonCount = p.PersonsWithThisPrivilege?.Count ?? 0
        });

        return View(vm);
    }

    //Get all person privilege links
    public async Task<IActionResult> GetAll()
    {
        var allPrivileges = await _privilegeRepo.ReadAllAsync();
        // This is a workaround to avoid circular references
        foreach (var privilege in allPrivileges)
        {
            foreach (var personPrivilege in privilege.PersonsWithThisPrivilege)
            {
                personPrivilege.Person = null;
                personPrivilege.Privilege = null;
            }
        }
        return Json(allPrivileges);
    }

    //Create Privilege
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Privilege privilege)
    {
        if (ModelState.IsValid)
        {
            await _privilegeRepo.CreateAsync(privilege);
            return RedirectToAction("Index");
        }

        return View(privilege);
    }

    //Read Privilege Details
    public async Task<IActionResult> Details(int id)
    {
        var privilege = await _privilegeRepo.GetByIdDetailsAsync(id);
        if (privilege == null)
        {
            return RedirectToAction("Index");
        }

        return View(privilege);
    }

    //Update Privilege
    public async Task<IActionResult> Update(int id)
    {
        var privilege = await _privilegeRepo.GetByIdAsync(id);
        if (privilege == null)
        {
            return RedirectToAction("Index");
        }
        return View(privilege);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Privilege privilege)
    {
        if (ModelState.IsValid)
        {
            await _privilegeRepo.UpdateAsync(privilege);
            return RedirectToAction("Index");
        }
        return View(privilege);
    }

    //Delete Privilege
    public async Task<IActionResult> Delete(int id)
    {
        var privilege = await _privilegeRepo.GetByIdAsync(id);
        if (privilege == null)
        {
            return RedirectToAction("Index");
        }

        return View(privilege); // Return a confirmation view
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _privilegeRepo.DeleteAsync(id);
        return RedirectToAction("Index");
    }

}
