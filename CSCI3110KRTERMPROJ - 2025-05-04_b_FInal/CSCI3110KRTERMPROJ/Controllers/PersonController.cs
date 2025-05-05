using CSCI3110KRTERMPROJ.Models;
using CSCI3110KRTERMPROJ.Models.ViewModels;
using CSCI3110KRTERMPROJ.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CSCI3110KRTERMPROJ.Controllers;

[Authorize]
public class PersonController : Controller
{
    private readonly IPersonRepository _personRepo;

    public PersonController(IPersonRepository personRepo)
    {
        _personRepo = personRepo;
    }

    //Read all persons for the person index view
    public async Task<IActionResult> Index()
    {
        var persons = await _personRepo.ReadAllAsync();

        var vm = persons.Select(p => new PersonDetailsVM
        {
            PersonId = p.PersonId,
            FName = p.FName,
            MName = p.MName,
            LName = p.LName,
            BirthDate = p.BirthDate,
            Email = p.Email,
            TypeCode = p.TypeCode,
            UserFlag = p.UserFlag,
            PrivilegeCount = p.PrivilegesOfaPerson?.Count ?? 0,
            SkillCount = p.SkillsOfaPerson?.Count ?? 0,
            RoleCount = p.RolesOfaPerson?.Count ?? 0
        });

        return View(vm);
    }

    //Assign privilege to a person - this is called from PersonIndex.js
    [HttpGet]
    public async Task<IActionResult> AssignPrivilege([Bind(Prefix = "id")] int personId, int privilegeId)
    {
        //Call the dbPersonRepository
        var assignStatus = await _personRepo.AssignPrivilegeToPersonAsync(personId, privilegeId);

        if (assignStatus == AssignmentStatus.Success)
        {
            return Json("Ok");
        }
        if (assignStatus == AssignmentStatus.LinkAlreadyExists)
        {
            return Json(new { message = "This privilege already exists for this person." });
        }
        if (assignStatus == AssignmentStatus.NoPersonExists)
        {
            return Json(new { message = "The person does not exist or is null." });
        }
        if (assignStatus == AssignmentStatus.NoPrivilegeExists)
        {
            return Json(new { message = "The privilege does not exist or is null." });
        }
        return Json("Unknown Status");
    }

    //Assign skill to a person - this is called from PersonIndex.js
    [HttpGet]
    public async Task<IActionResult> AssignSkill([Bind(Prefix = "id")] int personId, int skillId)
    {
        Debug.WriteLine("<<..debug..>> PersonController/AssignSkill:  personId=" + personId + "  skillId=" + skillId);

        //Call the dbPersonRepository
        var assignStatus = await _personRepo.AssignSkillToPersonAsync(personId, skillId);

        if (assignStatus == AssignmentStatus.Success)
        {
            return Json("Ok");
        }
        if (assignStatus == AssignmentStatus.LinkAlreadyExists)
        {
            return Json(new { message = "This skill already exists for this person." });
        }
        if (assignStatus == AssignmentStatus.NoPersonExists)
        {
            return Json(new { message = "The person does not exist or is null." });
        }
        if (assignStatus == AssignmentStatus.NoPrivilegeExists)
        {
            return Json(new { message = "The skill does not exist or is null." });
        }
        return Json("Unknown Status");
    }

    //Assign role to a person - this is called from PersonIndex.js
    [HttpGet]
    public async Task<IActionResult> AssignRole([Bind(Prefix = "id")] int personId, int roleId)
    {
        Debug.WriteLine("<<..debug..>> PersonController/AssignRole:  personId=" + personId + "  roleId=" + roleId);

        //Call the dbPersonRepository
        var assignStatus = await _personRepo.AssignRoleToPersonAsync(personId, roleId);

        if (assignStatus == AssignmentStatus.Success)
        {
            return Json("Ok");
        }
        if (assignStatus == AssignmentStatus.LinkAlreadyExists)
        {
            return Json(new { message = "This role already exists for this person." });
        }
        if (assignStatus == AssignmentStatus.NoPersonExists)
        {
            return Json(new { message = "The person does not exist or is null." });
        }
        if (assignStatus == AssignmentStatus.NoPrivilegeExists)
        {
            return Json(new { message = "The role does not exist or is null." });
        }
        return Json("Unknown Status");
    }

    //Unassign privilege from a person - this is called from personRole.js, function unAssignPrivilegeFromPerson
    [HttpGet]
    public async Task<IActionResult> UnAssignPrivilege([Bind(Prefix = "id")] int personId, int privilegeId)
    {
        Debug.WriteLine("<<..debug..>> PersonController/UnAssignPrivilege:  personId=" + personId + "  privilegeId=" + privilegeId);

        //Call the dbPersonRepository
        var assignStatus = await _personRepo.UnAssignPrivilegeFromPersonAsync(personId, privilegeId);

        if (assignStatus == AssignmentStatus.Success)
        {
            return Json("Ok");
        }
        if (assignStatus == AssignmentStatus.LinkDoesNotExist)
        {
            return Json(new { message = "This privilege is not currently assigned to this person." });
        }
        if (assignStatus == AssignmentStatus.NoPersonExists)
        {
            return Json(new { message = "The person does not exist or is null." });
        }
        if (assignStatus == AssignmentStatus.NoPrivilegeExists)
        {
            return Json(new { message = "The privilege does not currently exist or is null." });
        }
        return Json("Unknown Status");
    }

    //Unassign skill from a person - this is called from personRole.js, function unAssignSkillFromPerson
    [HttpGet]
    public async Task<IActionResult> UnAssignSkill([Bind(Prefix = "id")] int personId, int skillId)
    {
        Debug.WriteLine("<<..debug..>> PersonController/UnAssignSkill:  personId=" + personId + "  skillId=" + skillId);

        //Call the dbPersonRepository
        var assignStatus = await _personRepo.UnAssignSkillFromPersonAsync(personId, skillId);

        if (assignStatus == AssignmentStatus.Success)
        {
            return Json("Ok");
        }
        if (assignStatus == AssignmentStatus.LinkDoesNotExist)
        {
            return Json(new { message = "This skill is not assigned to this person." });
        }
        if (assignStatus == AssignmentStatus.NoPersonExists)
        {
            return Json(new { message = "The person does not exist or is null." });
        }
        if (assignStatus == AssignmentStatus.NoSkillExists)
        {
            return Json(new { message = "The skill does not exist or is null." });
        }
        return Json("Unknown Status");
    }

    //Unassign role from a person - this is called from personRole.js, function unAssignRoleFromPerson
    [HttpGet]
    public async Task<IActionResult> UnAssignRole([Bind(Prefix = "id")] int personId, int roleId)
    {
        Debug.WriteLine("<<..debug..>> PersonController/UnAssignRole:  personId=" + personId + "  roleId=" + roleId);

        //Call the dbPersonRepository
        var assignStatus = await _personRepo.UnAssignRoleFromPersonAsync(personId, roleId);

        if (assignStatus == AssignmentStatus.Success)
        {
            return Json("Ok");
        }
        if (assignStatus == AssignmentStatus.LinkDoesNotExist)
        {
            return Json(new { message = "This role is not currently assigned to this person." });
        }
        if (assignStatus == AssignmentStatus.NoPersonExists)
        {
            return Json(new { message = "The person does not exist or is null." });
        }
        if (assignStatus == AssignmentStatus.NoRoleExists)
        {
            return Json(new { message = "The role does not currently exist or is null." });
        }
        return Json("Unknown Status");
    }

    //Person Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Person person)
    {
        if (ModelState.IsValid)
        {
            await _personRepo.CreateAsync(person);
            return RedirectToAction("Index");
        }

        return View(person);
    }

    //Person Details
    public async Task<IActionResult> Details(int id)
    {
        var person = await _personRepo.GetByIdDetailsAsync(id);
        if (person == null)
        {
            return RedirectToAction("Index");
        }

        return View(person);
    }

    //Person Update
    public async Task<IActionResult> Update(int id)
    {
        var person = await _personRepo.GetByIdAsync(id);
        if (person == null)
        {
            return RedirectToAction("Index");
        }
        return View(person);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Person person)
    {
        if (ModelState.IsValid)
        {
            await _personRepo.UpdateAsync(person);
            return RedirectToAction("Index");
        }
        return View(person);
    }

    //Person Delete
    public async Task<IActionResult> Delete(int id)
    {
        var person = await _personRepo.GetByIdAsync(id);
        if (person == null)
        {
            return RedirectToAction("Index");
        }

        return View(person); // Return a confirmation view
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _personRepo.DeleteAsync(id);
        return RedirectToAction("Index");
    }

}
