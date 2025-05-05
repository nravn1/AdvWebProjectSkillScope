using CSCI3110KRTERMPROJ.Models;
using CSCI3110KRTERMPROJ.Models.ViewModels;
using CSCI3110KRTERMPROJ.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CSCI3110KRTERMPROJ.Controllers;

[Authorize]
public class RoleController : Controller
{
    private readonly IRoleRepository _roleRepo;

    public RoleController(IRoleRepository roleRepo)
    {
        _roleRepo = roleRepo;
    }

    //Read all
    public async Task<IActionResult> Index()
    {
        var roles = await _roleRepo.ReadAllAsync();

        var vm = roles.Select(r => new RoleDetailsVM
        {
            RoleId = r.RoleId,
            RoleTitle = r.RoleTitle,
            Description = r.Description,
            RoleSkillCount = r.SkillsOfaRole?.Count ?? 0,
            RolePersonCount = r.PersonsWithRole?.Count ?? 0
        });

        return View(vm);

    }

    //Get all person role links for the modal popup
    public async Task<IActionResult> GetAll()
    {
        var allRoles = await _roleRepo.ReadAllAsync();
        // This is a workaround to avoid circular references
        foreach (var role in allRoles)
        {
            foreach (var personRole in role.PersonsWithRole)
            {
                personRole.Person = null;
                personRole.Role = null;
            }
        }
        return Json(allRoles);
    }
    //Get all person role links
    public async Task<IActionResult> GetAllForPerson()
    {
        Debug.WriteLine("<<..debug..>> RoleController.cs/GetAllForPerson:");

        //Call the dbRoleRepository
        var allRoles = await _roleRepo.ReadAllForPersonAsync();
        // This is a workaround to avoid circular references
        foreach (var role in allRoles)
        {
            foreach (var personRole in role.PersonsWithRole)
            {
                personRole.Person = null;
                personRole.Role = null;
            }
        }
        return Json(allRoles);
    }

    //Assign skill to a role - this is called from indexRole.js, function assignSkillToRole
    [HttpGet]
    public async Task<IActionResult> AssignSkill([Bind(Prefix = "id")] int roleId, int skillId)
    {
        Debug.WriteLine("<<..debug..>> RoleController/AssignSkill:  roleId=" + roleId + "  skillId=" + skillId);

        //Call the dbRoleRepository
        var assignStatus = await _roleRepo.AssignSkillToRoleAsync(roleId, skillId);

        if (assignStatus == AssignmentStatus.Success)
        {
            return Json("Ok");
        }
        if (assignStatus == AssignmentStatus.LinkAlreadyExists)
        {
            return Json(new { message = "This skill already exists for this role." });
        }
        if (assignStatus == AssignmentStatus.NoRoleExists)
        {
            return Json(new { message = "The role does not exist or is null." });
        }
        if (assignStatus == AssignmentStatus.NoSkillExists)
        {
            return Json(new { message = "The skill does not exist or is null." });
        }
        return Json("Unknown Status");
    }

    //Unassign skill from a role - this is called from indexRole.js, function unAssignSkillToRole
    [HttpGet]
    public async Task<IActionResult> UnAssignSkill([Bind(Prefix = "id")] int roleId, int skillId)
    {
        Debug.WriteLine("<<..debug..>> RoleController/UnAssignSkill:  roleId=" + roleId + "  skillId=" + skillId);

        //Call the dbRoleRepository
        var assignStatus = await _roleRepo.UnAssignSkillFromRoleAsync(roleId, skillId);

        if (assignStatus == AssignmentStatus.Success)
        {
            return Json("Ok");
        }
        if (assignStatus == AssignmentStatus.LinkDoesNotExist)
        {
            return Json(new { message = "This skill is not assigned to this role." });
        }
        if (assignStatus == AssignmentStatus.NoRoleExists)
        {
            return Json(new { message = "The role does not exist or is null." });
        }
        if (assignStatus == AssignmentStatus.NoSkillExists)
        {
            return Json(new { message = "The skill does not exist or is null." });
        }
        return Json("Unknown Status");
    }

    //Create Role
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Role role)
    {
        if (ModelState.IsValid)
        {
            await _roleRepo.CreateAsync(role);
            return RedirectToAction("Index");
        }

        return View(role);
    }

    //Read Role Details
    public async Task<IActionResult> Details(int id)
    {
        var role = await _roleRepo.GetByIdDetailsAsync(id);

        if (role == null)
        {
            return RedirectToAction("Index");
        }

        return View(role);
    }

    //Update Role
    public async Task<IActionResult> Update(int id)
    {
        var role = await _roleRepo.GetByIdAsync(id);
        if (role == null)
        {
            return RedirectToAction("Index");
        }
        return View(role);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Role role)
    {
        if (ModelState.IsValid)
        {
            await _roleRepo.UpdateAsync(role);
            return RedirectToAction("Index");
        }
        return View(role);
    }

    //Delete
    public async Task<IActionResult> Delete(int id)
    {
        var role = await _roleRepo.GetByIdAsync(id);
        if (role == null)
        {
            return RedirectToAction("Index");
        }

        return View(role); // Return a confirmation view
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _roleRepo.DeleteAsync(id);
        return RedirectToAction("Index");
    }

}
