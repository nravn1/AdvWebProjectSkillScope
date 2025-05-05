using CSCI3110KRTERMPROJ.Models;
using CSCI3110KRTERMPROJ.Models.ViewModels;
using CSCI3110KRTERMPROJ.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Diagnostics;

namespace CSCI3110KRTERMPROJ.Controllers;

[Authorize]
public class SkillController : Controller
{
    private readonly ISkillRepository _skillRepo;

    public SkillController(ISkillRepository skillRepo)
    {
        _skillRepo = skillRepo;
    }

    //Read all skills for the skill index view
    public async Task<IActionResult> Index()
    {
        var skills = await _skillRepo.ReadAllAsync();

        var vm = skills.Select(s => new SkillDetailsVM
        {
            SkillId = s.SkillId,
            SkillTitle = s.SkillTitle,
            Description = s.Description,
            SkillRoleCount = s.RolesWithThisSkill?.Count ?? 0,
            SkillPersonCount = s.PersonsWithThisSkill?.Count ?? 0
        });

        return View(vm);

    }

    //Get all person skill links
    public async Task<IActionResult> GetAll()
    {
        Debug.WriteLine("<<..debug..>> SkillController.cs/GetAll:");

        var allSkills = await _skillRepo.ReadAllAsync();
        // This is a workaround to avoid circular references
        foreach (var skill in allSkills)
        {
            foreach (var personSkill in skill.PersonsWithThisSkill)
            {
                personSkill.Person = null;
                personSkill.Skill = null;
            }
        }
        return Json(allSkills);
    }

    //Get all person skill links
    public async Task<IActionResult> GetAllForPerson()
    {
        Debug.WriteLine("<<..debug..>> SkillController.cs/GetAllForPerson:");

        var allSkills = await _skillRepo.ReadAllForPersonAsync();
        // This is a workaround to avoid circular references
        foreach (var skill in allSkills)
        {
            foreach (var personSkill in skill.PersonsWithThisSkill)
            {
                personSkill.Person = null;
                personSkill.Skill = null;
            }
        }
        return Json(allSkills);
    }
    //Get all role skill links
    public async Task<IActionResult> GetAllForRole()
    {
        Debug.WriteLine("<<..debug..>> SkillController.cs/GetAllForRole:");

        var allSkills = await _skillRepo.ReadAllForRoleAsync();
        // This is a workaround to avoid circular references
        foreach (var skill in allSkills)
        {
            foreach (var roleSkill in skill.RolesWithThisSkill)
            {
                roleSkill.Role = null;
                roleSkill.Skill = null;
            }
        }
        return Json(allSkills);
    }

    //Create skill
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Skill skill)
    {
        if (ModelState.IsValid)
        {
            await _skillRepo.CreateAsync(skill);
            return RedirectToAction("Index");
        }

        return View(skill);
    }

    //Read Skill Details
    public async Task<IActionResult> Details(int id)
    {
        Debug.WriteLine("<<..debug..>> SkillController.cs/Details:");
        Debug.WriteLine("id=" + id);
        
        var skill = await _skillRepo.GetByIdDetailsAsync(id);
        if (skill == null)
        {
            return RedirectToAction("Index");
        }

        return View(skill);
    }

    //Update Skill
    public async Task<IActionResult> Update(int id)
    {
        var skill = await _skillRepo.GetByIdAsync(id);
        if (skill == null)
        {
            return RedirectToAction("Index");
        }
        return View(skill);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Skill skill)
    {
        if (ModelState.IsValid)
        {
            await _skillRepo.UpdateAsync(skill);
            return RedirectToAction("Index");
        }
        return View(skill);
    }

    //Delete Skill
    public async Task<IActionResult> Delete(int id)
    {
        var skill = await _skillRepo.GetByIdAsync(id);
        if (skill == null)
        {
            return RedirectToAction("Index");
        }

        return View(skill); // Return a confirmation view
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _skillRepo.DeleteAsync(id);
        return RedirectToAction("Index");
    }

}
