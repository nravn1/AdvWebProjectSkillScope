using CSCI3110KRTERMPROJ.Models.ViewModels;
using CSCI3110KRTERMPROJ.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSCI3110KRTERMPROJ.Controllers;

[Authorize]
public class RoleSkillController : Controller
{
    private readonly RoleSkillRepository _roleskillRepo;

    public RoleSkillController(RoleSkillRepository roleskillRepo)
    {
        _roleskillRepo = roleskillRepo;
    }

    //Read all
    //public async Task<IActionResult> Index()
    //{
    //    return View(await _roleRepo.ReadAllAsync());
    //}

    //public async Task<IActionResult> IndexVM()
    //{
    //    var roles = await _roleskillRepo.ReadAllAsync();

    //    var vm = roles.Select(r => new RoleDetailsVM
    //    {
    //        RoleId = r.RoleId,
    //        RoleTitle = r.RoleTitle,
    //        Description = r.Description,
    //        RoleSkillCount = r.RoleSkillCount?.Count ?? 0
    //    });

    //    return View(vm);
    //}

 
}
